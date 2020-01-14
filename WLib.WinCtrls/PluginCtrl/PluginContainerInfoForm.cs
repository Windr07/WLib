using System;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Plugins.Interface;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 修改插件容器信息的窗口
    /// </summary>
    public partial class PluginContainerInfoForm : Form
    {
        /// <summary>
        /// 标记是否触发ValueChanged事件，用于防止多次触发ValueChanged
        /// </summary>
        private bool _changedValue = false;
        /// <summary>
        /// 要修改信息的插件容器
        /// </summary>
        private IPluginContainer _pluginContainer;
        /// <summary>
        /// 要修改信息的插件容器
        /// </summary>
        public IPluginContainer PluginContainer
        {
            get
            {
                _pluginContainer.Name = this.txtName.Text;
                _pluginContainer.Text = this.txtCaption.Text;
                _pluginContainer.ShortcutKeys = this.cmbShortcutKeys.SelectedItem.ToString();
                return _pluginContainer;
            }
            set
            {
                _changedValue = false;
                this._pluginContainer = value;
                this.txtName.Text = _pluginContainer.Name;
                this.txtCaption.Text = _pluginContainer.Text;
                this.txtContainerType.Text = _pluginContainer.Type.GetDescription();
                if (!string.IsNullOrWhiteSpace(_pluginContainer.ShortcutKeys))
                    this.cmbShortcutKeys.SelectedItem = _pluginContainer.ShortcutKeys;
                else
                    this.cmbShortcutKeys.SelectedIndex = 0;
                _changedValue = true;
            }
        }
        /// <summary>
        /// 修改了插件容器信息的事件
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// 修改插件容器信息窗口
        /// </summary>
        /// <param name="pluginContainer"></param>
        public PluginContainerInfoForm(IPluginContainer pluginContainer = null)
        {
            InitializeComponent();

            this.txtCaption.Focus();
            this.cmbShortcutKeys.Items.AddRange(Enum.GetNames(typeof(Shortcut)));
            this.cmbShortcutKeys.SelectedIndex = 0;
            if (pluginContainer != null)
                this.PluginContainer = pluginContainer;
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            if (_changedValue) ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
