/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 14:55:17
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 表示出图时一个图层的设置信息
    /// </summary>
    public class LayerInfo
    {
        /// <summary>
        /// 图层在地图中的位置索引，索引≤-1表示图层在地图中没有固定索引位置
        /// </summary>
        public int LayerIndex { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName { get; set; }
        /// <summary>
        /// 图层定义查询
        /// </summary>
        public string Definition { get; set; }
        /// <summary>
        /// 图层数据源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 地图是否缩放至当前图层
        /// </summary>
        public bool ZoomTo { get; set; }


        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        public LayerInfo()
        {
            LayerIndex = -1;
        }

        /// <summary>
        /// 表示出图时一个图层的设置信息
        /// </summary>
        /// <param name="layerName">图层名称</param>
        /// <param name="dataSource">图层数据源</param>
        /// <param name="index">图层在地图中的索引，-1标识任意索引位置</param>
        /// <param name="layerDefinition">图层定义查询</param>
        /// <param name="zoomTo">地图是否缩放至当前图层</param>
        public LayerInfo(string layerName, string dataSource,
            int index = -1, string layerDefinition = null, bool zoomTo = false)
        {
            LayerName = layerName;
            DataSource = dataSource;
            LayerIndex = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
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
        public LayerInfo(string layerName, string workspacePath, string objectName,
            int index = -1, string layerDefinition = null, bool zoomTo = false)
        {
            LayerName = layerName;
            DataSource = System.IO.Path.Combine(workspacePath, objectName);
            LayerIndex = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
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
        public LayerInfo(string layerName, string workspacePath, string datasetName, string objectName,
            int index = -1, string layerDefinition = null, bool zoomTo = false)
        {
            LayerName = layerName;
            DataSource = System.IO.Path.Combine(workspacePath, datasetName, objectName);
            LayerIndex = index;
            Definition = layerDefinition;
            ZoomTo = zoomTo;
        }
    }
}
