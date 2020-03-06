/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Control.AttributeCtrl;
using WLib.ArcGis.Data;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.GeoDatabase.Table;
using WLib.WinCtrls.Dev._Extension;
using WLib.WinCtrls.MessageCtrl;
using static WLib.WinCtrls.Extension.MenuOpt;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 显示表格/图层属性表的控件
    /// </summary>
    public partial class AttributeCtrl : UserControl, IAttributeCtrl
    {
        /// <summary>
        /// 按属性查询窗体
        /// </summary>
        public IAttributeQueryCtrl AtrributeQueryCtrl { get; set; }
        /// <summary>
        /// 要获取属性表的表格
        /// </summary>
        public ITable Table { get; private set; }
        /// <summary>
        /// 图层在地图中的索引
        /// </summary>
        public int LayerIndex { get; private set; }
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
        /// 运行评价功能、显示评价数据的窗体
        /// </summary>
        /// <param name="pjDataPath"></param>
        /// <param name="paramsDb"></param>
        public AttributeCtrl()
        {
            InitializeComponent();
            gridView1.ShowRowNumber().FitColumns();
            AtrributeQueryCtrl = new AttributeQueryForm();
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.AddRange(new[]
            {
                NewMenuItem("缩放至图斑(&G)", Keys.G, (s,e) => gridView1_RowClick(null, null)),
                NewMenuItem("按属性查询(&Q)", Keys.Q, 按属性查询QToolStripMenuItem_Click),
                NewMenuItem("复制值(&C)", Keys.C, (s,e) => Clipboard.SetDataObject(gridView1.GetFocusedDisplayText())),
                NewMenuItem("复制整行(&R)", Keys.R,  复制整行RToolStripMenuItem_Click),
            });
            this.gridCtrlData.ContextMenuStrip = contextMenuStrip;
            this.paginationBar.PageChanged += (sender, e) =>
            {
                var fieldNames = this.ListBoxFields.Items.Cast<CheckedListBoxItem>().Select(v => v.Value.ToString());
                gridView1.Columns.Clear();
                if (Table != null)
                {
                    var dataTable = Table.CreateDataTable(fieldNames, null, WhereClause, this.paginationBar.StartRow, this.paginationBar.EndRow);
                    gridCtrlData.DataSource = dataTable;
                    gridView1.BestFitColumns();
                    foreach (CheckedListBoxItem item in this.ListBoxFields.Items)
                        this.gridView1.Columns[item.Value.ToString()].Visible = item.CheckState == CheckState.Checked;
                }
            };
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="table">要获取属性表的表</param>
        /// <param name="whereClause">筛选条件，表示从表加载的数据范围</param>
        /// <returns></returns>
        public void LoadAttribute(ITable table, string whereClause = null, int layerIndex = -1, EventHandler<FeatureLocationEventArgs> featureLocation = null)
        {
            try
            {
                WhereClause = whereClause;
                LayerIndex = layerIndex;
                Table = table ?? throw new Exception("表格或其数据源为空！");
                Text = ((IDataset)Table).Name + @" - 属性表";
                var items = Table.GetFieldsNames().Select(v => new CheckedListBoxItem(v, true)).ToArray();
                this.ListBoxFields.Items.Clear();
                this.ListBoxFields.Items.AddRange(items);
                int cnt = Table.QueryCount(WhereClause);
                paginationBar.GotoPage(1, cnt);

                bool asLayer = layerIndex > -1 && featureLocation != null;
                gridCtrlData.ContextMenuStrip.Items[0].Visible = asLayer;
                FeatureLocation -= featureLocation;
                if (asLayer)
                    FeatureLocation += featureLocation;
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            Text = @"属性表";
            Table = null;
            this.gridCtrlData.DataSource = null;
        }


        private void gridView1_RowClick(object sender, RowClickEventArgs e)//单击行时缩放到图斑
        {
            int[] rowsIndex = gridView1.GetSelectedRows();
            if (rowsIndex.Length <= 0) return;

            object value = gridView1.GetDataRow(rowsIndex[0])[Table.OIDFieldName];
            if (value == null || value == DBNull.Value) return;

            var name = (Table as IDataset).Name;
            FeatureLocation?.Invoke(this, new FeatureLocationEventArgs(LayerIndex, name, name, $"{Table.OIDFieldName} = {value}"));
        }

        private void AtrributeQueryCtrl_Query(object sender, EventArgs e) => WhereClause = AtrributeQueryCtrl.WhereClause;

        private void 按属性查询QToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Table == null) return;
            AtrributeQueryCtrl.LoadQueryInfo(Table);
            AtrributeQueryCtrl.Query -= AtrributeQueryCtrl_Query;
            AtrributeQueryCtrl.Query += AtrributeQueryCtrl_Query;
            AtrributeQueryCtrl.Show(this);
        }

        private void 复制整行RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rowIndexs = gridView1.GetSelectedRows();
            if (rowIndexs.Length > 0)
                Clipboard.SetDataObject(gridView1.GetDataRow(rowIndexs[0]).ItemArray.Select(v => v.ToString()).Aggregate((a, b) => a + "\t" + b));
        }

        private void CListBoxFields_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)//显示/隐藏字段
        {
            var item = this.ListBoxFields.Items[e.Index];
            this.gridView1.Columns[item.Value.ToString()].Visible = item.CheckState == CheckState.Checked;
        }

        private void txtSearchFields_EditValueChanged(object sender, EventArgs e)//查找字段
        {
            var value = this.txtSearchFields.EditValue.ToString().ToLower();
            var values = this.ListBoxFields.Items.Cast<CheckedListBoxItem>().Select(v => v.Value.ToString().ToLower()).ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                var selected = values[i].Contains(value);
                this.ListBoxFields.SetSelected(i, selected);
                if (selected)
                    this.ListBoxFields.TopIndex = i;
            }
        }


        #region 右键菜单
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e) =>
            this.ListBoxFields.Items.Cast<CheckedListBoxItem>().ToList().ForEach(v => v.CheckState = CheckState.Checked);

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e) =>
            this.ListBoxFields.Items.Cast<CheckedListBoxItem>().ToList().ForEach(v => v.CheckState = CheckState.Unchecked);

        private void 反选ToolStripMenuItem_Click(object sender, EventArgs e) =>
            this.ListBoxFields.Items.Cast<CheckedListBoxItem>().ToList().ForEach(v =>
            v.CheckState = v.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        #endregion
    }
}
