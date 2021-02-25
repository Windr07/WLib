/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Enum;
using WLib.Plugins.Interface;
using WLib.Plugins.Model;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 可加载插件、可设置皮肤的Ribbon窗体
    /// </summary>
    /// <typeparam name="TViewEventType"></typeparam>
    public partial class PluginForm<TViewEventType, TCmdInputData> : Form, IPluginView<TViewEventType>
    {
        #region 实现IPluginView
        /// <summary>
        /// 插件视图ID
        /// <para>继承该窗体的子类窗体应重新对此属性赋值</para>
        /// </summary>
        public virtual string Id => "{B4F31A65-2F3C-4A00-9F62-254168ABBE9A}";
        /// <summary>
        /// 插件容器
        /// </summary>
        private IList<IPluginContainer> _containers = null;
        /// <summary>
        /// 插件容器
        /// <para>将菜单栏（MenuStrip）等作为插件容器</para>
        /// </summary>
        public IList<IPluginContainer> Containers => _containers ?? (_containers = new List<IPluginContainer>
        {
            new PluginContainer(this.menuStrip1.Name, this.menuStrip1.Text, Plugins.Enum.EPluginContainerType.MenuStrip)
        });
        /// <summary>
        /// 插件视图事件
        /// <para><see cref="TViewEventType"/>耕地质量主界面事件类型</para>
        /// <para><see cref="EventArgs"/>耕地质量主界面事件参数</para>
        /// </summary>
        public Action<TViewEventType, EventArgs> ViewAction { get; set; }
        #endregion


        /// <summary>
        /// 调用插件命令的对象，一般是窗口或容器控件
        /// </summary>
        private object _caller;
        /// <summary>
        /// 
        /// </summary>
        public IPluginPlan CurrentPluginPlan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TCmdInputData CmdInputData { get; set; }
        /// <summary>
        /// 可加载插件、可设置皮肤的Ribbon窗体
        /// </summary>
        public PluginForm() => InitializeComponent();


        /// <summary>
        /// 加载控件中文、加载控件皮肤、加载插件
        /// <para>应当在子类中调用</para>
        /// </summary>
        /// <param name="caller">调用插件命令的对象，一般是窗口或容器控件</param>
        protected virtual void LoadPluginView(object caller)
        {
            _caller = caller;
            IPluginView pluginView = null;

            //1、显示程序版本信息
            this.lblVersion.Text = "版本：" + Assembly.GetEntryAssembly().GetName().Version.ToString();//程序版本

            //2、加载插件
            try { pluginView = ReloadPlugins(); }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }

            //3、在窗口加载时、窗口关闭时触发相应插件
            InvokePlugins(pluginView, EPluginInvokeType.OnViewLoad, caller);
            FormClosing += (sender, e) => InvokePlugins(pluginView, EPluginInvokeType.OnViewClose, caller);
        }
        /// <summary>
        /// 调用指定类型的插件，状态栏上显示相应提示信息
        /// </summary>
        /// <param name="pluginView"></param>
        /// <param name="invokeType"></param>
        /// <param name="caller"></param>
        protected virtual void InvokePlugins(IPluginView pluginView, EPluginInvokeType invokeType, object caller)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var plugins = pluginView?.QueryPlugins(invokeType).ToList();
                this.lblStatus.Text = "正在加载插件..";
                this.progressBarStart.Maximum = plugins.Count;
                this.progressBarStart.Visible = true;
                Application.DoEvents();

                int i = 0;
                plugins.ForEach(plugin =>
                {
                    plugin.Command?.Invoke(caller);
                    this.progressBarStart.Value = ++i;
                    Application.DoEvents();
                });
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
            finally
            {
                this.progressBarStart.Visible = true;
                this.progressBarStart.Text = "就绪";
                this.Cursor = Cursors.Default;
            }
        }


        protected virtual IPluginView ReloadPlugins()
        {
            var pluginView = CurrentPluginPlan.Views.First(v => v.Name == this.Name && v.Text == this.Text);

            this.lblStatus.Text = "正在加载插件";
            this.progressBarStart.Maximum = pluginView.QueryPlugins().Count();
            this.Cursor = Cursors.WaitCursor;
            this.progressBarStart.Value = 0;
            this.progressBarStart.Visible = true; Application.DoEvents();
            //this.menuStrip1.ClearAllPlugins();
            this.menuStrip1.LoadPlugins(pluginView, _caller, CmdInputData, CurrentPluginPlan.PluginImageDir, BingdingCommandEvent);//加载插件
            var plugins = pluginView.QueryPlugins(EPluginInvokeType.OnViewLoad).ToList();
            plugins.ForEach(v => v.Command.View = this);

            this.lblStatus.Text = "就绪";
            this.progressBarStart.Visible = true; Application.DoEvents();
            this.Cursor = Cursors.Default;
            return pluginView;
        }

        protected virtual void BingdingCommandEvent(ICommand cmd)
        {

        }
    }
}
