/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDatabase.FeatDataset
{
    /// <summary>
    /// 提供对要素数据集的增、删、改、查等操作
    /// </summary>
    public static partial class FeatureDatasetEx
    {
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
    }
}
