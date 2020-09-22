/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;
using System.Linq;

namespace WLib.ArcGis.Analysis.OnShape
{
    /// <summary>
    /// 几何检查的错误类型信息
    /// </summary>
    public class GeometryCheckInfo
    {
        /// <summary>
        /// 几何错误类型
        /// </summary>
        public esriNonSimpleReasonEnum eType { get; set; }
        /// <summary>
        /// 几何错误名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 几何错误别名
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 几何错误描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 几何检查的错误类型信息
        /// </summary>
        public GeometryCheckInfo() { }
        /// <summary>
        /// 几何检查的错误类型信息
        /// </summary>
        /// <param name="eType">几何错误类型</param>
        /// <param name="name">几何错误名称</param>
        /// <param name="aliasName">几何错误别名</param>
        /// <param name="description">几何错误描述</param>
        public GeometryCheckInfo(esriNonSimpleReasonEnum eType, string name, string aliasName, string description)
        {
            this.eType = eType;
            Name = name;
            AliasName = aliasName;
            Description = description;
        }



        private static GeometryCheckInfo[] _geometryCheckInfos;
        /// <summary>
        /// 获取所有的几何检查的错误类型信息
        /// </summary>
        /// <returns></returns>
        public static GeometryCheckInfo[] GetGeometryCheckInfos()
        {
            if (_geometryCheckInfos == null)
            {
                _geometryCheckInfos = new GeometryCheckInfo[]
                {
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleRingOrientation, "Ring Orientation", "环方向错误","构成多边形的环的方向不符合外环顺时针、内环逆时针的走向"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleSegmentOrientation, "Segment Orientation","线段方向错误","各条线段的走向不一致，不符合前一条线段的终点是后一条线段的起点的规则"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleShortSegments,"Short Segments", "线段过短","部分线段的长度小于空间参考系单位要求的容差范围"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleSelfIntersections, "SelfIntersections","自相交","多边形与自身相交"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleUnclosedRing,"Unclosed Ring", "环未闭合","构成多边形的环的终点没有与起点重合"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleEmptyPart,"Empty Part", "几何部件为空","几何图形存在多个部分，其中一个或几个部分为空"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleMismatchedAttributes,"Mismatched Attributes", "M/值不匹配","某线段端点的Z坐标或M坐标与下一条线段中与之重合的端点的Z坐标或M坐标不匹配"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleDiscontinuousParts, "Discontinuous Parts","几何部件不连续","几何图形的某部分由断开的或不连续的部分组成"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleEmptyZValues,"Empty Z Values", "Z值为空","几何图形的一个或多个折点Z值为空"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleDuplicateVertex, "Duplicate Vertex","折点重复","几何图形的两个或多个折点坐标相同"),
                    new GeometryCheckInfo(esriNonSimpleReasonEnum.esriNonSimpleOK, "Is OK","未发现几何错误","未发现几何错误")
                };
            }
            return _geometryCheckInfos;
        }
        /// <summary>
        /// 获取指定的几何检查的错误类型信息
        /// </summary>
        /// <returns></returns>
        public static GeometryCheckInfo GetGeometryCheckInfo(esriNonSimpleReasonEnum eType)
            => GetGeometryCheckInfos().FirstOrDefault(v => v.eType == eType);
    }
}
