namespace WLib.Plugins.Interface
{
    /// <summary>
    /// 应用软件系统信息
    /// </summary>
    public interface ISystemInfo : IItemBase
    {
        /// <summary>
        /// 应用软件所在目录
        /// </summary>
        string AppDir { get; }
        /// <summary>
        /// 应用软件程序集路径
        /// <para>即exe文件路径</para>
        /// </summary>
        string AppPath { get; set; }
    }
}
