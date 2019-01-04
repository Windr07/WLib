/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.Envir.DotNet.CheckDotNetEx
{
    /// <summary>
    /// 运行时类型
    /// </summary>
    public enum RuntimeType
    {
        /// <summary>
        /// 任意 runtime framework
        /// </summary>
        Any,   
        /// <summary>
        /// Microsoft .NET Framework
        /// </summary>
        Net, 
        /// <summary>
        /// Microsoft .NET Compact Framework
        /// </summary>
        NetCF, 
        /// <summary>
        /// Microsoft Shared Source CLI
        /// </summary>
        SSCLI, 
        /// <summary>
        /// Mono
        /// </summary>
        Mono, 
    }
}