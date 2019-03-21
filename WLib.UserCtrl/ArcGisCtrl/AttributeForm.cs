/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Data;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.Geomtry;
using WLib.Data;
using static WLib.WinForm.MenuOpt;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 图层属性表窗口
    /// </summary>
    public partial class AttributeForm : Form
    {
        /// <summary>
        /// 按属性查询窗体
        /// </summary>
        protected AttributeQueryForm AttQueryForm { get; set; }
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
        public string Filter { get; set; }
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
        /// </summary>
        public AttributeForm()
        {
            InitializeComponent();

            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.AddRange(new[]
            {
                NewMenuItem("缩放至图斑(&G)", Keys.G, (s,e) => dataGridView1_SelectionChanged(null, null)),
                NewMenuItem("按属性查询(&Q)", Keys.Q, 按属性查询QToolStripMenuItem_Click),
                NewMenuItem("复制值(&C)", Keys.C, (s,e) => Clipboard.SetDataObject(dataGridView1.SelectedCells[0].Value)),
                NewMenuItem("复制整行(&R)", Keys.R,  复制整行RToolStripMenuItem_Click),
            });
            this.dataGridView1.ContextMenuStrip = contextMenuStrip;
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="table">要获取属性表的表</param>
        /// <param name="whereClause">筛选条件，表示从表加载的数据范围</param>
        /// <param name="fieldNames">加载和显示的字段</param>
        /// <returns></returns>
        public DataTable LoadAttribute(ITable table, string whereClause = null, string[] fieldNames = null)
        {
            try
            {
                Table = table ?? throw new Exception("表格或其数据源为空！");

                WhereClause = whereClause;
                if (fieldNames == null)
                    fieldNames = table.GetFieldsNames().ToArray();
                var dataTable = table.CreateDataTable(fieldNames, whereClause).SwitchColumnNameAndCaption();//创建数据表

                Text = ((IDataset)Table).Name + @" - 属性表";
                dataGridView1.ContextMenuStrip.Items[0].Visible = false;
                dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dataTable;
                lblTips.Text = $@"共{dataTable.Rows.Count}条记录";
                return dataTable;
            }
            catch (Exception ex) { MessageBox.Show(@"无法显示属性表！" + ex.Message); return null; }
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="featureLayer">要获取属性表的图层</param>
        /// <param name="whereClause">筛选条件，表示从图层加载的数据范围</param>
        /// <param name="fieldNames">加载和显示的字段</param>
        /// <param name="featureLocation">定位到要素图斑的事件</param>
        public DataTable LoadAttribute(IFeatureLayer featureLayer, string whereClause = null,
            string[] fieldNames = null, EventHandler<FeatureLocationEventArgs> featureLocation = null)
        {
            if (featureLayer?.FeatureClass == null)
                throw new Exception("图层不是要素图层或其数据源为空！");

            FeatLayer = featureLayer;
            var shapeFieldName = featureLayer.FeatureClass.ShapeFieldName;
            var shapeTypeName = GeometryOpt.GetGeometryTypeCnName(featureLayer.FeatureClass.ShapeType);
            var dataTable = LoadAttribute(featureLayer.FeatureClass as ITable, whereClause, fieldNames);
            foreach (DataRow tmpRow in dataTable.Rows)
            {
                tmpRow[shapeFieldName] = shapeTypeName;
            }
            if (featureLocation != null)
                FeatureLocation += featureLocation;

            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.ContextMenuStrip.Items[0].Visible = true;

            return dataTable;
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            Text = @"属性表";
            FeatLayer = null;
            Table = null;
            dataGridView1.DataSource = null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            object value = dataGridView1.SelectedRows[0].Cells[Table.OIDFieldName];
            if (value == null || value == DBNull.Value) return;

            if (FeatLayer != null)
                FeatureLocation?.Invoke(this, new FeatureLocationEventArgs(FeatLayer, $"{Table.OIDFieldName} = {value}"));
        }


        private void 按属性查询QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Table == null) return;

            if (AttQueryForm == null || AttQueryForm.IsDisposed)
                AttQueryForm = new AttributeQueryForm(Table, false, null, (sender2, e2) => WhereClause = AttQueryForm.WhereClause);
            AttQueryForm.Show(this);
        }

        private void 复制整行RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rows = dataGridView1.SelectedRows;
            if (rows.Count > 0)
                Clipboard.SetDataObject(rows[0].Cells.Cast<DataGridViewCell>().Select(v => v.Value.ToString()).Aggregate((a, b) => a + "\t" + b));
        }

    }
}
