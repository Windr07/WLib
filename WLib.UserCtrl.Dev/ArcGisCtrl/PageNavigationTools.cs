/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/23
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;
using WLib.Attributes;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    public partial class PageNavigationTools : UserControl, IPageNavigationTools
    {
        /// <summary>
        /// 页面布局导航工具类别
        /// </summary>
        public EPageTools CurrentTool { get; private set; } = EPageTools.None;
        /// <summary>
        /// 页面导航工具条所绑定的页面布局控件
        /// </summary>
        public AxPageLayoutControl PageLayoutControl { get; set; }

        public PageNavigationTools()
        {
            InitializeComponent();
        }


        private void pageLayoutNavigationBtn_Click(object sender, EventArgs e) //布局地图导航条：Click
        {
            CurrentTool = ((SimpleButton)sender).Text.GetEnum<EPageTools>();
            ICommand command = null;
            switch (CurrentTool)
            {
                case EPageTools.FullExtent: command = new ControlsPageZoomWholePageCommand(); break;
                case EPageTools.ZoomIn: command = new ControlsPageZoomInTool(); break;
                case EPageTools.ZoomOut: command = new ControlsPageZoomOutTool(); break;
                case EPageTools.Pan: command = new ControlsPagePanTool(); break;
                case EPageTools.PreView: command = new ControlsPageZoomPageToLastExtentBackCommand(); break;
                case EPageTools.NextView: command = new ControlsPageZoomPageToLastExtentForwardCommand(); break;
            }
            if (command != null)
            {
                command.OnCreate(PageLayoutControl.Object);
                if (command is ITool tool)
                    PageLayoutControl.CurrentTool = tool;
                else
                    command.OnClick();
            }
        }
    }
}
