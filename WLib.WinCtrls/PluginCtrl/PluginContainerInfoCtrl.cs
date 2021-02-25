/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Plugins.Interface;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 修改插件容器信息的窗口
    /// </summary>
    public partial class PluginContainerInfoCtrl : UserControl
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
                _pluginContainer.Visible = this.cbVisible.Checked;
                return _pluginContainer;
            }
            set
            {
                _changedValue = false;
                this._pluginContainer = value;
                this.txtName.Text = _pluginContainer.Name;
                this.txtCaption.Text = _pluginContainer.Text;
                this.txtContainerType.Text = _pluginContainer.Type.GetDescriptionEx();
                this.cbVisible.Checked = _pluginContainer.Visible;
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
        public PluginContainerInfoCtrl()
        {
            InitializeComponent();

            this.txtCaption.Focus();
            this.cmbShortcutKeys.Items.AddRange(Enum.GetNames(typeof(Shortcut)));
            this.cmbShortcutKeys.SelectedIndex = 0;
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            if (_changedValue) ValueChanged?.Invoke(this, new EventArgs());
        }

        private void CbVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (!_changedValue) return;

            if (this.cbVisible.CheckState == CheckState.Unchecked &&
                MessageBox.Show("将菜单设置为不显示，其所有子级菜单都将不可见！\r\n是否不显示该菜单？",
                this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                this.cbVisible.CheckState = CheckState.Checked;

            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
