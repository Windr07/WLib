/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.Table;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Attributes.Description;
using WLib.WinCtrls.Extension;
using WLib.WinCtrls.PathCtrl;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 工作空间选择器
    /// </summary>
    [DefaultEvent("WorkspaceTypeChanged")]
    [DefaultProperty("WorkspaceTypeFilter")]
    public partial class WorkspaceSelectorControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region 基础属性
        /// <summary>
        /// "shp|gdb|mdb|sde|excel|sql"
        /// </summary>
        private string _defaultWorkspaceType = "shp|gdb|mdb|sde|excel|sql";
        /// <summary>
        /// 可选的工作空间类别
        /// </summary>
        private string _workspaceTypeFilter;
        /// <summary>
        /// 可选的工作空间类别
        /// </summary>
        public string WorkspaceTypeFilter
        {
            set
            {
                var description2 = EnumDescriptionExHelper.GetDescriptionExs<EWorkspaceType>(2);
                var workspaceTypes = value.Split('|').Where(v => description2.Contains(v)).ToArray();
                if (workspaceTypes.Length == 0)
                    workspaceTypes = _defaultWorkspaceType.Split('|');

                _workspaceTypeFilter = workspaceTypes.Aggregate((a, b) => a + "|" + b);
                this.cmbADBType.Properties.Items.Clear();
                this.cmbADBType.Properties.Items.AddRange(workspaceTypes.Select(v => v.GetEnum<EWorkspaceType>(2).GetDescriptionEx()).ToArray());
                this.cmbADBType.SelectedIndex = 0;
            }
            get => this._workspaceTypeFilter;
        }
        /// <summary>
        /// 获取所选的工作空间类别
        /// </summary>
        public EWorkspaceType WorkspaceType
        {
            get => this.cmbADBType.SelectedItem.ToString().GetEnum<EWorkspaceType>();
            set => this.cmbADBType.SelectedItem = value;
        }
        /// <summary>
        /// 获取或设置可选工作空间列表中选中项的索引
        /// </summary>
        public int WorkspaceIndex { get => this.cmbADBType.SelectedIndex; set => this.cmbADBType.SelectedIndex = value; }
        /// <summary>
        /// 标示除浏览按钮外，其余操作是否可用
        /// </summary>
        public bool OptEnable { get => this.SourcePathBox.OptEnable; set => this.SourcePathBox.OptEnable = this.cmbADBType.Enabled = value; }
        /// <summary>
        /// 控件左侧的提示信息（eg:工作空间）
        /// </summary>
        public string Description { get => this.lblWorkspaceDesc.Text; set => this.lblWorkspaceDesc.Text = value; }
        /// <summary>
        /// 路径或连接字符串
        /// </summary>
        public string PathOrConnStr
        {
            get => this.SourcePathBox.Path;
            set
            {
                var wsType = string.IsNullOrEmpty(value) ? EWorkspaceType.ShapeFile : WorkspaceEx.GetDefaultWorkspaceType(value);
                WorkspaceIndex = wsType == EWorkspaceType.Default ? 0 : this.cmbADBType.Properties.Items.IndexOf(wsType.GetDescriptionEx());
                this.SourcePathBox.Path = value;
            }
        }
        #endregion


        #region 基础事件
        /// <summary>
        /// 选择工作空间并获取要素类名称完成事件
        /// （同时代表子控件PathBox的AfterSelectPath）
        /// </summary>
        public event EventHandler AfterSelectPath;
        /// <summary>
        /// 改变工作空间类型选项完成的事件
        /// </summary>
        public event EventHandler WorkspaceTypeChanged;
        /// <summary>
        /// 触发AfterSelectPath事件
        /// </summary>
        internal void OnAfterSelectPath() => AfterSelectPath?.Invoke(this, new EventArgs());
        /// <summary>
        /// 触发AfterSelectPath事件
        /// </summary>
        internal void OnWorkspaceTypeChanged() => WorkspaceTypeChanged?.Invoke(this, new EventArgs());
        #endregion


        #region GIS相关属性、方法
        /// <summary>
        /// 获取的工作空间
        /// </summary>
        public IWorkspace Workspace { get; private set; }
        #endregion


        /// <summary>
        /// 工作空间选择器
        /// </summary>
        public WorkspaceSelectorControl()
        {
            InitializeComponent();

            this.SourcePathBox.OnPathTextChanged += (sender, e) =>
            {
                var path = this.SourcePathBox.Text?.Trim();
                if (string.IsNullOrWhiteSpace(path))
                    return;
                var wsType = WorkspaceEx.GetDefaultWorkspaceType(path);
                if (wsType != EWorkspaceType.Default && wsType != WorkspaceType)
                {
                    var index = this.cmbADBType.Properties.Items.IndexOf(wsType.GetDescriptionEx());
                    if (index > 0)
                    {
                        WorkspaceIndex = index;
                        this.SourcePathBox.SelectPath(path);
                    }
                }
            };

            WorkspaceTypeChanged = (sender, e) => { this.SourcePathBox.Text = ""; };
            WorkspaceTypeFilter = "shp|gdb|mdb|sde";
        }
        /// <summary>
        /// 根据路径或连接字符打开对应工作空间，并获取要素类或表格
        /// </summary>
        /// <param name="pathOrConStr"></param>
        public void LoadWorkspace(string pathOrConStr)
        {
            var eType = WorkspaceEx.GetDefaultWorkspaceType(PathOrConnStr);
            this.cmbADBType.SelectedItem = eType.GetDescriptionEx();
            this.PathOrConnStr = pathOrConStr;
            this.SourcePathBox.SelectPath(pathOrConStr);
        }

        private void cmbWorkspaceType_SelectedIndexChanged(object sender, EventArgs e)//选择工作空间类别后，改变控件状态并触发WorkspaceTypeChanged事件
        {
            Workspace = null;
            var item = this.cmbADBType.SelectedItem.ToString().GetEnum<EWorkspaceType>();
            switch (item)
            {
                case EWorkspaceType.ShapeFile:
                case EWorkspaceType.FileGDB:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.Folder;
                    break;
                case EWorkspaceType.Access:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.OpenFile;
                    this.SourcePathBox.FileFilter = "*.mdb|*.mdb";

                    break;
                case EWorkspaceType.Excel:
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.ViewSelect;
                    this.SourcePathBox.SelectPathType = ESelectPathType.OpenFile;
                    this.SourcePathBox.FileFilter = "*.xls;*.xlsx|*.xls;*.xlsx";
                    break;
                case EWorkspaceType.Sde:
                case EWorkspaceType.Sql:
                    this.SourcePathBox.Text = string.Empty;
                    this.SourcePathBox.ShowButtonOption = EShowButtonOption.None;
                    break;
            }
            this.SourcePathBox.SelectTips = item.GetDescriptionEx();
            OnWorkspaceTypeChanged();
        }

        private void txtASourcePath_AfeterSelectPath(object sender, EventArgs e)//选择工作空间后，获取要素类名称，触发AfterSelectPath
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                Workspace = null;
                var eType = this.cmbADBType.SelectedItem.ToString().GetEnum<EWorkspaceType>();
                if (string.IsNullOrWhiteSpace(this.SourcePathBox.Path))
                    return;

                Workspace = WorkspaceEx.GetWorkSpace(this.SourcePathBox.Path, eType);

                if (Workspace != null)
                    OnAfterSelectPath();
            }
            catch (Exception ex) { throw ex; }
            finally { this.Cursor = Cursors.Default; }

        }

        private void WorkspaceSelector_Load(object sender, EventArgs e)//加载控件时，选中第一个工作空间
        {
            if (this.cmbADBType.SelectedIndex == -1) this.cmbADBType.SelectedIndex = 0;
        }
    }
}
