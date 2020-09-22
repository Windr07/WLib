using System;
using System.IO;
using System.Linq;
using WLib.Files.Zip;

namespace WLib.Web.Update
{
    //1、在服务端程序/网站启动时，开启文件监测，生成最新的文件MD5和版本信息的配置文件
    //2、提供检查更新服务（对比客户端数据和服务端配置文件信息，全部一致返回false,不一致返回true）
    //3、提供更新服务（对比客户端数据和服务端配置文件信息，返回不一致的文件的文件流）

    /// <summary>
    /// 提供软件自动更新的相关服务方法（服务端）
    /// </summary>
    public class UpdaterServer
    {
        /// <summary>
        /// 需要包含在更新范围的文件类型
        /// <para>默认更新的文件类型包括：.dll .exe .db .pdb .txt .json</para>
        /// </summary>
        public string[] UpdateFileExtensions { get; set; } = new string[] { ".dll", ".exe", ".db", ".pdb", ".txt", ".json" };
        /// <summary>
        /// 下载的更新包在本地的存放目录
        /// <para>默认目录：当前程序所在目录\UpdateFiles</para>
        /// </summary>
        public string UpdateFileDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "\\UpdateFiles";


        /// <summary>
        /// 提供软件自动更新的相关服务方法（服务端）
        /// </summary>
        public UpdaterServer(string updateFileDir, params string[] updateFileExtensions)
        {
            if (updateFileDir != null)
                UpdateFileDir = updateFileDir;
            if (updateFileExtensions != null && updateFileExtensions.Length != 0)
                UpdateFileExtensions = updateFileExtensions;
        }


        /// <summary>
        /// 对比客户端和服务端的文件信息，判断是否存在需要更新的文件
        /// </summary>
        /// <param name="clientFileInfos">客户端文件信息</param>
        /// <returns></returns>
        public bool CheckUpdate(FileMd5InfoCollection clientFileInfos)
        {
            var updateFileInfos = GetUpdateFileInfos(clientFileInfos);
            return updateFileInfos.FileInfos.Count > 0;
        }
        /// <summary>
        /// 对比客户端和服务端的文件信息，返回需要更新的文件的数据流
        /// </summary>
        /// <param name="clientFileInfos">客户端文件信息</param>
        /// <returns></returns>
        public MemoryStream GetUpdateFileStream(FileMd5InfoCollection clientFileInfos)
        {
            var updateFileInfos = GetUpdateFileInfos(clientFileInfos);
            var filePaths = updateFileInfos.FileInfos.Select(v => v.Path).ToArray();
            var memoryStream = new MemoryStream();
            SharpZip.ZipFiles(filePaths, memoryStream);
            return memoryStream;
        }
        /// <summary>
        /// 对比客户端和服务端的文件信息，返回需要更新的文件的信息
        /// </summary>
        /// <param name="clientFileInfos">客户端文件信息</param>
        /// <returns></returns>
        private FileMd5InfoCollection GetUpdateFileInfos(FileMd5InfoCollection clientFileInfos)
        {
            var serverFileInfos = UpdaterClient.CreateFileInfoCollection(UpdateFileDir, UpdateFileExtensions);
            var diffFileInfos = serverFileInfos.FileInfos.Where(s =>
                !clientFileInfos.FileInfos.Exists(c => c.MD5 == s.MD5 && c.Name == s.Name && c.Path == s.Path));
            return new FileMd5InfoCollection()
            {
                Version = serverFileInfos.Version,
                FileInfos = diffFileInfos.ToList()
            };
        }
    }
}
