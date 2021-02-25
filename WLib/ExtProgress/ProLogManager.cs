/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/10
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WLib.Database;
using WLib.ExtProgress.Core;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 日志信息管理，提供将<see cref="IProgressOperation"/>记录的进度信息写入日志文件等方法
    /// </summary>
    public static class ProLogManager
    {
        /// <summary>
        /// 生成日志文件，将处理信息写入日志文件
        /// <para>日志文件格式示例：Log2019-10-11 10：01：21.txt</para>
        /// </summary>
        /// <param name="logDirectory">日志保存目录，值为null或空白时自动赋值为：[软件生成目录]\Log</param>
        /// <param name="logPrefix">日志文件名的前缀</param>
        public static void WriteLogFile(this IProgressOperation opt, string logDirectory = null, string logPrefix = "Log")
        {
            var Msgs = opt.Msgs;
            var sb = new StringBuilder();
            sb.AppendLine("操作ID：" + Msgs.Id);
            sb.AppendLine("功能名称：" + Msgs.Name);
            sb.AppendLine("功能描述：" + Msgs.Description);
            sb.AppendLine("模块代码：" + Msgs.Code);
            sb.AppendLine("开始时间：" + Msgs.StartTime);
            sb.AppendLine("完成时间：" + Msgs.EndTime);
            sb.AppendLine("程序名称：" + Msgs.AssemblyName);
            sb.AppendLine("程序版本：" + Msgs.AssemblyVersion);
            sb.AppendLine("操作异常：" + Msgs.Error);
            sb.AppendLine("\r\n---------------------------------------进程信息---------------------------------------");
            opt.WriteOptMessages(sb);

            logDirectory = string.IsNullOrWhiteSpace(logDirectory) ? AppDomain.CurrentDomain.BaseDirectory + "Log" : logDirectory;
            Directory.CreateDirectory(logDirectory);
            string logFilePath = Path.Combine(logDirectory, logPrefix + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
            File.WriteAllText(logFilePath, sb.ToString());
        }
        /// <summary>
        /// 将进度信息写入到<see cref="StringBuilder"/>当中
        /// </summary>
        /// <param name="sbMessage"></param>
        /// <param name="opt"></param>
        private static void WriteOptMessages(this IProgressOperation opt, StringBuilder sbMessage)
        {
            sbMessage.AppendLine($"【{opt.Name}】");
            sbMessage.AppendLine(opt.Msgs.GetAllMessage());
            foreach (var subOpt in opt.SubProgressOperations)
                WriteOptMessages(subOpt, sbMessage);
        }
        /// <summary>
        /// 将进度信息写入数据库中
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="tableName"></param>
        public static void WriteLogDb(this IProgressOperation opt, DbHelper dbHelper, string tableName)
        {
            //var properties = opt.Msgs.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(v => v.CanWrite && v.CanWrite).ToArray();
            //var propertyName = properties.Select(v => v.Name);
            //var fields = propertyName.Aggregate((a, b) => a + "," + b);
            //var values = properties.Select(v => v.GetValue(opt, null).ToString());
            var m = opt.Msgs;
            var fields = $"{nameof(m.Id)},{nameof(m.Name)},{nameof(m.Description)},{nameof(m.Code)},{nameof(m.StartTime)},{nameof(m.EndTime)},{nameof(m.AssemblyName)},{nameof(m.AssemblyVersion)},{nameof(m.Error)},{nameof(m.AllMessage)}";
            var values = $"{m.Id},'{m.Name}','{m.Description}','{m.Code}','{m.StartTime}','{m.EndTime}','{m.AssemblyName}','{m.AssemblyVersion}','{m.Error}','{m.AllMessage}'";
            var sql = $"insert into {tableName} ({fields}) values ({values})";
            dbHelper.ExcNonQuery(sql);
        }
        /// <summary>
        /// 获取最近生成的日志文件
        /// （日志目录或文件不存在则返回null）
        /// </summary>
        public static string GetRecentlyLogFilePath(string logDirctory, string logPrefix = "Log")
        {
            string resultPath = null;
            if (Directory.Exists(logDirctory))
            {
                List<DateTime> times = new List<DateTime>();
                var logPaths = Directory.GetFiles(logDirctory, logPrefix + "*.txt");
                foreach (var path in logPaths)
                {
                    var strTime = Path.GetFileNameWithoutExtension(path).Replace(logPrefix, "").Replace("：", ":");
                    if (DateTime.TryParse(strTime, out var dateTime))
                        times.Add(dateTime);
                }
                if (times.Count > 0)
                {
                    var lastTime = times.Max();
                    resultPath = Path.Combine(logDirctory, logPrefix + lastTime.ToString("yyyy-MM-dd HH：mm：ss") + ".txt");
                }
            }
            return resultPath;
        }
        /// <summary>
        ///  定位到最近生成的日志文件
        ///  （日志目录或文件不存在则不进行任何操作）
        /// </summary>
        /// <returns></returns>
        public static string LocateRecentlyLogFile(string logDirctory, string logPrefix = "Log")
        {
            string logPath = GetRecentlyLogFilePath(logDirctory, logPrefix);
            if (logPath != null && File.Exists(logPath))
                System.Diagnostics.Process.Start("Explorer.exe", "/select,\"" + logPath + "\"");
            return logPath;
        }
        /// <summary>
        /// 清空日志文件
        /// </summary>
        public static void ClearLogFiles(string logDirctory)
        {
            Directory.Delete(logDirctory, true);
            Directory.CreateDirectory(logDirctory);
        }
    }
}
