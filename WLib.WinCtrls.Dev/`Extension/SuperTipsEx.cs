/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.Utils;

namespace WLib.WinCtrls.Dev.Extension
{
    /// <summary>
    /// <see cref="SuperToolTip"/>控件的扩展功能
    /// </summary>
    public static class SuperTipsEx
    {
        /// <summary>
        /// 创建提示框（SuperToolTips）
        /// </summary>
        /// <param name="content">提示具体内容</param>
        /// <param name="title">提示标题</param>
        /// <param name="footer">提示页脚</param>
        /// <returns></returns>
        public static SuperToolTip CreateSuperTips(string content, string title = null, string footer = null)
        {
            var superToolTip = new SuperToolTip();
            if (!string.IsNullOrEmpty(footer))
            {
                superToolTip.Items.Add(new ToolTipTitleItem { Text = title });  //标题
                superToolTip.Items.Add(new ToolTipSeparatorItem());             //第一条分割线
            }
            superToolTip.Items.Add(new ToolTipItem { Text = content });         //内容
            if (!string.IsNullOrEmpty(footer))
            {
                superToolTip.Items.Add(new ToolTipSeparatorItem());             //第二条分割线
                superToolTip.Items.Add(new ToolTipTitleItem { Text = footer }); //页脚
            }
            return superToolTip;
        }
        /// <summary>
        /// 创建提示框（SuperToolTips）
        /// <para>使用简化的代码创建提示框，效果与<see cref="CreateSuperTips"/>一样</para>
        /// </summary>
        /// <param name="content">提示具体内容</param>
        /// <param name="title">提示标题</param>
        /// <param name="footer">提示页脚</param>
        /// <returns></returns>
        public static SuperToolTip CreateSuperTipsSimple(string content, string title = null, string footer = null)
        {
            var superToolTip = new SuperToolTip();
            if (!string.IsNullOrEmpty(footer))
            {
                superToolTip.Items.AddTitle(title);     //标题
                superToolTip.Items.AddSeparator();      //第一条分割线
            }
            superToolTip.Items.Add(content);            //内容
            if (!string.IsNullOrEmpty(footer))
            {
                superToolTip.Items.AddSeparator();      //第二条分割线
                superToolTip.Items.AddTitle(footer);    //页脚
            }
            return superToolTip;
        }
    }
}
