/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.OnShape
{
    /// <summary>
    /// 提供判断图形之间的空间关系的方法
    /// </summary>
    public class RelationalOpt
    {
        /// <summary> 
        /// 检测几何图形A是否包含几何图形B，True为包含，False为不包含
        /// </summary> 
        /// <param name="geometryA">几何图形A</param>         
        /// <param name="geometryB">几何图形B</param>         
        /// <returns>True为包含，False为不包含</returns> 
        public static bool CheckGeometryContains(IGeometry geometryA, IGeometry geometryB)
        {
            return ((IRelationalOperator)geometryA).Contains(geometryB);
        }

        /// <summary>
        /// 检测几何图形A是否与几何图形B相交，True为相交，False为不相交
        /// </summary>
        /// <param name="geometryA">几何图形A</param>
        /// <param name="geometryB">几何图形B</param>
        /// <returns>True为相交，False为不相交</returns>
        public static bool CheckGeometryCrosses(IGeometry geometryA, IGeometry geometryB)
        {
            return ((IRelationalOperator)geometryA).Crosses(geometryB);
        }
    }
}
