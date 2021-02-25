/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Text;

namespace WLib.ArcGis.Analysis.Gp
{
    /** GP工具调用成功条件总结（可调用GpHelper.CheckClassValidate()等方法进行检验）
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
     *   3)参数为路径：路径不能太深，确保输入路径的对象存在，输出目录存在但输出位置不能有同名文件/图层/表等对象
     *   4)参数为路径：一些GP工具要求路径中不能有小数点、空格或其他特殊字符
     *   5)输入输出参数有些情况不能为featureClass或featureLayer，有些情况则不能为路径
     * 5、锁定问题：GP操作和输入、输出的文件/要素类是否被锁定
     * 6、坐标系问题：输入各个图层坐标系是否一致（除坐标系名称外，具体坐标系参数也必须一致，是否Unknown坐标系）
     * 7、数据问题：不能有空几何等情况，注意使用几何修复
     *  （ArcToolbox -> Datamanagement tools -> features -> check geometry / repair geometry）
     * 8、特殊情况：安装Desktop和SDK后，再安装AE Runtime，则GP工具调用失败且程序直接崩溃，卸载Runtime后，GP工具调用成功
     * 超出边界问题：坐标系统的XY属性域过小
     * 
     * GP工具效率低、问题多、成功率低，无法调试，能使用其他方式实现功能则最好不用GP工具
     * GIS数据操作优选：SQL和.NET > ArcEngine一般接口 > GP工具
     */

    /// <summary>
    /// 运行GP（<see cref=" Geoprocessor"/>）工具的帮助类
    /// <para> 使用示例：GpHelper.RunTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
    /// </summary>
    public partial class GpHelper : Geoprocessor
    {
        /// <summary>
        /// GP处理执行出错时默认的错误提示
        /// </summary>
        public string ErrorTips { get; set; } = "请检查图层或表所在路径不能太深，确保输入路径的对象存在；\r\n" +
                                                "输出目录存在，输出位置没有同名文件/图层/表；路径中请勿包含小数点、空格或其他特殊字符；\r\n" +
                                                "输入、输出对象或地理处理工具(GP)没有被其他软件占用；必要时请安装ArcGIS相关补丁，以及在勾选空间分析权限";
        /// <summary>
        /// 运行GP（<see cref=" Geoprocessor"/>）工具的帮助类
        /// <para> 使用示例：new GpHelper().RunTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
        /// </summary>
        /// <param name="overwriteOutput">输出时是否覆盖同名文件</param>
        public GpHelper(bool overwriteOutput = true) => OverwriteOutput = overwriteOutput;
        /// <summary>
        /// 运行GP工具
        /// </summary>
        /// <param name="process">GP工具</param>
        /// <param name="message">GP工具执行结果信息或异常信息</param>
        public bool RunTool(IGPProcess process, out string message)
        {
            bool result;
            IGeoProcessorResult gpResult = null;
            try
            {
                gpResult = this.Execute(process, null) as IGeoProcessorResult;
                IGPUtilities gpUilities = new GPUtilitiesClass();
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
        /// 运行GP工具
        /// <para>注意承接outFeatureClass参数后，可能要在外部对outFeatureClass进行COM组件释放</para>
        /// </summary>
        /// <param name="process">GP工具</param>
        /// <param name="outFeatureClass">输出的要素类</param>
        /// <param name="message">GP工具执行结果信息或异常信息</param>
        public bool RunTool(IGPProcess process, out IFeatureClass outFeatureClass, out string message)
        {
            bool result;
            outFeatureClass = null;
            IGeoProcessorResult gpResult = null;
            try
            {
                gpResult = this.Execute(process, null) as IGeoProcessorResult;
                IGPUtilities gpUilities = new GPUtilitiesClass();
                if (gpResult != null && gpResult.OutputCount > 0)
                {
                    var outTypeName = gpResult.GetOutput(0).DataType.Name;
                    if (outTypeName.Equals("DEFeatureClass") || outTypeName.Equals("DEShapeFile") || outTypeName.Equals("GPFeatureLayer"))
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


        /// <summary>
        /// 运行GP工具
        /// </summary>
        /// <param name="process">GP工具</param>
        /// <param name="message">GP工具执行结果信息或异常信息</param>
        /// <param name="overwriteOutput">输出时是否覆盖同名文件或图层</param>
        /// <returns></returns>
        public static bool RunTool(IGPProcess process, out string message, bool overwriteOutput = true)
        {
            return new GpHelper(overwriteOutput).RunTool(process, out message);
        }
        /// <summary>
        /// 运行GP工具
        /// <para>注意承接outFeatureClass参数后，可能要在外部对outFeatureClass进行COM组件释放</para>
        /// </summary>
        /// <param name="process">GP工具</param>
        /// <param name="outFeatureClass">输出的要素类</param>
        /// <param name="message">GP工具执行结果信息或异常信息</param>
        /// <param name="overwriteOutput">输出时是否覆盖同名文件或图层</param>
        /// <returns></returns>
        public static bool RunTool(IGPProcess process, out IFeatureClass outFeatureClass, out string message, bool overwriteOutput = true)
        {
            return new GpHelper(overwriteOutput).RunTool(process, out outFeatureClass, out message);
        }
    }
}
