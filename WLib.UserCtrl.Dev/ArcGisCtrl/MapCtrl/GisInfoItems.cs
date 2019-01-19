using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using ESRI.ArcGIS.Geometry;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    class GisInfoItems
    {
        /// <summary>
        /// 坐标信息栏
        /// </summary>
        public BarStaticItem BarSiCurCoors { get; set; }
        /// <summary>
        /// 比例尺信息栏
        /// </summary>
        public BarStaticItem BarSiScaleInfo { get; set; }
        /// <summary>
        /// 工具提示栏
        /// </summary>
        public BarStaticItem BarSiTips { get; set; }

        public GisInfoItems()
        {
            BarSiCurCoors = new BarStaticItem();
            BarSiScaleInfo = new BarStaticItem();
            BarSiTips = new BarStaticItem { Caption = "就绪" };
        }

        public void LoadInfo(long scale, double mapX, double mapY)
        {
            BarSiScaleInfo.Caption = $@"比例尺：1/{scale}";
            BarSiCurCoors.Caption = $@" 当前坐标: X={mapX}, Y={mapY}";
        }
    }
}
