/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.UserCtrls.PathCtrl
{
    /// <summary>
    /// 从工作空间中选取图层或表格的窗体
    /// </summary>
    public partial class DataSelectorForm : Form
    {
        /// <summary>
        /// 表示从工作空间中筛选获得哪些类型的数据（表格、要素类等）
        /// </summary>
        private EObjectFilter _filter;
        /// <summary>
        /// 表示从工作空间中筛选获得哪些类型的数据（表格、要素类等）
        /// </summary>
        public EObjectFilter Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                cbTables.Visible = _filter == EObjectFilter.All || _filter == EObjectFilter.Tables;
                cbLayers.Visible = cbPoint.Visible = cbLine.Visible = cbPolygon.Visible = _filter == EObjectFilter.All || _filter == EObjectFilter.FeatureClasses;
            }
        }
        /// <summary>
        /// 是否允许多选
        /// </summary>
        public bool MultiSelect { get => listViewLayers.MultiSelect; set => cbAll.Enabled = cbAll.Visible = listViewLayers.MultiSelect = value; }
        /// <summary>
        /// 所选的工作空间
        /// </summary>
        public IWorkspace SelectWorkspace => workspaceSelector1.Workspace;
        /// <summary>
        /// 所选的工作空间的路径
        /// </summary>
        public string SelectWorkspacePath { get => SelectWorkspace.PathName; set => workspaceSelector1.LoadWorkspace(value); }
        /// <summary>
        /// 所选的图层或表格的名称
        /// </summary>
        public string[] SelectedObjectNames => listViewLayers.SelectedItems.Cast<ListViewItem>().Select(v => v.Text).ToArray();
        /// <summary>
        /// 单选时所选的图层或表格，或多选时所选的第一个图层或表格的名称
        /// </summary>
        public string SelectedObjectName
        {
            get
            {
                var selectedItems = listViewLayers.SelectedItems.Cast<ListViewItem>().Select(v => v.Text).ToArray();
                return selectedItems.Length > 0 ? selectedItems[0] : null;
            }
        }


        /// <summary>
        /// 返回指定名称或别名的要素类（未连接工作空间或找不到时返回null）
        /// </summary>
        /// <param name="name">要素类名称或要素类别名</param>
        /// <returns></returns>
        public IFeatureClass GetFeatureClassByName(string name)
        {
            return workspaceSelector1.GetFeatureClassByName(name);
        }
        /// <summary>
        /// 返回指定名称或别名的表格（未连接工作空间或找不到时返回null）
        /// </summary>
        /// <param name="name">表名或表的别名</param>
        /// <returns></returns>
        public ITable GetTableByName(string name)
        {
            return workspaceSelector1.GetTableByName(name);
        }
        /// <summary>
        /// 从工作空间中选取图层或表格的窗体
        /// </summary>
        public DataSelectorForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 从工作空间中选取图层或表格的窗体
        /// </summary>
        /// <param name="filter">表示从工作空间中筛选获得哪些类型的数据（表格、要素类等）</param>
        /// <param name="multiSelect">是否允许多选图层或表格</param>
        public DataSelectorForm(EObjectFilter filter, bool multiSelect)
        {
            InitializeComponent();
            this.Filter = filter;
            this.MultiSelect = multiSelect;
        }


        private void workspaceSelector1_WorkspaceTypeChanged(object sender, EventArgs e)//改变工作空间类型时，清空图层/表格列表
        {
            listViewLayers.Items.Clear();
        }

        private void workspaceSelector1_AfterSelectPath(object sender, EventArgs e)//选定工作空间后，将图层或表格显示到ListView中
        {
            listViewLayers.Items.Clear();
            var classNames = workspaceSelector1.FeatureClassNames;
            var tableNames = workspaceSelector1.TableNames;

            switch (Filter)
            {
                case EObjectFilter.FeatureClasses:
                    if (classNames != null)
                        foreach (var name in classNames) { listViewLayers.Items.Add(new ListViewItem(name, 0)); }
                    break;
                case EObjectFilter.Tables:
                    if (tableNames != null)
                        foreach (var name in tableNames) { listViewLayers.Items.Add(new ListViewItem(name, 1)); }
                    break;
                case EObjectFilter.All:
                    if (classNames != null)
                        foreach (var name in classNames) { listViewLayers.Items.Add(new ListViewItem(name, 0)); }
                    if (tableNames != null)
                        foreach (var name in tableNames) { listViewLayers.Items.Add(new ListViewItem(name, 1)); }
                    break;
            }
        }

        private void listViewLayers_ItemCheck(object sender, ItemCheckEventArgs e)//单选
        {
            if (!MultiSelect && listViewLayers.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listViewLayers.Items.Count; i++)
                {
                    if (i != e.Index)
                        listViewLayers.Items[i].Selected = false;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)//确定
        {
            if (SelectedObjectName == null)
            {
                MessageBox.Show(@"请至少选择一个图层！", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)//取消
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        #region CheckBox事件处理
        private void cbAll_cckedChanged(object sender, EventArgs e)//全选
        {
            for (int i = 0; i < listViewLayers.Items.Count; i++)
            {
                listViewLayers.Items[i].Selected = cbAll.Checked;
            }
        }

        #endregion

        private void cbShowItems_CheckedChanged(object sender, EventArgs e)
        {
            listViewLayers.Items.Clear();
            var classNames = workspaceSelector1.FeatureClassNames;
            var tableNames = workspaceSelector1.TableNames;

            if (cbTables.Checked && tableNames != null)
                foreach (var name in classNames) { listViewLayers.Items.Add(new ListViewItem(name, 0)); }

            if (cbLayers.Checked && classNames != null)
            {
                var shapeTypeGroups = workspaceSelector1.FeatureClasses.GroupBy(v => v.ShapeType);
                foreach (var group in shapeTypeGroups)
                {
                    if (group.Key == esriGeometryType.esriGeometryPoint && !cbPoint.Checked) continue;
                    if (group.Key == esriGeometryType.esriGeometryPolyline && !cbLine.Checked) continue;
                    if (group.Key == esriGeometryType.esriGeometryPolygon && !cbPolygon.Checked) continue;

                    foreach (var cls in group) { listViewLayers.Items.Add(new ListViewItem(((IDataset)cls).Name, 0)); }
                }
            }
        }
    }
}
