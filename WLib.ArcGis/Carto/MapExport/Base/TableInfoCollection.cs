/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/17 15:58:30
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.ArcGis.Carto.MapExport.Base
{
    /// <summary>
    /// 出图时的表格的设置信息集合
    /// </summary>
    [Serializable]
    public class TableInfoCollection : List<TableInfo>
    {
        /// <summary>
        /// 出图时的表格的设置信息集合
        /// </summary>
        public TableInfoCollection() { }
        /// <summary>
        /// 出图时的表格的设置信息集合
        /// </summary>
        /// <param name="collection"></param>
        public TableInfoCollection(IEnumerable<TableInfo> collection) : base(collection) { }

        /// <summary>
        /// 根据表格名获取表格信息
        /// </summary>
        /// <param name="tableName">表格名称</param>
        /// <returns></returns>
        public TableInfo this[string tableName] => this.First(v => v.Name == tableName);
    }
}
