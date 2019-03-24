using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace WLib.Samples.WinForm.View
{
    public partial class frmPageLayout : Form
    {
        //int Compass = 0;
        enum sur { none,compass,scale}
        sur draw = sur.none;

        public frmPageLayout(IMapDocument mapDocument)
        {
            InitializeComponent();
            if (mapDocument != null)
            {
                this.axPageLayoutControl1.PageLayout = mapDocument.PageLayout;
                this.axPageLayoutControl1.ActiveView.Refresh();
            }
        }

        
        private IMapSurround CreateSurround(UID pID, IEnvelope pEnv, string strName, IPageLayout pageLayout)
        {
            //MapSurround是指南针、比例尺和图例一类的对象,创建一个MapSurround对象，
            //指明 类型UID，名称strName,位置pEnv，所属的PageLayOut

            IGraphicsContainer graphicsC = (IGraphicsContainer)pageLayout;//用于图形管理
            IActiveView activeView = (IActiveView)pageLayout;
            IMap map = activeView.FocusMap;
            IMapFrame pMapFrame = (IMapFrame)graphicsC.FindFrame(map);
            IMapSurroundFrame mapSFrame = pMapFrame.CreateSurroundFrame(pID, null);

            mapSFrame.MapSurround.Name = strName;
            IElement element = (IElement)mapSFrame;
            element.Geometry = pEnv;
            element.Activate(activeView.ScreenDisplay);
            ITrackCancel track = new CancelTracker();
            element.Draw(activeView.ScreenDisplay, track);
            graphicsC.AddElement(element, 0);
            return mapSFrame.MapSurround;
        }

        //加入指南针
        public void AddMapSurrounds(IEnvelope pEnv, string value, string name)
        {
            UID uid = new UIDClass();
            uid.Value = value;//指北针
            IMapSurround mapSurround = CreateSurround(
                uid, pEnv, name, axPageLayoutControl1.PageLayout);
        }


        private void axPageLayoutControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            if (draw == sur.compass)
            {
                AddMapSurrounds(axPageLayoutControl1.TrackRectangle(), "esriCarto.MarkerNorthArrow", "North Arrow");
                axPageLayoutControl1.ActiveView.Refresh();
            }
        }

        private void axToolbarControl1_OnItemClick(object sender, ESRI.ArcGIS.Controls.IToolbarControlEvents_OnItemClickEvent e)
        {
            draw = sur.none;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem.ToString() == "指南针")
            {
                draw = sur.compass;
                axToolbarControl1.CurrentTool = null;
            }
            else if (this.comboBox1.SelectedItem.ToString() == "比例尺")
            {
                draw = sur.compass;
                axToolbarControl1.CurrentTool = null;
            }
        }

       


    }
}
