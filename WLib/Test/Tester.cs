/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Test
{
    /// <summary>
    /// 用于记录输出测试信息的测试类（单例模式）
    /// </summary>
    public sealed class Tester
    {
        /// <summary>
        /// 所有的测试信息
        /// </summary>
        private readonly List<string> _allMessage;
        /// <summary>
        /// 所有的测试信息
        /// </summary>
        public string[] AllMessage => _allMessage.ToArray();
        /// <summary>
        /// 测试信息保存目录
        /// </summary>
        public string LogDir { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Log";
        /// <summary>
        /// 测试信息是否包含时间，默认True
        /// </summary>
        public bool WithTime { get; set; }
        /// <summary>
        /// 当前要记录的信息
        /// </summary>
        public string CurMsg { set => _allMessage.Add(WithTime ? value + "\t" + DateTime.Now : value); }


        /// <summary>
        /// 当前类的一个单例
        /// </summary>
        private static volatile Tester _instance;
        /// <summary>
        /// 
        /// </summary>
        private static readonly object Obj = new object();
        /// <summary>
        /// 用于记录输出测试信息的测试类
        /// </summary>
        public static Tester Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (Obj)
                    {
                        if (null == _instance)
                            _instance = new Tester();
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 用于记录输出测试信息的测试类（单例模式）
        /// </summary>
        /// <param name="withTime"></param>
        private Tester(bool withTime = true)
        {
            _allMessage = new List<string>();
            WithTime = withTime;
        }


        /// <summary>
        /// 在程序目录下创建Log文件夹，将测试信息写入Log文件夹的txt文件中
        /// </summary>
        public void Write()
        {
            Write(LogDir);
        }
        /// <summary>
        /// 将测试信息写入指定目录中
        /// </summary>
        /// <param name="dir"></param>
        public void Write(string dir)
        {
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);

            var logPath = System.IO.Path.Combine(dir, "log" + DateTime.Now.ToString("yyyyMMdd hh-mm-ss") + ".txt");
            System.IO.File.WriteAllText(logPath, _allMessage.Aggregate((a, b) => a + Environment.NewLine + b));
        }
        /// <summary>
        /// 弹窗显示测试信息
        /// </summary>
        public void ShowToMessageBox()
        {
            System.Windows.Forms.MessageBox.Show(_allMessage.Aggregate((a, b) => a + Environment.NewLine + b), "测试信息");
        }
    }
}
