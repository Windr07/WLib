using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WLib.ArcGis.Analysis.OnShape;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.Geometry;
using WLib.Data.Format;
using WLib.Files;

namespace WLib.ArcGis.Analysis.Gp
{
    /// <summary>
    /// <see cref=" Geoprocessor"/>操作帮助类
    /// <para> 使用示例：new GpHelper().RunTool(GpHelper.Intersect("a.shp;b.shp", "result.shp"), out _, out _); </para>
    /// </summary>
    public partial class GpHelper
    {
        /// <summary>
        /// 检查路径及数据是否符合GP工具以下规范：
        /// <para>路径长度限制、特殊字符限制、小数点限制、空格限制，文件或图层占用限制，坐标系一致</para>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>符合规范返回null,否则返回提示信息</returns>
        public static string CheckClassValidate(params string[] paths)
        {
            var sb = new StringBuilder();
            foreach (var path in paths)
            {
                var message = CheckClassPath(path);
                if (message != null)
                    sb.AppendLine($"路径“{path}”不合法：{message}");
            }
            sb.Append(CheckCoordinates(paths));
            return sb.Length > 0 ? sb.ToString() : null;
        }
        /// <summary>
        /// 检查路径及数据是否符合GP工具以下规范：
        /// <para>路径长度、特殊字符、小数点、空格限制，文件或图层占用限制</para>
        /// </summary>
        /// <param name="path"></param>
        /// <returns>符合规范返回null,否则返回提示信息</returns>
        public static string CheckClassPath(string path)
        {
            string message;
            if ((message = CheckPathValidate(path)) != null) return message;//路径合法性
            if ((message = CheckPathLength(path)) != null) return message;//路径长度
            if ((message = CheckPathLock(path)) != null) return message;//文件占用
            if ((message = CheckClassLock(path)) != null) return message;//图层占用
            if ((message = CheckPathEmpty(path)) != null) return message;//路径包含空格
            if ((message = CheckPathDot(path)) != null) return message;//路径包含额外小数点
            return message;
        }


        /// <summary>
        /// 检查路径是否存在空白字符
        /// </summary>
        /// <param name="path"></param>
        /// <returns>存在空白字符返回提示信息，否则返回null</returns>
        public static string CheckPathEmpty(string path)
        {
            return Regex.IsMatch(path, @"\s") ? "路径中包含空格" : null;
        }
        /// <summary>
        /// 检查路径是否包含中文字符或中文标点
        /// </summary>
        /// <param name="path"></param>
        /// <returns>存在中文字符或标点返回提示信息，否则返回null</returns>
        public static string CheckPathChinese(string path)
        {
            var chars = path.ToCharArray();
            foreach (var c in chars)
                if (c.IsChinese() || c.IsChinesePunctuation())
                    return "路径中包含中文字符或中文标点";

            return null;
        }
        /// <summary>
        /// 检查文件名或目录是否合法，不能为空或空白字符，不能包含字符\/:*?"&lt;>|
        /// </summary>
        /// <param name="path"></param>
        /// <returns>不合法则返回提示信息，否则返回null</returns>
        public static string CheckPathValidate(string path)
        {
            return FileOpt.ValidFileName(path) && DirectoryOpt.ValidFolderName(path) ?
                null : "路径为空或包含特殊字符：\\/:*?\"&<>|";
        }
        /// <summary>
        /// 检查路径是否超出支持的长度
        /// </summary>
        /// <param name="path"></param>
        /// <returns>路径过长（路径太深）则返回提示信息，否则返回null</returns>
        public static string CheckPathLength(string path)
        {
            return path.Length > 100 ? "源文件名或目录长度大于软件支持的长度" : null;
        }
        /// <summary>
        /// 检查路径中除了扩展名部分，是否包含额外的小数点
        /// </summary>
        /// <param name="path"></param>
        /// <returns>路径包含额外小数点则返回提示信息，否则返回null</returns>
        public static string CheckPathDot(string path)
        {
            var extension = System.IO.Path.GetExtension(path);
            path = path.Replace(extension, "");
            return path.Contains(".") ? "文件名除扩展名外，包含额外的小数点" : null;
        }
        /// <summary>
        /// 检查路径文件是否被占用
        /// </summary>
        /// <param name="path"></param>
        /// <returns>路径文件存在且被占用则返回提示信息，否则返回null</returns>
        public static string CheckPathLock(string path)
        {
            return File.Exists(path) && FileOpt.FileIsUsed(path)
                ? "文件被占用，请关闭占用该文件的相关软件，例如ArcMap、ArcCatlog、其他使用数据的程序"
                : null;
        }
        /// <summary>
        /// 检查路径下的要素类是否被占用
        /// </summary>
        /// <param name="path">要素类的完整路径，参考<see cref="FeatureClassEx.FromPath"/></param>
        /// <param name="autoAddExtension">是否自动在路径末尾增加.shp/.mdb/.dwg后缀，以再次查找要素类</param>
        /// <returns>若要素类被锁定则返回提示信息，要素类不存在或未锁定返回null，若检查失败抛出异常</returns>
        public static string CheckClassLock(string path, bool autoAddExtension = false)
        {
            try
            {
                var featureClass = FeatureClassEx.FromPath(path, autoAddExtension);
                if (featureClass == null)
                    return null;

                featureClass.CheckClassLock(out var message);
                return message;
            }
            catch (Exception ex) { throw new Exception($"打开或检查要素类是否锁定失败：{ex.Message}\r\n请检查要素类路径“{path}”无误", ex); }
        }
        /// <summary>
        /// 检查多个路径下的要素类的坐标系是否完全一致
        /// </summary>
        /// <param name="paths"></param>
        /// <returns>若坐标系不一致则返回提示信息，一致则返回null</returns>
        public static string CheckCoordinates(params string[] paths)
        {
            var classes = new List<IFeatureClass>();
            foreach (var path in paths)
            {
                var featureClass = FeatureClassEx.FromPath(path);
                if (featureClass != null)
                    classes.Add(featureClass);
            }
            return SpatialRefOpt.CheckSpatialRef(classes, out var message) ? message : null;
        }


