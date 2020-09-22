/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/10
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;

namespace WLib.ExtProgress
{
    /// <summary>
    /// 日志信息分组
    /// </summary>
    public class ProLogGroup : IEquatable<ProLogGroup>
    {
        /// <summary>
        /// 分组值
        /// <para>0-进度信息</para>
        /// <para>1-调试信息</para>
        /// <para>2-警告信息</para>
        /// <para>3-异常信息</para>
        /// </summary>
        public byte Group { get; set; }
        /// <summary>
        /// 根据分组显示信息类型
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (Group)
            {
                case 0: return "进度信息";
                case 1: return "调试信息";
                case 2: return "警告信息";
                case 3: return "异常信息";
                default: return Group.ToString();
            }
        }
        /// <summary>
        /// 比较两个分组是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ProLogGroup other) => Group == other.Group;

        /// <summary>
        /// 进度信息组
        /// </summary>
        public static readonly ProLogGroup Info = new ProLogGroup { Group = 0 };
        /// <summary>
        /// 调试信息组
        /// </summary>
        public static readonly ProLogGroup Debug = new ProLogGroup { Group = 1 };
        /// <summary>
        /// 警告信息组
        /// </summary>
        public static readonly ProLogGroup Warnning = new ProLogGroup { Group = 2 };
        /// <summary>
        /// 错误信息组
        /// </summary>
        public static readonly ProLogGroup Error = new ProLogGroup { Group = 3 };
    }
}
