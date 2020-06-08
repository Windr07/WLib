using System;
using System.IO;
using System.Linq;

namespace WLib.Files
{
    /// <summary>
    /// 对目录或文件夹的相关操作
    /// </summary>
    public class DirectoryOpt
    {
        /// <summary>
        /// 检查文件夹是否合法：不能为空或空白字符，不能包含字符\/:*?"&lt;>|
        /// </summary>
        /// <param name="folderName">文件夹名称，不包含路径</param>
        /// <returns></returns>
        public static bool ValidFolderName(string folderName)
        {
            if (string.IsNullOrWhiteSpace(folderName))
                return false;
            return !Path.GetInvalidPathChars().Any(folderName.Contains);
        }

        /// <summary>
        /// 按照给定的“子级文件夹或文件名<paramref name="subFolders"/>”，依次查找子目录或文件，返回子目录或文件路径
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="subFolders"></param>
        /// <returns></returns>
        public static string FindSubPath(string dir, bool unfindException, params string[] subFolders)
        {
            string resultPath = dir;
            for (int i = 0; i < subFolders.Length; i++)
            {
                var subPath = Directory.GetDirectories(resultPath, subFolders[i], SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (subPath == null)
                    subPath = Directory.GetFiles(resultPath, subFolders[i], SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (subPath == null)
                {
                    if (unfindException)
                        throw new Exception($"在目录“{resultPath}”中找不到名称包含“{subFolders[i]}”的子文件夹");
                    else
                        break;
                }
                resultPath = subPath;
            }
            return resultPath;
        }
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
            for (DirectoryInfo temp = targetDir.Parent; temp != null; temp = temp.Parent)
            {
                if (temp.FullName == sourceDir.FullName)
                    throw new Exception("无法复制！目标文件夹是源文件夹的子目录！");
            }
            var files = sourceDir.GetFiles();
            var dirs = sourceDir.GetDirectories();
            if (Directory.Exists(targetDir.FullName) == false) // 检查目标文件夹是否存在，不存在则创建
                Directory.CreateDirectory(targetDir.FullName);

            foreach (FileInfo file in files)  //复制所有文件
                file.CopyTo(Path.Combine(targetDir.ToString(), file.Name), overwrite);

            foreach (DirectoryInfo diSourceSubDir in dirs) //递归复制子目录
            {
                DirectoryInfo targetSubDir = targetDir.CreateSubdirectory(diSourceSubDir.Name);
                CopyDir(diSourceSubDir, targetSubDir, overwrite);
            }
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
        ///  <summary>
        /// 移动文件夹，
        /// 若不允许覆盖/合并同名文件夹，则向文件夹名末尾添加序号“(n)”直到不存在同名文件夹为止
        ///  </summary>
        ///  <param name="fromPath">源文件路径</param>
        ///  <param name="destPath">目标文件夹位置</param>
        /// <param name="isOverride">是否覆盖/合并现有文件夹</param>
        /// <returns></returns>
        public static string MoveFolder(string fromPath, string destPath, bool isOverride)
        {
            if (Directory.Exists(destPath))
            {
                if (isOverride)
                {
                    Directory.Move(fromPath, destPath);
                }
                else
                {
                    var i = 0;
                    var dir = Path.GetDirectoryName(destPath);
                    while (Directory.Exists($"{dir}({i})"))
                        i++;

                    destPath = $"{dir}({i})";
                    Directory.Move(fromPath, destPath);
                }
            }
            else
            {
                Directory.Move(fromPath, destPath);
            }
            return destPath;
        }
    }
}
