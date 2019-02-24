/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WLib.Files
{
    /// <summary>
    /// 文件夹与文件获取
    /// </summary>
    public class DirectoryFileAceess
    {
        /// <summary>
        /// 获取所有文件夹及各级子文件夹的路径
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        private static List<string> GetDirectories(string dirPath)
        {
            var tmpDirs = Directory.GetDirectories(dirPath).ToList();
            var allDirs = new List<string>();
            if (tmpDirs.Count > 0)
            {
                foreach (var path in tmpDirs)
                {
                    allDirs.Add(path);
                    allDirs.AddRange(GetDirectories(path)); //递归
                }
            }
            return allDirs;
        }

        /// <summary>
        /// 获取给出文件夹及其各级子文件夹下的所有文件路径
        /// </summary>
        /// <param name="rootPath">需要获取文件的目录</param>
        /// <returns></returns>
        private static List<string> GetAllFilePaths(string rootPath)
        {
            var dirs = new List<string>();
            var list = new List<string>();

            dirs.Add(rootPath);
            dirs.AddRange(GetDirectories(rootPath));
            foreach (var p in dirs)
            {
                list.AddRange(Directory.GetFiles(p));
            }

            return list;
        }

        /// <summary>
        /// 从指定目录及其各级子目录中，选取指定扩展名的文件，返回文件路径
        /// </summary>
        /// <param name="rootPath">需要筛选文件的目录</param>
        /// <param name="extensions">要筛选的多个扩展名，空值时选取所有类型的文件</param>
        /// <returns></returns>
        public static List<string> GetAllFilePaths(string rootPath, string[] extensions)
        {
            var paths = GetAllFilePaths(rootPath);
            var resultPaths = new List<string>();
            foreach (var path in paths)
            {
                var fileName = new FileInfo(path).Name;
                var isNotSpecialFile = !fileName.Substring(0, 2).Equals("~$"); //文件不应该是以"~$"开头的文件

                //若extensions为空，或者文件扩展名符合要求，返回True
                var isNecessaryFile = extensions?.Any(v => Path.GetExtension(path).ToLower().Equals(v)) ?? true;

                if (isNotSpecialFile && isNecessaryFile)
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
                var isNotSpecialFile = !fileName.Substring(0, 2).Equals("~$"); //文件不应该是以"~$"开头的文件

                //若extensions为空，或者文件扩展名符合要求，返回True
                var isNecessaryFile = extensions?.Any(v => Path.GetExtension(path).ToLower().Equals(v)) ?? true;

                if (isNotSpecialFile && isNecessaryFile)
                {
                    var preFileName = fileNameToPathDict.Keys.FirstOrDefault(v => v.Equals(fileName));
                    if (preFileName != null)
                    {
                        if (ignoreSameName) //忽略同名文件
                            continue;

                        throw new Exception($"成果目录中，以下两处存在同名文件，请检查是否有误：\r\n{path}\r\n{fileNameToPathDict[preFileName]}");
                    }
                    fileNameToPathDict.Add(fileName, path);
                }
            }
            return fileNameToPathDict;
        }

    }
}
