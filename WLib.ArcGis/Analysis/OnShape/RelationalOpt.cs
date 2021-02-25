/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： 参考：https://blog.csdn.net/yinjun151/article/details/51811555
                http://resources.esri.com/help/9.3/ArcGISEngine/dotnet/40de6491-9b2d-440d-848b-2609efcd46b1.htm
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.OnShape
{
    /// <summary>
    /// 提供判断图形之间的空间关系的方法
    /// <para> 空间关系包括：包含Contains、相交Crosses、相等Equals、相接Touches、内部Within、不相交Disjoint、重叠Overlaps</para>
    /// </summary>
    public static class RelationalOpt
    {
        /// <summary> 
        /// 检测几何图形A是否包含几何图形B
        /// <para>包括：点包含点、线包含点、线包含线、面包含点、面包含线、面包含面</para>
        /// </summary> 
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为包含，False为不包含</returns> 
        public static bool Contains(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Contains(geometryB);
        /// <summary>
        /// 检测几何图形A是否与几何图形B相交
        /// <para>包括：线与线、面与线、线与面相交</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>
        /// <param name="geometryB">几何图形B</param>
        /// <returns>True为相交，False为不相交</returns>
        public static bool Crosses(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Crosses(geometryB);
        /// <summary>
        ///  检测几何图形A是否与几何图形B相同（即同一几何类型且构成图形的所有坐标点完全一致）
        /// <para>只存在三种比较：点与点、线与线、面与面</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为相同，False为不相同</returns> 
        public static bool Equals(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Equals(geometryB);
        /// <summary>
        ///  检测几何图形A是否与几何图形B相接
        /// <para>除了不能用与点与点，其他图形都可以进行判断</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为相接，False为不相接</returns> 
        public static bool Touches(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Touches(geometryB);
        /// <summary>
        /// 检测几何图形A是否在几何图形B内部（与Contains相反）
        /// <para>包括：点被点、点被线、点被面、线被线、线被面和面被面包含</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为被包含，False为不被包含</returns> 
        public static bool Within(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Within(geometryB);
        /// <summary>
        /// 检测几何图形A是否与几何图形B不相交
        /// <para>涵盖点、线、面所有的相互关系</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为不相交，False为存在相交</returns> 
        public static bool Disjoint(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Disjoint(geometryB);
        /// <summary>
        /// 检测几何图形A是否与几何图形B重叠
        /// <para>多点与多点、线与线、面与面，若图形为空，则不存在重叠关系</para>
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为B重叠，False为不B重叠</returns> 
        public static bool Overlaps(this IGeometry geometryA, IGeometry geometryB) => ((IRelationalOperator)geometryA).Overlaps(geometryB);
        /// <summary>
        /// 检测几何图形A是否与几何图形B存在指定关系
        /// （使用参考：http://resources.esri.com/help/9.3/ArcGISEngine/dotnet/40de6491-9b2d-440d-848b-2609efcd46b1.htm）
        /// </summary>
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <param name="relationDescription">参考： http://resources.esri.com/help/9.3/ArcGISEngine/dotnet/40de6491-9b2d-440d-848b-2609efcd46b1.htm</param>
        public static bool Relation(this IGeometry geometryA, IGeometry geometryB, string relationDescription) => ((IRelationalOperator)geometryA).Relation(geometryB, relationDescription);
    }
}
