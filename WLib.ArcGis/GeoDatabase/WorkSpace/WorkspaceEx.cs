/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using WLib.ArcGis.GeoDatabase.FeatClass;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// 提供在工作空间中操作表格、要素类、数据集的操作
    /// </summary>
    public static partial class WorkspaceEx
    {
        #region 获取各类数据集
        /// <summary>
        /// 获取工作空间下指定类型、指定名称或别名的数据集，找不到返回null
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="datasetType">数据集类型</param>
        /// <param name="datasetName">数据集名称，例如要素数据集名、栅格数据集名、表格名或别名、要素类名或别名、拓扑名等</param>
        /// <returns></returns>
        public static IDataset GetDataset(this IWorkspace workspace, esriDatasetType datasetType, string datasetName)
        {
            datasetName = datasetName.ToLower();
            IEnumDataset enumDataset = workspace.Datasets[datasetType];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                if (dataset.Name.ToLower() == datasetName || (dataset as IObjectClass)?.AliasName.ToLower() == datasetName)
                    return dataset;
            }
            return null;
        }
        /// <summary>
        /// 获取工作空间下指定类型的第一个数据集，找不到返回null
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="datasetType">数据集类型</param>
        /// <returns></returns>
        public static IDataset GetFirstDataset(this IWorkspace workspace, esriDatasetType datasetType)
        {
            return workspace.Datasets[datasetType].Next();
        }
        /// <summary>
        /// 获取工作空间下指定类型的数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="datasetType">数据集类型</param>
        /// <returns></returns>
        public static IEnumerable<IDataset> GetDatasets(this IWorkspace workspace, esriDatasetType datasetType)
        {
            IEnumDataset enumDataset = workspace.Datasets[datasetType];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                yield return dataset;
            }
        }
        #endregion


        #region 获取要素数据集
        /// <summary>
        /// 获取工作空间下指定名称的要素数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="datasetName">要素数据集名称</param>
        /// <returns></returns>
        public static IFeatureDataset GetFeatureDataset(this IWorkspace workspace, string datasetName)
        {
            var dataset = GetDataset(workspace, esriDatasetType.esriDTFeatureDataset, datasetName);
            return dataset == null ? null : dataset as IFeatureDataset;
        }
        /// <summary>
        /// 获取工作空间下全部要素数据集
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static IEnumerable<IFeatureDataset> GetFeatureDatasets(this IWorkspace workspace)
        {
            return GetDatasets(workspace, esriDatasetType.esriDTFeatureDataset).Cast<IFeatureDataset>();
        }
        #endregion


        #region 创建要素数据集
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


        #region 创建要素类
        /// <summary>
        /// 将指定要素类复制到工作空间中
        /// </summary>
        /// <param name="workspace"></param>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetClassName">新要素类名称，若为null则使用源要素类名称</param>
        /// <param name="whereClause">筛选条件，从源要素类中筛选指定的要素复制到目标要素，为null或Empty时将复制全部要素</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(this IWorkspace workspace, IFeatureClass sourceClass, string targetClassName = null, string whereClause = null)
        {
            return FeatureClassEx.CopyDataTo(sourceClass, workspace, targetClassName, whereClause);
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="obj">IWorkspace、IFeatureWorkspace或IFeatureDataset对象，在该对象中创建要素类</param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="fields">要创建的字段集（必须包含SHAPE字段），可参考<see cref="FieldOpt.CreateBaseFields"/>等方法创建字段集</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(this IWorkspace workspace, string name, IFields fields)
        {
            return FeatureClassEx.Create(workspace, name, fields);
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="sptialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="SpatialRefOpt.CreateSpatialRef(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="geometryType">几何类型（点/线/面等）</param>
        /// <param name="fields">要创建的字段集（可以为null，该方法自动修改或加入OID和SHAPE字段以确保几何类型、坐标系与参数一致）</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(this IWorkspace workspace, string name, ISpatialReference sptialRef, esriGeometryType geometryType, IFields fields = null)
        {
            return FeatureClassEx.Create(workspace, name, sptialRef, geometryType, fields);
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
            foreach (IFeatureClass featureClass in GetAllFeatureClasses(workspace))
            {
                if (featureClass.AliasName.ToLower() == featureClassName || (featureClass as IDataset)?.Name == featureClassName.ToLower())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 根据要素类的名称或别名，查找工作空间中的数据要素类，找不到则返回null
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns>返回要素类，找不到则返回null</returns>
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
        /// 根据要素类的名称或别名（或名称/别名的关键字），查找工作空间中的数据要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureClassName">要素类名称或别名</param>
        /// <param name="unFindException">当找不到要素类时，是否抛出异常</param>
        /// <param name="nameIsKeyword">表示是否按关键字查找，True则按关键字模糊匹配，False则名称/别名必须完全匹配</param>
        /// <returns></returns>
        public static IFeatureClass GetFeatureClassByName(this IWorkspace workspace, string featureClassName, bool unFindException = true, bool nameIsKeyword = false)
        {
            if (string.IsNullOrEmpty(featureClassName))
                throw new Exception("查找的数据类名称为空。");

            //遍历FeatureClass
            featureClassName = featureClassName.ToLower();
            foreach (IFeatureClass featureClass in GetAllFeatureClasses(workspace))
            {
                if (nameIsKeyword)
                {
                    if (featureClass.AliasName.ToLower().Contains(featureClassName) || ((IDataset)featureClass).Name.ToLower().Contains(featureClassName))
                        return featureClass;
                }
                else
                {
                    if (featureClass.AliasName.ToLower() == featureClassName || ((IDataset)featureClass).Name.ToLower() == featureClassName)
                        return featureClass;
                }
            }
            if (unFindException)
                throw new Exception("找不到名为：" + featureClassName + "的要素类，请检查数据");
            else
                return null;
        }
        /// <summary>
        /// 根据要素类的名称或别名，查找工作空间中的数据要素类（找不到自动跳过）
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="featureClassNames">要素类名称或别名</param>
        /// <returns></returns>
        public static IEnumerable<IFeatureClass> GetFeatureClassesByNames(this IWorkspace workspace, params string[] featureClassNames)
        {
            if (workspace == null) throw new Exception("工作空间为空，检查连接信息。");
            if (featureClassNames == null || featureClassNames.Length == 0) throw new Exception("查找的数据类名称为空。");

            featureClassNames = featureClassNames.Select(v => v.ToLower()).ToArray();
            foreach (IFeatureClass featureClass in GetAllFeatureClasses(workspace))
            {
                if (featureClassNames.Contains(featureClass.AliasName.ToLower()) ||
                    featureClassNames.Contains((featureClass as IDataset)?.Name.ToLower()))
                    yield return featureClass;
            }
        }
        /// <summary>
        /// 根据要素类的名称/别名的关键字，查找工作空间中的数据要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <param name="keyName">要素类名称或别名</param>
        /// <returns></returns>
        public static IEnumerable<IFeatureClass> GetFeatureClasses(this IWorkspace workspace, string keyName)
        {
            if (workspace == null) throw new Exception("工作空间为空，检查连接信息。");
            if (keyName == null) throw new Exception("查找的数据类名称为空。");

            keyName = keyName.ToLower();
            foreach (IFeatureClass featureClass in GetAllFeatureClasses(workspace))
            {
                if (featureClass.AliasName.ToLower().Contains(keyName) || ((IDataset)featureClass).Name.Contains(keyName))
                    yield return featureClass;
            }
        }
        /// <summary>
        /// 获取工作空间的全部要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static List<IFeatureClass> GetFeatureClasses(this IWorkspace workspace)
        {
            return GetAllFeatureClasses(workspace).Cast<IFeatureClass>().ToList();
        }
        /// <summary>
        /// 以迭代形式返回工作空间下的所有要素类，包括数据集中的要素类
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns>要素类枚举数（要素类集合）</returns>
        private static IEnumerable<IFeatureClass> GetAllFeatureClasses(this IWorkspace workspace)
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
        #endregion


        #region 删除要素
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
        public static IEnumerable<ITable> GetTables(this IWorkspace workspace)
        {
            IEnumDataset enumDataset = workspace.Datasets[esriDatasetType.esriDTTable];
            IDataset dataset;
            while ((dataset = enumDataset.Next()) != null)
            {
                yield return dataset as ITable;
            }
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
    }
}
