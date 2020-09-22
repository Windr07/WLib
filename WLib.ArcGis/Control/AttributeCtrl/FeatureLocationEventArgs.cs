/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Carto;

namespace WLib.ArcGis.Control.AttributeCtrl
{
    /// <summary>
    /// 为<see cref="IAttributeForm.FeatureLocation"/>事件提供的事件参数
    /// </summary>
    public class FeatureLocationEventArgs : EventArgs
    {
        /// <summary>
        /// 定位的图层的名称
        /// </summary>
        public string LayerName { get; set; }
        /// <summary>
        /// 定位的图层的要素类名称
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// 定位的图层的索引
        /// </summary>
        public int LayerIndex { get; set; }
        /// <summary>
        /// 定位的图斑的筛选条件
        /// </summary>
        public string WhereClause { get; set; }
        /// <summary>
        /// 定位的图层
        /// </summary>
        public IFeatureLayer LocationLayer { get; set; }


        /// <summary>
        /// 为<see cref="IAttributeForm.FeatureLocation"/>事件提供的事件参数
        /// </summary>
        /// <param name="locationLayer">定位的图层</param>
        /// <param name="whereClause">定位的图斑的筛选条件</param>
        public FeatureLocationEventArgs(IFeatureLayer locationLayer, string whereClause)
        {
            LocationLayer = locationLayer;
            WhereClause = whereClause;
        }
        /// <summary>
        /// 为<see cref="IAttributeForm.FeatureLocation"/>事件提供的事件参数
        /// </summary>
        /// <param name="layerName">定位的图层的名称</param>
        /// <param name="className">定位的图层的要素类名称</param>
        /// <param name="whereClause">定位的图斑的筛选条件</param>
        public FeatureLocationEventArgs(int layerIndex, string layerName, string className, string whereClause)
        {
            LayerIndex = layerIndex;
            LayerName = layerName;
            ClassName = className;
            WhereClause = whereClause;
        }
    }
}
