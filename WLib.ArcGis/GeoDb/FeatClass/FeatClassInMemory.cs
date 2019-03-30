/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 提供操作内存要素类的方法
    /// </summary>
    public class FeatClassInMemory
    {
        /// <summary>
        /// 创建一个新的内存要素类对象
        /// </summary>
        /// <param name="className">要素类名称</param>
        /// <param name="fields">字段集</param>
        /// <param name="strWorkspaceName">内存工作空间的名称</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClassInMemory(string className, IFields fields, string strWorkspaceName = "InMemoryWorkspace")
        {
            IWorkspaceFactory inMemoryWorkspaceFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName workspaceName = inMemoryWorkspaceFactory.Create("", strWorkspaceName, null, 0);
            IName wsName = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)wsName.Open();
            IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;
            return featureWorkspace.CreateFeatureClass(className, fields, null, null, esriFeatureType.esriFTSimple, "SHAPE", "");
        }
        /// <summary>
        /// 在内存中创建新要素类，根据查询条件复制要素到新要素类，返回新要素类
        /// </summary> 
        /// <param name="sourceClass">要素类</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="memoryClassName">内存要素类的名称</param>
        /// <returns></returns>
        public static IFeatureClass CopyFeatureClassToMemory(IFeatureClass sourceClass, string whereClause, string memoryClassName = "tempFeatureClass")
        {
            IFields fields = Fields.FieldOpt.CloneFeatureClassFields(sourceClass);
            IFeatureClass memoryClass = CreateFeatureClassInMemory(memoryClassName, fields);
            IQueryFilter filter = new QueryFilterClass();
            filter.WhereClause = whereClause;
            IFeatureCursor cursor = sourceClass.Search(filter, false);
            IFeature feature;
            while ((feature = cursor.NextFeature()) != null)
            {
                IFeature tmpFeature = memoryClass.CreateFeature();
                tmpFeature.Shape = feature.ShapeCopy;
                tmpFeature.Store();
            }
            return memoryClass;
        }
    }
}
