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
using System.Text;
using System.Security.Cryptography;

namespace WLib.Files
{
    /// <summary>
    /// 对文件的相关操作
    /// </summary>
    public class FileOpt
    {
        /// <summary>
        /// 获取文件md5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5Hash</returns>
        public static string GetMD5HashFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(string.Format("文件“{0}”不存在！", filePath));

            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                var sb = new StringBuilder();
                foreach (byte b in retVal) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        /// <summary>
        /// 通过HashAlgorithm的TransformBlock方法对流进行叠加运算获得MD5，
        /// 可使用于传输文件或接收文件时同步计算MD5值，
        /// 可自定义缓冲区大小，计算速度较快
        /// (https://blog.csdn.net/qiujuer/article/details/19344527)
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5Hash</returns>
        public static string GetMD5ByHashAlgorithm(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(string.Format("<{0}>, 不存在", filePath));
            int bufferSize = 1024 * 16;//自定义缓冲区大小16K
            byte[] buffer = new byte[bufferSize];
            Stream inputStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
            int readLength = 0;//每次读取长度
            var output = new byte[bufferSize];
            while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);//计算MD5
            }
            //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            inputStream.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }
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
