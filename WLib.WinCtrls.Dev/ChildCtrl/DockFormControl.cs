/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using WLib.WindowsAPI;

namespace WLib.WinCtrls.Dev.ChildCtrl
{
    /// <summary>
    /// 提供窗体停靠的容器控件，
    /// 内置DevExpress.XtraBars.Docking.DockManger组件并提供将窗体放入DockPanel内部等方法。
    /// </summary>
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class DockFormControl : UserControl
    {
        /// <summary>
        /// 获取或设置点击停靠面板关闭按钮时，是关闭面板(true,默认)还是隐藏面板(false)
        /// </summary>
        public bool IsCloseDockPanel { get; set; } = true;
        /// <summary>
        /// 获取或设置创建面板时，面板在界面的默认停靠方位
        /// </summary>
        public DockingStyle DockingStyle { get; set; } = DockingStyle.Left;
        /// <summary>
        /// 获取或设置创建面板时，面板出现的位置
        /// </summary>
        public Point FloatLocation { get; set; } = new Point(100, 100);
        /// <summary>
        /// 构造窗口停靠容器控件实例
        /// </summary>
        public DockFormControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 创建、显示停靠面板并放置窗体，面板自动与已有面板组合成标签式面板。
        /// 不重复打开拥有相同窗体的面板。
        /// </summary>
        /// <param name="form">内置在停靠面板的窗体</param>
        /// <param name="formTag">窗体标识，值为null则忽略，不为null则面板Tag值相同则认为面板拥有相同窗体</param>
        /// <param name="width">面板宽度</param>
        /// <param name="height">面板高度</param>
        /// <param name="visibility">指示面板显示/隐藏/自动隐藏的枚举</param>
        public void AddFormToTabPanel(Form form, string formTag = null, int width = 600, int height = 600, DockVisibility visibility = DockVisibility.Visible)
        {
            // 查找包含指定窗体的DockPanel，通过Tag属性区分面板(窗体)
            var allPanels = dockManager1.Panels.Cast<DockPanel>();
            var existPanel = allPanels.FirstOrDefault(v => (v.Tag != null) && v.Tag.Equals(form.Name + form.Text));

            DockPanel dockPanel;
            if (existPanel == null)//不存在拥有指定窗体的DockPanel，则创建之
            {
                int panelCnt = dockManager1.Panels.Count;
                if (panelCnt == 1)//停靠到其他面板，形成标签面板
                {
                    dockPanel = dockManager1.AddPanel(DockingStyle);
                    dockPanel.DockAsTab(dockManager1.Panels[0]);
                }
                else if (panelCnt > 1)//停靠到其他面板，形成标签面板
                {
                    var tabPanel = allPanels.FirstOrDefault(v => v.Tabbed.Equals(true));
                    if (tabPanel != null)
                        dockPanel = tabPanel.AddPanel();
                    else
                    {
                        dockPanel = dockManager1.AddPanel(DockingStyle);
                        dockPanel.DockAsTab(dockManager1.Panels[0]);
                    }
                }
                else//创建第一个面板
                {
                    dockPanel = dockManager1.AddPanel(DockingStyle);
                }
                dockPanel.Size = new Size(width, height);
                dockPanel.Tag = form.Name + form.Text;//标记窗体，以区分不同窗体
                dockPanel.Text = form.Text;
                dockPanel.Visibility = visibility;
                dockPanel.FloatLocation = FloatLocation;

                FormIntoDockPanel(form, dockPanel);
            }
            else
            {
                dockPanel = existPanel;
            }
            dockPanel.Show();
            dockPanel.BringToFront();
            form.Show();
        }
        /// <summary>
        /// 创建、显示浮动面板并放置窗体
        /// 不重复打开拥有相同窗体的面板。
        /// </summary>
        /// <param name="form">内置在停靠面板的窗体</param>
        /// <param name="point">面板显示位置</param>
        /// <param name="width">面板宽度</param>
        /// <param name="height">面板高度</param>
        public void AddFormToFloatPanel(Form form, Point point, int width = 600, int height = 600)
        {
            //查找已是否存在包含指定窗体的DockPanel，通过Tag属性区分面板(窗体)
            var existPanel = dockManager1.Panels.Cast<DockPanel>().FirstOrDefault(v => (v.Tag != null) && v.Tag.Equals(form.Name + form.Text));
            DockPanel dockPanel;
            if (existPanel == null)
            {
                dockPanel = dockManager1.AddPanel(FloatLocation);
                dockPanel.FloatSize = new Size(width, height);
                dockPanel.Tag = form.Name + form.Text;//标记窗体，以区分不同窗体
                dockPanel.Text = form.Text;

                FormIntoDockPanel(form, dockPanel);
            }
            else
            {
                dockPanel = existPanel;
            }
            dockPanel.Show();
            dockPanel.BringToFront();
            form.Show();
        }
        /// <summary>
        /// 将窗体内置到面板工作区中，设置窗体大小自适应、面板和窗体互相关闭
        /// </summary>
        /// <param name="form"></param>
        /// <param name="dockPanel"></param>
        private void FormIntoDockPanel(Form form, DockPanel dockPanel)
        {
            //将窗体内置到面板工作区中
            int dockPanelWnd = dockPanel.ControlContainer.Handle.ToInt32();
            WinApi.SetParent(form.Handle.ToInt32(), dockPanelWnd);
            OnResize(dockPanelWnd, form.Handle);

            //窗体自适应停靠面板大小
            dockPanel.ControlContainer.SizeChanged += (sender, e) => OnResize(dockPanelWnd, form.Handle);

            //关闭面板时关闭窗体
            dockPanel.ClosingPanel += (sender, e) =>
            {
                if (!form.IsDisposed && IsCloseDockPanel)
                    form.Close();
            };
            dockPanel.ClosingPanel += dockFormPanel_ClosingPanel;

            //关闭窗体时关闭面板
            form.FormClosed += (sender, e) =>
            {
                FloatLocation = dockPanel.FloatLocation;
                dockManager1.RemovePanel(dockPanel);
            };
        }
        /// <summary>
        /// 当容器大小发生改变时，窗体自适应外部容器大小
        /// </summary>
        /// <param name="handleId">容器控件的句柄</param>
        /// <param name="innerFormHandle">内部窗体句柄</param>
        public void OnResize(int handleId, IntPtr innerFormHandle)
        {
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = 8;
            WinApi.MoveWindow(
                innerFormHandle,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                FromHandle((IntPtr)handleId).Bounds.Width + 4 * borderWidth,
                FromHandle((IntPtr)handleId).Bounds.Height + captionHeight + 4 * borderHeight + statusHeight,
                false);
            //true);
        }
        /// <summary>
        /// 关闭面板时，根据IsCloseDockPanel确定隐藏面板还是销毁面板
        /// </summary>
        private void dockFormPanel_ClosingPanel(object sender, DockPanelCancelEventArgs e)
        {
            if (IsCloseDockPanel)
            {
                e.Cancel = true;    //默认情况下，点击面板的关闭按钮面板隐藏但未被销毁，设置此值以重写它的默认行为
                FloatLocation = e.Panel.FloatLocation;
                e.Panel.Dispose();
            }
        }
    }
}
