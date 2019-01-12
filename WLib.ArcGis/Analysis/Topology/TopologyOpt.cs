/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDb.FeatClass;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.GeoDb.WorkSpace;

namespace WLib.ArcGis.Analysis.Topology
{
    //注意 建立拓扑时会出现的问题：
    //1：当要素类已经参加建立其它拓扑的时候，再使用该要素类新建拓扑会报错；
    //2：当要素类已参加网络分析（Geometry Network）运算的时候，建立拓扑也会报错；
    //3：当要素类是一个注记层或多维图层，不能建立拓扑；
    //4：当要素类已被注册为有版本，不能建立拓扑；
    //*添加拓扑规则时，使用esriTopologyRuleType.esriTRTAny会直接报错

    //验证拓扑：ITopology.ValidateTopology 用来验证指定区域内的拓扑。
    //需要注意：没有版本的拓扑可以在任何时候验证。而有版本的拓扑必须在编辑会话中验证。
    //通过验证后，当前的拓扑就可以检查出相应的拓扑错误，并生成拓扑图层ITopologyLayer，ITopologyLayer继承自ILayer


    /// <summary>
    /// 提供判断、获取、创建、删除拓扑（拓扑图层/拓扑规则/拓扑错误图形）的方法
    /// </summary>
    public class TopologyOpt
    {
        /// <summary>
        /// 判断要素数据集中是否存在指定拓扑
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static bool IsExisitTopolgy(IFeatureDataset featureDataset, string topoName)
        {
            return (featureDataset.Workspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTopology, topoName);
        }
      /// <summary>
        /// 从地图中获取拓扑图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<ITopologyLayer> GetTopologyLayers(IMap map)
        {
            List<ITopologyLayer> result = new List<ITopologyLayer>();
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.get_Layer(i) is ITopologyLayer topoLayer)
                    result.Add(topoLayer);
            }
            return result;
        }
        /// <summary>
        /// 将中文版的拓扑规则枚举，转换成esri拓扑规则枚举  
        /// </summary>
        /// <param name="eCnTopoRuleType">中文版的拓扑规则类型枚举，各枚举项的整型值与esriTopologyRuleType一致</param>
        /// <returns></returns>
        public static esriTopologyRuleType ConvertTopologyRuleType(ECnTopoRuleType eCnTopoRuleType)
        {
            return (esriTopologyRuleType)(int)eCnTopoRuleType;
        }


        #region 获取拓扑名称
        /// <summary>
        /// 获取要素数据集中所有拓扑的名称
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <returns></returns>
        public static List<string> GetAllTopoNames(IFeatureDataset featureDataset)
        {
            List<string> result = new List<string>();
            IFeatureDatasetName2 featureDatasetName = featureDataset.FullName as IFeatureDatasetName2;
            IEnumDatasetName enumDsName = featureDatasetName.TopologyNames;
            IDatasetName dsName;
            while ((dsName = enumDsName.Next()) != null)
            {
                result.Add(dsName.Name);
            }
            return result;
        }
        /// <summary>
        /// 获取工作空间中所有拓扑的名称
        /// </summary>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public static List<string> GetAllTopoNames(IWorkspace workspace)
        {
            var result = new List<string>();
            var featureDatasets = workspace.GetFeatureDatasets();
            foreach (var ds in featureDatasets)
            {
                result.AddRange(GetAllTopoNames(ds));
            }
            return result;
        }
        /// <summary>
        /// 获取拓扑的名称
        /// </summary>
        /// <param name="topology"></param>
        /// <returns></returns>
        public static string GetTopologyName(ITopology topology)
        {
            return (topology as IDataset)?.Name;
        }
        #endregion


        #region 获取拓扑
        /// <summary>
        /// 从工作空间中获得指定的拓扑
        /// </summary>
        /// <param name="featureWorkspace">要素工作空间</param>
        /// <param name="topoDatasetName">包含拓扑的要素数据集名称</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static ITopology GetTopologyByName(IFeatureWorkspace featureWorkspace, string topoDatasetName, string topoName)
        {
            IFeatureDataset featureDataset = featureWorkspace.OpenFeatureDataset(topoDatasetName);
            ITopologyContainer topologyContainer = (ITopologyContainer)featureDataset;
            return topologyContainer.get_TopologyByName(topoName);
        }
        /// <summary>
        /// 从工作空间中获得指定的拓扑
        /// </summary>
        /// <param name="featureWorkspace">要素工作空间</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static ITopology GetTopologyByName(IFeatureWorkspace featureWorkspace, string topoName)
        {
            ITopology topology = null;
            var featureDatasets = (featureWorkspace as IWorkspace).GetFeatureDatasets();
            foreach (var ds in featureDatasets)
            {
                topology = GetTopologyByName(ds, topoName);
                if (topology != null)
                    break;
            }
            return topology;
        }
        /// <summary>
        /// 从要素数据集中获得指定的拓扑
        /// </summary>
        /// <param name="topoFeatureDataset">包含拓扑的要素数据集名称</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static ITopology GetTopologyByName(IFeatureDataset topoFeatureDataset, string topoName)
        {
            ITopologyContainer topologyContainer = (ITopologyContainer)topoFeatureDataset;
            return topologyContainer.get_TopologyByName(topoName);
        }
        /// <summary>
        /// 从地图中获得指定的拓扑
        /// </summary>
        /// <param name="map"></param>
        /// <param name="topoName"></param>
        /// <returns></returns>
        public static ITopology GetTopologyByName(IMap map, string topoName)
        {
            for (int i = 0; i < map.LayerCount; i++)
            {
                ILayer layer = map.get_Layer(i);
                if (layer is ITopologyLayer topoLayer && layer.Name == topoName)
                    return topoLayer.Topology;
            }
            return null;
        }
        /// <summary>
        /// 从要素数据集中获得第一个拓扑
        /// </summary>
        /// <param name="topoFeatureDataset"></param>
        /// <returns></returns>
        public static ITopology GetFirstTopology(IFeatureDataset topoFeatureDataset)
        {
            ITopologyContainer topologyContainer = (ITopologyContainer)topoFeatureDataset;
            return topologyContainer.get_Topology(0);
        }
        /// <summary>
        /// 从地图中获得第一个拓扑
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static ITopology GetFirstTopology(IMap map)
        {
            for (int i = 0; i < map.LayerCount; i++)
            {
                if (map.get_Layer(i) is ITopologyLayer topoLayer)
                    return topoLayer.Topology;
            }
            return null;
        }
        /// <summary>
        /// 获取要素数据集中的所有拓扑
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <returns></returns>
        public static List<ITopology> GetAllTopology(IFeatureDataset featureDataset)
        {
            List<ITopology> result = new List<ITopology>();
            ITopologyContainer topologyContainer = (ITopologyContainer)featureDataset;
            for (int i = 0; i < topologyContainer.TopologyCount; i++)
            {
                result.Add(topologyContainer.get_Topology(i));
            }
            return result;
        }
        /// <summary>
        /// 获取工作空间中的所有拓扑
        /// </summary>
        /// <param name="workspace">工作空间</param>
        /// <returns></returns>
        public static List<ITopology> GetAllTopology(IWorkspace workspace)
        {
            List<ITopology> result = new List<ITopology>();
            var featureDatasetList = workspace.GetFeatureDatasets();
            foreach (var featureDataset in featureDatasetList)
            {
                result.AddRange(GetAllTopology(featureDataset));
            }
            return result;
        }

        #endregion


        #region 创建拓扑
        /// <summary>  
        /// 在数据集中构建拓扑  
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <param name="topoName">要生成拓扑的名称</param>  
        /// <param name="tolerance">拓扑容差，可选，默认0.001</param>  
        public static ITopology CreatTopology(IFeatureDataset featureDataset, string topoName, double tolerance = 0.001)
        {
            if (IsExisitTopolgy(featureDataset, topoName))
                throw new Exception($"已存在名为“{topoName}”的拓扑，无法添加！");
            try
            {
                return ((ITopologyContainer)featureDataset).CreateTopology(topoName, tolerance, -1, "");
            }
            catch (Exception ex)
            {
                throw new Exception($"拓扑创建出错，描述: {ex.Message}");
            }
        }
        #endregion


        #region 删除拓扑
        /// <summary>
        /// 删除指定拓扑
        /// </summary>
        /// <param name="topoFeatureDataset">拓扑所在的要素数据集</param>
        /// <param name="topoName">要删除的拓扑的名称</param>
        public static void DeleteTopolgy(IFeatureDataset topoFeatureDataset, string topoName)
        {
            var topology = GetTopologyByName(topoFeatureDataset, topoName);
            DeleteTopology(topology);
        }
        /// <summary>
        /// 删除指定拓扑
        /// </summary>
        /// <param name="topology">要删除的拓扑</param>
        public static void DeleteTopology(ITopology topology)
        {
            IDataset dataset = (IDataset)topology;
            dataset.Delete();
        }
        #endregion


        #region 获取、创建拓扑规则
        /// <summary>
        /// 创建拓扑规则
        /// </summary>
        /// <param name="ruleType">拓扑规则类型</param>
        /// <param name="featureClassA">第一个要素类</param>
        /// <param name="featureClassB">第二个要素类</param>
        /// <returns></returns>
        public static ITopologyRule CreateTopologyRule(esriTopologyRuleType ruleType, IFeatureClass featureClassA, IFeatureClass featureClassB = null)
        {
            ITopologyRule topologyRule = new TopologyRuleClass();
            topologyRule.TopologyRuleType = ruleType;
            topologyRule.Name = ruleType.ToString();
            topologyRule.OriginClassID = featureClassA.FeatureClassID;
            topologyRule.AllOriginSubtypes = true;
            if (featureClassB != null)
            {
                topologyRule.DestinationClassID = featureClassB.FeatureClassID;
                topologyRule.AllDestinationSubtypes = true;
            }
            return topologyRule;
        }
        /// <summary>  
        /// 添加拓扑规则  
        /// </summary>  
        /// <param name="topology"></param>
        /// <param name="ruleType">要添加的双要素规则</param>  
        /// <param name="featureClassA">第一个要素类</param>  
        /// <param name="featureClassB">第二个要素类</param>  
        public static void AddRuleToTopology(ITopology topology, esriTopologyRuleType ruleType, IFeatureClass featureClassA, IFeatureClass featureClassB = null)
        {
            if (topology == null)
                throw new Exception("请先构建拓扑");

            var topologyRule = CreateTopologyRule(ruleType, featureClassA, featureClassB);
            AddRuleToTopology(topology, topologyRule);
        }
        /// <summary>
        /// 添加拓扑规则
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="topologyRule"></param>
        public static void AddRuleToTopology(ITopology topology, ITopologyRule topologyRule)
        {
            ITopologyRuleContainer topologyRuleContainer = (ITopologyRuleContainer)topology;//构建容器  
            try
            {
                topologyRuleContainer.get_CanAddRule(topologyRule);//不能添加的话直接报错  
                try
                {
                    topologyRuleContainer.DeleteRule(topologyRule);//删除已存在的规则后再添加  
                    topologyRuleContainer.AddRule(topologyRule);//规则存在的话直接报错  
                }
                catch
                {
                    topologyRuleContainer.AddRule(topologyRule);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("不支持添加拓扑规则：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取拓扑的全部拓扑规则
        /// </summary>
        /// <param name="topology"></param>
        /// <returns></returns>
        public static List<ITopologyRule> GetTopologyRules(ITopology topology)
        {
            List<ITopologyRule> result = new List<ITopologyRule>();
            ITopologyRuleContainer topologyRuleContainer = (ITopologyRuleContainer)topology;
            IEnumRule enumRule = topologyRuleContainer.Rules;
            enumRule.Reset();

            IRule rule;
            while ((rule = enumRule.Next()) != null)
            {
                result.Add((ITopologyRule)rule);
            }
            return result;
        }
        /// <summary>
        /// 获取指定的拓扑规则类型
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="topoRuleName">拓扑规则名称或esriTopologyRuleType.ToString()的结果（eg:）</param>
        /// <returns></returns>
        public static ITopologyRule GetTopoRuleTypeByName(ITopology topology, string topoRuleName)
        {
            ITopologyRuleContainer topologyRuleContainer = (ITopologyRuleContainer)topology;
            IEnumRule enumRule = topologyRuleContainer.Rules;
            enumRule.Reset();

            IRule rule;
            while ((rule = enumRule.Next()) != null)
            {
                var topoRule = rule as ITopologyRule;
                if (topoRule.Name == topoRuleName || topoRule.TopologyRuleType.ToString() == topoRuleName)
                    return topoRule;
            }
            return null;
        }
        #endregion


        #region 获取、导出拓扑错误图形
        /// <summary>
        /// 获取指定类型的拓扑错误要素枚举
        /// </summary>
        /// <param name="topologyRule">拓扑规则</param>
        /// <param name="topology">拓扑</param>
        /// <returns></returns>
        public static IEnumTopologyErrorFeature GetEnumTopoErrorFeature(ITopologyRule topologyRule, ITopology topology)
        {
            IEnvelope envelope = (topology as IGeoDataset).Extent;
            IErrorFeatureContainer errorContainer = topology as IErrorFeatureContainer;

            IEnumTopologyErrorFeature enumErrorFeature = errorContainer.get_ErrorFeatures(
                ((IGeoDataset)topology.FeatureDataset).SpatialReference, topologyRule, envelope, true, true);

            return enumErrorFeature;
        }
        /// <summary>
        /// 获取指定类型的拓扑错误要素
        /// </summary>
        /// <param name="topologyRule">拓扑规则</param>
        /// <param name="topology">拓扑</param>
        /// <returns></returns>
        public static List<ITopologyErrorFeature> GetTopoErrorFeatures(ITopologyRule topologyRule, ITopology topology)
        {
            List<ITopologyErrorFeature> result = new List<ITopologyErrorFeature>();

            ITopologyErrorFeature errorFeature;
            var enumErrorFeature = GetEnumTopoErrorFeature(topologyRule, topology);
            while ((errorFeature = enumErrorFeature.Next()) != null)
            {
                result.Add(errorFeature);
            }
            return result;
        }
        /// <summary>
        /// 将拓扑错误要素存入要素类中
        /// </summary>
        /// <param name="topoErrorFeatures">拓扑错误要素</param>
        /// <param name="resultFeatureClass">保存拓扑错误要素的要素类，注意其坐标系和类型(点/线/面)必须与拓扑错误要素相同</param>
        public static void TopoErrorInsertToFeatureClass(List<ITopologyErrorFeature> topoErrorFeatures, IFeatureClass resultFeatureClass)
        {
            int typeIndex = resultFeatureClass.AddField("TRuleType", "拓扑规则", esriFieldType.esriFieldTypeInteger);
            int orClassIdIndex = resultFeatureClass.AddField("OriClsID", "源要素类ID", esriFieldType.esriFieldTypeInteger);
            int deClassIdIndex = resultFeatureClass.AddField("DesClsID", "目标要素类ID", esriFieldType.esriFieldTypeInteger);
            int orOidIndex = resultFeatureClass.AddField("OriOID", "源要素OID", esriFieldType.esriFieldTypeInteger);
            int deOidIndex = resultFeatureClass.AddField("DesOID", "目标要素OID", esriFieldType.esriFieldTypeInteger);

            IWorkspaceEdit tmpWorkspaceEdit = (IWorkspaceEdit)(resultFeatureClass as IDataset).Workspace;
            tmpWorkspaceEdit.StartEditing(true);
            tmpWorkspaceEdit.StartEditOperation();
            IFeatureBuffer featureBuffer = resultFeatureClass.CreateFeatureBuffer();

            //在目标要素类中插入所有错误要素  
            IFeatureCursor featureCursor = resultFeatureClass.Insert(true);
            foreach (var errorFeature in topoErrorFeatures)
            {
                IFeature tmpFeature = errorFeature as IFeature;
                featureBuffer.set_Value(typeIndex, errorFeature.TopologyRuleType);
                featureBuffer.set_Value(orClassIdIndex, errorFeature.OriginClassID);
                featureBuffer.set_Value(deClassIdIndex, errorFeature.DestinationClassID);
                featureBuffer.set_Value(orOidIndex, errorFeature.OriginOID);
                featureBuffer.set_Value(deOidIndex, errorFeature.DestinationOID);

                featureBuffer.Shape = tmpFeature.Shape;
                object featureOID = featureCursor.InsertFeature(featureBuffer);
            }
            featureCursor.Flush();//保存要素  
            tmpWorkspaceEdit.StopEditOperation();
            tmpWorkspaceEdit.StopEditing(true);
            Marshal.ReleaseComObject(featureCursor);
        }
        /// <summary>
        /// 导出拓扑错误图形， 在指定要素数据集(IFeatureDataset)或工作空间(IWorkspace)新建要素类以保存拓扑错误图形
        /// </summary>
        /// <param name="topologyRule">拓扑规则</param>
        /// <param name="topology">拓扑</param>
        /// <param name="resultObject">指定的要素数据集(IFeatureDataset)或工作空间(IWorkspace)，用于创建新要素类，保存拓扑错误图形</param>
        public static IFeatureClass TopoErrorToNewFeatureClass(ITopologyRule topologyRule, ITopology topology, object resultObject, string resultClassName = null)
        {
            //获得指定拓扑规则类型的拓扑错误要素
            var errorFeatures = GetTopoErrorFeatures(topologyRule, topology);
            if (errorFeatures.Count < 1)
                return null;

            //创建保存拓扑错误的要素类
            var feature = errorFeatures[0] as IFeature;
            var geoType = feature.Shape.GeometryType;

            if (string.IsNullOrEmpty(resultClassName))
                resultClassName = topologyRule.TopologyRuleType.ToString();

            IFeature tmpFeature = errorFeatures[0] as IFeature;
            var resultFeatureClass = CreateFeatClass.Create(resultObject, resultClassName,
                tmpFeature.Shape.SpatialReference, geoType, new FieldsClass());

            //将拓扑错误要素存入要素类中
            TopoErrorInsertToFeatureClass(errorFeatures, resultFeatureClass);
            return resultFeatureClass;
        }
        /// <summary>
        /// 导出拓扑错误图形， 在指定要素类中保存扑错误图形
        /// </summary>
        /// <param name="workespace"></param>
        /// <param name="topoDatasetName"></param>
        /// <param name="topoName"></param>
        /// <param name="envelope"></param>
        /// <param name="shpFeatureClass"></param>
        /// <returns></returns>
        public static IFeatureClass TopoErrorToNewFeatureClass(IWorkspace workespace, string topoDatasetName,
            string topoName, IEnvelope envelope, IFeatureClass shpFeatureClass)
        {
            //获得拓扑
            var topology = GetTopologyByName(workespace as IFeatureWorkspace, topoDatasetName, topoName);

            //打开编辑并验证拓扑
            workespace.StartEdit(() => topology.ValidateTopology(envelope));

            IErrorFeatureContainer errorFeatureContainer = (IErrorFeatureContainer)topology;
            ISpatialReference spatialReference = ((IGeoDataset)topology).SpatialReference;

            IWorkspace shpWorkspace = (shpFeatureClass as IDataset).Workspace;
            shpWorkspace.StartEdit(() =>
            {
                var topoRules = GetTopologyRules(topology);
                foreach (ITopologyRule rule in topoRules)
                {
                    IEnumTopologyErrorFeature enumTopologyErrorFeature = errorFeatureContainer.get_ErrorFeatures(spatialReference, rule, envelope, true, true);
                    ITopologyErrorFeature topologyErrorFeature = null;
                    while ((topologyErrorFeature = enumTopologyErrorFeature.Next()) != null)
                    {
                        IFeature errorFeature = shpFeatureClass.CreateFeature();
                        errorFeature.Shape = (topologyErrorFeature as IFeature).ShapeCopy;
                        errorFeature.Store();
                    }
                }
            });
            return shpFeatureClass;
        }
        #endregion
    }
}
