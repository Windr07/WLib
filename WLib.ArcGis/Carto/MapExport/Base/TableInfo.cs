/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/17 15:54:09
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 表示出图时一个表格的设置信息
    /// </summary>
    [Serializable]
    public class TableInfo
    {
        /// <summary>
        /// 表格名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表格定义查询
        /// </summary>
        public string Definition { get; set; }
        /// <summary>
        /// 表格数据源
        /// </summary>
        public string DataSource { get; set; }
        /// <summary>
        /// 表示出图时一个表格的设置信息
        /// </summary>
        public TableInfo()
        {
        }
        /// <summary>
        /// 表示出图时一个表格的设置信息
        /// </summary>
        /// <param name="name">表格名称</param>
        /// <param name="definition">表格定义查询</param>
        /// <param name="dataSource">表格数据源</param>
        public TableInfo(string name, string definition = null, string dataSource = null)
        {
            Name = name;
            Definition = definition;
            DataSource = dataSource;
        }
    }
}
