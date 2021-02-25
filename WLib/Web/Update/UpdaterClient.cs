/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/06/15
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLib.Files;
using WLib.Files.Zip;

namespace WLib.Web.Update
{
    /// <summary>
    /// 提供软件自动更新的相关方法（客户端）
    /// <para>使用示例：</para>
    /// <code>var updater = new UpdaterClient(null, @"https://xxx/Update/CheckUpdate", @"https://xxx/Update/DownloadUpdateFiles",@"c:\", "myProgram.exe");</code>
    /// <code>updater.CheckUpdate(); //①检查是否需要更新</code>
    /// <code>updater.DownloadUpdateFiles(); //②调用更新服务下载文件更新包</code>
    /// <code>updater.RestartForUpdate(); //③解压缩更新包文件，退出主程序，替换主程序中的文件，重启主程序</code>
    /// </summary>
    [Serializable]
    public class UpdaterClient
    {
        /// <summary>
        /// 需要包含在更新范围的文件类型
        /// </summary>
        public string[] UpdateFileExtensions { get; set; }
        /// <summary>
        /// 判断软件是否需要更新的服务的url
        /// </summary>
        public string CheckUpdateServiceUrl { get; set; }
        /// <summary>
        /// 下载需要更新的文件的服务的url
        /// </summary>
        public string DownloadUpdateFilesUrl { get; set; }
        /// <summary>
        /// 需要进行更新的软件的进程名
        /// </summary>
        public string UpdateProcessName { get; set; }
        /// <summary>
        /// 需要进行更新的软件所在目录
        /// <para>默认是当前程序所在目录</para>
        /// </summary>
        public string ProgramDir { get; set; }

        /// <summary>
        /// 下载的更新包在本地的存放目录
        /// <para>默认目录：当前程序所在目录\UpdateFiles</para>
        /// </summary>
        public string UpdateFileDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "UpdateFiles";
        /// <summary>
        /// 下载的更新包文件名
        /// </summary>
        public string UpdatePakageName { get; set; } = "更新包" + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss");
        /// <summary>
        /// 检查更新的超时等待时间
        /// </summary>
        public int CheckTimeout { get; set; } = 10000;


        /// <summary>
        /// 提供软件自动更新的相关方法
        /// </summary>
        public UpdaterClient() { }
        /// <summary>
        /// 提供软件自动更新的相关方法
        /// </summary>
        /// <param name="updateFileExtensions">需要包含在更新范围的文件类型，空值时将更新全部类型的文件</param>
        /// <param name="checkUpdateServiceUrl">判断软件是否需要更新的服务的url</param>
        /// <param name="downloadUpdateFilesUrl">下载需要更新的文件的服务的url</param>
        /// <param name="updateProcessName">需要进行更新的软件的进程名</param>
        /// <param name="programDir">需要进行更新的软件所在目录</param>
        /// <param name="updateFileDir">下载的更新包在本地的存放目录，如果值为null， 则默认为：当前程序所在目录\UpdateFiles</param>
        /// <param name="checkTimeout">检查更新的超时等待时间</param>
        public UpdaterClient(string[] updateFileExtensions, string checkUpdateServiceUrl, string downloadUpdateFilesUrl,
            string programDir, string updateProcessName, string updateFileDir = null, int checkTimeout = 10000)
        {
            if (string.IsNullOrWhiteSpace(checkUpdateServiceUrl)) throw new ArgumentException(nameof(checkUpdateServiceUrl));
            if (string.IsNullOrWhiteSpace(downloadUpdateFilesUrl)) throw new ArgumentException(nameof(downloadUpdateFilesUrl));
            if (string.IsNullOrWhiteSpace(programDir)) throw new ArgumentException(nameof(programDir));
            if (string.IsNullOrWhiteSpace(updateProcessName)) throw new ArgumentException(nameof(updateProcessName));
            if (checkTimeout < 0) throw new ArgumentException($"超时等待时间“{nameof(checkTimeout)}”不能小于0");

            UpdateFileExtensions = updateFileExtensions;
            CheckUpdateServiceUrl = checkUpdateServiceUrl;
            DownloadUpdateFilesUrl = downloadUpdateFilesUrl;
            UpdateProcessName = updateProcessName;
            CheckTimeout = checkTimeout;
            ProgramDir = programDir;
            if (!string.IsNullOrWhiteSpace(updateFileDir)) UpdateFileDir = updateFileDir;
        }


