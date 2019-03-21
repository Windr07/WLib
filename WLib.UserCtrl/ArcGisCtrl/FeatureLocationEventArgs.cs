using System;
using ESRI.ArcGIS.Carto;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 为YYGISLib.UserDevControls.AttributeDevForm的FeatureLocation事件提供的事件参数
    /// </summary>
    public class FeatureLocationEventArgs : EventArgs
    {
        /// <summary>
        /// 定位的图层
        /// </summary>
        public IFeatureLayer LocationLayer { get; set; }
        /// <summary>
        /// 定位的图斑的筛选条件
        /// </summary>
        public string WhereClause { get; set; }
        /// <summary>
        /// 为FeatureLocation事件提供的事件参数
        /// </summary>
        /// <param name="locationLayer"></param>
        /// <param name="whereClause"></param>
        public FeatureLocationEventArgs(IFeatureLayer locationLayer, string whereClause)
        {
            this.LocationLayer = locationLayer;
            this.WhereClause = whereClause;
        }
    }
}
