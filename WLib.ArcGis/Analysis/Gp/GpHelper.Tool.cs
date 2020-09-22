/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Linq;
using WLib.ArcGis.Analysis.GpEnum;

namespace WLib.ArcGis.Analysis.Gp
{
    /// <summary>
    /// 运行GP（<see cref=" Geoprocessor"/>）工具的帮助类
    /// <para> 使用示例：new GpHelper().RunTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
    /// </summary>
    public partial class GpHelper
    {
        /// <summary>  
        /// 面转线
        /// </summary>
        /// <param name="inFeatureClassPath">输入要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="outputPath">输出要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <returns></returns>
        [Obsolete("方法暂未测试通过")]
        public static IGPProcess PolygonToPolyline(string inFeatureClassPath, string outputPath)
        {
            //IDENTIFY_NEIGHBORS：识别面邻域关系。如果某个面的不同线段与不同的面共用边界，那么该边界将被分割成各个唯一公用的线段，这些线段的两个邻近面 FID 值将存储在输出中。
            //IGNORE_NEIGHBORS：忽略面邻域关系；每个面边界均将变为线要素，并且边界原始面要素ID将存储在输出中。 
            return new PolygonToLine
            {
                in_features = inFeatureClassPath,
                out_feature_class = outputPath,
                neighbor_option = "IGNORE_NEIGHBORS"
            };
        }
        /// <summary>
        /// 面转线
        /// </summary>
        /// <param name="inFeatureclass"></param>
        /// <param name="resultClassName"></param>
        /// <param name="saveNeighborInfo"></param>
        /// <returns></returns>
        [Obsolete("方法暂未测试通过")]
        public static IGPProcess PolygonToPolyline(IFeatureClass inFeatureclass, string resultClassName, bool saveNeighborInfo)
        {
            var featureDataset = inFeatureclass.FeatureDataset;//要素数据集  
            var featuredatasetPath = System.IO.Path.Combine(featureDataset.Workspace.PathName, featureDataset.Name);//要素数据集路径  
            return new PolygonToLine
            {
                in_features = System.IO.Path.Combine(featuredatasetPath, inFeatureclass.AliasName),
                neighbor_option = saveNeighborInfo.ToString().ToLower(),
                out_feature_class = System.IO.Path.Combine(featuredatasetPath, resultClassName)
            };
        }
        /// <summary>
        /// 要素转线
        /// </summary>
        /// <param name="inFeatureClassPath"></param>
        /// <param name="outputPath"></param>
        [Obsolete("方法暂未测试通过")]
        public static IGPProcess FeatureToPolyline(string inFeatureClassPath, string outputPath)
        {
            return new FeatureToLine(inFeatureClassPath, outputPath);
        }
        /// <summary>
        /// 要素转点
        /// </summary>
        /// <param name="inFeatureClassPath">输入的要素类（路径）</param>
        /// <param name="outputPath">输出的要素类（路径）</param>
        /// <param name="pointLocation">输出点的位置</param>
        /// <returns></returns>
        public static IGPProcess FeatureToPoint(string inFeatureClassPath, object outputPath, EFpPointLocation pointLocation = EFpPointLocation.CENTROID)
        {
            return new FeatureToPoint
            {
                in_features = inFeatureClassPath,
                out_feature_class = outputPath,
                point_location = Enum.GetName(typeof(EFpPointLocation), pointLocation),
            };
        }
        /// <summary>
        /// 调用裁剪工具，裁剪要素
        /// </summary>
        /// <param name="inFeatureClassPath">输入要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="clipFeatureClassPath">裁剪的要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="outFeatureClassPath">输出要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <returns></returns>
        public static IGPProcess Clip(string inFeatureClassPath, string clipFeatureClassPath, string outFeatureClassPath)
        {
            return new ESRI.ArcGIS.AnalysisTools.Clip
            {
                in_features = inFeatureClassPath,
                clip_features = clipFeatureClassPath,
                out_feature_class = outFeatureClassPath
            };
        }
        /// <summary>
        /// 调用相交工具，将一个或多个图层的相交部分输出到指定路径中（注意输入要素类和叠加要素类不能有空几何等问题）
        /// <para>关于相交：</para>
        /// <para>（1）输入单个要素类，则输出该要素类内部存在重叠的图斑的重叠区域，输出字段也仅有一个要素类的字段，通过面积相同等方式判断是哪两个图斑重叠</para>
        /// <para>（2）输入多个要素类，则输出各个要素类两两之间存在重叠的图斑的重叠区域，输出字段也包含各个要素类的字段，重名字段以"_1"等形式重命名</para>
        /// </summary>
        /// <param name="paths">进行相交的一个或多个要素类或要素类的完整路径，e.g. @"F:\foshan\Data\wuqutu_b.shp","F:\foshan\Data\world30.shp"</param>
        /// <param name="outFeatureClassPath">相交结果要素类的存放路径，e.g. @"F:\foshan\Data\intersect_result.shp"</param>
        /// <param name="joinAttributes">输入要素的哪些属性将传递到输出要素类：ALL, NO_FID, ONLY_FID</param>
        /// <param name="tolerance">XY容差，建议设置为0.001（创建要素类时的默认XY容差）,若值小于0则不设置容差</param>
        /// <returns></returns>
        public static IGPProcess Intersect(string[] paths, string outFeatureClassPath, EIsJoinAttributes joinAttributes = EIsJoinAttributes.ALL, double tolerance = -1)
        {
            var path = paths.Aggregate((a, b) => a + ";" + b);
            return Intersect(path, outFeatureClassPath, joinAttributes, tolerance);
        }
        /// <summary>
        /// 调用相交工具，将一个或多个图层的相交部分输出到指定路径中（注意输入要素类和叠加要素类不能有空几何等问题）
        /// <para>关于相交：</para>
        /// <para>（1）输入单个要素类，则输出该要素类内部存在重叠的图斑的重叠区域，输出字段也仅有一个要素类的字段，通过面积相同等方式判断是哪两个图斑重叠</para>
        /// <para>（2）输入多个要素类，则输出各个要素类两两之间存在重叠的图斑的重叠区域，输出字段也包含各个要素类的字段，重名字段以"_1"等形式重命名</para>
        /// </summary>
        /// <param name="in_features">进行相交的一个或多个要素类或要素类的完整路径，多个路径用分号隔开，e.g. @"F:\foshan\Data\wuqutu_b.shp;F:\foshan\Data\world30.shp"</param>
        /// <param name="outFeatureClassPath">相交结果要素类的存放路径，e.g. @"F:\foshan\Data\intersect_result.shp"</param>
        /// <param name="joinAttributes">输入要素的哪些属性将传递到输出要素类：ALL, NO_FID, ONLY_FID</param>
        /// <param name="tolerance">XY容差，建议设置为0.001（创建要素类时的默认XY容差）,若值小于0则不设置容差</param>
        /// <returns></returns>
        public static IGPProcess Intersect(object in_features, string outFeatureClassPath, EIsJoinAttributes joinAttributes = EIsJoinAttributes.ALL, double tolerance = -1)
        {
            var intersect = new Intersect
            {
                in_features = in_features,
                out_feature_class = outFeatureClassPath,
                join_attributes = Enum.GetName(typeof(EIsJoinAttributes), joinAttributes),
            };
            if (tolerance >= 0) intersect.cluster_tolerance = tolerance;

            return intersect;
        }
        /// <summary>
        /// 调用相交工具，将两个图层的相交部分输出到指定路径中（注意输入要素类和叠加要素类不能有空几何等问题）
        ///  </summary>
        /// <param name="inputClass1">进行相交的第一个要素类</param>
        /// <param name="inputClass2">进行相交的第二个要素类</param>
        /// <param name="outFeatureClassPath">相交结果要素类的存放路径，e.g. @"F:\foshan\Data\intersect_result.shp"</param>
        /// <param name="joinAttributes">输入要素的哪些属性将传递到输出要素类：ALL, NO_FID, ONLY_FID</param>
        /// <param name="tolerance">XY容差</param>
        /// <returns></returns>
        public static IGPProcess Intersect(IFeatureClass inputClass1, IFeatureClass inputClass2, string outFeatureClassPath, EIsJoinAttributes joinAttributes = EIsJoinAttributes.ALL, double tolerance = -1)
        {
            //使用值表赋值多值参数（管理相交的多个输入图层）：http://help.arcgis.com/en/sdk/10.0/arcobjects_net/conceptualhelp/index.html#/Using_value_tables/00010000028m000000/
            IGpValueTableObject gpValueTableObject = new GpValueTableObjectClass();
            gpValueTableObject.SetColumns(2);

            object row = inputClass1;
            object rank = 1;
            gpValueTableObject.SetRow(0, ref row);
            gpValueTableObject.SetValue(0, 1, ref rank);

            row = inputClass2;
            gpValueTableObject.SetRow(1, ref row);
            rank = 2;
            gpValueTableObject.SetValue(1, 1, ref rank);

            //创建相交工具
            var intersect = new Intersect
            {
                in_features = gpValueTableObject,
                out_feature_class = outFeatureClassPath,
                join_attributes = Enum.GetName(typeof(EIsJoinAttributes), joinAttributes),
            };
            if (tolerance >= 0) intersect.cluster_tolerance = tolerance;

            return intersect;
        }
        /// <summary>
        /// 调用要素类至要素类工具，即导出要素类至指定位置
        /// </summary>
        /// <param name="inFeatureClass">输入要素类</param>
        /// <param name="whereClause">条件语句，根据条件语句筛选要素至新的要素类，可为null或Empty</param>
        /// <param name="outFeatureClassName">输出要素类名称</param>
        /// <param name="outDir">输出要素类的目录</param>
        public static IGPProcess FeautureClassToFeatureClass(IFeatureClass inFeatureClass, string whereClause, string outFeatureClassName, string outDir)
        {
            return new FeatureClassToFeatureClass
            {
                in_features = inFeatureClass,
                out_feature_class = outFeatureClassName,
                out_name = outFeatureClassName,
                out_path = outDir,
                where_clause = whereClause
            };
        }
        /// <summary>
        /// 调用创建拓扑工具，创建拓扑
        /// </summary>
        /// <param name="topoFeatureDataset"></param>
        /// <param name="topoName"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IGPProcess CreateTopology(IFeatureDataset topoFeatureDataset, string topoName, double tolerance = 0.001)
        {
            return new CreateTopology
            {
                in_dataset = topoFeatureDataset,
                out_name = topoName,
                in_cluster_tolerance = tolerance
            };
            //return ((ITopologyWorkspace)topoFeatureDataset.Workspace).OpenTopology(topoName);
        }
        /// <summary>
        /// 调用空间连接工具，生成空间连接后的新图层
        /// </summary>
        /// <param name="targetClass">目标要素类或要素类路径</param>
        /// <param name="joinClass">连接要素类或连接要素类路径</param>
        /// <param name="outFeatureClassPath">连接后的要素类保存路径</param>
        /// <param name="joinOperation">连接操作类型</param>
        /// <param name="matchOption">行匹配选项类型</param>
        /// <returns></returns>
        public static IGPProcess SpatialJoin(object targetClass, object joinClass, string outFeatureClassPath,
            ESjOperation joinOperation = ESjOperation.JOIN_ONE_TO_ONE, ESjMatchOption matchOption = ESjMatchOption.INTERSECT)
        {
            return new SpatialJoin
            {
                target_features = targetClass,
                join_features = joinClass,
                out_feature_class = outFeatureClassPath,
                join_operation = Enum.GetName(typeof(ESjOperation), joinOperation),
                match_option = Enum.GetName(typeof(ESjMatchOption), matchOption)
            };
        }
    }
}
