/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.GeoDb.Table;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 属性查询帮助类
    /// </summary>
    public class AttributeQueryHelper
    {
        /// <summary>
        /// 查询的表(或图层)
        /// </summary>
        public ITable SearchTable { get; }
        /// <summary>
        /// 确定查询的是字段别名还是字段名
        /// </summary>
        public bool IsQueryAaliasName { get; }
        /// <summary>
        /// 要查询的字段（名称或别名均可，此值为null时则查询所有字段）
        /// </summary>
        public string[] QueryFieldNames { get; }
        /// <summary>
        /// 查询的表(或图层)的名称
        /// </summary>
        public string TableName => ((IDataset)SearchTable).Name;
        /// <summary>
        /// 字段集
        /// </summary>
        public FieldItem[] FieldItems => SearchTable.Fields.GetFieldItems(QueryFieldNames).ToArray();
        /// <summary>
        /// 属性查询帮助类
        /// </summary>
        /// <param name="searchTable"></param>
        /// <param name="isQueryAaliasName"></param>
        /// <param name="queryFieldNames"></param>
        public AttributeQueryHelper(ITable searchTable, bool isQueryAaliasName, string[] queryFieldNames)
        {
            SearchTable = searchTable;
            IsQueryAaliasName = isQueryAaliasName;
            QueryFieldNames = queryFieldNames;
        }


        /// <summary>
        /// 根据字段名或别名查找字段
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public FieldItem QueryField(string value)
        {
            return FieldItems.FirstOrDefault(v => v.Name.StartsWith(value) || v.AliasName.StartsWith(value));
        }
        /// <summary>
        /// 获取指定字段的全部唯一值
        /// </summary>
        /// <param name="fieldItem"></param>
        /// <returns></returns>
        public object[] GetUnqiueValues(FieldItem fieldItem)
        {
            return SearchTable.GetUniqueValues(fieldItem.Name).ToArray();
        }
    }
}
