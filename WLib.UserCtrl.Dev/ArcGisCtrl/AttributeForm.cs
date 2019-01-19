/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Data;
using WLib.ArcGis.Geomtry;
using WLib.Data;
using WLib.UserCtrls.Dev.ArcGisCtrl.Base;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 图层属性表窗口
    /// （实例化窗口后，通过调用LoadAttribute方法载入属性表）
    /// </summary>
    public partial class AttributeForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 按属性查询窗体
        /// </summary>
        protected QueryForm AttQueryForm { get; set; }
        /// <summary>
        /// 要获取属性表的图层
        /// </summary>
        public IFeatureLayer FeatLayer { get; private set; }
        /// <summary>
        /// 要获取属性表的表格
        /// </summary>
        public ITable Table { get; private set; }
        /// <summary>
        /// 数据显示的筛选条件，表示当前显示的数据范围（对WhereClause筛选后的数据的进一步筛选）
        /// </summary>
        public string Filter { get => gridView1.ActiveFilterString; set => gridView1.ActiveFilterString = value; }
        /// <summary>
        /// 数据加载的筛选条件，表示从表格(ITable)或图层(IFeatureLayer)加载的数据范围，只能通过LoadAttribute方法指定
        /// </summary>
        public string WhereClause { get; private set; }
        /// <summary>
        /// 定位到要素图斑的事件
        /// </summary>
        public event EventHandler<FeatureLocationEventArgs> FeatureLocation;
        /// <summary>
        /// 图层属性表窗口
        /// （实例化窗口后，通过调用LoadAttribute方法载入属性表）
        /// </summary>
        public AttributeForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 触发定位到要素图斑的事件
        /// </summary>
        /// <param name="featureLayer">要定位要素的图层</param>
        /// <param name="featureWhereClause">表示要定位的要素的查询条件</param>
        protected void OnFeatureLocation(IFeatureLayer featureLayer, string featureWhereClause)
        {
            if (featureLayer != null)
                FeatureLocation?.Invoke(this, new FeatureLocationEventArgs(featureLayer, featureWhereClause));
        }
        /// <summary>
        /// 获取属性表数据并绑定到GridView中
        /// </summary>
        /// <param name="table"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        private DataTable InnerLoadAttribute(ITable table, string whereClause = null)
        {
            var dataTable = table.CreateDataTable(whereClause).SwitchColumnNameAndCaption();//创建数据表
            gridView1.Columns.Clear();
            dataGridView1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.BestFitColumns();
            gridView1.GroupPanelText = $"共{dataTable.Rows.Count}条记录";
            return dataTable;
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="table">要获取属性表的表格</param>
        /// <param name="whereClause">筛选条件，表示从表格加载的数据范围</param>
        public DataTable LoadAttribute(ITable table, string whereClause = null)
        {
            try
            {
                Table = table;
                Text = ((IDataset)Table).Name + " - 属性表";
                gridView1.RowClick -= gridView1_RowClick;
                WhereClause = whereClause;
                缩放至图斑GToolStripMenuItem.Visible = false;
                return InnerLoadAttribute(table);
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message); return null; }
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="featureLayer">要获取属性表的图层</param>
        /// <param name="whereClause">筛选条件，表示从图层加载的数据范围</param>
        /// <param name="featureLocation">定位到要素图斑的事件</param>
        public DataTable LoadAttribute(IFeatureLayer featureLayer, string whereClause = null, EventHandler<FeatureLocationEventArgs> featureLocation = null)
        {
            DataTable dataTable = null;
            try
            {
                FeatLayer = featureLayer;
                if (featureLayer?.FeatureClass == null)
                    throw new Exception("图层不是要素图层或其数据源未空！");

                dataTable = LoadAttribute(featureLayer.FeatureClass as ITable, whereClause);
                gridView1.RowClick += gridView1_RowClick;
                缩放至图斑GToolStripMenuItem.Visible = true;

                var shapeFieldName = featureLayer.FeatureClass.ShapeFieldName;
                var shapeTypeName = GeometryOpt.GetGeometryTypeCnName(featureLayer.FeatureClass.ShapeType);
                foreach (DataRow tmpRow in dataTable.Rows)
                {
                    tmpRow[shapeFieldName] = shapeTypeName;
                }
                if (featureLocation != null)
                    FeatureLocation += featureLocation;
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message); }
            return dataTable;
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            Text = "属性表";
            FeatLayer = null;
            Table = null;
            dataGridView1.DataSource = null;
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)//单击行时缩放到图斑
        {
            int[] rowsIndex = gridView1.GetSelectedRows();
            if (rowsIndex.Length <= 0) return;

            object value = gridView1.GetDataRow(rowsIndex[0])[Table.OIDFieldName];
            if (value == null || value == DBNull.Value) return;

            OnFeatureLocation(FeatLayer, $"{Table.OIDFieldName} = {value}");
        }

        private void sBtnClose_Click(object sender, EventArgs e)//隐藏的关闭按钮，按Esc键即触发关闭按钮，关闭此窗体
        {
            Close();
        }

        private void 按属性查询QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Table == null) return;

            if (AttQueryForm == null || AttQueryForm.IsDisposed)
                AttQueryForm = new QueryForm(Table, false, null, (sender2, e2) => WhereClause = AttQueryForm.WhereClause);
            AttQueryForm.Show(this);
        }

        private void 缩放至图斑GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1_RowClick(null, null);
        }

        private void 复制值CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(gridView1.GetFocusedDisplayText());
        }

        private void 复制整行RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndexs = gridView1.GetSelectedRows();
            if (rowIndexs.Length <= 0) return;

            var str = gridView1.GetDataRow(rowIndexs[0]).ItemArray.Select(v => v.ToString()).Aggregate((a, b) => a + "\t" + b);
            Clipboard.SetDataObject(str);
        }
    }
}
