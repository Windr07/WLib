using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// 菜单操作
    /// </summary>
    public class MenuOpt
    {
        /// <summary>
        /// 创建一个新的菜单项（ToolStripItem）
        /// </summary>
        /// <param name="text">菜单项显示的文本</param>
        /// <param name="key">菜单项快捷键，与Ctrl键组合使用</param>
        /// <param name="onClick">菜单项的点击事件</param>
        /// <param name="visible">菜单项是否显示</param>
        /// <returns></returns>
        public static ToolStripItem NewMenuItem(string text, Keys key, EventHandler onClick, bool visible = false)
        {
            return new ToolStripMenuItem(text, null, onClick) { ShortcutKeys = Keys.Control | key, Visible = visible };
        }
    }
}
