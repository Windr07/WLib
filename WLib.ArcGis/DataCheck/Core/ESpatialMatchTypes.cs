using System;
using WLib.Attributes.Description;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 图斑空间关系匹配方式
    /// </summary>
    [Flags]
    public enum ESpatialMatchTypes
    {
        /// <summary>
        /// 查找最小外接矩形与目标图斑最小外接矩形相交的要素
        /// </summary>
        [DescriptionEx("最小外接矩形相交")]
        EnvelopeIntersects = 1,
        /// <summary>
        /// 查找几何图形的空间范围和目标图斑的索引范围相交的要素
        /// </summary>
        [DescriptionEx("空间索引范围相交")]
        IndexIntersects = 2,


        /// <summary>
        /// 查找与目标图斑相交的要素
        /// （Touches、Overlaps、Crosses、Within、Contains都属于相交）
        /// </summary>
        [DescriptionEx("相交")]
        Intersects = 4,
        /// <summary>
        /// 查找与目标图斑边界相接的要素
        /// （除点与点之间的关系外，其它的要素之间都可以具有该关系）
        /// </summary>
        [DescriptionEx("边界相接")]
        Touches = 8,
        /// <summary>
        /// 查找与目标图斑有重叠的要素
        /// （只能在同维度图形之间判断，即点与点之间、线与线之间、面与面之间进行判断，且相交结果与源图形维度相同）
        /// </summary>
        [DescriptionEx("重叠")]
        Overlaps = 16,
        /// <summary>
        /// 查找与目标图斑有交叉的要素
        /// （只能在线与面、线与线之间判断，且线与面的相交结果为线，线与线相交结果为点）
        /// </summary>
        [DescriptionEx("交叉")]
        Crosses = 32,
        /// <summary>
        /// 查找包含目标图斑的要素
        /// （包括面与面、面与线、面与点、线与线、线与点、点与点）
        /// </summary>
        [DescriptionEx("包含")]
        Within = 64,
        /// <summary>
        /// 查找被目标图斑包含的要素
        /// （包括面与面、面与线、面与点、线与线、线与点、点与点）
        /// </summary>
        [DescriptionEx("被包含")]
        Contains = 128,


        /// <summary>
        /// 查找与目标图斑空间关联的要素
        /// （具体关联方式参考：http://resources.esri.com/help/9.3/ArcGISEngine/ArcObjects/esriGeoDatabase/ISpatialFilter_SpatialRelDescription.htm）
        /// </summary>
        [DescriptionEx("空间关联")]
        Relation = 256,


        /// <summary>
        /// 查找与目标图斑相交的要素，并且只返回相交面积最大的要素
        /// （只能在面图斑中查找）（Touches、Overlaps、Crosses、Within、Contains都属于相交）
        /// </summary>
        [DescriptionEx("相交面积最大")]
        InteresctMaxArea = 512,
        /// <summary>
        /// 查找与目标图斑相交的要素，并且只返回相交长度最大的要素
        /// （只能在面或线图斑中查找）（Touches、Overlaps、Crosses、Within、Contains都属于相交）
        /// </summary>
        [DescriptionEx("相交长度最大")]
        InteresctMaxLength = 1024,
    }
}
