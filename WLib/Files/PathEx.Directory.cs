/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WLib.Files
{
    /// <summary>
    /// 对目录或文件夹的相关操作
    /// </summary>
    public static partial class PathEx
    {
        /// <summary>
        /// 检查文件夹是否合法：不能为空或空白字符，不能包含字符\/:*?"&lt;>|
        /// </summary>
        /// <param name="folderName">文件夹名称，不包含路径</param>
        /// <returns></returns>
        public static bool FolderNameIsValid(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return false;
            return !Path.GetInvalidPathChars().Any(folderName.Contains);
        }


        /// <summary>
        /// 从指定目录及其各级子目录中，选取指定扩展名的文件，返回文件路径
        /// </summary>
        /// <param name="rootDirectory">需要筛选文件的目录</param>
        /// <param name="extensions">要筛选的多个扩展名，空值时选取所有类型的文件</param>
        /// <returns></returns>
        public static List<string> GetAllFilePaths(string rootDirectory, params string[] extensions)
        {
            bool selectAll = extensions == null || extensions.Length == 0;
            var paths = Directory.GetFiles(rootDirectory, "*", SearchOption.AllDirectories);
            var resultPaths = new List<string>();
            foreach (var path in paths)
            {
                var fileName = new FileInfo(path).Name;
                var extension = Path.GetExtension(path).ToLower();
                if (fileName.Substring(0, 2).Equals("~$")) continue; //跳过以"~$"开头的文件

                if (selectAll || extensions.Any(v => extension.Equals(v, StringComparison.OrdinalIgnoreCase)))
                    resultPaths.Add(path);
            }
            return resultPaths;
        }
        /// <summary>
        /// 从指定目录及其各级子目录中，选取指定扩展名的文件，返回文件名与路径构成的键值对
        /// </summary>
        /// <param name="rootPath">需要筛选文件的目录</param>
        /// <param name="extensions">要筛选的多个扩展名，空值时选取所有类型的文件</param>
        /// <param name="ignoreSameName">是否忽略同名文件</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFileNameToPathDict(string rootPath, string[] extensions, bool ignoreSameName = false)
        {
            var fileNameToPathDict = new Dictionary<string, string>();
            var paths = GetAllFilePaths(rootPath);
            foreach (var path in paths)
            {
                var fileName = new FileInfo(path).Name;
                if (fileName.Substring(0, 2).Equals("~$")) continue; //文件不应该是以"~$"开头的文件

                //若extensions为空，或者文件扩展名符合要求，返回True
                var isNecessaryFile = extensions?.Any(v => Path.GetExtension(path).ToLower().Equals(v)) ?? true;
                if (isNecessaryFile)
                {
                    var preFileName = fileNameToPathDict.Keys.FirstOrDefault(v => v.Equals(fileName));
                    if (preFileName != null)
                    {
                        if (ignoreSameName) //忽略同名文件
                            continue;

                        throw new Exception($"目录中，以下两处存在同名文件，请检查是否有误：\r\n{path}\r\n{fileNameToPathDict[preFileName]}");
                    }
                    fileNameToPathDict.Add(fileName, path);
                }
            }
            return fileNameToPathDict;
        }


        /// <summary>
        /// 按层级查找子目录或文件
        /// <para>指定各层级的子级文件夹名或文件名<paramref name="subNames"/>（可以包含*或?等通配符），依次查找各级子目录或文件，返回最后可查找的子目录或文件路径</para>
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="unfindException">找不到时是否抛出异常</param>
        /// <param name="subNames">子级文件夹名或文件名，可以包含*或?等通配符</param>
        /// <returns></returns>
        public static string FindSubPath(string dir, bool unfindException, params string[] subNames)
        {
            string resultPath = dir;
            for (int i = 0; i < subNames.Length; i++)
            {
                var subPath = Directory.GetDirectories(resultPath, subNames[i], SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (subPath == null)
                    subPath = Directory.GetFiles(resultPath, subNames[i], SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (subPath == null)
                {
                    if (unfindException)
                        throw new Exception($"在目录“{resultPath}”中找不到名称包含“{subNames[i]}”的子文件夹");
                    else
                        break;
                }
                resultPath = subPath;   //subPath作为resultPath进入下一次循环，即进入下一层级子目录中查找
            }
            return resultPath;
        }
        /// <summary>
        /// 按层级查找子目录或文件
        /// <para>指定各层级的子级文件夹名或文件名<paramref name="subNames"/>（支持正则表达式），依次查找各级子目录或文件，返回最后可查找的子目录或文件路径</para>
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="unfindException">找不到时是否抛出异常</param>
        /// <param name="subNames">子级文件夹名或文件名，支持正则表达式</param>
        /// <returns></returns>
        public static string FindSubPath_ByRegex(string dir, bool unfindException, params string[] subNames)
        {
            string resultPath = dir;
            for (int i = 0; i < subNames.Length; i++)
            {
                var regex = new Regex(subNames[i]);
                var subPath = Directory.GetDirectories(resultPath, "*", SearchOption.TopDirectoryOnly).FirstOrDefault(v => regex.IsMatch(new DirectoryInfo(v).Name));
                if (subPath == null)
                    subPath = Directory.GetFiles(resultPath, "*", SearchOption.TopDirectoryOnly).FirstOrDefault(v => regex.IsMatch(Path.GetFileName(v)));
                if (subPath == null)
                {
                    if (unfindException)
                        throw new Exception($"在目录“{resultPath}”中找不到名称包含“{subNames[i]}”的子文件夹");
                    else
                        break;
                }
                resultPath = subPath;   //subPath作为resultPath进入下一次循环，即进入下一层级子目录中查找
            }
            return resultPath;
        }



        /// <summary>
        /// 同步目录中的文件
        /// <para>即判断源目录下的文件，与目标目录的文件的大小和最后修改时间是否一致，不一致则目标目录文件被同步替换（不存在则创建）</para>
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="targetDir">目标目录，该目录下的文件若与源目录不一致则被替换</param>
        /// <param name="option">指定是同步当前目录的文件，还是同步当前目录及其所有子目录的文件</param>
        public static void SyncDirectory(string sourceDir, string targetDir, SearchOption option = SearchOption.AllDirectories)
        {
            var filePaths = Directory.GetFiles(sourceDir, "*.*", option);
            foreach (var filePath in filePaths)
            {
                var targetFilePath = filePath.Replace(sourceDir, targetDir);
                var sourceFileInfo = new FileInfo(filePath);
                var targetFileInfo = new FileInfo(targetFilePath);
                if (targetFileInfo.Exists)
                {
                    if (sourceFileInfo.Length != targetFileInfo.Length || sourceFileInfo.LastWriteTime != targetFileInfo.LastWriteTime)
                        File.Copy(filePath, targetFilePath, true);
                }
                else
                {
                    if (!targetFileInfo.Directory.Exists)
                        targetFileInfo.Directory.Create();
                    File.Copy(filePath, targetFilePath, true);
                }
            }
        }


        #region 复制、移动、重命名文件夹
        /// <summary>
        /// 复制指定目录下的所有数据（各级文件和文件夹）到目标目录
        /// </summary>
        /// <param name="soureceDir">源目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overwrite">是否覆盖同名文件</param>
        public static void CopyDir(string soureceDir, string targetDir, bool overwrite)
        {
            if (!Directory.Exists(targetDir))
                Directory.CreateDirectory(targetDir);

            var paths = Directory.GetFileSystemEntries(soureceDir);
            foreach (string path in paths)//遍历所有的文件和目录
            {
                if (Directory.Exists(path))
                    CopyDir(path, Path.Combine(targetDir, Path.GetFileName(path)), overwrite);
                else
                    File.Copy(path, Path.Combine(targetDir, Path.GetFileName(path)), overwrite);
            }
        }
        /// <summary>
        /// 递归复制目录下的所有文件和文件夹
        /// </summary>
        /// <param name="sourceDir">源目录</param>
        /// <param name="targetDir">目标目录</param>
        /// <param name="overwrite">是否覆盖同名文件</param>
        public static void CopyDir(DirectoryInfo sourceDir, DirectoryInfo targetDir, bool overwrite)
        {
            //判断目标文件夹是否是源文件夹的子目录
            for (var dirInfo = targetDir.Parent; dirInfo != null; dirInfo = dirInfo.Parent)
            {
                if (dirInfo.FullName == sourceDir.FullName)
                    throw new Exception("无法复制！目标文件夹是源文件夹的子目录！");
            }
            var fileInfos = sourceDir.GetFiles();
            var dirInfos = sourceDir.GetDirectories();
            if (Directory.Exists(targetDir.FullName) == false) // 检查目标文件夹是否存在，不存在则创建
                Directory.CreateDirectory(targetDir.FullName);

            foreach (var fileInfo in fileInfos)  //复制所有文件
                fileInfo.CopyTo(Path.Combine(targetDir.ToString(), fileInfo.Name), overwrite);

            foreach (var dirInfo in dirInfos) //递归复制子目录
            {
                DirectoryInfo targetSubDir = targetDir.CreateSubdirectory(dirInfo.Name);
                CopyDir(dirInfo, targetSubDir, overwrite);
            }
        }
        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="newFolderName">新文件夹名称</param>
        /// <param name="overwrite">是否覆盖同名文件夹，若不允许覆盖同名文件夹，则向文件夹名称末尾添加序号“(n)”直到不存在同名文件为止</param>
        /// <returns></returns>
        public static string ReNameFolder(string dir, string newFolderName, bool overwrite = true)
        {
            return MoveFolder(dir, Path.Combine(Path.GetDirectoryName(dir), newFolderName), overwrite);
        }
        ///  <summary>
        /// 移动文件夹，
        /// 若不允许覆盖/合并同名文件夹，则向文件夹名末尾添加序号“(n)”直到不存在同名文件夹为止
        ///  </summary>
        ///  <param name="fromDir">源文件夹路径</param>
        ///  <param name="destDir">目标文件夹位置</param>
        /// <param name="isOverride">是否覆盖/合并现有文件夹</param>
        /// <returns></returns>
        public static string MoveFolder(string fromDir, string destDir, bool isOverride)
        {
            if (Directory.Exists(destDir))
            {
                if (isOverride)
                {
                    Directory.Move(fromDir, destDir);
                }
                else
                {
                    var i = 0;
                    var dir = Path.GetDirectoryName(destDir);
                    while (Directory.Exists($"{dir}({i})"))
                        i++;

                    destDir = $"{dir}({i})";
                    Directory.Move(fromDir, destDir);
                }
            }
            else
            {
                Directory.Move(fromDir, destDir);
            }
            return destDir;
        }
        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="parentDirectory">文件夹所在目录</param>
        /// <param name="oldFolerName">原文件夹名称</param>
        /// <param name="newFolderName">新文件夹名称</param>
        /// <param name="overwrite">是否覆盖同名文件夹，若不允许覆盖同名文件夹，则向文件夹名称末尾添加序号“(n)”直到不存在同名文件为止</param>
        /// <returns></returns>
        public static string ReNameFolder(string parentDirectory, string oldFolerName, string newFolderName, bool overwrite = true)
        {
            return MoveFolder(Path.Combine(parentDirectory, oldFolerName), Path.Combine(parentDirectory, newFolderName), overwrite);
        }
        /// <summary>
        /// 构造并返回文件夹路径，
        /// 若不允许覆盖同名文件夹，则向文件夹名称末尾添加序号“(n)”直到不存在同名文件为止
        /// </summary>
        /// <param name="parentDirectory"></param>
        /// <param name="folderName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static string CombineReNameFolder(string parentDirectory, string folderName, bool overwrite = true)
        {
            var dir = Path.Combine(parentDirectory, folderName);
            if (Directory.Exists(dir) && !overwrite)
            {
                int tempAddNumber = 0;
                while (true)
                {
                    dir = Path.Combine(parentDirectory, $"{folderName}({++tempAddNumber})");
                    if (!Directory.Exists(dir))
                        break;
                }
            }
            return dir;
        }
        #endregion
    }
}
