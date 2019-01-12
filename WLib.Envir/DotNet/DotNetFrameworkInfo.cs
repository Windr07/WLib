/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016/12/16 16:51:46
// desc： None
// mdfy:  None
// ver.:  V1.0.0
//----------------------------------------------------------------*/

namespace WLib.Envir.DotNet
{
    /// <summary>
    /// .NET Framework版本信息
    /// </summary>
    public class DotNetFrameworkInfo
    {
        /// <summary>
        /// 名称（eg:v2.0）
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本（eg:2.0.50727.4927）
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 安装路径
        /// </summary>
        public string InstallPath { get; set; }
        /// <summary>
        /// 补丁版本（eg:1）
        /// </summary>
        public string Sp { get; set; }
        /// <summary>
        /// .NET Framework版本信息
        /// </summary>
        /// <param name="name">名称（eg:v2.0）</param>
        /// <param name="version">版本（eg:2.0.50727.4927）</param>
        /// <param name="installPath">安装路径</param>
        /// <param name="sp">补丁版本（eg:1）</param>
        public DotNetFrameworkInfo(string name, string version, string installPath, string sp)
        {
            this.Name = name;
            this.Version = version;
            this.InstallPath = installPath;
            this.Sp = sp;
        }
        /// <summary>
        /// 输出版本信息（eg v2.0 SP1）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strSp = string.IsNullOrEmpty(Sp) ? "" : "sp" + Sp;
            return $"{Name} {strSp}";
        }
    }
}
