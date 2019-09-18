using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.WinCtrls.Dev.SkinCtrl;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.Dev.PluginCtrl
{
    /// <summary>
    /// 可加载插件、可设置皮肤的Ribbon窗体
    /// </summary>
    public abstract partial class PluginRibbonForm : RibbonForm, IPluginView
    {
        #region 实现IPluginView接口
        /// <summary>
        /// 插件视图ID
        /// </summary>
        public abstract  string Id { get; }
        /// <summary>
        /// 插件容器
        /// </summary>
        private IList<IPluginContainer> _containers = null;
        /// <summary>
        /// 插件容器
        /// <para>将RibbonControl作为插件容器</para>
        /// </summary>
        public IList<IPluginContainer> Containers => _containers ?? (_containers = ribbonCtrl.ConvertToContainer());
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
        /// 可加载插件、可设置皮肤的Ribbon窗体
        /// </summary>
        public PluginRibbonForm()
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
                new ChineseLanguage.Chinese();
                defaultLookAndFeel1.SetSkin(SkinOpt.Office2010蓝色);
                if (PluginPlan == null)
                    return;

                ribbonCtrl.LoadPlugins(PluginPlan, CmdData);
                this.InvokePlugins(PluginPlan, EPluginInvokeType.OnViewLoad);
            }
            catch (Exception ex)
            {
                MessageBoxEx.ShowError(ex);
            }
        }
    }
}
