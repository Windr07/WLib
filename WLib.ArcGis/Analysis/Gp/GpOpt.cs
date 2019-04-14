/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/11 16:05:21
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace WLib.ArcGis.Analysis.Gp
{
    /**
     * ------------------------------------GP工具调用成功条件总结----------------------------------------
     * 1、运行问题：部分GP工具要先在ArcMap上运行一遍
     * 2、补丁问题：若是ArcGIS 10.0，要安装SP5
     * 3、权限问题：Lincense权限：Products为权限最高的ArcInfo(在License控件中不要同时选中其他项)， 并选择扩展项：Spatial Analyst
     * 4、参数问题：(具体参考ArcGIS帮助文档)
     *   1)参数为whereClause：sql语句不能有误且要与数据库类型适配
     *   2)参数为图层或表名：开头不能是数字，不能加上".shp"，或不能有小数点/空格/其他特殊字符
     *   3)参数为路径：路径不能太深，确保输入路径的对象存在，输出目录存在但输出位置不能有同名文件/图层/表
     *   4)参数为路径：一些GP工具要求路径中不能有小数点、空格或其他特殊字符
     *   5)输入输出参数有些情况不能为featureClass或featureLayer，有些情况则不能为路径
     * 5、锁定问题：GP操作和输入、输出的文件/要素类是否被锁定
     * 6、坐标系问题：输入各个图层坐标系是否一致（除坐标系名称外，具体坐标系参数也必须一致，是否Unknown坐标系）
     * 7、数据问题：不能有空几何等情况，注意使用几何修复
     * 8、特殊情况：安装Desktop和SDK后，再安装AE Runtime，则GP工具调用失败且程序直接崩溃，卸载Runtime后，GP工具调用成功
     *
     * GP工具效率低、问题多、成功率低，ArcEngine能使用其他方式实现功能则最好别用GP工具
     * GIS数据操作优选：SQL和.NET > ArcEngine一般接口 > GP工具
     */

    /// <summary>
    /// 提供封装简化GP工具的方法
    /// </summary>
    public static class GpOpt
    {
        #region 要素转线，这三个方法暂未测试通过
        /// <summary>  
        /// 面转线
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureClassPath">输入要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="outputPath">输出要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <returns></returns>  
        public static IGeoProcessorResult PolygonToPolyline(this Geoprocessor gp, string inFeatureClassPath, string outputPath)
        {
            var polygonToLine = new PolygonToLine
            {
                in_features = inFeatureClassPath,
                out_feature_class = outputPath,
                neighbor_option = "IGNORE_NEIGHBORS"
            };
            //IDENTIFY_NEIGHBORS：识别面邻域关系。如果某个面的不同线段与不同的面共用边界，那么该边界将被分割成各个唯一公用的线段，这些线段的两个邻近面 FID 值将存储在输出中。
            //IGNORE_NEIGHBORS：忽略面邻域关系；每个面边界均将变为线要素，并且边界原始面要素ID将存储在输出中。 
            return (IGeoProcessorResult)gp.Execute(polygonToLine, null);
        }
        /// <summary>
        /// 面转线
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureclass"></param>
        /// <param name="resultClassName"></param>
        /// <param name="saveNeighborInfo"></param>
        /// <returns></returns>
        public static IGeoProcessorResult PolygonToPolyline(this Geoprocessor gp, IFeatureClass inFeatureclass, string resultClassName, bool saveNeighborInfo)
        {
            var featureDataset = inFeatureclass.FeatureDataset;//要素数据集  
            var featuredatasetPath = System.IO.Path.Combine(featureDataset.Workspace.PathName, featureDataset.Name);//要素数据集路径  
            var polygonToLine = new PolygonToLine
            {
                in_features = System.IO.Path.Combine(featuredatasetPath, inFeatureclass.AliasName),
                neighbor_option = saveNeighborInfo.ToString().ToLower(),
                out_feature_class = System.IO.Path.Combine(featuredatasetPath, resultClassName)
            };
            return (IGeoProcessorResult)gp.Execute(polygonToLine, null);
        }
        /// <summary>
        /// 要素转线
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureClassPath"></param>
        /// <param name="outputPath"></param>
        public static IGeoProcessorResult FeatureToPolyline(this Geoprocessor gp, string inFeatureClassPath, string outputPath)
        {
            return (IGeoProcessorResult)gp.Execute(new FeatureToLine(inFeatureClassPath, outputPath), null);
        }
        #endregion


        /// <summary>
        /// 调用裁剪工具，裁剪要素
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureClassPath">输入要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="clipFeatureClassPath">裁剪的要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <param name="outFeatureClassPath">输出要素类的路径（eg:D:\xx.mdb\xx或D:\xx.shp）</param>
        /// <returns></returns>
        public static IGeoProcessorResult Clip(this Geoprocessor gp, string inFeatureClassPath, string clipFeatureClassPath, string outFeatureClassPath)
        {
            var clip = new ESRI.ArcGIS.AnalysisTools.Clip
            {
                in_features = inFeatureClassPath,
                clip_features = clipFeatureClassPath,
                out_feature_class = outFeatureClassPath
            };
            return (IGeoProcessorResult)gp.Execute(clip, null);
        }
        /// <summary>
        /// 调用相交工具，将一个或多个图层的相交部分输出到指定路径中（注意输入要素类和叠加要素类不能有空几何等问题）
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureClassPaths">进行相交的一个或多个要素类路径，多个时用分号隔开，eg: @"F:\foshan\Data\wuqutu_b.shp;F:\foshan\Data\world30.shp"</param>
        /// <param name="outFeatureClassPath">输出要素类路径</param>
        /// <param name="joinAttributes">输入要素的哪些属性将传递到输出要素类：ALL, NO_FID, ONLY_FID</param>
        /// <returns></returns>
        public static IGeoProcessorResult Intersect(this Geoprocessor gp, string inFeatureClassPaths, string outFeatureClassPath, string joinAttributes = "ALL")
        {
            var intersect = new ESRI.ArcGIS.AnalysisTools.Intersect
            {
                in_features = inFeatureClassPaths,
                out_feature_class = outFeatureClassPath,
                join_attributes = joinAttributes
            };
            return (IGeoProcessorResult)gp.Execute(intersect, null);
        }
        /// <summary>
        /// 调用要素类至要素类工具，即导出要素类至指定位置
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="inFeatureClass">输入要素类</param>
        /// <param name="whereClause">条件语句，根据条件语句筛选要素至新的要素类，可为null或Empty</param>
        /// <param name="outFeatureClassName">输出要素类名称</param>
        /// <param name="outDir">输出要素类的目录</param>
        public static IGeoProcessorResult FeautureClassToFeatureClass(this Geoprocessor gp, IFeatureClass inFeatureClass, string whereClause, string outFeatureClassName, string outDir)
        {
            var featClstToFeatCls = new FeatureClassToFeatureClass
            {
                in_features = inFeatureClass,
                out_feature_class = outFeatureClassName,
                out_name = outFeatureClassName,
                out_path = outDir,
                where_clause = whereClause
            };
            return (IGeoProcessorResult)gp.Execute(featClstToFeatCls, null);
        }
        /// <summary>
        /// 调用创建拓扑工具，创建拓扑
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="topoFeatureDataset"></param>
        /// <param name="topoName"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IGeoProcessorResult CreateTopology(this Geoprocessor gp, IFeatureDataset topoFeatureDataset, string topoName, double tolerance = 0.001)
        {
            var createTopo = new CreateTopology
            {
                in_dataset = topoFeatureDataset,
                out_name = topoName,
                in_cluster_tolerance = tolerance
            };
            return (IGeoProcessorResult)gp.Execute(createTopo, null);
            //return ((ITopologyWorkspace)topoFeatureDataset.Workspace).OpenTopology(topoName);
        }
        /// <summary>
        /// 调用空间连接工具，生成空间连接后的新图层
        /// </summary>
        /// <param name="gp"></param>
        /// <param name="targetClass"></param>
        /// <param name="joinClass"></param>
        /// <param name="outFeatureClassPath"></param>
        /// <param name="joinOperation"></param>
        /// <param name="matchOption"></param>
        /// <returns></returns>
        public static IGeoProcessorResult SpatialJoin(this Geoprocessor gp,
            IFeatureClass targetClass, IFeatureClass joinClass, string outFeatureClassPath,
            ESjOperation joinOperation = ESjOperation.JOIN_ONE_TO_ONE, ESjMatchOption matchOption = ESjMatchOption.INTERSECT)
        {
            var spatialJoin = new SpatialJoin
            {
                target_features = targetClass,
                join_features = joinClass,
                out_feature_class = outFeatureClassPath,
                join_operation = Enum.GetName(typeof(ESjOperation), joinOperation),
                match_option = Enum.GetName(typeof(ESjMatchOption), matchOption)
            };
            return (IGeoProcessorResult)gp.Execute(spatialJoin, null);
        }
    }
}
