using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.ArcGis.Analysis.Topology
{
    /// <summary>
    /// 指定拓扑规则下的全部拓扑错误图形的信息
    /// </summary>
    public class TopoErrorInfoCollection : List<TopoErrorInfo>
    {
        /// <summary>
        /// 拓扑规则
        /// </summary>
        public esriTopologyRuleType TopoRuleType { get; set; }

        /// <summary>
        /// 显示拓扑规则描述
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ((ECnTopoRuleType)(int)TopoRuleType).ToString();


        /// <summary>
        /// 指定拓扑规则下的全部拓扑错误图形的信息
        /// </summary>
        public TopoErrorInfoCollection() { }
        /// <summary>
        /// 指定拓扑规则下的全部拓扑错误图形的信息
        /// </summary>
        /// <param name="topoRuleType">拓扑规则</param>
        /// <param name="infos">拓扑错误图形的信息</param>
        public TopoErrorInfoCollection(esriTopologyRuleType topoRuleType, params TopoErrorInfo[] infos)
        {
            TopoRuleType = topoRuleType;
            AddRange(infos);
        }
    }
}
