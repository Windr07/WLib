/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 提供对要素数据集的增、删、改、查等操作
    /// </summary>
    public static class FeatDatasetOpt
    {
        /// <summary>
        /// 获取要素数据集中指定名称或别名的要素类
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <param name="featureClassName">要素类名称或别名</param>
        /// <returns></returns>
        public static IFeatureClass GetFeatureClassByName(this IFeatureDataset featureDataset, string featureClassName)
        {
            IFeatureClassContainer featureClassContainer = (IFeatureClassContainer)featureDataset;
            IEnumFeatureClass enumFeatureClass = featureClassContainer.Classes;
            IFeatureClass featureClass;
            while ((featureClass = enumFeatureClass.Next()) != null)
            {
                if (featureClass.AliasName == featureClassName || (featureClass as IDataset)?.Name == featureClassName)
                    return featureClass;
            }
            return null;
        }

        /// <summary>  
        /// 获取要素数据集中所有的要素类  
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns>返回数据集中所有包含的要素类</returns>  
        public static List<IFeatureClass> GetAllFeatureClass(this IFeatureDataset featureDataset)
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
            return result;
        }

        /// <summary>  
        /// 获取要素数据集中所有的要素类的名称和别名的键值对
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns>返回数据集中所有包含的要素类的名称和别名键值对</returns>  
        public static Dictionary<string, string> GetAllFeatureClassNames(this IFeatureDataset featureDataset)
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
            return result;
        }
    }
}
