/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.WinCtrls.PathCtrl
{
    /// <summary>
    /// 表示从工作空间中筛选获得哪些类型的数据（表格、要素图层、栅格图层等）
    /// </summary>
    [Flags]
    public enum EObjectFilter
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 表格
        /// </summary>
        Tables = 1,
        /// <summary>
        /// 点图层
        /// </summary>
        PointLayer = 2,
        /// <summary>
        /// 线图层
        /// </summary>
        PolylineLayer = 4,
        /// <summary>
        /// 面图层
        /// </summary>
        PolygonLayer = 8,
        /// <summary>
        /// 栅格图层
        /// </summary>
        Raster = 16,
        /// <summary>
        /// 要素图层
        /// </summary>
        FeatureLayer = PointLayer | PolylineLayer | PolygonLayer,
        /// <summary>
        /// 全部
        /// </summary>
        All = Tables| PointLayer| PolylineLayer| PolygonLayer| Raster,
    }
}
