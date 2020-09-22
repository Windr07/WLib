/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using WLib.Data.Format;

namespace WLib.Files
{
    /// <summary>
    /// 文件夹与文件获取
    /// </summary>
    public static partial class PathEx
    {
        /// <summary>
        /// 判断路径长度是否符合要求（Windows默认的字符的长度限制为260，一个中文字符长度为2）（路径超长不一定出错，要看具体软件和数据）
        /// <para>True-路径长度符合要求</para>
        /// <para>False-路径超出260字符的长度限制</para>
        /// </summary>
        /// <param name="length">路径长度，一个中文字符长度为2</param>
        /// <returns></returns>
        public static bool PathLengthValidate(string path, out int length)
        {
            length = path.GetCNLength();
            return length < 260;
        }
        /// <summary>
        /// 将路径转为绝对路径
        /// <para>若路径为绝对路径则直接返回，若是相对路径则在路径前拼接当前程序目录，形成绝对路径</para>
        /// </summary>
        /// <param name="path">源路径，即要转为绝对路径的路径，此路径不能为空</param>
        /// <param name="appPath">当前程序目录路径，源路径以此为基准转为绝对路径</param>
        /// <returns></returns>
        public static string GetRootPath(string path, string appPath = null)
        {
            if (Path.IsPathRooted(path))
                return path;

            if (appPath == null)
                appPath = AppDomain.CurrentDomain.BaseDirectory;
            var currentDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(appPath);
            var newPath = Path.GetFullPath(path);
            Directory.SetCurrentDirectory(currentDir);
            return newPath;
        }
        /// <summary>
        /// 将路径转为相对路径
        /// <para>返回源路径相对于当前程序目录或指定目录的路径</para>
        /// </summary>
        /// <param name="path">源路径，即要转为相对路径的路径</param>
        /// <param name="appPath">应用程序路径，源路径将转为此路径的相对路径，此值为null则自动获取当前程序目录</param>
        /// <returns></returns>
        public static string GetRelativePath(string path, string appPath = null)
        {
            if (appPath == null)
                appPath = AppDomain.CurrentDomain.BaseDirectory;

            if (path.Contains(appPath))
                return path.Replace(appPath, "").TrimStart('/').TrimStart('\\');
            else
            {
                var intersectPath = new string(path.Intersect(appPath).ToArray());
                return path.Replace(intersectPath, "");
            }
        }
    }
}