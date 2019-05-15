/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace WLib.Files
{
    /// <summary>
    /// 对文件的相关操作
    /// </summary>
    public class FileOpt
    {
        /// <summary>
        /// 检查文件名是否合法：不能为空或空白字符，不能包含字符\/:*?"&lt;>|
        /// </summary>
        /// <param name="fileName">文件名，不包含路径</param>
        /// <returns></returns>
        public static bool ValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;
            return !Path.GetInvalidFileNameChars().Any(fileName.Contains);
        }
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
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="newFileName">新文件夹名称</param>
        /// <param name="overwrite">是否覆盖同名文件，若不允许覆盖同名文件夹，则向文件夹名称末尾添加序号“(n)”直到不存在同名文件为止</param>
        /// <returns></returns>
        public static string ReNameFile(string filePath, string newFileName, bool overwrite = true)
        {
            return MoveFile(filePath, Path.Combine(Path.GetDirectoryName(filePath), newFileName), overwrite);
        }
        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="directory">文件所在目录</param>
        /// <param name="oldFileName">原文件夹名称</param>
        /// <param name="newFileName">新文件夹名称</param>
        /// <param name="overwrite">是否覆盖同名文件，若不允许覆盖同名文件夹，则向文件夹名称末尾添加序号“(n)”直到不存在同名文件为止</param>
        /// <returns></returns>
        public static string ReNameFile(string directory, string oldFileName, string newFileName, bool overwrite = true)
        {
            return MoveFile(Path.Combine(directory, oldFileName), Path.Combine(directory, newFileName), overwrite);
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
        /// <summary>
        /// 构造并返回文件路径，
        /// 若不允许覆盖同名文件，则向文件名末尾添加序号“(n)”直到不存在同名文件为止
        /// </summary>
        /// <param name="outPutDir">目录（例如：@"E:\test"）</param>
        /// <param name="fileName">文件名（例如："test"）</param>
        /// <param name="extension">文件扩展名（例如：".mxd"）</param>
        /// <param name="overwrite">是否覆盖同名文件</param>
        /// <returns></returns>
        public static string CombineReNameFile(string outPutDir, string fileName, string extension, bool overwrite = true)
        {
            var filePath = Path.Combine(outPutDir, fileName + extension);
            if (File.Exists(filePath) && !overwrite)
            {
                int tempAddNumber = 0;
                while (true)
                {
                    filePath = Path.Combine(outPutDir, $"{fileName}({++tempAddNumber}){extension}");
                    if (!File.Exists(filePath))
                        break;
                }
            }
            return filePath;
        }


        ///  <summary>
        /// 移动文件，
        /// 若不允许覆盖同名文件，则向文件名末尾添加序号“(n)”直到不存在同名文件为止
        ///  </summary>
        ///  <param name="fromPath">源文件路径</param>
        ///  <param name="destPath">目标文件位置</param>
        /// <param name="isOverride">是否覆盖现有文件</param>
        /// <returns></returns>
        public static string MoveFile(string fromPath, string destPath, bool isOverride)
        {
            if (File.Exists(destPath))
            {
                if (isOverride)
                {
                    File.Delete(destPath);
                    File.Move(fromPath, destPath);
                }
                else
                {
                    int i = 0;
                    string path = Path.GetFileNameWithoutExtension(destPath);
                    string extension = Path.GetExtension(destPath);
                    while (File.Exists($"{path}({i}){extension}"))
                        i++;

                    destPath = $"{path}({i}){extension}";
                    File.Move(fromPath, destPath);
                }
            }
            else
            {
                File.Move(fromPath, destPath);
            }
            return destPath;
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


        /// <summary>
        /// 复制文件到剪贴板
        /// </summary>
        /// <param name="filePaths">文件的路径集合</param>
        /// <returns></returns>
        public static void CopyFileToClipBoard(string[] filePaths)
        {
            var strColl = new System.Collections.Specialized.StringCollection();
            strColl.AddRange(filePaths);
            Clipboard.SetFileDropList(strColl);
        }
        /// <summary>
        /// 将现有文件复制到指定目录下，
        /// 若不允许覆盖同名文件，则向文件名末尾添加序号“(n)”直到不存在同名文件为止
        /// </summary>
        /// <param name="fromFilePath">源文件路径</param>
        /// <param name="toDir">目标文件目录</param>
        /// <param name="overwrite">是否覆盖同名文件</param>
        /// <returns></returns>
        public static bool CopyFile(string fromFilePath, string toDir, bool overwrite)
        {
            bool result;
            try
            {
                string fromFileName = Path.GetFileName(fromFilePath);
                string toFilePath = Path.Combine(toDir, fromFileName);
                if (File.Exists(toFilePath) && !overwrite)
                {
                    string shortName = Path.GetFileNameWithoutExtension(fromFilePath);
                    string extension = Path.GetExtension(fromFilePath);
                    int tempAddNumber = 0;
                    while (true)
                    {
                        toFilePath = Path.Combine(toDir, $"{shortName}({tempAddNumber++}){extension}");
                        if (!File.Exists(toFilePath))
                            break;
                    }
                }
                File.Copy(fromFilePath, toFilePath, overwrite);
                result = true;
            }
            catch { result = false; }
            return result;
        }
    }
}
