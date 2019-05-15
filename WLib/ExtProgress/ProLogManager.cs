/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/2/23 10:35:03
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 对YYGISLib.Progress.ProgressOperation实例生成的操作信息的日志管理
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
        /// 对YYGISLib.Progress.ProgressOperation实例生成的操作信息的日志管理，日志默认存放在“程序目录\Log”目录下
        /// </summary>
        /// <param name="logDirctory">日志保存目录，值为null时使用“程序所在目录\\Log”</param>
        /// <param name="logPrefix">日志文件名的前缀</param>
        public ProLogManager(string logDirctory = null, string logPrefix = "Log")
        {
            this.LogDirctory = string.IsNullOrEmpty(logDirctory) ? AppDomain.CurrentDomain.BaseDirectory + "Log" : logDirctory;
            this.LogPrefix = logPrefix;
        }

        /// <summary>
        /// 生成日志文件，将处理信息写入日志文件
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="errMsg">异常信息</param>
        public void WriteLog(ProgressOperation opt, string errMsg = "")
        {
            StringBuilder sb = new StringBuilder();
            AssemblyName assemblyName = Assembly.GetEntryAssembly().GetName();
            sb.AppendLine("功能名称：" + opt.Name);
            sb.AppendLine("功能描述：" + opt.Description);
            sb.AppendLine("模块代码：" + opt.GetType().Name);
            sb.AppendLine("输出目录：" + opt.OutputDirectory);
            sb.AppendLine("输出对象：" + opt.OutputName);
            sb.AppendLine("开始时间：" + opt.StartTime);
            sb.AppendLine("完成时间：" + opt.EndTime);
            sb.AppendLine("程序名称：" + assemblyName.Name);
            sb.AppendLine("程序版本：" + assemblyName.Version);
            sb.AppendLine("操作异常：" + errMsg);

            sb.AppendLine("\r\n\r\n\r\n---------------------------------------结果信息---------------------------------------");
            sb.AppendLine(opt.ResultInfo);
            sb.AppendLine("\r\n\r\n\r\n---------------------------------------进程信息---------------------------------------");
            GetMessages(opt, sb);

            WriteLog(sb.ToString());
        }
        /// <summary>
        /// 递归获取
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="sb"></param>
        private void GetMessages(ProgressOperation opt, StringBuilder sb)
        {
            sb.AppendLine(opt.Messages);
            opt.SubProgressOperations.ForEach(subOpt => GetMessages(subOpt, sb));
        }
        /// <summary>
        /// 生成日志文件，将处理信息写入日志文件
        /// </summary>
        /// <param name="msg">处理信息</param>
        public void WriteLog(string msg)
        {
            if (!System.IO.Directory.Exists(LogDirctory))
                System.IO.Directory.CreateDirectory(LogDirctory);

            string logFilePath = System.IO.Path.Combine(LogDirctory, LogPrefix + System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
            System.IO.File.WriteAllText(logFilePath, msg);
        }
        /// <summary>
        /// 获取所有日志的关键信息及所在路径
        /// </summary>
        /// <returns></returns>
        public ProLogFileInfo[] GetAllLogFilesInfo()
        {
            List<ProLogFileInfo> logFileInfoList = new List<ProLogFileInfo>();
            if (System.IO.Directory.Exists(LogDirctory))
            {
                var logPaths = System.IO.Directory.GetFiles(LogDirctory, LogPrefix + "*.txt");
                foreach (var path in logPaths)
                {
                    string time = System.IO.Path.GetFileNameWithoutExtension(path).Replace(LogPrefix, "");
                    StreamReader sr = new StreamReader(path);
                    string firstLine = sr.ReadLine();
                    firstLine = firstLine.Replace("功能描述：", "").Replace("功能名称：", "");
                    sr.Close();

                    logFileInfoList.Add(new ProLogFileInfo(time, firstLine, path));
                }
            }
            logFileInfoList = logFileInfoList.OrderByDescending(v => v.TimeString).ToList();
            return logFileInfoList.ToArray();
        }
        /// <summary>
        /// 获取最近生成的日志文件
        /// （日志目录或文件不存在则返回null）
        /// </summary>
        public string GetRecentlyLogFilePath()
        {
            string resultPath = null;
            if (System.IO.Directory.Exists(LogDirctory))
            {
                DateTime dt;
                List<DateTime> times = new List<DateTime>();
                var logPaths = System.IO.Directory.GetFiles(LogDirctory, LogPrefix + "*.txt");
                foreach (var path in logPaths)
                {
                    var strTime = System.IO.Path.GetFileNameWithoutExtension(path).Replace(LogPrefix, "").Replace("：", ":");
                    if (DateTime.TryParse(strTime, out dt))
                        times.Add(dt);
                }
                if (times.Count > 0)
                {
                    var t = times.Max();
                    resultPath = System.IO.Path.Combine(LogDirctory, LogPrefix + t.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
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
            if (logPath != null && System.IO.File.Exists(logPath))
                System.Diagnostics.Process.Start("Explorer.exe", "/select,\"" + logPath + "\"");
            return logPath;
        }
        /// <summary>
        /// 打开日志目录
        /// </summary>
        /// <returns></returns>
        public string OpenLogDirectory()
        {
            if (System.IO.Directory.Exists(LogDirctory))
                System.Diagnostics.Process.Start(LogDirctory);
            return LogDirctory;
        }
        /// <summary>
        /// 清空日志文件
        /// </summary>
        public void ClearLogFiles()
        {
            if (System.IO.Directory.Exists(LogDirctory))
            {
                System.IO.Directory.Delete(LogDirctory, true);
                System.IO.Directory.CreateDirectory(LogDirctory);
            }
        }
    }
}
