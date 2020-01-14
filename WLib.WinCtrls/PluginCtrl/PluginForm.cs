using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 可加载插件的窗体
    /// </summary>
    public partial class PluginForm : Form, IPluginView
    {
        #region 实现IPluginView接口
        /// <summary>
        /// 插件视图ID
        /// <para>继承该窗体的子类窗体应重新对此属性赋值</para>
        /// </summary>
        public virtual string Id => "{FF7F36C8-3B55-4505-AB5D-1B2FB4647B57}";
        /// <summary>
        /// 插件容器
        /// </summary>
        private IList<IPluginContainer> _containers = null;
        /// <summary>
        /// 插件容器
        /// <para>将RibbonControl作为插件容器</para>
        /// </summary>
        public IList<IPluginContainer> Containers => _containers ?? (_containers = menuStrip1.ConvertToContainer());
        #endregion

        /// <summary>
        /// 传入给插件命令的参数
        /// </summary>
        public object CmdData { get; set; }
        /// <summary>
        /// 当前使用的插件方案
        /// </summary>
        public IPluginPlan PluginPlan { get; set; }
        /// <summary>
        /// 可加载插件的窗体
        /// </summary>
        public PluginForm()
        {
            InitializeComponent();
            FormClosing += (sender, e) => this.InvokePlugins(PluginPlan, EPluginInvokeType.OnViewClose);//窗体关闭前调用相应类型的插件
        }


        /// <summary>
        /// 加载窗体的同时加载中文、皮肤、插件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (PluginPlan == null)
                    return;

                menuStrip1.LoadPlugins(PluginPlan, CmdData);
                this.InvokePlugins(PluginPlan, EPluginInvokeType.OnViewLoad);
            }
            catch (Exception ex)
            {
                MessageBoxEx.ShowError(ex);
            }
        }
    }
}
