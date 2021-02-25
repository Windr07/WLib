/*---------------------------------------------------------------- 
// auth： YSMC
// date： 2020/06/15
// desc： Source by AutoUpdaterClass.AppCode.HttpRequestApiHelper
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System.IO;
using System.Net;
using System.Text;

namespace WLib.Web
{
    /// <summary>
    /// Http帮助类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout">超时等待时间（毫秒）</param>
        /// <returns></returns>
        public static string Get(string url, int timeout = 10000)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = timeout;

            var response = (HttpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            var streamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            var resString = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            return resString;
        }
        /// <summary>
        /// Post(json对象)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="json"></param>
        /// <param name="timeout">超时等待时间（毫秒）</param>
        public static HttpWebResponse Post(string url, string json, int timeout = 10000)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = timeout;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return httpResponse;
        }
        /// <summary>
        /// 保存http请求结果
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="savePath"></param>
        public static void ResponseToSaveFile(HttpWebResponse httpWebResponse, string savePath)
        {
            using (var stream = httpWebResponse.GetResponseStream())
            {
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite);
                var bytes = new byte[1024];
                int size = stream.Read(bytes, 0, bytes.Length);
                while (size > 0)
                {
                    fileStream.Write(bytes, 0, size);
                    size = stream.Read(bytes, 0, bytes.Length);
                }
                fileStream.Close();
            }
        }
        /// <summary>
        /// 下载文件（以流形式）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="saveDirectory"></param>
        /// <param name="fileName"></param>
        public static void DownloadFile(string url, string saveDirectory, string fileName)
        {
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            var savePath = Path.Combine(saveDirectory, fileName);
            new WebClient().DownloadFile(url, savePath);
        }
        /// <summary>
        /// 下载文件（以流形式）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savePath"></param>
        public static void DownloadFile(string url, string savePath) =>
            DownloadFile(url, Path.GetDirectoryName(savePath), Path.GetFileName(savePath));
    }
}
