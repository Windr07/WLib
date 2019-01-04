/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;

namespace WLib.ArcGis.Carto.DataSource
{
    /// <summary>
    /// 地图数据框中，图层或表格与其对应数据源名称信息
    /// </summary>
    public class MapSource
    {
        /// <summary>
        /// 图层或表所在的地图数据框
        /// </summary>
        public string MapFrameName { get; set; }
        /// <summary>
        /// 工作空间路径
        /// </summary>
        public string SourcePath { get; set; }
        /// <summary>
        /// 地图数据框中的表格（图层）名称与其对应数据源名称
        /// </summary>
        /// <example>key:接图表; value:jtb2000</example>
        public Dictionary<string, string> ViewNames2SourceNames { get; }
        /// <summary>
        /// 获取指定图层名(表名)对应的数据源
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string this[string tableName] => ViewNames2SourceNames[tableName];


        /// <summary>
        /// 地图数据框中，图层或表格与其对应数据源名称信息
        /// （要求这些图层或表格在同一数据源(dataset或workspace)当中）
        /// </summary>
        public MapSource()
        {
            
        }
        /// <summary>
        /// 地图数据框中，图层或表格与其对应数据源名称信息
        /// （要求这些图层或表格在同一数据源(dataset或workspace)当中）
        /// </summary>
        /// <param name="mapFrameName">图层或表所属的地图数据框</param>
        /// <param name="sourcePath">图层或表数据源路径</param>
        public MapSource(string mapFrameName, string sourcePath)
        {
            MapFrameName = mapFrameName;
            SourcePath = sourcePath;
            ViewNames2SourceNames = new Dictionary<string, string>();
        }


        /// <summary>
        /// 添加一个图层名或表名与其对应的数据源名称
        /// </summary>
        /// <param name="tableName">图层名</param>
        /// <param name="sourceName">图层关联的数据源对象名称</param>
        public void Add(string tableName, string sourceName)
        {
            ViewNames2SourceNames.Add(tableName, sourceName);
        }
        /// <summary>
        /// 添加图层名(表名)与其对应的数据源名称键值对
        /// </summary>
        /// <param name="tableName2SourceNameDict"></param>
        public void AddRange(Dictionary<string, string> tableName2SourceNameDict)
        {
            foreach (var p in tableName2SourceNameDict)
            {
                ViewNames2SourceNames.Add(p.Key, p.Value);
            }
        }
    }
}
