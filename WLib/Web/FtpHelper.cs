/*---------------------------------------------------------------- 
// auth： Unknown
// date： 2019/05/10
// desc： 参考来源：https://www.cnblogs.com/skynetfy/p/3340125.html
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace WLib.Web
{
    /*  FTP全状态码查询词典
     
     1xx - 肯定的初步答复
     这些状态代码指示一项操作已经成功开始，但客户端希望在继续操作新命令前得到另一个答复。 • 110 重新启动标记答复。 
     • 120 服务已就绪，在 nnn 分钟后开始。 
     • 125 数据连接已打开，正在开始传输。 
     • 150 文件状态正常，准备打开数据连接。 
     
     2xx - 肯定的完成答复
     一项操作已经成功完成。客户端可以执行新命令。 • 200 命令确定。 
     • 202 未执行命令，站点上的命令过多。 
     • 211 系统状态，或系统帮助答复。 
     • 212 目录状态。 
     • 213 文件状态。 
     • 214 帮助消息。 
     • 215 NAME 系统类型，其中，NAME 是 Assigned Numbers 文档中所列的正式系统名称。 
     • 220 服务就绪，可以执行新用户的请求。 
     • 221 服务关闭控制连接。如果适当，请注销。 
     • 225 数据连接打开，没有进行中的传输。 
     • 226 关闭数据连接。请求的文件操作已成功（例如，传输文件或放弃文件）。 
     • 227 进入被动模式 (h1,h2,h3,h4,p1,p2)。 
     • 230 用户已登录，继续进行。 
     • 250 请求的文件操作正确，已完成。 
     • 257 已创建“PATHNAME”。 
     
     3xx - 肯定的中间答复
     该命令已成功，但服务器需要更多来自客户端的信息以完成对请求的处理。 • 331 用户名正确，需要密码。 
     • 332 需要登录帐户。 
     • 350 请求的文件操作正在等待进一步的信息。 
     
     4xx - 瞬态否定的完成答复
     该命令不成功，但错误是暂时的。如果客户端重试命令，可能会执行成功。 • 421 服务不可用，正在关闭控制连接。如果服务确定它必须关闭，将向任何命令发送这一应答。 
     • 425 无法打开数据连接。 
     • 426 Connection closed; transfer aborted. 
     • 450 未执行请求的文件操作。文件不可用（例如，文件繁忙）。 
     • 451 请求的操作异常终止：正在处理本地错误。 
     • 452 未执行请求的操作。系统存储空间不够。 
     
     5xx - 永久性否定的完成答复
     该命令不成功，错误是永久性的。如果客户端重试命令，将再次出现同样的错误。 • 500 语法错误，命令无法识别。这可能包括诸如命令行太长之类的错误。 
     • 501 在参数中有语法错误。 
     • 502 未执行命令。 
     • 503 错误的命令序列。 
     • 504 未执行该参数的命令。 
     • 530 未登录。 
     • 532 存储文件需要帐户。 
     • 550 未执行请求的操作。文件不可用（例如，未找到文件，没有访问权限）。 
     • 551 请求的操作异常终止：未知的页面类型。 
     • 552 请求的文件操作异常终止：超出存储分配（对于当前目录或数据集）。 
     • 553 未执行请求的操作。不允许的文件名。 
     常见的 FTP 状态代码及其原因
     • 150 - FTP 使用两个端口：21 用于发送命令，20 用于发送数据。状态代码 150 表示服务器准备在端口 20 上打开新连接，发送一些数据。 
     • 226 - 命令在端口 20 上打开数据连接以执行操作，如传输文件。该操作成功完成，数据连接已关闭。 
     • 230 - 客户端发送正确的密码后，显示该状态代码。它表示用户已成功登录。 
     • 331 - 客户端发送用户名后，显示该状态代码。无论所提供的用户名是否为系统中的有效帐户，都将显示该状态代码。 
     • 426 - 命令打开数据连接以执行操作，但该操作已被取消，数据连接已关闭。 
     • 530 - 该状态代码表示用户无法登录，因为用户名和密码组合无效。如果使用某个用户帐户登录，可能键入错误的用户名或密码，也可能选择只允许匿名访问。如果使用匿名帐户登录，IIS 的配置可能拒绝匿名访问。 
     • 550 - 命令未被执行，因为指定的文件不可用。例如，要 GET 的文件并不存在，或试图将文件 PUT 到您没有写入权限的目录。
    */

    /// <summary>
    /// 提供FTP连接、查看、上传、下载、删除文件等方法
    /// </summary>
    public class FtpHelper
    {
        /// <summary>
        /// 远端路径
        /// </summary>
        private string _remotePath = "/";
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserId { get; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; } = string.Empty;
        /// <summary>
        /// 代理
        /// </summary>
        public IWebProxy Proxy { get; set; } = null;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 21;
        /// <summary>
        /// 设置是否允许Ssl 
        /// </summary>
        public bool EnableSsl { get; } = false;
        /// <summary>
        /// 是否使用被动模式
        /// </summary>
        public bool UsePassive { get; set; } = true;
        /// <summary>
        /// 二进制方式
        /// </summary>
        public bool UseBinary { get; set; } = true;
        /// <summary>
        /// 远端路径
        /// <para>
        ///     返回FTP服务器上的当前路径(可以是 / 或 /a/../ 的形式)
        /// </para>
        /// </summary>
        public string RemotePath
        {
            get => _remotePath;
            set
            {
                string result = "/";
                if (!string.IsNullOrEmpty(value) && value != "/")
                    result = "/" + value.TrimStart('/').TrimEnd('/') + "/";
                this._remotePath = result;
            }
        }
        /// <summary>
        /// 获取FTP服务器上的当前路径
        /// </summary>
        public string CurrentDirectory
        {
            get
            {
                string result = string.Empty;
                string url = Host.TrimEnd('/') + RemotePath;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.PrintWorkingDirectory);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    string temp = response.StatusDescription;
                    int start = temp.IndexOf('"') + 1;
                    int end = temp.LastIndexOf('"');
                    if (end >= start)
                    {
                        result = temp.Substring(start, end - start);
                    }
                }
                return result;

            }
        }
        /// <summary>
        /// 传输进度事件
        /// </summary>
        public event EventHandler<TransferProgressEventArgs> TransferProgressChanged;


        /// <summary>
        /// 创建FTP工具
        /// <para>
        /// 默认不使用SSL,使用二进制传输方式,使用被动模式
        /// </para>
        /// </summary>
        /// <param name="host">主机名称，例如：localhost、127.0.0.1</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        public FtpHelper(string host, string userId, string password)
            : this(host, userId, password, 21, null, false, true, true)
        {
        }
        /// <summary>
        /// 创建FTP工具
        /// </summary>
        /// <param name="host">主机名称，例如：localhost、127.0.0.1</param>
        /// <param name="userId">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="port">端口，一般为21</param>
        /// <param name="enableSsl">是否允许Ssl</param>
        /// <param name="proxy">代理</param>
        /// <param name="useBinary">是否允许二进制</param>
        /// <param name="usePassive">是否允许被动模式</param>
        public FtpHelper(string host, string userId, string password, int port, IWebProxy proxy, bool enableSsl, bool useBinary, bool usePassive)
        {
            this.UserId = userId;
            this.Password = password;
            this.Host = host.ToLower().StartsWith("ftp://") ? host : "ftp://" + host;
            this.Port = port;
            this.Proxy = proxy;
            this.EnableSsl = enableSsl;
            this.UseBinary = useBinary;
            this.UsePassive = usePassive;
        }


        /// <summary>
        /// 把文件上传到FTP服务器的RemotePath下
        /// </summary>
        /// <param name="localFile">本地文件路径</param>
        /// <param name="remoteFileName">要保存到FTP文件服务器上的文件的名称</param>
        public bool Upload(string localFilePath, string remoteFileName)
        {
            var result = false;
            var fileInfo = new FileInfo(localFilePath);
            if (!fileInfo.Exists)
                throw new Exception($"本地文件不存在,文件路径:{fileInfo.FullName}");

            var url = Host.TrimEnd('/') + RemotePath + remoteFileName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);

            using (Stream stream = ftpWebRequest.GetRequestStream())//上传数据
            using (FileStream fileStream = fileInfo.OpenRead())
            {
                long length = fileStream.Length;//文件总大小
                long uploadLength = 0L;          //已上传文件大小
                byte[] buffer = new byte[4096]; //每次上传大小4KB
                int contentLength;
                while ((contentLength = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, contentLength);
                    uploadLength += contentLength;
                    OnTransferProgressChanged(uploadLength, length);
                }
                fileStream.Close();
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 从当前目录下下载文件
        /// <para>
        /// 如果本地文件存在，则从本地文件结束的位置开始下载.
        /// </para>
        /// </summary>
        /// <param name="serverName">服务器上的文件名称</param>
        /// <param name="localFilePath">本地文件保存路径</param>
        /// <returns>返回一个值,指示是否下载成功</returns>
        public bool Download(string serverName, string localFilePath)
        {
            var result = false;
            using (FileStream fileStream = new FileStream(localFilePath, FileMode.OpenOrCreate)) //创建或打开本地文件
            {
                var url = Host.TrimEnd('/') + RemotePath + serverName;
                var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile);
                ftpWebRequest.ContentOffset = fileStream.Length;
                using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
                {
                    fileStream.Position = fileStream.Length;
                    byte[] buffer = new byte[4096];//4K
                    int contentLength;
                    using (Stream ftpStream = response.GetResponseStream())
                    {
                        while ((contentLength = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, contentLength);
                        }
                    }
                }
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 重命名FTP服务器上的文件
        /// </summary>
        /// <param name="oldFileName">原文件名</param>
        /// <param name="newFileName">新文件名</param>
        /// <returns>返回一个值,指示更名是否成功</returns>
        public bool Rename(string oldFileName, string newFileName)
        {
            var result = false;
            var url = Host.TrimEnd('/') + RemotePath + oldFileName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.Rename);
            ftpWebRequest.RenameTo = newFileName;
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 获取当前目录下文件列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList()
        {
            var results = new List<string>();
            var url = Host.TrimEnd('/') + RemotePath;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.ListDirectory);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                var line = reader.ReadLine();
                while (line != null)
                {
                    results.Add(line);
                    line = reader.ReadLine();
                }
            }
            return results;
        }
        /// <summary>
        /// 从FTP服务器上获取文件和文件夹列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileDetails()
        {
            var results = new List<string>();
            var url = Host.TrimEnd('/') + RemotePath;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.ListDirectoryDetails);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                var line = reader.ReadLine();
                while (line != null)
                {
                    results.Add(line);
                    line = reader.ReadLine();
                }
            }
            return results;
        }
        /// <summary>
        /// 删除FTP服务器上的文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteFile(string fileName)
        {
            var result = false;
            var url = Host.TrimEnd('/') + RemotePath + fileName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.DeleteFile);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                result = true;
            }

            return result;
        }
        /// <summary>
        /// 在当前目录下创建文件夹（在FTP服务器上创建目录）
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否创建成功</returns>
        public bool MakeDirectory(string dirName)
        {
            var result = false;
            var url = Host.TrimEnd('/') + RemotePath + dirName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.MakeDirectory);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 从FTP服务器上删除目录
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteDirectory(string dirName)
        {
            var result = false;
            var url = Host.TrimEnd('/') + RemotePath + dirName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.RemoveDirectory);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 从FTP服务器上获取文件大小
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public long GetFileSize(string fileName)
        {
            var result = 0L;
            var url = Host.TrimEnd('/') + RemotePath + fileName;
            var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.GetFileSize);
            using (FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse())
            {
                result = response.ContentLength;
            }

            return result;
        }
        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="filePath">本地文件</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(string filePath, string remoteFileName)
        {
            if (!new FileInfo(filePath).Exists)
                throw new Exception(string.Format("本地文件不存在,文件路径:{0}", filePath));

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return Append(fs, remoteFileName);
            }
        }
        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="stream">数据流(可通过设置偏移来实现从特定位置开始上传)</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(Stream stream, string remoteFileName)
        {
            var result = false;
            if (stream != null && stream.CanRead)
            {
                var url = Host.TrimEnd('/') + RemotePath + remoteFileName;
                var ftpWebRequest = CreateRequest(url, WebRequestMethods.Ftp.AppendFile);
                using (Stream rs = ftpWebRequest.GetRequestStream())
                {
                    //上传数据
                    byte[] buffer = new byte[4096];//4K
                    int count = stream.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        rs.Write(buffer, 0, count);
                        count = stream.Read(buffer, 0, buffer.Length);
                    }
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 检查当前路径上是否存在某个文件
        /// </summary>
        /// <param name="fileName">要检查的文件名</param>
        /// <returns>返回一个值,指示要检查的文件是否存在</returns>
        public bool CheckFileExist(string fileName)
        {
            var result = false;
            if (fileName != null && fileName.Trim().Length > 0)
            {
                fileName = fileName.Trim();
                List<string> files = GetFileList();
                if (files != null && files.Count > 0)
                {
                    foreach (string file in files)
                    {
                        if (file.ToLower() == fileName.ToLower())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 创建一个FTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        /// <returns>FTP请求</returns>
        private FtpWebRequest CreateRequest(string url, string method)
        {
            //建立连接
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(this.UserId, this.Password);
            request.Proxy = this.Proxy;
            request.KeepAlive = false;//命令执行完毕之后关闭连接
            request.UseBinary = UseBinary;
            request.UsePassive = UsePassive;
            request.EnableSsl = EnableSsl;
            request.Method = method;
            return request;
        }
        /// <summary>
        /// 触发传输进度事件
        /// </summary>
        /// <param name="currentValue">当前进度值</param>
        /// <param name="totalValue">总进度值</param>
        protected void OnTransferProgressChanged(long currentValue, long totalValue)
        {
            TransferProgressChanged?.Invoke(this, new TransferProgressEventArgs(currentValue, totalValue));
        }
    }
}
