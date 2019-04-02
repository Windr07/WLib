/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.ArcGis.GeoDatabase.Table
{
    /// <summary>
    /// 提供创建表格的方法
    /// </summary>
    public static class TableCreate
    {
        /// <summary>
        /// 创建一个空表格（仅包含OID字段）
        /// </summary>
        /// <param name="featureWorkspace">创建表格的工作空间</param>
        /// <param name="name">表格名称</param>
        /// <returns></returns>
        public static ITable Create(this IFeatureWorkspace featureWorkspace, string name)
        {
            IFields fields = new FieldsClass();
            IFieldsEdit fieldsEdit = (IFieldsEdit)fields;
            fieldsEdit.AddField(FieldOpt.CreateOidField());
            return featureWorkspace.CreateTable(name, fields, null, null, "");
        }
        /// <summary>
        /// 创建一个空表格
        /// </summary>
        /// <param name="featureWorkspace">创建表格的工作空间</param>
        /// <param name="name">表格名称</param>
        /// <param name="fields">字段集，注意必须包含OID字段</param>
        /// <returns></returns>
        public static ITable Create(this IFeatureWorkspace featureWorkspace, string name, IFields fields)
        {
            return featureWorkspace.CreateTable(name, fields, null, null, "");
        }
        /// <summary>
        /// 复制源表格的表结构，创建一个空的表格
        /// </summary>
        /// <param name="sourceTable">源表格</param>
        /// <param name="featureWorkspace">创建新表格的工作空间</param>
        /// <param name="tableName">新表格的名称</param>
        /// <param name="tableAliasName">新表格的别名，值为null则别名与名字相同</param>
        /// <returns></returns>
        public static ITable Create(this IFeatureWorkspace featureWorkspace, ITable sourceTable, string tableName, string tableAliasName = null)
        {
            var feilds = sourceTable.CloneTableFields(true);
            var table = Create(featureWorkspace, tableName, feilds);

            if (!string.IsNullOrEmpty(tableAliasName))
                table.RenameTableAliasName(tableAliasName);
            return table;
        }
    }
}
