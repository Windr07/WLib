/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Runtime.InteropServices;

namespace WLib.ArcGis
{
    /// <summary>
    /// 提供对COM组件对象进行管理的方法
    /// </summary>
    public static class ComObjManager
    {
        /// <summary>
        /// 释放COM组件对象
        /// </summary>
        /// <param name="comObjects">多个COM组件对象</param>
        public static void Realease(params object[] comObjects)
        {
            foreach (var obj in comObjects)
            {
                if (obj == null) continue;

                int count = 0;
                while ((count = Marshal.ReleaseComObject(obj)) > 0)
                {
                }
            }
        }
    }
}
