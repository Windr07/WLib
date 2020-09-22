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
using WLib.Attributes.Description;
using WLib.Files;
using WLib.Files.Zip;

namespace WLib.Web.Update
{
    //1、检查是否需要更新
    //2、调用更新服务，下载文件更新包（压缩包）
    //3、解压缩更新包文件，退出主程序，替换主程序中的文件，重启主程序

    /// <summary>
    /// 提供软件自动更新的相关方法（客户端）
    /// </summary>
    public class UpdaterClient
    {
        /// <summary>
        /// 需要包含在更新范围的文件类型
        /// <para>默认更新的文件类型包括：.dll .exe .db .pdb .txt .json</para>
        /// </summary>
        public string[] UpdateFileExtensions { get; set; } = new string[] { ".dll", ".exe", ".db", ".pdb", ".txt", ".json" };
        /// <summary>
        /// 需要进行更新的软件所在目录
        /// <para>默认是当前程序所在目录</para>
        /// </summary>
        public string ProgramDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 下载的更新包在本地的存放目录
        /// <para>默认目录：当前程序所在目录\UpdateFiles</para>
        /// </summary>
        public string UpdateFileDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "\\UpdateFiles";
        /// <summary>
        /// 下载的更新包文件名
        /// </summary>
        public string UpdatePakageName { get; set; } = "更新包" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        /// <summary>
        /// 提供软件自动更新的相关方法
        /// </summary>
        public UpdaterClient() { }
        /// <summary>
        /// 提供软件自动更新的相关方法
        /// </summary>
        /// <param name="updateFileExtensions">需要包含在更新范围的文件类型</param>
        /// <param name="programDir">需要进行更新的软件所在目录</param>
        public UpdaterClient(string[] updateFileExtensions, string programDir = null, string updateFileDir = null)
        {
            UpdateFileExtensions = updateFileExtensions;
            if (!string.IsNullOrWhiteSpace(programDir)) ProgramDir = programDir;
            if (!string.IsNullOrWhiteSpace(updateFileDir)) UpdateFileDir = updateFileDir;
        }



        /// <summary>
        /// 调用更新检查服务，判断软件是否需要更新
        /// </summary>
        /// <param name="serviceUrl">更新检查服务URL</param>
        /// <returns>表示是否需要更新</returns>
        public bool CheckUpdate(string serviceUrl)
        {
            var fileInfoColl = CreateFileInfoCollection(ProgramDir, UpdateFileExtensions);
            var json = JsonConvert.SerializeObject(fileInfoColl);
            var httpResponse = HttpHelper.Post(serviceUrl, json);
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
        /// <param name="serviceUrl">更新服务URL</param>
        /// <param name="resultMessage">更新情况信息</param>
        /// <returns>更新结果状态</returns>
        public EDownloadState DownloadUpdateFiles(string serviceUrl, out string resultMessage)
        {
            EDownloadState state;
            resultMessage = string.Empty;
            try
            {
                var fileInfoColl = CreateFileInfoCollection(ProgramDir, UpdateFileExtensions);
                var json = JsonConvert.SerializeObject(fileInfoColl);

                var httpWebResponse = HttpHelper.Post(serviceUrl, json);
                if (httpWebResponse.ContentLength == 0)
                    state = EDownloadState.NoNeedToUpdate;
                else
                {
                    HttpHelper.ResponseToSaveFile(httpWebResponse, Path.Combine(UpdateFileDir, UpdatePakageName));
                    state = EDownloadState.Dowloaded;
                }
            }
            catch (Exception ex) { resultMessage = ex.Message; state = EDownloadState.Fail; }
            resultMessage.Insert(0, state.GetDescription());
            return state;
        }
        /// <summary>
        /// 根据软件进程名关闭全部同名进程（软件），解压缩软件更新包进行软件更新，然后重启软件
        /// </summary>
        /// <param name="appName"></param>
        public void RestartForUpdate(string appName)
        {
            var processes = Process.GetProcessesByName(appName);
            if (processes.Length == 0)
                throw new Exception($"找不到进程“{appName}”");

            foreach (var process in processes)
            {
                process.Close();
                process.WaitForExit();
            }
            var path = Path.Combine(UpdateFileDir, UpdatePakageName);
            SharpZip.UnZipFile(path, ProgramDir);
            Process.Start(processes[0].StartInfo.FileName);
        }


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
            var filePaths = PathEx.GetAllFilePaths(rootDirectory, extensions);
            foreach (var filePath in filePaths)
            {
                var path = filePath.Replace(rootDirectory, "").TrimStart('\\');
                yield return new FileMd5Info(path, PathEx.GetMD5HashFromFile(filePath));
            }
        }
    }
}
