using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WLib.WinCtrls.MessageCtrl
{
    /// <summary>
    /// 用于替换<see cref="MessageBox"/>.Show相关方法，弹出新的消息窗口
    /// </summary>
    public static class MessageBoxEx
    {
        /// <summary>
        /// 软件意见反馈的联系方式
        /// </summary>
        private static List<ContactInfo> Contacts { get; } = new List<ContactInfo>();
        /// <summary>
        /// 点击帮助按钮时触发的操作
        /// </summary>
        private static Action<Exception> HelpAction { get; set; }
        
        

        /// <summary>
        /// 弹出错误/异常消息框
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="suggestionActions">处理建议及点击建议对应的跳转操作</param>
        public static void ShowError(Exception ex, Dictionary<string, Action> suggestionActions)
        {
            new ErrorHandlerBox(ex, suggestionActions, Contacts, HelpAction).ShowDialog();
        }
        /// <summary>
        /// 弹出错误/异常消息框
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="suggestion">针对异常的处理建议信息</param>
        /// <param name="suggestionAction">点击建议信息对应的跳转操作</param>
        public static void ShowError(Exception ex, string suggestion = null, Action suggestionAction = null)
        {
            if (string.IsNullOrWhiteSpace(suggestion))
                new ErrorHandlerBox(ex, string.Empty, Contacts, HelpAction).ShowDialog();
            else
                new ErrorHandlerBox(ex, new Dictionary<string, Action> { { suggestion, suggestionAction } }, Contacts, HelpAction).ShowDialog();
        }
        /// <summary>
        /// 弹出普通的消息提示框
        /// <para>该提示框默认会在显示若干秒之后关闭</para>
        /// </summary>
        /// <param name="owner">消息提示框所属控件/窗体</param>
        /// <param name="info">提示消息</param>
        /// <param name="closeSecond">总共多少秒后自动关闭消息提示框，值小于等于0则不会自动关闭</param>
        public static void ShowInfo(Control owner, string info, int closeSecond = 3)
        {
            new InfoTipBox(info, owner.Text, closeSecond).Show(owner);
        }
    }
}
