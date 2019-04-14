/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/11 17:37:58
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ArcGis.Analysis.Gp
{
    /// <summary>
    /// 空间连接工具匹配选项枚举（Spatial Join - match_option，from arcgis desktop help）
    /// </summary>
    public enum ESjMatchOption
    {
        /// <summary>
        /// 如果连接要素与目标要素相交，将匹配连接要素中相交的要素（这是默认设置）
        /// </summary>
        INTERSECT,
        /// <summary>
        /// 如果连接要素中的要素与三维空间（x、y 和 z）中的某一目标要素相交，则将匹配这些要素
        /// </summary>
        INTERSECT_3D,
        /// <summary>
        /// 如果连接要素在目标要素的指定距离之内，将匹配处于该距离内的要素。在搜索半径参数中指定距离
        /// </summary>
        WITHIN_A_DISTANCE,
        /// <summary>
        /// 如果在三维空间中，连接要素在目标要素的指定距离之内，将匹配处于该距离内的要素。在搜索半径参数中指定距离
        /// </summary>
        WITHIN_A_DISTANCE_3D,
        /// <summary>
        /// 如果目标要素中包含连接要素中的要素，将匹配连接要素中被包含的要素。
        /// 目标要素必须是面或折线。目标要素不能为点，且仅当目标要素为面时连接要素才能为面
        /// </summary>
        CONTAINS,
        /// <summary>
        /// 如果目标要素完全包含连接要素中的要素，将匹配连接要素中被包含的要素。
        /// 面可以完全包含任意要素。点不能完全包含任意要素，甚至不能包含点。面只能完全包含折线和点
        /// </summary>
        COMPLETELY_CONTAINS,
        /// <summary>
        /// 该空间关系产生的结果同 COMPLETELY_CONTAINS，但有一种情况例外：
        /// 如果连接要素完全位于目标要素的边界上（没有任何一部分完全位于里面或外面），则不会匹配要素。
        /// CLEMENTINI 将边界面定义为用来分隔内部和外部的线，将线的边界定义为其端点，点的边界始终为空
        /// </summary>
        CONTAINS_CLEMENTINI,
        /// <summary>
        /// 如果目标要素位于连接要素内，将匹配连接要素中包含目标要素的要素。它与 CONTAINS 相反。
        /// 对于此选项，只有当连接要素也为面时目标要素才可为面。只有当点为目标要素时点才可为连接要素
        /// </summary>
        WITHIN,
        /// <summary>
        /// 如果目标要素完全在连接要素范围内，则匹配连接要素中完全包含目标要素的要素。这与 COMPLETELY_CONTAINS 相反
        /// </summary>
        COMPLETELY_WITHIN,
        /// <summary>
        /// 结果同 WITHIN，但下述情况例外：如果连接要素中的全部要素均位于目标要素的边界上，则不会匹配要素。
        /// CLEMENTINI 将边界面定义为用来分隔内部和外部的线，将线的边界定义为其端点，点的边界始终为空
        /// </summary>
        WITHIN_CLEMENTINI,
        /// <summary>
        /// 如果连接要素与目标要素相同，将匹配连接要素中相同的要素。
        /// 连接要素和目标要素必须具有相同的 shape 类型：点到点、线到线和面到面
        /// </summary>
        ARE_IDENTICAL_TO,
        /// <summary>
        /// 如果连接要素中具有边界与目标要素相接的要素，将匹配这些要素。
        /// 连接要素和目标要素必须是线或面。此外，连接要素中的要素必须在目标面的外部或完全在其内部
        /// </summary>
        BOUNDARY_TOUCHES,
        /// <summary>
        /// 如果连接要素中具有与目标要素共线的要素，将匹配这些要素。连接要素和目标要素必须是线或面
        /// </summary>
        SHARE_A_LINE_SEGMENT_WITH,
        /// <summary>
        /// 如果连接要素中具有轮廓与目标要素交叉的要素，则将匹配这些要素。连接要素和目标要素必须是线或面。
        /// 如果将面用于连接或目标要素，则会使用面的边界（线）。将匹配在某一点交叉的线，而不是共线的线
        /// </summary>
        CROSSED_BY_THE_OUTLINE_OF,
        /// <summary>
        /// 如果目标要素的中心位于连接要素内，将匹配这些要素。
        /// 要素中心的计算方式如下：对于面和多点，将使用几何的质心；对于线输入，则会使用几何的中点。 
        /// </summary>
        HAVE_THEIR_CENTER_IN,
        /// <summary>
        /// 匹配连接要素中与目标要素最近的要素
        /// </summary>
        CLOSEST,
    }
}