        #region 同步方法
        /// <summary>
        /// 调用更新检查服务，判断软件是否需要更新
        /// </summary>
        /// <returns>表示是否需要更新，True则需要更新，False则不需要更新</returns>
        public bool CheckUpdate()
        {
            var fileInfoColl = CreateFileInfoCollection(ProgramDir, UpdateFileExtensions);
            var json = JsonConvert.SerializeObject(fileInfoColl);
            var httpResponse = HttpHelper.Post(CheckUpdateServiceUrl, json, CheckTimeout);
            using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                if (result == "0" || result.ToLower() == "false") return false;
            }
            return true;
        }
        /// <summary>
        /// 调用更新服务，下载需要更新的文件（被打包的文件集）保存到本地
        /// </summary>
        /// <returns>更新结果状态</returns>
        public EDownloadState DownloadUpdateFiles()
        {
            var fileInfoColl = CreateFileInfoCollection(ProgramDir, UpdateFileExtensions);
            var json = JsonConvert.SerializeObject(fileInfoColl);
            var httpWebResponse = HttpHelper.Post(DownloadUpdateFilesUrl, json);
            if (httpWebResponse.ContentLength == 0)
                return EDownloadState.NoNeedToUpdate;
            else
            {
                HttpHelper.ResponseToSaveFile(httpWebResponse, Path.Combine(UpdateFileDir, UpdatePakageName));
                return EDownloadState.Dowloaded;
            }
        }
        /// <summary>
        /// 根据软件进程名关闭全部同名进程（软件），解压缩软件更新包进行软件更新，然后重启软件
        /// </summary>
        /// <param name="appName"></param>
        public void RestartForUpdate()
        {
            var processes = Process.GetProcessesByName(UpdateProcessName);
            if (processes.Length == 0)
                throw new Exception($"找不到进程“{UpdateProcessName}”");

            foreach (var process in processes)
            {
                process.Close();
                process.WaitForExit();
            }
            var path = Path.Combine(UpdateFileDir, UpdatePakageName);
            SharpZip.UnZipFile(path, ProgramDir);
            Process.Start(processes[0].StartInfo.FileName);
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 异步调用更新检查服务，判断软件是否需要更新
        /// </summary>
        /// <returns>表示是否需要更新，True则需要更新，False则不需要更新</returns>
        public Task<bool> CheckUpdateAsync() => Task.Factory.StartNew(() => CheckUpdate());
        /// <summary>
        /// 异步调用更新服务，下载需要更新的文件（被打包的文件集）保存到本地
        /// </summary>
        /// <returns>更新结果状态</returns>
        public Task<EDownloadState> DownloadFilesAsync() => Task.Factory.StartNew(() => DownloadUpdateFiles());
        /// <summary>
        /// 异步执行以下操作：
        /// 根据软件进程名关闭全部同名进程（软件），解压缩软件更新包进行软件更新，然后重启软件
        /// </summary>
        /// <param name="appName"></param>
        public Task RestartForUpdateAsync() => Task.Factory.StartNew(() => RestartForUpdate());
        #endregion


        #region 内部方法
        /// <summary>
        ///  获取指定目录下文件及其MD5信息，以及获取软件的版本信息
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        internal static FileMd5InfoCollection CreateFileInfoCollection(string rootDirectory, string[] extensions)
        {
            return new FileMd5InfoCollection()
            {
                Version = DateTime.Now.ToString("yyyyMMddHHmmss"),
                FileInfos = CreateFileMd5Infos(rootDirectory, extensions).ToList()
            };
        }
        /// <summary>
        /// 获取指定目录下文件及其MD5信息
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        private static IEnumerable<FileMd5Info> CreateFileMd5Infos(string rootDirectory, string[] extensions)
        {
            if (!Directory.Exists(rootDirectory))
                throw new DirectoryNotFoundException($"找不到目录：“{rootDirectory}”");

            var filePaths = PathEx.GetAllFilePaths(rootDirectory, extensions);
            foreach (var filePath in filePaths)
            {
                var path = filePath.Replace(rootDirectory, "").TrimStart('\\');
                yield return new FileMd5Info(path, PathEx.GetMD5HashFromFile(filePath));
            }
        }
        #endregion
    }
}
