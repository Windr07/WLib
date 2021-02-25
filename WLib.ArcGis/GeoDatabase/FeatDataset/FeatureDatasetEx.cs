/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.FeatClass;

namespace WLib.ArcGis.GeoDatabase.FeatDataset
{
    /// <summary>
    /// 提供对要素数据集的增、删、改、查等操作
    /// </summary>
    public static partial class FeatureDatasetEx
    {
        /// <summary>  
        /// 获取要素数据集中所有的要素类的名称和别名的键值对
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns>返回数据集中所有包含的要素类的名称和别名键值对</returns>  
        public static Dictionary<string, string> GetFeatureClassNames(this IFeatureDataset featureDataset)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
            IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
            IFeatureClass featureClass = enumFeatureClass.Next();
            while (featureClass != null)
            {
                result.Add(((IDataset)featureClass).Name, featureClass.AliasName);
                featureClass = enumFeatureClass.Next();
            }
            Marshal.ReleaseComObject(enumFeatureClass);
            Marshal.ReleaseComObject(featureClassContainer);
            return result;
        }


        #region 获取要素类
        /// <summary>
        /// 获取要素数据集中指定名称或别名的要素类
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <param name="featureClassName">要素类名称或别名</param>
        /// <returns></returns>
        public static IFeatureClass GetFeatureClassByName(this IFeatureDataset featureDataset, string featureClassName)
        {
            featureClassName = featureClassName.ToLower();
            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
            IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
            IFeatureClass featureClass;
            while ((featureClass = enumFeatureClass.Next()) != null)
            {
                if (featureClass.AliasName.ToLower() == featureClassName || (featureClass as IDataset)?.Name.ToLower() == featureClassName)
                    break;
            }

            Marshal.ReleaseComObject(enumFeatureClass);
            Marshal.ReleaseComObject(featureClassContainer);
            return featureClass;
        }
        /// <summary>
        /// 获取要素数据集中指定名称或别名的要素类
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns></returns>
        public static IFeatureClass GetFirstFeatureClass(this IFeatureDataset featureDataset)
        {
            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
            IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
            Marshal.ReleaseComObject(enumFeatureClass);
            Marshal.ReleaseComObject(featureClassContainer);
            return enumFeatureClass.Next();
        }
        /// <summary>  
        /// 获取要素数据集中所有的要素类  
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns>返回数据集中所有包含的要素类</returns>  
        public static List<IFeatureClass> GetFeatureClasses(this IFeatureDataset featureDataset)
        {
            List<IFeatureClass> result = new List<IFeatureClass>();
            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
            IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
            IFeatureClass featureClass = enumFeatureClass.Next();
            while (featureClass != null)
            {
                result.Add(featureClass);
                featureClass = enumFeatureClass.Next();
            }
            Marshal.ReleaseComObject(enumFeatureClass);
            Marshal.ReleaseComObject(featureClassContainer);
            return result;
        }
        #endregion


        #region 创建、导入要素类
        /// <summary>
        /// 将指定要素类复制到要素数据集中
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <param name="sourceClass">源要素类</param>
        /// <param name="targetClassName">新要素类名称，若为null则使用源要素类名称</param>
        /// <param name="whereClause">筛选条件，从源要素类中筛选指定的要素复制到目标要素，为null或Empty时将复制全部要素</param>
        /// <returns></returns>
        public static IFeatureClass ImportFeatureClass(this IFeatureDataset featureDataset, IFeatureClass sourceClass, string targetClassName = null, string whereClause = null)
        {
            return FeatureClassEx.CopyTo(sourceClass, featureDataset, targetClassName, whereClause);
        }
        /// <summary>
        /// 将指定要素类复制到要素数据集中
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <param name="sourceClassPath">源要素类的完整路径，支持shp、mdb、dwg、gdb中的要素类，详情参考<see cref="FeatureClassEx.FromPath(string, bool)"/>方法</param>
        /// <param name="targetClassName">新要素类名称，若为null则使用源要素类名称</param>
        /// <param name="whereClause">筛选条件，从源要素类中筛选指定的要素复制到目标要素，为null或Empty时将复制全部要素</param>
        /// <returns></returns>
        public static IFeatureClass ImportFeatureClass(this IFeatureDataset featureDataset, string sourceClassPath, string targetClassName = null, string whereClause = null)
        {
            var sourceClass = FeatureClassEx.FromPath(sourceClassPath);
            return FeatureClassEx.CopyTo(sourceClass, featureDataset, targetClassName, whereClause);
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="fields">要创建的字段集（必须包含SHAPE字段），可参考<see cref="FieldOpt.CreateBaseFields"/>等方法创建字段集</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(this IFeatureDataset featureDataset, string name, IFields fields)
        {
            return FeatureClassEx.Create(featureDataset, name, fields);
        }
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <param name="name">要素类名称（如果为shapefile,不能包含文件扩展名".shp"）</param>
        /// <param name="sptialRef">空间参考坐标系。若参数obj为IFeatureDataset则应赋值为null；否则不能为null，
        /// 可使用<see cref="SpatialRefOpt.CreateSpatialRef(esriSRProjCS4Type)"/>或其重载方法进行创建</param>
        /// <param name="geometryType">几何类型（点/线/面等）</param>
        /// <param name="fields">要创建的字段集（可以为null，该方法自动修改或加入OID和SHAPE字段以确保几何类型、坐标系与参数一致）</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(this IFeatureDataset featureDataset, string name, ISpatialReference sptialRef, esriGeometryType geometryType, IFields fields = null)
        {
            return FeatureClassEx.Create(featureDataset, name, sptialRef, geometryType, fields);
        }
        #endregion
    }
}
