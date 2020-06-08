using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.DataCheck.Core
{
    public class SpatialMatchTypesHelper
    {
        /// <summary>
        /// 全部图斑空间关系匹配方式
        /// </summary>
        public static ESpatialMatchTypes[] AllSpatialMatchTypes { get; } = Enum.GetValues(typeof(ESpatialMatchTypes)).Cast<ESpatialMatchTypes>().ToArray();

        /// <summary>
        /// “空间关系匹配方式”与对应的“ArcGIS空间关系枚举”键值对
        /// </summary>
        public static Dictionary<ESpatialMatchTypes, esriSpatialRelEnum> SpatialRelDict { get; } = new Dictionary<ESpatialMatchTypes, esriSpatialRelEnum>()
        {
            { ESpatialMatchTypes.Intersects, esriSpatialRelEnum.esriSpatialRelIntersects },
            { ESpatialMatchTypes.EnvelopeIntersects, esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects },
            { ESpatialMatchTypes.IndexIntersects, esriSpatialRelEnum.esriSpatialRelIndexIntersects },
            { ESpatialMatchTypes.Touches, esriSpatialRelEnum.esriSpatialRelTouches },
            { ESpatialMatchTypes.Overlaps, esriSpatialRelEnum.esriSpatialRelOverlaps },
            { ESpatialMatchTypes.Crosses, esriSpatialRelEnum.esriSpatialRelCrosses },
            { ESpatialMatchTypes.Within, esriSpatialRelEnum.esriSpatialRelWithin },
            { ESpatialMatchTypes.Contains, esriSpatialRelEnum.esriSpatialRelContains },
            { ESpatialMatchTypes.Relation, esriSpatialRelEnum.esriSpatialRelRelation },
            { ESpatialMatchTypes.InteresctMaxArea, esriSpatialRelEnum.esriSpatialRelIntersects },
            { ESpatialMatchTypes.InteresctMaxLength, esriSpatialRelEnum.esriSpatialRelIntersects },
        };
    }
}
