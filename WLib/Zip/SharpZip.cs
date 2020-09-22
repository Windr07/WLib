/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： 需要引用ICSharpCode.SharpZipLib.dll
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace WLib.Files.Zip
{
    /// <summary>
    /// 提供压缩与解压缩的方法
    /// </summary>
    public static class SharpZip
    {
        /// <summary>
        /// 缓存字节数
        /// </summary>
        private const int BufferSize = 4096;
        /// <summary>
        /// 压缩最小等级
        /// </summary>
        public const int ZipLevelMin = 0;
        /// <summary>
        /// 压缩最大等级
        /// </summary>
        public const int ZipLevelMax = 9;


        #region 流压缩与解压缩
        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="readStream">要压缩的流</param>
        /// <param name="compressionLevel">压缩等级</param>
        /// <param name="password">密码</param>
        /// <returns>压缩后的流</returns>
        public static MemoryStream ZipStream(Stream readStream, string password = null, int compressionLevel = 6)
        {
            MemoryStream resultStream = new MemoryStream();
            try
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(resultStream))
                {
                    zipStream.SetZipStream(password, compressionLevel, readStream.Length);
                    int readLength = 0;
                    byte[] buffer = new byte[BufferSize];
                    do
                    {
                        readLength = readStream.Read(buffer, 0, BufferSize);
                        zipStream.Write(buffer, 0, readLength);
                    } while (readLength == BufferSize);

                    readStream.Close();
                    zipStream.Flush();
                    zipStream.Finish();
                    zipStream.Close();
                }
            }
            catch (Exception ex) { throw new Exception("压缩字节数组发生错误：" + ex.Message, ex); }
            return resultStream;
        }
        /// <summary>
        /// 解压缩流
        /// </summary>
        /// <param name="readStream">要解压缩的流</param>
        /// <param name="password">密码</param>
        /// <returns>解压后的流</returns>
        public static MemoryStream UnZipStream(Stream readStream, string password = null)
        {
            MemoryStream writeStream = new MemoryStream();
            try
            {
                using (ZipInputStream zipStream = new ZipInputStream(readStream))
                {
                    zipStream.Password = password;
                    ZipEntry zipEntry = zipStream.GetNextEntry();
                    if (zipEntry != null)
                    {
                        byte[] buffer = new byte[BufferSize];
                        int readLength = 0;
                        do
                        {
                            readLength = zipStream.Read(buffer, 0, BufferSize);
                            writeStream.Write(buffer, 0, readLength);
                        } while (readLength == BufferSize);

                        writeStream.Flush();
                        writeStream.Close();
                    }
                    zipStream.Close();
                }
            }
            catch (Exception ex) { throw new Exception("解压字节数组发生错误：" + ex.Message, ex); }
            return writeStream;
        }
        #endregion


        #region 字节压缩与解压缩
        /// <summary>
        /// 压缩字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="compressionLevel">压缩等级</param>
        /// <param name="password">密码</param>
        /// <returns>压缩后的字节数组</returns>
        public static byte[] ZipBytes(byte[] sourceBytes, string password = null, int compressionLevel = 6)
        {
            if (sourceBytes == null || sourceBytes.Length == 0)
                throw new Exception($"要压缩的字节数组“{nameof(sourceBytes)}”为空或数组长度为0");

            using (var memoryStream = ZipStream(new MemoryStream(sourceBytes), password, compressionLevel))
            {
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// 解压字节数组
        /// </summary>
        /// <param name="sourceBytes">源字节数组</param>
        /// <param name="password">密码</param>
        /// <returns>解压后的字节数组</returns>
        public static byte[] UnZipBytes(byte[] sourceBytes, string password = null)
        {
            if (sourceBytes == null || sourceBytes.Length == 0)
                throw new Exception($"要解压缩的字节数组“{nameof(sourceBytes)}”为空或数组长度为0");

            using (var memoryStream = UnZipStream(new MemoryStream(sourceBytes), password))
            {
                return memoryStream.ToArray();
            }
        }
        #endregion


        #region 文件压缩与解压缩
        /// <summary>
        /// 压缩单个文件/文件夹
        /// </summary>
        /// <param name="filePath">源文件/文件夹路径</param>
        /// <param name="zipFilePath">压缩结果文件路径</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="zipLevel">压缩等级，范围从0到9，可选，默认为6</param>
        /// <returns></returns>
        public static void ZipFile(string filePath, string zipFilePath, string comment = null, string password = null, int zipLevel = 6)
        {
            ZipFiles(new[] { filePath }, zipFilePath, comment, password, zipLevel);
        }
        /// <summary>
        /// 压缩单个文件/文件夹
        /// </summary>
        /// <param name="filePath">源文件/文件夹路径</param>
        /// <param name="stream">压缩结果流（文件流或内存流等）</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="zipLevel">压缩等级，范围从0到9，可选，默认为6</param>
        /// <returns></returns>
        public static void ZipFile(string filePath, Stream stream, string comment = null, string password = null, int zipLevel = 6)
        {
            ZipFiles(new[] { filePath }, stream, comment, password, zipLevel);
        }
        /// <summary>
        /// 压缩多个文件/文件夹
        /// </summary>
        /// <param name="filePaths">源文件/文件夹路径列表</param>
        /// <param name="zipFilePath">压缩结果文件路径</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="zipLevel">压缩等级，范围从0到9，可选，默认为6</param>
        public static void ZipFiles(IEnumerable<string> filePaths, string zipFilePath, string comment = null, string password = null, int zipLevel = 6)
        {
            //检测目标文件所属的文件夹是否存在，如果不存在则建立
            var zipFileDirectory = Path.GetDirectoryName(zipFilePath);
            if (!Directory.Exists(zipFileDirectory))
                Directory.CreateDirectory(zipFileDirectory);

            using (var stream = File.Create(zipFilePath))
            {
                ZipFiles(filePaths, stream, comment, password, zipLevel);
            }
        }
        /// <summary>
        /// 压缩多个文件/文件夹
        /// </summary>
        /// <param name="filePaths">源文件/文件夹路径列表</param>
        /// <param name="stream">压缩结果流（文件流或内存流等）</param>
        /// <param name="comment">注释信息</param>
        /// <param name="password">压缩密码</param>
        /// <param name="zipLevel">压缩等级，范围从0到9，可选，默认为6</param>
        public static void ZipFiles(IEnumerable<string> filePaths, Stream stream, string comment = null, string password = null, int zipLevel = 6)
        {
            try
            {
                var dictionary = GetFileSystemEntities(filePaths);
                using (ZipOutputStream zipStream = new ZipOutputStream(stream))
                {
                    zipStream.Password = password;//设置密码
                    zipStream.SetComment(comment);//添加注释
                    zipStream.SetLevel(CheckZipLevel(zipLevel));//设置压缩等级

                    foreach (string key in dictionary.Keys)//从字典取文件添加到压缩文件
                    {
                        if (File.Exists(key))//判断是文件还是文件夹
                        {
                            FileInfo fileItem = new FileInfo(key);
                            using (FileStream readStream = fileItem.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                ZipEntry zipEntry = new ZipEntry(dictionary[key]);
                                zipEntry.DateTime = fileItem.LastWriteTime;
                                zipEntry.Size = readStream.Length;
                                zipStream.PutNextEntry(zipEntry);
                                int readLength = 0;
                                byte[] buffer = new byte[BufferSize];

                                do
                                {
                                    readLength = readStream.Read(buffer, 0, BufferSize);
                                    zipStream.Write(buffer, 0, readLength);
                                } while (readLength == BufferSize);

                                readStream.Close();
                            }
                        }
                        else//对文件夹的处理
                        {
                            ZipEntry zipEntry = new ZipEntry(dictionary[key] + "/");
                            zipStream.PutNextEntry(zipEntry);
                        }
                    }
                    zipStream.Flush();
                    zipStream.Finish();
                    zipStream.Close();
                }
            }
            catch (Exception ex) { throw new Exception("压缩文件失败：" + ex.Message, ex); }
        }
        /// <summary>
        /// 解压文件到指定文件夹
        /// </summary>
        /// <param name="sourceFilePath">压缩文件</param>
        /// <param name="destinationDirectory">目标文件夹，如果为空则解压到当前文件夹下</param>
        /// <param name="password">密码</param>
        public static void UnZipFile(string sourceFilePath, string destinationDirectory = null, string password = null)
        {
            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("要解压的文件不存在", sourceFilePath);

            try
            {
                if (string.IsNullOrWhiteSpace(destinationDirectory))
                    destinationDirectory = Path.GetDirectoryName(sourceFilePath);
                if (!Directory.Exists(destinationDirectory))
                    Directory.CreateDirectory(destinationDirectory);

                using (var zipStream = new ZipInputStream(File.Open(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    zipStream.Password = password;
                    ZipEntry zipEntry = null;
                    while ((zipEntry = zipStream.GetNextEntry()) != null)
                    {
                        if (zipEntry.IsDirectory)//如果是文件夹则创建
                        {
                            Directory.CreateDirectory(Path.Combine(destinationDirectory, Path.GetDirectoryName(zipEntry.Name)));
                        }
                        else
                        {
                            string fileName = Path.GetFileName(zipEntry.Name);
                            if (!string.IsNullOrEmpty(fileName) && fileName.Trim().Length > 0)
                            {
                                var fileInfo = new FileInfo(Path.Combine(destinationDirectory, zipEntry.Name));
                                using (FileStream writeStream = fileInfo.Create())
                                {
                                    byte[] buffer = new byte[BufferSize];
                                    int readLength = 0;

                                    do
                                    {
                                        readLength = zipStream.Read(buffer, 0, BufferSize);
                                        writeStream.Write(buffer, 0, readLength);
                                    } while (readLength == BufferSize);

                                    writeStream.Flush();
                                    writeStream.Close();
                                }
                                fileInfo.LastWriteTime = zipEntry.DateTime;
                            }
                        }
                    }
                    zipStream.Close();
                }
            }
            catch (Exception ex) { throw new Exception("解压缩文件失败：" + ex.Message, ex); }
        }
        #endregion


        /// <summary>
        /// 为压缩准备文件系统对象
        /// </summary>
        /// <param name="sourceFilePaths"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetFileSystemEntities(IEnumerable<string> sourceFilePaths)
        {
            var fileDict = new Dictionary<string, string>();//文件字典
            foreach (string filePath in sourceFilePaths)
            {
                string path = filePath;
                //保证传入的文件夹也被压缩进文件
                if (path.EndsWith(@"\"))
                    path = path.Remove(path.LastIndexOf(@"\"));

                var parentDirectoryPath = Path.GetDirectoryName(path) + @"\";

                if (parentDirectoryPath.EndsWith(@":\\"))//防止根目录下把盘符压入的错误
                    parentDirectoryPath = parentDirectoryPath.Replace(@"\\", @"\");

                //获取目录中所有的文件系统对象
                Dictionary<string, string> subDictionary = GetAllFileSystemEntities(path, parentDirectoryPath);

                //将文件系统对象添加到总的文件字典中
                foreach (string tmpPath in subDictionary.Keys)
                {
                    if (!fileDict.ContainsKey(tmpPath))//检测重复项
                        fileDict.Add(tmpPath, subDictionary[tmpPath]);
                }
            }
            return fileDict;
        }
        /// <summary>
        /// 获取所有文件系统对象(Key为完整路径，Value为文件或文件夹名称)
        /// </summary>
        /// <param name="source">源路径</param>
        /// <param name="topDirectory">顶级文件夹的目录</param>
        /// <returns>字典中Key为完整路径，Value为文件(夹)名称</returns>
        private static Dictionary<string, string> GetAllFileSystemEntities(string source, string topDirectory)
        {
            var pathNameDict = new Dictionary<string, string>();
            pathNameDict.Add(source, source.Replace(topDirectory, ""));

            if (Directory.Exists(source))
            {
                //一次性获取下级所有目录，避免递归
                var directories = Directory.GetDirectories(source, "*.*", SearchOption.AllDirectories);
                foreach (string directory in directories)
                    pathNameDict.Add(directory, directory.Replace(topDirectory, ""));

                var filePaths = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories);
                foreach (string filePath in filePaths)
                    pathNameDict.Add(filePath, filePath.Replace(topDirectory, ""));
            }

            return pathNameDict;
        }
        /// <summary>
        /// 校验压缩等级
        /// </summary>
        /// <param name="zipLevel"></param>
        /// <returns></returns>
        private static int CheckZipLevel(int zipLevel)
        {
            zipLevel = zipLevel < ZipLevelMin ? ZipLevelMin : zipLevel;
            zipLevel = zipLevel > ZipLevelMax ? ZipLevelMax : zipLevel;
            return zipLevel;
        }
        /// <summary>
        /// 设置压缩流的密码、压缩等级、大小等
        /// </summary>
        /// <param name="zipStream"></param>
        /// <param name="password"></param>
        /// <param name="compressionLevel"></param>
        /// <param name="bytesLength"></param>
        private static void SetZipStream(this ZipOutputStream zipStream, string password, int compressionLevel, long bytesLength)
        {
            zipStream.Password = password;//设置密码
            zipStream.SetLevel(CheckZipLevel(compressionLevel));//设置压缩等级
            ZipEntry zipEntry = new ZipEntry("ZipBytes");
            zipEntry.DateTime = DateTime.Now;
            zipEntry.Size = bytesLength;
            zipStream.PutNextEntry(zipEntry);
        }
    }
}