        /// <summary>
        /// 检查where条件语句是否正确
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="whereClause"></param>
        /// <returns>where条件语句错误则返回报错信息，正确则返回null</returns>
        public static string CheckWhereClause(IFeatureClass featureClass, string whereClause)
        {
            IQueryFilter queryFilter = new QueryFilterClass();
            queryFilter.WhereClause = whereClause;
            try
            {
                featureClass.Search(queryFilter, true);
            }
            catch (Exception ex) { return ex.Message; }
            return null;
        }
        /// <summary>
        /// 检查是否存在几何错误
        /// </summary>
        /// <param name="featureClass"></param>
        /// <param name="whereClause"></param>
        /// <param name="showDetail">错误信息中，是否包含几何错误的描述</param>
        /// <returns>存在几何错误则返回几何错误信息，正确则返回null</returns>
        public static string CheckGeometryValidate(IFeatureClass featureClass, string whereClause = null, bool showDetail = false)
        {
            var sbMessage = new StringBuilder();
            var geoCheckInfos = GeometryCheckInfo.GetGeometryCheckInfos();
            var oidFieldName = featureClass.OIDFieldName;
            featureClass.QueryFeatures(whereClause, feature =>
            {
                int oid = feature.OID;
                if (feature.Shape.IsEmpty)
                    sbMessage.AppendLine($"{oidFieldName}为{oid}的图斑是空的");
                else
                {
                    feature.Shape.Check(out var esriNonSimpleReason);
                    var checkInfo = geoCheckInfos.FirstOrDefault(v => v.eType == esriNonSimpleReason);
                    if (checkInfo != null)
                        sbMessage.AppendLine(
                            showDetail ? $"{oidFieldName}为{oid}的图斑{checkInfo.AliasName}，即{checkInfo.Description}" : $"{oidFieldName}为{oid}的图斑{checkInfo.AliasName}");
                }
            });
            return sbMessage.ToString();
        }


        /// <summary>
        /// 检查几何坐标是否超出范围
        /// </summary>
        /// <param name="featureClass"></param>
        /// <returns></returns>
        [Obsolete("未实现该方法")]
        public static string CheckGeometryRange(IFeatureClass featureClass)
        {
            return null;
        }
        /// <summary>
        /// 检查ArcMap和ArcEngine是否同时安装导致出错
        /// </summary>
        /// <returns></returns>
        [Obsolete("未实现该方法")]
        public static string CheckArcMap_Engine_Install()
        {
            //WLib.Envir.ArcGis.ArcGisEnvironment.GetCheckArcGisInstall(EArcGisProductType.Desktop);
            //WLib.Envir.ArcGis.ArcGisEnvironment.GetCheckArcGisInstall(EArcGisProductType.ArcEngineRuntime);
            return null;
        }
        /// <summary>
        /// 检查当前ArcGIS版本是否为10.0且安装了SP1补丁
        /// </summary>
        /// <returns></returns>
        [Obsolete("未实现该方法")]
        public static string CheckArcMap10_SP1_Install()
        {
            return null;
        }
        /// <summary>
        /// 检查License是否启用了空间分析的权限
        /// </summary>
        [Obsolete("未实现该方法")]
        public static string CheckSpatialLicense()
        {
            //LicenseInitializer licenseInitializer = new LicenseInitializer();
            //licenseInitializer.InitializeApplication(
            //new[]  {  esriLicenseProductCode.esriLicenseProductCodeAdvanced  },
            //new[]  {  esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst,    esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork });
            return null;
        }
    }
}
