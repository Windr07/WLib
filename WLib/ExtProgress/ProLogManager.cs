/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/2/23 10:35:03
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WLib.ExtProgress.Core;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 进度信息的日志管理
    /// </summary>
    public class ProLogManager
    {
        /// <summary>
        /// 日志文件名的前缀（默认为"Log"）
        /// </summary>
        public string LogPrefix { get; set; }
        /// <summary>
        /// 日志保存目录
        /// </summary>
        public string LogDirctory { get; set; }
        /// <summary>
        /// 进度信息的日志管理，日志默认存放在“程序目录\Log”目录下
        /// </summary>
        /// <param name="logDirctory">日志保存目录，值为null时使用“程序所在目录\\Log”</param>
        /// <param name="logPrefix">日志文件名的前缀</param>
        public ProLogManager(string logDirctory = null, string logPrefix = "Log")
        {
            LogDirctory = string.IsNullOrEmpty(logDirctory) ? AppDomain.CurrentDomain.BaseDirectory + "Log" : logDirctory;
            LogPrefix = logPrefix;
        }


        /// <summary>
        /// 生成日志文件，将处理信息写入日志文件
        /// </summary>
        /// <param name="opt">进度信息</param>
        /// <param name="errMsg">异常信息</param>
        public void WriteLog(IProgressOperation opt, string errMsg = "")
        {
            StringBuilder sb = new StringBuilder();
            AssemblyName assemblyName = Assembly.GetEntryAssembly().GetName();
            sb.AppendLine("功能名称：" + opt.Name);
            sb.AppendLine("功能描述：" + opt.Description);
            sb.AppendLine("模块代码：" + opt.GetType().Name);
            sb.AppendLine("开始时间：" + opt.StartTime);
            sb.AppendLine("完成时间：" + opt.EndTime);
            sb.AppendLine("程序名称：" + assemblyName.Name);
            sb.AppendLine("程序版本：" + assemblyName.Version);
            sb.AppendLine("操作异常：" + errMsg);
            sb.AppendLine("\r\n\r\n\r\n---------------------------------------进程信息---------------------------------------");
            sb.AppendLine(opt.Msgs.AllMessage);

            WriteLog(sb.ToString());
        }
        /// <summary>
        /// 生成日志文件，将处理信息写入日志文件
        /// </summary>
        /// <param name="message">处理信息</param>
        public void WriteLog(string message)
        {
            Directory.CreateDirectory(LogDirctory);

            string logFilePath = Path.Combine(LogDirctory, LogPrefix + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
            File.WriteAllText(logFilePath, message);
        }
        /// <summary>
        /// 获取所有日志的关键信息及所在路径
        /// </summary>
        /// <returns></returns>
        public ProLogFileInfo[] GetAllLogFilesInfo()
        {
            var logFileInfos = new List<ProLogFileInfo>();
            if (Directory.Exists(LogDirctory))
            {
                var logPaths = Directory.GetFiles(LogDirctory, LogPrefix + "*.txt");
                foreach (var path in logPaths)
                {
                    var strTime = Path.GetFileNameWithoutExtension(path).Replace(LogPrefix, "");
                    var streamReader = new StreamReader(path);
                    var firstLine = streamReader.ReadLine();
                    firstLine = firstLine.Replace("功能描述：", "").Replace("功能名称：", "");
                    streamReader.Close();

                    logFileInfos.Add(new ProLogFileInfo(strTime, firstLine, path));
                }
            }
            logFileInfos = logFileInfos.OrderByDescending(v => v.TimeString).ToList();
            return logFileInfos.ToArray();
        }
        /// <summary>
        /// 获取最近生成的日志文件
        /// （日志目录或文件不存在则返回null）
        /// </summary>
        public string GetRecentlyLogFilePath()
        {
            string resultPath = null;
            if (Directory.Exists(LogDirctory))
            {
                List<DateTime> times = new List<DateTime>();
                var logPaths = Directory.GetFiles(LogDirctory, LogPrefix + "*.txt");
                foreach (var path in logPaths)
                {
                    var strTime = Path.GetFileNameWithoutExtension(path).Replace(LogPrefix, "").Replace("：", ":");
                    if (DateTime.TryParse(strTime, out var dateTime))
                        times.Add(dateTime);
                }
                if (times.Count > 0)
                {
                    var lastTime = times.Max();
                    resultPath = Path.Combine(LogDirctory, LogPrefix + lastTime.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
                }
            }
            return resultPath;
        }
        /// <summary>
        ///  定位到最近生成的日志文件
        ///  （日志目录或文件不存在则不进行任何操作）
        /// </summary>
        /// <returns></returns>
        public string LocateRecentlyLogFile()
        {
            string logPath = GetRecentlyLogFilePath();
            if (logPath != null && File.Exists(logPath))
                System.Diagnostics.Process.Start("Explorer.exe", "/select,\"" + logPath + "\"");
            return logPath;
        }
        /// <summary>
        /// 打开日志目录
        /// </summary>
        /// <returns></returns>
        public string OpenLogDirectory()
        {
            if (Directory.Exists(LogDirctory))
                Process.Start(LogDirctory);
            return LogDirctory;
        }
        /// <summary>
        /// 清空日志文件
        /// </summary>
        public void ClearLogFiles()
        {
            Directory.Delete(LogDirctory, true);
            Directory.CreateDirectory(LogDirctory);
        }
    }
}
