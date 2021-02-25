/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Files;
using WLib.Plugins.Enum;
using WLib.Plugins.Interface;
using WLib.WinCtrls.Extension;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 修改插件信息的窗口
    /// </summary>
    public partial class PluginInfoCtrl : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        private string _sourceImgDir = null;
        /// <summary>
        /// 标记是否触发ValueChanged事件，用于防止多次触发ValueChanged
        /// </summary>
        private bool _changedValue = false;
        /// <summary>
        /// 要修改信息的插件
        /// </summary>
        private IPluginItem _plugin;
        /// <summary>
        /// 要修改信息的插件
        /// </summary>
        public IPluginItem Plugin
        {
            get
            {
                _plugin.Text = this.txtName.Text;
                _plugin.ShortcutKeys = this.cmbShortcutKeys.SelectedItem.ToString();
                _plugin.Tips = this.txtTips.Text;
                _plugin.TypeName = this.txtCmd.Text;
                _plugin.AssemblyPath = this.txtAssembly.Text;
                _plugin.Visible = this.cbVisible.Checked;
                _plugin.InvokType = this.cmbInvokeType.SelectedItem.ToString().GetEnum<EPluginInvokeType>();
                _plugin.IconPath = this.txtIcon.Text;
                return this._plugin;
            }
            set
            {
                _changedValue = false;
                this._plugin = value;
                this.txtName.Text = _plugin.Text;
                if (!string.IsNullOrWhiteSpace(_plugin.ShortcutKeys))
                    this.cmbShortcutKeys.SelectedItem = _plugin.ShortcutKeys;
                else
                    this.cmbShortcutKeys.SelectedIndex = 0;
                this.cmbInvokeType.SelectedItem = _plugin.InvokType.GetDescriptionEx();
                this.txtTips.Text = _plugin.Tips;
                this.txtCmd.Text = _plugin.TypeName;
                this.txtAssembly.Text = _plugin.AssemblyPath;
                this.txtIcon.Text = _plugin.IconPath;
                if (!string.IsNullOrWhiteSpace(_plugin.IconPath))
                {
                    var iconPath = PathEx.GetRootPath(_plugin.IconPath, ImageDir);
                    this.pictureBox1.Image = File.Exists(iconPath) ? Image.FromFile(iconPath) : null;
                }
                else
                    this.pictureBox1.Image = null;
                _changedValue = true;
            }
        }
        /// <summary>
        /// 配置目录（或应用程序目录），插件图标放置在该目录Images文件夹下
        /// </summary>
        public string CfgDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 插件图标的存放目录（应用软件所在目录\Images）
        /// </summary>
        public string ImageDir => Path.Combine(CfgDir, "Images");
        /// <summary>
        /// 修改了插件容器信息的事件
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// 修改插件信息的窗口
        /// </summary>
        public PluginInfoCtrl()
        {
            InitializeComponent();
            this.cmbShortcutKeys.Items.AddRange(Enum.GetNames(typeof(Shortcut)));
            this.cmbShortcutKeys.SelectedIndex = 0;
            this.cmbInvokeType.Items.AddRange(EnumDescriptionExHelper.GetDescriptionExs<EPluginInvokeType>());
            this.cmbInvokeType.SelectedIndex = 0;
        }


        private void PictureBox1_Click(object sender, EventArgs e)
        {
            var extension = "*.bmp;*.gif;*.jpg;*.jpeg;*.gif;*.png;*.ico|*.bmp;*.gif;*.jpg;*.jpeg;*.gif;*.png;*.ico";
            if (_sourceImgDir == null)
                _sourceImgDir = ImageDir;
            var filePath = DialogOpt.ShowOpenFileDialog(extension, "选择图标", null, _sourceImgDir);
            if (filePath != null)
                this.pictureBox1.Image = Image.FromFile(this.txtIcon.Text = filePath);
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            if (_changedValue) ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
