/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.Fields;
using WLib.ArcGis.GeoDatabase.WorkSpace;

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
    public static class TopologyOpt
    {
        /// <summary>
        /// 判断要素数据集中是否存在指定拓扑
        /// </summary>
        /// <param name="featureDataset">要素数据集</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static bool IsExisitTopolgy(this IFeatureDataset featureDataset, string topoName)
        {
            return ((IWorkspace2)featureDataset.Workspace).get_NameExists(esriDatasetType.esriDTTopology, topoName);
        }
        /// <summary>
        /// 从地图中获取拓扑图层
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static List<ITopologyLayer> GetTopologyLayers(this IMap map)
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
        public static esriTopologyRuleType ConvertTopologyRuleType(this ECnTopoRuleType eCnTopoRuleType)
        {
            return (esriTopologyRuleType)(int)eCnTopoRuleType;
        }


        #region 获取拓扑名称
        /// <summary>
        /// 获取要素数据集中所有拓扑的名称
        /// </summary>
        /// <param name="featureDataset"></param>
        /// <returns></returns>
        public static List<string> GetAllTopoNames(this IFeatureDataset featureDataset)
        {
            List<string> result = new List<string>();
            IFeatureDatasetName2 featureDatasetName = (IFeatureDatasetName2)featureDataset.FullName;
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
        public static List<string> GetAllTopoNames(this IWorkspace workspace)
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
        public static string GetTopologyName(this ITopology topology)
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
        public static ITopology GetTopology(this IFeatureWorkspace featureWorkspace, string topoDatasetName, string topoName)
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
        public static ITopology GetTopology(this IFeatureWorkspace featureWorkspace, string topoName)
        {
            ITopology topology = null;
            var featureDatasets = (featureWorkspace as IWorkspace).GetFeatureDatasets();
            foreach (var ds in featureDatasets)
            {
                topology = GetTopology(ds, topoName);
                if (topology != null)
                    break;
            }
            return topology;
        }
        /// <summary>
        /// 从要素数据集中获得指定的拓扑，若不存在则进行创建
        /// </summary>
        /// <param name="topoFeatureDataset">包含拓扑的要素数据集名称</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static ITopology GetOrCreateTopology(this IFeatureDataset topoFeatureDataset, string topoName)
        {
            return IsExisitTopolgy(topoFeatureDataset, topoName)
                ? GetTopology(topoFeatureDataset, topoName)
                : CreateTopology(topoFeatureDataset, topoName);
        }
        /// <summary>
        /// 从要素数据集中获得指定的拓扑
        /// </summary>
        /// <param name="topoFeatureDataset">包含拓扑的要素数据集名称</param>
        /// <param name="topoName">拓扑名称</param>
        /// <returns></returns>
        public static ITopology GetTopology(this IFeatureDataset topoFeatureDataset, string topoName)
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
        public static ITopology GetTopology(this IMap map, string topoName)
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
        public static ITopology GetFirstTopology(this IFeatureDataset topoFeatureDataset)
        {
            ITopologyContainer topologyContainer = (ITopologyContainer)topoFeatureDataset;
            return topologyContainer.get_Topology(0);
        }
        /// <summary>
        /// 从地图中获得第一个拓扑
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public static ITopology GetFirstTopology(this IMap map)
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
        public static List<ITopology> GetAllTopology(this IFeatureDataset featureDataset)
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
        public static List<ITopology> GetAllTopology(this IWorkspace workspace)
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
        /// <param name="tolerance">拓扑容差，若值小于0则使用：<code>((ITopologyContainer2)featureDataset).DefaultClusterTolerance</code></param>  
        /// <param name="featureClasses">在同一要素数据集中，参与拓扑的要素类</param>
        public static ITopology CreateTopology(this IFeatureDataset featureDataset, string topoName, double tolerance = -1, IEnumerable<IFeatureClass> featureClasses = null)
        {
            if (IsExisitTopolgy(featureDataset, topoName))
                throw new Exception($"已存在名为“{topoName}”的拓扑，无法添加！");
            try
            {
                ITopologyContainer2 topologyContainer = (ITopologyContainer2)featureDataset;
                if (tolerance < 0) tolerance = topologyContainer.DefaultClusterTolerance;
                var topology = topologyContainer.CreateTopology(topoName, tolerance, -1, "");
                if (featureClasses != null)
                {
                    foreach (var featureClass in featureClasses)
                        topology.AddClass(featureClass, 5, 1, 1, false);
                }
                return topology;
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
        public static void DeleteTopolgy(this IFeatureDataset topoFeatureDataset, string topoName)
        {
            var topology = GetTopology(topoFeatureDataset, topoName);
            DeleteTopology(topology);
        }
        /// <summary>
        /// 删除指定拓扑
        /// </summary>
        /// <param name="topology">要删除的拓扑</param>
        public static void DeleteTopology(this ITopology topology)
        {
            IDataset dataset = (IDataset)topology;
            dataset.Delete();
        }
        #endregion


        #region 添加、获取要素类
        /// <summary>
        /// 添加参与拓扑的要素类
        /// <para>1、已添加的要素类自动跳过，不会重复添加</para>
        ///  <para>2、必须是存放在同一要素数据集中的要素类</para>
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="featureClasses"></param>
        public static void AddClasses(this ITopology topology, params IFeatureClass[] featureClasses)
        {
            var classNames = topology.GetFeatureClasses().Select(v => v.GetName());
            foreach (var featureClass in featureClasses)
            {
                if (!classNames.Contains(featureClass.GetName()))
                    topology.AddClass(featureClass, 5, 1, 1, false);
            }
        }
        /// <summary>
        /// 获取全部参与拓扑的要素类
        /// </summary>
        /// <param name="topology"></param>
        /// <returns></returns>
        public static IEnumerable<IFeatureClass> GetFeatureClasses(this ITopology topology)
        {
            var featureClassContainer = topology as IFeatureClassContainer;
            return featureClassContainer.ToEnumerable();
        }
        #endregion


        #region 获取、创建拓扑规则
        /// <summary>
        /// 添加拓扑规则
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="topologyRule"></param>
        public static ITopology AddRule(this ITopology topology, ITopologyRule topologyRule)
        {
            if (topology == null)
                throw new Exception("拓扑对象为空（null），请先构建拓扑");

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
                throw new Exception($"不支添加拓扑规则 - {(ECnTopoRuleType)topologyRule.TopologyRuleType}({topologyRule.TopologyRuleType})：{ex.Message}\r\n" +
                    "请检查以下情况：\r\n①拓扑规则正确（不能使用esriTopologyRuleType.esriTRTAny）；\r\n②已将参与拓扑的图层添加到拓扑（ITopology.AddClass）；" +
                    "③图层未参与其他拓扑或几何网络；\r\n④图层不是注记层或多维图层；\r\n⑤图层未被注册为有版本等", ex);
            }
            return topology;
        }
        /// <summary>  
        /// 添加拓扑规则  
        /// </summary>  
        /// <param name="topology"></param>
        /// <param name="ruleType">要添加的双要素规则</param>  
        /// <param name="featureClassA">第一个要素类</param>  
        /// <param name="featureClassB">第二个要素类</param>  
        public static ITopologyRule AddRule(this ITopology topology, esriTopologyRuleType ruleType, IFeatureClass featureClassA, IFeatureClass featureClassB = null)
        {
            if (topology == null)
                throw new Exception("拓扑对象为空（null），请先构建拓扑");

            var topologyRule = CreateRule(ruleType, featureClassA, featureClassB);
            topology.AddClasses(featureClassA);
            if (featureClassB != null)
                topology.AddClasses(featureClassB);

            topology.AddRule(topologyRule);
            return topologyRule;
        }
        /// <summary>
        /// 创建拓扑规则
        /// </summary>
        /// <param name="ruleType">拓扑规则类型</param>
        /// <param name="featureClassA">第一个要素类</param>
        /// <param name="featureClassB">第二个要素类</param>
        /// <returns></returns>
        public static ITopologyRule CreateRule(this esriTopologyRuleType ruleType, IFeatureClass featureClassA, IFeatureClass featureClassB = null)
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
        /// 获取拓扑的全部拓扑规则
        /// </summary>
        /// <param name="topology"></param>
        /// <returns></returns>
        public static List<ITopologyRule> GetRules(this ITopology topology)
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
        public static ITopologyRule GetRule(this ITopology topology, string topoRuleName)
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


        #region  验证拓扑
        /// <summary>
        /// 验证拓扑
        /// </summary>
        /// <param name="topology"></param>
        /// <param name="envelope">验证拓扑的范围，若值为null则验证全部区域</param>
        public static IEnvelope Validate(this ITopology topology, IEnvelope envelope = null)
        {
            if (envelope == null)
                envelope = ((IGeoDataset)topology).Extent;

            IPolygon locationPolygon = new PolygonClass();
            ISegmentCollection segmentCollection = (ISegmentCollection)locationPolygon;
            segmentCollection.SetRectangle(envelope);
            IPolygon polygon = topology.get_DirtyArea(locationPolygon);
            if (!polygon.IsEmpty)
            {
                IEnvelope areaToValidate = polygon.Envelope;
                return topology.ValidateTopology(areaToValidate);
            }
            return null;
        }
        #endregion


        #region 获取、导出拓扑错误图形
        /// <summary>
        /// 导出指定类型的拓扑错误图形， 在指定要素数据集或工作空间（<paramref name="resultObject"/>）中新建要素类以保存拓扑错误图形
        /// <para>将创建以下字段并且写入数据：</para>
        /// <para>TRuleType - 拓扑规则</para>
        /// <para>OriClsID - 源要素类ID</para>
        /// <para>DesClsID - 目标要素类ID</para>
        /// <para>OriOID - 源要素OID</para>
        /// <para>DesOID - 目标要素OID</para>
        /// </summary>
        /// <param name="topologyRule">拓扑规则</param>
        /// <param name="topology">拓扑</param>
        /// <param name="resultObject">指定的要素数据集<see cref="IFeatureDataset"/>或工作空间<see cref="IWorkspace"/>对象，用于创建新要素类，保存拓扑错误图形</param>
        public static IFeatureClass ExportTopoError(this ITopology topology, ITopologyRule topologyRule, object resultObject, string resultClassName)
        {
            //获得指定拓扑规则类型的拓扑错误要素
            var errorFeatures = GetTopoErrorFeatures(topology, topologyRule).ToList();
            if (errorFeatures.Count < 1)
                return null;

            //创建保存拓扑错误的要素类
            var errFeature = errorFeatures[0] as IFeature;
            var geoType = errFeature.Shape.GeometryType;
            var spatialRef = errFeature.Shape.SpatialReference;
            if (string.IsNullOrEmpty(resultClassName))
                resultClassName = topologyRule.TopologyRuleType.ToString();
            var featureClass = FeatureClassEx.Create(resultObject, resultClassName, spatialRef, geoType);

            //将拓扑错误要素存入要素类中
            ExportTopoError(errorFeatures, featureClass);
            return featureClass;
        }
        /// <summary>
        /// 获取指定类型的拓扑错误要素枚举
        /// </summary>
        /// <param name="topologyRule">拓扑规则</param>
        /// <param name="topology">拓扑</param>
        /// <returns></returns>
        public static IEnumTopologyErrorFeature GetEnumTopoErrorFeature(this ITopology topology, ITopologyRule topologyRule)
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
        public static IEnumerable<ITopologyErrorFeature> GetTopoErrorFeatures(this ITopology topology, ITopologyRule topologyRule)
        {
            ITopologyErrorFeature errorFeature;
            var enumErrorFeature = GetEnumTopoErrorFeature(topology, topologyRule);
            while ((errorFeature = enumErrorFeature.Next()) != null)
            {
                yield return errorFeature;
            }
        }
        /// <summary>
        /// 将拓扑错误要素存入要素类中
        /// <para>将创建字段并且写入数据：TRuleType（拓扑规则）、OriClsID（源要素类ID）、DesClsID（目标要素类ID）、OriOID（源要素OID）、DesOID（目标要素OID）</para>
        /// </summary>
        /// <param name="topoErrorFeatures">拓扑错误要素</param>
        /// <param name="featureClass">保存拓扑错误要素的要素类，注意其坐标系和类型(点/线/面)必须与拓扑错误要素相同</param>
        public static void ExportTopoError(this IEnumerable<ITopologyErrorFeature> topoErrorFeatures, IFeatureClass featureClass)
        {
            int typeIndex = featureClass.AddField("TRuleType", "拓扑规则", esriFieldType.esriFieldTypeInteger);
            int orClassIdIndex = featureClass.AddField("OriClsID", "源要素类ID", esriFieldType.esriFieldTypeInteger);
            int deClassIdIndex = featureClass.AddField("DesClsID", "目标要素类ID", esriFieldType.esriFieldTypeInteger);
            int orOidIndex = featureClass.AddField("OriOID", "源要素OID", esriFieldType.esriFieldTypeInteger);
            int deOidIndex = featureClass.AddField("DesOID", "目标要素OID", esriFieldType.esriFieldTypeInteger);

            var worksapce = (featureClass as IDataset).Workspace;
            worksapce.StartEdit(() =>
            {
                featureClass.InsertFeatures((featureCursor, featureBuffer) =>
                {
                    foreach (var errorFeature in topoErrorFeatures)
                    {
                        featureBuffer.set_Value(typeIndex, errorFeature.TopologyRuleType);
                        featureBuffer.set_Value(orClassIdIndex, errorFeature.OriginClassID);
                        featureBuffer.set_Value(deClassIdIndex, errorFeature.DestinationClassID);
                        featureBuffer.set_Value(orOidIndex, errorFeature.OriginOID);
                        featureBuffer.set_Value(deOidIndex, errorFeature.DestinationOID);
                        featureBuffer.Shape = (errorFeature as IFeature).Shape;
                        object featureOID = featureCursor.InsertFeature(featureBuffer);
                    }
                });
            });
        }
        #endregion
    }
}
