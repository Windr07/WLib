/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 14:55:17
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 表示出图时一个图层的设置信息
    /// </summary>
    [Serializable]
    public class LayerInfo : TableInfo
    {
        /// <summary>
        /// 图层在地图中的位置索引
        /// <para>优先通过索引从地图中查找图层，若索引≤-1表示图层在地图中没有固定索引位置，此时通过<see cref="Name"/>来查找图层</para>
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public new string Name { get; set; }
        /// <summary>
        /// 图层定义查询
        /// </summary>
        public new string Definition { get; set; }
        /// <summary>
        /// 图层数据源
        /// </summary>
        public new string DataSource { get; set; }
        /// <summary>
        /// 地图是否缩放至当前图层
        /// <para>值为True时，地图应当缩放至当前图层的<see cref="Definition"/>的指定范围内，若<see cref="Definition"/>为空则缩放至全图层范围</para>
        /// </summary>
        public bool ZoomTo { get; set; }
        /// <summary>
        /// 该图层是否可选
        /// <para>True为可选图层，该图层在找不到时可跳过</para>
        /// <para>False为非可选图层，该图层在找不到时抛出异常，默认False</para>
        /// </summary>
        public bool Optional { get; set; }


        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        public LayerInfo()
        {
            Index = -1;
        }

        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        /// <param name="layerName">图层名称</param>
        /// <param name="dataSource">图层数据源</param>
        /// <param name="index">图层在地图中的索引，-1标识任意索引位置</param>
        /// <param name="layerDefinition">图层定义查询</param>
        /// <param name="zoomTo">地图是否缩放至当前图层</param>
        /// <param name="optional">该图层是否可选（即该图层不是必须的，找不到时可跳过）</param>
        public LayerInfo(string layerName, string dataSource,
            int index = -1, string layerDefinition = null, bool zoomTo = false, bool optional = false)
        {
            Name = layerName;
            DataSource = dataSource;
            Index = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
            Optional = optional;
        }

        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        /// <param name="layerName">图层名称</param>
        /// <param name="workspacePath">图层数据源的工作空间路径</param>
        /// <param name="objectName">图层数据源名称（即要素类名或栅格数据名）</param>
        /// <param name="index">图层在地图中的索引，-1标识任意索引位置</param>
        /// <param name="layerDefinition">图层定义查询</param>
        /// <param name="zoomTo">地图是否缩放至当前图层</param>
        /// <param name="optional">该图层是否可选（即该图层不是必须的，找不到时可跳过）</param>
        public LayerInfo(string layerName, string workspacePath, string objectName,
            int index = -1, string layerDefinition = null, bool zoomTo = false, bool optional = false)
        {
            Name = layerName;
            DataSource = System.IO.Path.Combine(workspacePath, objectName);
            Index = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
            Optional = optional;
        }

        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        /// <param name="layerName">图层名称</param>
        /// <param name="workspacePath">图层数据源的工作空间路径</param>
        /// <param name="datasetName">图层数据源所在数据集名称</param>
        /// <param name="objectName">图层数据源名称（即要素类名或栅格数据名）</param>
        /// <param name="index">图层在地图中的索引，-1标识任意索引位置</param>
        /// <param name="layerDefinition">图层定义查询</param>
        /// <param name="zoomTo">地图是否缩放至当前图层</param>
        /// <param name="optional">该图层是否可选（即该图层不是必须的，找不到时可跳过）</param>
        public LayerInfo(string layerName, string workspacePath, string datasetName, string objectName,
            int index = -1, string layerDefinition = null, bool zoomTo = false, bool optional = false)
        {
            Name = layerName;
            DataSource = System.IO.Path.Combine(workspacePath, datasetName, objectName);
            Index = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
            Optional = optional;
        }
    }
}
