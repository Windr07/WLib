/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Text;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;

namespace WLib.ArcGis.Analysis.Gp
{
    /// <summary>
    /// Geoprocessor操作帮助类
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
        /// </summary>
        /// <param name="overwriteOutput">输出时是否覆盖同名文件</param>
        public GpHelper(bool overwriteOutput) => OverwriteOutput = overwriteOutput;


        /// <summary>
        /// 运行GP工具
        /// </summary>
        /// <param name="process"></param>
        /// <param name="outFeatureClass"></param>
        /// <param name="message"></param>
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
                    //注意：此处仅判断输出类型为DEFeatureClassType和GPFeatureLayerType为合法结果
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
        public string GetGpMessage(IGeoProcessorResult gpResult)
        {
            var sb = new StringBuilder();
            if (gpResult != null && gpResult.Status == esriJobStatus.esriJobSucceeded)
                for (int i = 0; i <= gpResult.MessageCount - 1; i++)
                    sb.AppendLine(gpResult.GetMessage(i));
            else
                sb.AppendLine("GP运行出错，GP结果信息为空！");

            return sb.ToString();
        }
    }
}
