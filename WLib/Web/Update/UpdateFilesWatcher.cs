/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/6
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace WLib.Web.Update
{
    /// <summary>
    /// 服务器端存放软件更新包的目录的监测器
    /// <para>每当更新包目录文件发生变化时，将当前时间作为新的版本号记录到<see cref="VersionFilePath"/>（即version.json）文件中</para>
    /// </summary>
    public class UpdateFilesWatcher
    {
        /// <summary>
        /// 
        /// </summary>
        private Timer _timer;
        /// <summary>
        /// 
        /// </summary>
        private object _lockObject = new object();
        /// <summary>
        /// 标识文件系统版本是否需要更新
        /// </summary>
        private bool _hasUpdate = false;
        /// <summary>
        /// 被改动的文件及改动的时间
        /// </summary>
        public FileMd5InfoCollection _changedFiles = new FileMd5InfoCollection();
        /// <summary>
        /// 文件系统监测器
        /// </summary>
        private readonly FileSystemWatcher _fileSystemWatcher = new FileSystemWatcher();


        /// <summary>
        /// 监测目录
        /// </summary>
        public string WatchPath { get => _fileSystemWatcher.Path; set => _fileSystemWatcher.Path = value; }
        /// <summary>
        /// 获取或设置筛选字符串，用于确定在目录中监视哪些文件
        /// </summary>
        public string Filter { get => _fileSystemWatcher.Filter; set => _fileSystemWatcher.Filter = value; }
        /// <summary>
        /// 记录当前文件系统版本的文件
        /// </summary>
        public string VersionFilePath { get; private set; }
        /// <summary>
        /// 当前文件系统版本
        /// </summary>
        public string Version { get; private set; }


        /// <summary>
        /// 服务器端存放软件更新包的目录的监测器
        /// <para>每当更新包目录文件发生变化时，将当前时间作为新的版本号记录到<see cref="VersionFilePath"/>（即version.json）文件中</para>
        /// </summary>
        protected UpdateFilesWatcher()
        {
            _fileSystemWatcher.IncludeSubdirectories = true;//监测子目录
            _fileSystemWatcher.NotifyFilter =
                  NotifyFilters.Attributes
                | NotifyFilters.CreationTime
                | NotifyFilters.DirectoryName
                | NotifyFilters.FileName
                | NotifyFilters.LastAccess
                | NotifyFilters.LastWrite
                | NotifyFilters.Security
                | NotifyFilters.Size;
            _fileSystemWatcher.Changed += FileSystemChangedHandler;
            _fileSystemWatcher.Deleted += FileSystemChangedHandler;
            _fileSystemWatcher.Renamed += FileSystemChangedHandler;
            _fileSystemWatcher.Created += FileSystemChangedHandler;

        }
        /// <summary>
        /// 服务器端存放软件更新包的目录的监测器
        /// <para>每当更新包目录文件发生变化时，将当前时间作为新的版本号记录到<see cref="VersionFilePath"/>（即version.json）文件中</para>
        /// </summary>
        /// <param name="watchPath">监测目录</param>
        /// <param name="filter">获取或设置筛选字符串，用于确定在目录中监视哪些文件</param>
        public UpdateFilesWatcher(string watchPath, string versionFilePath = null, string filter = "*.*") : this()
        {
            this.WatchPath = watchPath;
            this.VersionFilePath = versionFilePath ?? Path.Combine(watchPath, "version.json");
            this.Filter = filter;
        }


        /// <summary>
        /// 启动文件监测器
        /// </summary>
        public void Start()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
            _timer = new Timer(state =>
            {
                lock (_lockObject)
                {
                    if (_hasUpdate)
                    {
                        _fileSystemWatcher.EnableRaisingEvents = false;
                        Version = DateTime.Now.ToString("yyyyMMddHHmmss");
                        File.WriteAllText(VersionFilePath, Version);
                        _fileSystemWatcher.EnableRaisingEvents = true;
                        _hasUpdate = false;
                    }
                }
            }, null, 0, 2000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSystemChangedHandler(object sender, FileSystemEventArgs e)
        {
            lock (_lockObject)
            {
                _hasUpdate = true;
                var info = _changedFiles.FileInfos.FirstOrDefault(v => v.Path == e.FullPath);
                if (info == null)
                    _changedFiles.FileInfos.Add(new FileMd5Info(e.FullPath, null, DateTime.Now, e.ChangeType));
                else
                    info.UpdateStates.Add(DateTime.Now, e.ChangeType);
            }
        }
    }
}
