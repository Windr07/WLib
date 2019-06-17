/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// 提供在工作空间中操作表格、要素类、数据集的操作
    /// </summary>
    public static partial class WorkspaceEx
    {
        #region 删除要素类
        /// <summary>
        /// 删除一个或多个要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featueClassNames">要删除的要素类的名称，名称不区分大小写</param>
        public static void DeleteFeatureClasses(this IWorkspace workspace, params string[] featueClassNames)
        {
            if (!(workspace is IFeatureWorkspace featureWorkspace))
                throw new Exception("工作空间不是要素类工作空间！");

            featueClassNames = featueClassNames.Select(v => v.ToLower()).ToArray();
            IFeatureWorkspaceManage featureWorkspaceMange = (IFeatureWorkspaceManage)featureWorkspace;
            IEnumDatasetName enumDatasetName = workspace.DatasetNames[esriDatasetType.esriDTFeatureClass];
            IDatasetName datasetName;
            while ((datasetName = enumDatasetName.Next()) != null)
            {
                if (featueClassNames.Contains(datasetName.Name.ToLower()))
                    featureWorkspaceMange.DeleteByName(datasetName);//删除指定要素类
            }
        }
        /// <summary>
        /// 删除名称包含指定关键字的要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="keyWord">关键字，不区分大小写</param>
        public static void DeleteFeatureClassesByKeyWord(this IWorkspace workspace, string keyWord)
        {
            if (!(workspace is IFeatureWorkspace featureWorkspace))
                throw new Exception("工作空间不是要素类工作空间！");

            keyWord = keyWord.ToLower();
            IFeatureWorkspaceManage featureWorkspaceMange = (IFeatureWorkspaceManage)featureWorkspace;
            IEnumDatasetName enumDatasetName = workspace.DatasetNames[esriDatasetType.esriDTFeatureClass];
            IDatasetName datasetName;
            while ((datasetName = enumDatasetName.Next()) != null)
            {
                if (datasetName.Name.ToLower().Contains(keyWord))
                    featureWorkspaceMange.DeleteByName(datasetName);//删除指定要素类
            }
        }
        /// <summary>
        /// 删除所有符合查询条件的要素（执行sql，此方法执行速度较ITable.DeleteSearchedRows更快）
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="featureClassName"></param>
        /// <param name="whereClause"></param>
        public static void DeleteFeatures(this IWorkspace workspace, string featureClassName, string whereClause)
        {
            whereClause = string.IsNullOrEmpty(whereClause) ? "1=1" : whereClause;
            string sql = $"delete from {featureClassName} where {whereClause}";
            workspace.ExecuteSQL(sql);
        }
        #endregion


        #region 获取表格
        /// <summary>
        /// 判断工作空间中是否存在指定名称/别名的表格
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="tableName">表格名称或别名</param>
        /// <returns></returns>
        public static bool IsTableExsit(this IWorkspace workspace, string tableName)
        {
            if (workspace == null)
                throw new Exception("工作空间不能为空！");
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("参数表格名称(featureClassName)不能为空！");

            tableName = tableName.ToLower();
            foreach (var table in GetTables(workspace))
            {
                if ((table as IObjectClass)?.AliasName.ToLower() == tableName || (table as IDataset)?.Name.ToLower() == tableName)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 获取工作空间下的所有表格
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static ITable GetFirstTable(this IWorkspace workspace)
        {
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTTable];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                return dataset as ITable;
            }
            return null;
        }
        /// <summary>
        /// 获取工作空间下的所有表格
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static List<ITable> GetTables(this IWorkspace workspace)
        {
            var tables = new List<ITable>();
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTTable];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                tables.Add(dataset as ITable);
            }
            return tables;
        }
        /// <summary>
        /// 根据表名或别名，查找工作空间下的表格（不包含放在要素集里面的表，找不到返回null）
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="tableName">表格名称或别名</param>
        /// <returns></returns>
        public static ITable GetITableByName(this IWorkspace workspace, string tableName)
        {
            tableName = tableName.ToLower();
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTTable];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                if (dataset.Name.ToLower() == tableName || (dataset as ITable as IObjectClass)?.AliasName.ToLower() == tableName)
                    return dataset as ITable;
            }
            return null;
        }
        /// <summary>
        /// 根据名称或别名，查找工作空间下表格或要素类，返回ITable对象
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="tableName">表格或要素类的名称或别名</param>
        /// <returns></returns>
        public static ITable GetITableByNameExtent(this IWorkspace workspace, string tableName)
        {
            var featureClass = GetFeatureClassByName(workspace, tableName, false);
            return featureClass == null ? GetITableByName(workspace, tableName) : featureClass as ITable;
        }
        #endregion


        #region 获取要素类
        /// <summary>
        /// 判断工作空间中是否存在指定名称或别名的要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureClassName">要素类名称或别名</param>
        /// <returns></returns>
        public static bool IsFeatureClassExsit(this IWorkspace workspace, string featureClassName)
        {
            if (workspace == null)
                throw new Exception("工作空间不能为空！");
            if (string.IsNullOrEmpty(featureClassName))
                throw new Exception("参数要素类名称(featureClassName)不能为空！");

            featureClassName = featureClassName.ToLower();
            foreach (IFeatureClass featureClass in GetDataset(workspace))
            {
                if (featureClass.AliasName.ToLower() == featureClassName || (featureClass as IDataset)?.Name == featureClassName.ToLower())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 根据要素类的名称或别名，查找工作空间中的数据要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static IFeatureClass GetFirstFeatureClass(this IWorkspace workspace)
        {
            IFeatureClass featureClass;
            var enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            while ((featureClass = enumDataset.Next() as IFeatureClass) != null)
            {
                return featureClass;
            }
            IEnumDataset dsEnumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IDataset dataset;
            while ((dataset = dsEnumDataset.Next()) != null) //遍历要数集
            {
                featureClass = dataset as IFeatureClass;
            }
            return featureClass;
        }
        /// <summary>
        /// 根据要素类的名称或别名，查找工作空间中的数据要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureClassName">要素类名称或别名</param>
        /// <param name="unFindException">当找不到要素类时，是否抛出异常</param>
        /// <returns></returns>
        public static IFeatureClass GetFeatureClassByName(this IWorkspace workspace, string featureClassName, bool unFindException = true)
        {
            if (string.IsNullOrEmpty(featureClassName))
                throw new Exception("查找的数据集名称为空。");

            //遍历FeatureClass
            featureClassName = featureClassName.ToLower();
            foreach (IFeatureClass featureClass in GetDataset(workspace))
            {
                if (featureClass.AliasName.ToLower() == featureClassName || (featureClass as IDataset)?.Name.ToLower() == featureClassName)
                    return featureClass;
            }
            if (unFindException)
                throw new Exception("找不到要素名为：" + featureClassName + "的要素，请检查数据");
            else
                return null;
        }
        /// <summary>
        /// 根据要素类的名称或别名，查找工作空间中的数据要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureClassNames">要素类名称或别名</param>
        /// <returns></returns>
        public static List<IFeatureClass> GetFeatureClassesByNames(this IWorkspace workspace, params string[] featureClassNames)
        {
            var featureClassList = new List<IFeatureClass>();
            if (workspace == null)
                throw new Exception("工作空间为空，检查连接信息。");
            if (featureClassNames == null || featureClassNames.Length == 0)
                throw new Exception("查找的数据集名称为空。");

            //遍历FeatureClass
            featureClassNames = featureClassNames.Select(v => v.ToLower()).ToArray();
            foreach (IFeatureClass featureClass in GetDataset(workspace))
            {
                if (featureClassNames.Contains(featureClass.AliasName.ToLower()) ||
                    featureClassNames.Contains((featureClass as IDataset)?.Name.ToLower()))
                    featureClassList.Add(featureClass);
            }
            return featureClassList;
        }
        /// <summary>
        /// 获取工作空间的全部要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static List<IFeatureClass> GetFeatureClasses(this IWorkspace workspace)
        {
            return GetDataset(workspace).Cast<IFeatureClass>().ToList();
        }
        /// <summary>
        /// 以迭代形式返回工作空间下的所有要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns>要素类枚举数（要素类集合）</returns>
        private static IEnumerable GetDataset(this IWorkspace workspace)
        {
            //工作空间下的要素类
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            IFeatureClass featureClass = enumDataset.Next() as IFeatureClass;
            while (featureClass != null)
            {
                yield return featureClass;
                featureClass = enumDataset.Next() as IFeatureClass;
            }

            //工作空间下的要素集
            IEnumDataset dsEnumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IDataset dataset = dsEnumDataset.Next();
            while (dataset != null)//遍历要数集
            {
                IFeatureDataset featureDataset = (IFeatureDataset)dataset;
                IFeatureClassContainer featureclassContainer = (IFeatureClassContainer)featureDataset;
                IEnumFeatureClass enumFeatureClass = featureclassContainer.Classes;
                IFeatureClass dsFeatureClass = enumFeatureClass.Next();
                while (dsFeatureClass != null)//在每一个数据集中遍历数据层IFeatureClass
                {
                    yield return dsFeatureClass;
                    dsFeatureClass = enumFeatureClass.Next();
                }
                dataset = dsEnumDataset.Next();
            }
        }
        #endregion


        #region 获取、创建要素数据集
        /// <summary>
        /// 获取指定名称的要素数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="datasetName">要素数据集名称</param>
        /// <returns></returns>
        public static IFeatureDataset GetFeatureDataset(this IWorkspace workspace, string datasetName)
        {
            datasetName = datasetName.ToLower();
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                if (dataset.Name.ToLower() == datasetName)
                    return (IFeatureDataset)dataset;
            }
            return null;
        }
        /// <summary>
        /// 获取要素数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static List<IFeatureDataset> GetFeatureDatasets(this IWorkspace workspace)
        {
            List<IFeatureDataset> result = new List<IFeatureDataset>();
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                result.Add((IFeatureDataset)dataset);
            }
            return result;
        }
        /// <summary>
        /// 创建要素数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureDatasetName">要素数据集名称</param>
        /// <param name="spatialRef">坐标系</param>
        /// <returns></returns>
        public static IFeatureDataset CreateFeatureDataset(this IWorkspace workspace, string featureDatasetName, ISpatialReference spatialRef)
        {
            return ((IFeatureWorkspace)workspace).CreateFeatureDataset(featureDatasetName, spatialRef);
        }
        #endregion
    }
}
