using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Data;
using WLib.ArcGis.Geomtry;

namespace WLib.UserCtrls.Dev.ArcGisControl
{
    /// <summary>
    /// 图层属性表窗口
    /// </summary>
    public partial class AttributeDevForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 按属性查询窗体
        /// </summary>
        private QueryDevForm _queryForm;
        /// <summary>
        /// 要获取属性表的图层
        /// </summary>
        public IFeatureLayer Layer { get; private set; }
        /// <summary>
        /// 要获取属性表的表格
        /// </summary>
        public ITable Table { get; private set; }
        /// <summary>
        /// 筛选条件
        /// </summary>
        public string WhereClause { get => this.gridView1.ActiveFilterString; set => this.gridView1.ActiveFilterString = value; }
        /// <summary>
        /// 定位到要素图斑的事件
        /// </summary>
        public event EventHandler<FeatureLocationEventArgs> FeatureLocation;

        /// <summary>
        /// 图层属性表窗口
        /// </summary>
        public AttributeDevForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 触发定位到要素图斑的事件
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="whereClause"></param>
        protected void OnFeatureLocation(IFeatureLayer layer, string whereClause)
        {
            FeatureLocation?.Invoke(this, new FeatureLocationEventArgs(layer, whereClause));
        }
        /// <summary>
        /// 获取属性表数据并绑定到GridView中
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private DataTable InnerLoadAttribute(ITable table)
        {
            DataTable dataTable = DataConvert.CreateDataTable(Table);//创建数据表
            foreach (DataColumn column in dataTable.Columns)
            {
                var tmp = column.ColumnName;
                column.ColumnName = column.Caption;
                column.Caption = tmp;
            }

            this.gridView1.Columns.Clear();
            this.dataGridView1.DataSource = dataTable;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.BestFitColumns();
            this.gridView1.GroupPanelText = $"共{dataTable.Rows.Count}条记录";
            return dataTable;
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="table"></param>
        public DataTable LoadAttribute(ITable table, string whereClause = null)
        {
            DataTable dataTable = null;
            try
            {
                this.Table = table;
                this.Text = (Table as IDataset).Name + " - 属性表";
                dataTable = InnerLoadAttribute(Table);
                this.gridView1.RowClick -= gridView1_RowClick;
                this.WhereClause = whereClause;
                this.缩放至图斑GToolStripMenuItem.Visible = false;
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message); }
            return dataTable;
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="mapControl"></param>
        public void LoadAttribute(IFeatureLayer featureLayer, string whereClause = null)
        {
            try
            {
                this.Layer = featureLayer;
                if (featureLayer == null || featureLayer.FeatureClass == null)
                    throw new Exception("图层不是要素图层或其数据源未空！");

                var dataTable = LoadAttribute(featureLayer.FeatureClass as ITable, whereClause);
                this.gridView1.RowClick += gridView1_RowClick;
                this.缩放至图斑GToolStripMenuItem.Visible = true;

                string shapeFieldName = featureLayer.FeatureClass.ShapeFieldName;
                string shapeTypeName = GeometryEx.GetGeometryTypeCnName(featureLayer.FeatureClass.ShapeType);
                foreach (DataRow tmpRow in dataTable.Rows)
                {
                    tmpRow[shapeFieldName] = shapeTypeName;
                }
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message); }
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            this.Text = "属性表";
            this.Layer = null;
            this.Table = null;
            this.dataGridView1.DataSource = null;
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)//单击行时缩放到图斑
        {
            int[] rowsIndex = this.gridView1.GetSelectedRows();
            if (rowsIndex.Length <= 0) return;

            object value = this.gridView1.GetDataRow(rowsIndex[0])[Table.OIDFieldName];
            if (value == null || value == DBNull.Value) return;

            OnFeatureLocation(Layer, $"{Table.OIDFieldName} = {value}");
        }

        private void sBtnClose_Click(object sender, EventArgs e)//隐藏的关闭按钮(CancelButton)，按Esc关闭此窗体
        {
            this.Close();
        }

        #region 右键菜单
        private void 按属性查询QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Table != null)
            {
                if (_queryForm == null || _queryForm.IsDisposed)
                {
                    _queryForm = new QueryDevForm(Table, false);
                    _queryForm.Query += (sender2, e2) => { WhereClause = _queryForm.WhereClause; };
                }
                _queryForm.Show(this);
            }
        }

        private void 缩放至图斑GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1_RowClick(null, null);
        }

        private void 复制值CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.gridView1.GetFocusedDisplayText());
        }

        private void 复制整行RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndexs = this.gridView1.GetSelectedRows();
            if (rowIndexs.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var value in this.gridView1.GetDataRow(rowIndexs[0]).ItemArray)
                {
                    sb.AppendFormat("{0}\t", value);
                }
                Clipboard.SetDataObject(sb.ToString());
            }
        }

        #endregion
    }
}
