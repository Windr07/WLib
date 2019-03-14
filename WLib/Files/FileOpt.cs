using System;
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
        /// 检查文件名是否合法：文字名中不能包含字符\/:*?"&lt;>|
        /// </summary>
        /// <param name="fileName">文件名，不包含路径</param>
        /// <returns></returns>
        public static bool ValidFileName(string fileName)
        {
            bool isValid = true;
            string errChars = "\\/:*?\"<>|";
            if (string.IsNullOrEmpty(fileName))
            {
                isValid = false;
            }
            else
            {
                foreach (var errChar in errChars)
                {
                    if (fileName.Contains(errChar.ToString()))
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
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
            string filePath = Path.Combine(outPutDir, fileName + extension);
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

        ///  <summary>
        /// 移动文件，
        /// 若不允许覆盖同名文件，则向文件名末尾添加序号“(n)”直到不存在同名文件为止
        ///  </summary>
        ///  <param name="fromPath">源文件路径</param>
        ///  <param name="destPath">目标文件位置</param>
        /// <param name="isOverride">是否覆盖现有文件</param>
        /// <returns></returns>
        public static void MoveFilePlus(string fromPath, string destPath, bool isOverride)
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
        }
    }
}
