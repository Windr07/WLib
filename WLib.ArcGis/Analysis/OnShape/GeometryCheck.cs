/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/3/30 10:40:57
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.OnShape
{
    //参考：http://blog.csdn.net/yh0503/article/details/52432193
    //参考：http://resources.arcgis.com/zh-cn/help/main/10.2/index.html#//001700000034000000
    //代码参考：WLib.ArcGis.Analysis.OnShape.GeometryCheckInfo.GetGeometryCheckInfos()
    //检查几何：ArcToolbox->Datamanagement tools->features->check geometry
    //修复几何：ArcToolbox->Datamanagement tools->features->repair geometry
    //检查和修复几何处理以下问题【esriNonSimpleReasonEnum】：
    //   1、Short segment（短片段）：有些片段比空间参考的系统的单位所允许的值要短，这些空间参考与几何图形相连。短片段错误将被repair geometry工具删除。
    //   2、cfield（无几何图形）：在某些shape字段中，有些要素没有几何图形。无几何图形将被repair geometry工具删除。
    //   3、Incorrect ring ordering（不正确的环走向）：一个面在拓扑学上来说是很简单的，但是它的环的走向不一定是正确的，外环—顺时针；内环—逆时针，不正确的环走向将被修改。
    //   4、Incorrect segment orientation（不正确的片段方向）：不正确的片段有不一致的方向，不正确的片段方向将被修改。
    //   5、Self-intersections（自相交）：每一个部分的内部不能与自己或者其它部分相交，自相交将被修改。
    //   6、Unclosed rings（未封闭的环）：环的首位点必须相连，未封闭的环将被修改。
    //   7、Empty parts（空的部分）：几何图像包含空的部分，空的部分将被修改。
    //   8、重复折点 - 几何的两个或多个折点坐标相同。
    //   9、不匹配的属性 - 某线段端点的 Z 坐标或 M 坐标与下一条线段中与之重合的端点的 Z 坐标或 M 坐标不匹配。
    //   10、不连续的部分 - 几何的某部分由断开的或不连续的部分组成。
    //   11、空的 Z 值 - 几何的一个或多个折点 Z 值为空（例如，NaN）。

    /// <summary>
    /// 检查几何（提供判断图形是否自相交等方法）
    /// </summary>
    public static class GeometryCheck
    {
        /// <summary>
        /// 判断是否自相交
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns>True表示自相交，False表示不自相交</returns>
        public static bool IsSelfCross(this IGeometry geometry)
        {
            ITopologicalOperator3 topologicalOperator = (ITopologicalOperator3)geometry;
            topologicalOperator.IsKnownSimple_2 = false;//布尔型，指示此几何图形是否是已知的（或假设）是拓扑正确。这里赋值false,就是非已知的几何图形

            //reason枚举参考：http://resources.arcgis.com/zh-cn/help/main/10.2/index.html#//001700000034000000
            //返回布尔值，指示该几何图形是否为简单的。如果返回的是false，则可以对输出的"reason"参数检查审查
            if (!topologicalOperator.get_IsSimpleEx(out var reason))
            {
                if (reason == esriNonSimpleReasonEnum.esriNonSimpleSelfIntersections)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断图形是否存在指定几何错误，
        /// 根据esriNonSimpleReasonEnum可判断的几何错误有：自相交、非闭合环、不正确环走向、不正确线段方向、短线段等
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="eNonSimpleReason">几何错误类型，参考：http://resources.arcgis.com/zh-cn/help/main/10.2/index.html#//001700000034000000 ，或参考<see cref="GeometryCheckInfo.GetGeometryCheckInfos"/></param>
        /// <returns>True表示存在指定几何错误，False表示不存在指定几何错误</returns>
        public static bool Check(this IGeometry geometry, esriNonSimpleReasonEnum eNonSimpleReason)
        {
            ITopologicalOperator3 topologicalOperator = (ITopologicalOperator3)geometry;
            topologicalOperator.IsKnownSimple_2 = false;//布尔型，指示此几何图形是否是已知的（或假设）是拓扑正确。这里赋值false,就是非已知的几何图形

            if (!topologicalOperator.get_IsSimpleEx(out var reason))//返回布尔值，指示该几何图形是否为简单的。如果返回的是false，则可以对输出的"reason"参数检查审查
            {
                if (reason == eNonSimpleReason)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 判断图形是否存在几何错误， 
        /// <para>True表示存在错误，False表示未检查出错误，</para>
        /// <para>esriNonSimpleReasonEnum参数表示检查出的错误类型（自相交、非闭合环、不正确环走向、不正确线段方向、短线段等）</para>
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="eNonSimpleReason">几何错误类型，参考：http://resources.arcgis.com/zh-cn/help/main/10.2/index.html#//001700000034000000 ，或参考<see cref="GeometryCheckInfo.GetGeometryCheckInfos"/></param>
        /// <returns>True表示存在指定几何错误，False表示不存在指定几何错误</returns>
        public static bool Check(this IGeometry geometry, out esriNonSimpleReasonEnum eNonSimpleReason)
        {
            ITopologicalOperator3 topologicalOperator = (ITopologicalOperator3)geometry;
            topologicalOperator.IsKnownSimple_2 = false;//布尔型，指示此几何图形是否是已知的（或假设）是拓扑正确。这里赋值false,就是非已知的几何图形

            eNonSimpleReason = esriNonSimpleReasonEnum.esriNonSimpleOK;
            if (!topologicalOperator.get_IsSimpleEx(out var reason))//返回布尔值，指示该几何图形是否为简单的。如果返回的是false，则可以对输出的"reason"参数检查审查
                eNonSimpleReason = reason;

            return eNonSimpleReason != esriNonSimpleReasonEnum.esriNonSimpleOK;
        }
    }
}
