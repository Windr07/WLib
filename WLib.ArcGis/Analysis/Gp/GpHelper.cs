/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Text;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using WLib.ArcGis.Analysis.GpEnum;

namespace WLib.ArcGis.Analysis.Gp
{
    /** GP工具调用成功条件总结
     * 1、运行问题：部分GP工具要先在ArcMap上运行一遍
     * 2、补丁问题：若是ArcGIS 10.0，要安装SP5
     * 3、权限问题：
     *   1)Lincense控件设置：Products为权限最高的ArcInfo(在License控件中不要同时选中其他项)， 并选择扩展项：Spatial Analyst
     *   2)LicenseInitializer类设置：只能设置esriLicenseProductCode.esriLicenseProductCodeAdvanced，或者esriLicenseProductCodeDesktop，不要再加其他权限
     * 4、参数问题：(具体参考ArcGIS帮助文档)
     *   1)参数为whereClause：sql语句不能有误且要与数据库类型适配，例如：
     *     mdb: select [fieldName] from City
     *     shp: select "fieldName" from City
     *     gdb: select  fieldName  from City
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
    /// Geoprocessor操作帮助类
    /// <para> 使用示例：new GpHelper().RunGpTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
    /// </summary>
    public class GpHelper : Geoprocessor
    {
        /// <summary>
        /// GP处理执行出错时默认的错误提示
        /// </summary>
        public string ErrorTips { get; set; } = "请检查图层或表所在路径不能太深，确保输入路径的对象存在；\r\n" +
                                                "输出目录存在，输出位置没有同名文件/图层/表；路径中请勿包含小数点、空格或其他特殊字符；\r\n" +
                                                "输入、输出对象或地理处理工具(GP)没有被其他软件占用；必要时请安装ArcGIS相关补丁，以及在勾选空间分析权限";
        /// <summary>
        /// Geoprocessor操作帮助类
        /// <para> 使用示例：new GpHelper().RunGpTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
        /// </summary>
        /// <param name="overwriteOutput">输出时是否覆盖同名文件</param>
        public GpHelper(bool overwriteOutput = true) => OverwriteOutput = overwriteOutput;
        /// <summary>
        /// 运行GP工具
        /// </summary>
        /// <param name="process">GP工具</param>
        /// <param name="outFeatureClass">输出的要素类</param>
        /// <param name="message">GP工具执行结果信息或异常信息</param>
        public bool RunGpTool(IGPProcess process, out IFeatureClass outFeatureClass, out string message)
        {
            bool result;
            message = null;
            outFeatureClass = null;
            IGeoProcessorResult gpResult = null;
            try
            {
                gpResult = this.Execute(process, null) as IGeoProcessorResult;
                IGPUtilities gpUilities = new GPUtilitiesClass();
                if (gpResult != null && gpResult.OutputCount > 0)
                {
                    if (gpResult.GetOutput(0).DataType.Name.Equals("DEFeatureClass") ||
                        gpResult.GetOutput(0).DataType.Name.Equals("GPFeatureLayer"))
                        gpUilities.DecodeFeatureLayer(gpResult.GetOutput(0), out outFeatureClass, out _);
                }
                message = GetGpMessage(gpResult);
                result = gpResult != null && gpResult.Status == esriJobStatus.esriJobSucceeded;
            }
            catch (Exception ex)
            {
                result = false;
                message = ex + Environment.NewLine + GetGpMessage(gpResult);
            }
            return result;
        }
        /// <summary>
        /// 获取GP工具的执行结果信息
        /// </summary>
        /// <param name="gpResult">GP工具执行结果</param>
        /// <returns></returns>
        private string GetGpMessage(IGeoProcessorResult gpResult)
        {
            var sb = new StringBuilder();
            if (gpResult != null && gpResult.Status == esriJobStatus.esriJobSucceeded)
                for (int i = 0; i <= gpResult.MessageCount - 1; i++)
                    sb.AppendLine(gpResult.GetMessage(i));
            else
                sb.AppendLine("GP运行出错，GP结果信息为空！");

            return sb.ToString();
        }



        #region 创建各类工具
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
        /// </summary>
        /// <param name="inFeatureClassPaths">进行相交的一个或多个要素类路径，多个时用分号隔开，eg: @"F:\foshan\Data\wuqutu_b.shp;F:\foshan\Data\world30.shp"</param>
        /// <param name="outFeatureClassPath">输出要素类路径</param>
        /// <param name="joinAttributes">输入要素的哪些属性将传递到输出要素类：ALL, NO_FID, ONLY_FID</param>
        /// <returns></returns>
        public static IGPProcess Intersect(string inFeatureClassPaths, string outFeatureClassPath, EIsJoinAttributes joinAttributes = EIsJoinAttributes.ALL)
        {
            return new Intersect
            {
                in_features = inFeatureClassPaths,
                out_feature_class = outFeatureClassPath,
                join_attributes = Enum.GetName(typeof(EIsJoinAttributes), joinAttributes),
            };
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
        /// <param name="targetClass"></param>
        /// <param name="joinClass"></param>
        /// <param name="outFeatureClassPath"></param>
        /// <param name="joinOperation"></param>
        /// <param name="matchOption"></param>
        /// <returns></returns>
        public static IGPProcess SpatialJoin(
            IFeatureClass targetClass, IFeatureClass joinClass, string outFeatureClassPath,
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
        #endregion
    }
}
