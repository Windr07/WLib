using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.WinForm;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 修改插件信息的窗口
    /// </summary>
    public partial class PluginInfoForm : Form
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
                this.cmbInvokeType.SelectedItem = _plugin.InvokType.GetDescription();
                this.txtTips.Text = _plugin.Tips;
                this.txtCmd.Text = _plugin.TypeName;
                this.txtAssembly.Text = _plugin.AssemblyPath;
                this.txtIcon.Text = _plugin.IconPath;
                if (File.Exists(_plugin.IconPath))
                    this.pictureBox1.Image = Image.FromFile(_plugin.IconPath);
                else
                    this.pictureBox1.Image = null;
                _changedValue = true;
            }
        }
        /// <summary>
        /// 软件中存放图片的目录
        /// </summary>
        public string AppImageDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Images";
        /// <summary>
        /// 修改了插件容器信息的事件
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// 修改插件信息的窗口
        /// </summary>
        /// <param name="plugin"></param>
        public PluginInfoForm(IPluginItem plugin = null)
        {
            InitializeComponent();
            this.cmbShortcutKeys.Items.AddRange(Enum.GetNames(typeof(Shortcut)));
            this.cmbShortcutKeys.SelectedIndex = 0;
            this.cmbInvokeType.Items.AddRange(EnumDescriptionHelper.GetDescriptions<EPluginInvokeType>());
            this.cmbInvokeType.SelectedIndex = 0;
            if (plugin != null)
                Plugin = plugin;
        }

        private void BtnSelectIcon_Click(object sender, EventArgs e)
        {
            var extension = "*.bmp;*.gif;*.jpg;*.jpeg;*.gif;*.png;*.ico|*.bmp;*.gif;*.jpg;*.jpeg;*.gif;*.png;*.ico";
            if (_sourceImgDir == null)
                _sourceImgDir = AppImageDir;
            var filePath = DialogOpt.ShowOpenFileDialog(extension, "选择图标", null, _sourceImgDir);
            if (filePath != null)
            {
                _sourceImgDir = Path.GetDirectoryName(filePath);
                if (AppImageDir != _sourceImgDir)
                {
                    var newPath = Path.Combine(AppImageDir, DateTime.Now.Ticks.ToString() + Path.GetExtension(filePath));
                    Directory.CreateDirectory(AppImageDir);
                    File.Copy(filePath, newPath);
                    filePath = newPath;
                }
                this.pictureBox1.Image = Image.FromFile(filePath);
                this.txtIcon.Text = filePath;
            }
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            if (_changedValue)
                ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
