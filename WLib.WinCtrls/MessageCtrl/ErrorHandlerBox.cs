using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WLib.ExtProgram;
using WLib.ExtProgram.Contact;
using WLib.WinCtrls.Properties;

namespace WLib.WinCtrls.MessageCtrl
{
    /// <summary>
    /// 错误显示和意见反馈框
    /// </summary>
    public partial class ErrorHandlerBox : Form
    {
        /// <summary>
        /// 异常
        /// </summary>
        public Exception Error { get; protected set; }
        /// <summary>
        /// 意见反馈的联系方式信息
        /// </summary>
        public IEnumerable<ContactInfo> ContactInfos { get; protected set; }
        /// <summary>
        /// 处理建议及点击建议对应的跳转操作
        /// </summary>
        public Dictionary<string, Action> SuggestionActions { get; protected set; }
        /// <summary>
        /// 帮助按钮执行的操作
        /// </summary>
        public Action<Exception> HelpAction { get; set; }
        /// <summary>
        /// 查看日志事件
        /// </summary>
        private event EventHandler _openLog;
        /// <summary>
        /// 查看日志事件
        /// </summary>
        public event EventHandler OpenLog
        {
            add { _openLog += value; this.btnOpenLog.Visible = true; }
            remove { _openLog -= value; this.btnOpenLog.Visible = false; }
        }


        /// <summary>
        /// 错误显示和意见反馈框
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="suggestion">处理建议信息</param>
        /// <param name="contactType">联系方式类型</param>
        /// <param name="contact">联系方式内容</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        public ErrorHandlerBox(Exception exception, string suggestion, EContactType contactType, string contact, Action<Exception> helpAction = null)
        {
            var suggestionActions = string.IsNullOrWhiteSpace(suggestion) ? null : new Dictionary<string, Action>() { { suggestion, null } };
            LoadViewInfo(exception, suggestionActions, helpAction, new ContactInfo(contactType, "联系我们", contact));
        }
        /// <summary>
        /// 错误显示和意见反馈框
        /// </summary>
        /// <param name="errorMessage">异常或错误信息</param>
        /// <param name="suggestion">处理建议信息</param>
        /// <param name="contactType">联系方式类型</param>
        /// <param name="contact">联系方式内容</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        public ErrorHandlerBox(string errorMessage, string suggestion, EContactType contactType, string contact, Action<Exception> helpAction = null)
        {
            var suggestionActions = string.IsNullOrWhiteSpace(suggestion) ? null : new Dictionary<string, Action>() { { suggestion, null } };
            LoadViewInfo(new Exception(errorMessage), suggestionActions, helpAction, new ContactInfo(contactType, "联系我们", contact));
        }
        /// <summary>
        /// 错误显示和意见反馈框
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="suggestion">处理建议信息</param>
        /// <param name="contactInfos">联系信息</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        public ErrorHandlerBox(Exception exception, string suggestion = null, IEnumerable<ContactInfo> contactInfos = null, Action<Exception> helpAction = null)
        {
            var suggestionActions = string.IsNullOrWhiteSpace(suggestion) ? null : new Dictionary<string, Action>() { { suggestion, null } };
            LoadViewInfo(exception, suggestionActions, helpAction, contactInfos.ToArray());
        }
        /// <summary>
        /// 错误显示和意见反馈框
        /// </summary>
        /// <param name="errorMessage">异常或错误信息</param>
        /// <param name="suggestion">处理建议信息</param>
        /// <param name="contactInfos">联系信息</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        public ErrorHandlerBox(string errorMessage, string suggestion = null, IEnumerable<ContactInfo> contactInfos = null, Action<Exception> helpAction = null)
        {
            var suggestionActions = string.IsNullOrWhiteSpace(suggestion) ? null : new Dictionary<string, Action>() { { suggestion, null } };
            LoadViewInfo(new Exception(errorMessage), suggestionActions, helpAction, contactInfos.ToArray());
        }
        /// <summary>
        /// 错误显示和意见反馈框
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="suggestionActions">处理建议及点击建议对应的跳转操作</param>
        /// <param name="contactInfos">联系信息</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        public ErrorHandlerBox(Exception exception, Dictionary<string, Action> suggestionActions, IEnumerable<ContactInfo> contactInfos = null, Action<Exception> helpAction = null)
        {
            LoadViewInfo(exception, suggestionActions, helpAction, contactInfos.ToArray());
        }


        /// <summary>
        /// 初始化窗口，显示错误信息、处理建议、联系方式
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="suggestionActions">处理建议及点击建议对应的跳转操作</param>
        /// <param name="contactInfos">联系信息</param>
        /// <param name="helpAction">帮助按钮执行的操作</param>
        private void LoadViewInfo(Exception exception, Dictionary<string, Action> suggestionActions, Action<Exception> helpAction, params ContactInfo[] contactInfos)
        {
            InitializeComponent();
            this.lblMessage.MouseEnter += (sender, e) => this.lblCopyMsg.Visible = true;
            this.lblMessage.MouseLeave += (sender, e) => this.lblCopyMsg.Visible = false;

            //错误信息
            ShowExceptionMessage(this.Error = exception);

            //处理建议
            CreateLabelBySuggestions(this.SuggestionActions = suggestionActions ?? new Dictionary<string, Action>());

            //联系方式
            AddContactInfoButtons(this.ContactInfos = contactInfos);

            //帮助操作
            this.HelpButton = (this.HelpAction = helpAction) != null;
        }
        /// <summary>
        /// 显示错误基本信息和详细信息
        /// </summary>
        /// <param name="ex"></param>
        private void ShowExceptionMessage(Exception ex)
        {
            if (ex == null) return;

            var sb = new StringBuilder();
            sb.AppendLine($"【异常出现时间】：{DateTime.Now}");
            sb.AppendLine($"【引发异常的程序集】：{ex.Source}");
            sb.AppendLine($"【引发异常的方法】：{ ex.TargetSite}");
            sb.AppendLine($"【异常类型】：{ex.GetType().Name}");
            sb.AppendLine($"【异常信息】：{ex.Message}");
            sb.AppendLine($"【调用堆栈】：");
            sb.AppendLine(ex.StackTrace);
            this.lblMessage.Text = ex.Message;
            this.txtMessageDetail.Text = sb.ToString();
        }
        /// <summary>
        /// 根据联系方式信息加载联系按钮
        /// </summary>
        /// <param name="contactInfos"></param>
        private void AddContactInfoButtons(IEnumerable<ContactInfo> contactInfos)
        {
            if (contactInfos == null || contactInfos.Count() == 0) return;

            var infos = contactInfos.ToArray();
            this.btnContact.Visible = true;
            this.btnContact.Image = GetImageByContactType(infos[0].ContactType);
            this.btnContact.Text = "   " + infos[0].Name;
            this.btnContact.Click += (sender, e) => ActionByContactType(infos[0]);
            this.btnContact.SplitMenuStrip = this.contextMenuStrip1;
            foreach (var info in infos)
            {
                var menuItem = new ToolStripMenuItem(info.Name);
                menuItem.Image = GetImageByContactType(info.ContactType);
                menuItem.ToolTipText = info.Content;
                menuItem.Click += (sender, e) => ActionByContactType(info);
                this.contextMenuStrip1.Items.Add(menuItem);
            }
        }
        /// <summary>
        /// 根据联系方式返回对应的联系方式图标
        /// </summary>
        /// <param name="eType"></param>
        /// <returns></returns>
        private Image GetImageByContactType(EContactType eType)
        {
            switch (eType)
            {
                case EContactType.EMail: return Resources.email;
                case EContactType.Phone: return Resources.phone;
                case EContactType.QQ: return Resources.qq;
                case EContactType.QQGroup: return Resources.QQqun;
                case EContactType.Wechat: return Resources.wechat;
                case EContactType.WebSite: return Resources.homePage;
                default: return Resources.phone;
            }
        }
        /// <summary>
        /// 根据联系方式类型执行不同的操作
        /// </summary>
        /// <param name="eType"></param>
        private void ActionByContactType(ContactInfo info)
        {
            switch (info.ContactType)
            {
                case EContactType.EMail:
                case EContactType.Wechat:
                case EContactType.Phone:
                    Clipboard.SetDataObject(info.Content);
                    new Thread(() =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (!this.IsDisposed)
                                Invoke(new Action(() => this.lblConcatTips.Visible = !this.lblConcatTips.Visible));
                            Thread.Sleep(300);
                        }
                    }).Start();
                    break;
                case EContactType.QQ: new QQInvoke().OpenQQ(info.Content); break;
                case EContactType.QQGroup: new QQInvoke().OpenQQGroup(info.Content); break;
                case EContactType.WebSite: Process.Start(info.Content); break;
            }
        }
        /// <summary>
        /// 根据处理建议及对应的操作链接，创建和设置Label或LinkLabel控件
        /// </summary>
        /// <param name="suggestionActions"></param>
        private void CreateLabelBySuggestions(Dictionary<string, Action> suggestionActions)
        {
            int index = 0;
            foreach (var pair in suggestionActions)
            {
                if (pair.Value == null)
                {
                    var label = new Label
                    {
                        Text = pair.Key,
                        Location = new Point(0, 6 + index * 15),
                        Font = new Font("宋体", 10.5f),
                        Margin = new Padding(0, 0, 0, 0)
                    };
                    this.panelSuggestion.Controls.Add(label);
                }
                else
                {
                    var linkLabel = new LinkLabel
                    {
                        Text = pair.Key,
                        Location = new Point(0, 6 + index * 15),
                        Font = new Font("宋体", 10.5f),
                        Margin = new Padding(0, 0, 0, 0)
                    };
                    linkLabel.LinkClicked += (sender, e) => pair.Value.Invoke();
                }
                index++;
            }
        }


        private void BtnOK_Click(object sender, EventArgs e) => Close();

        private void BtnOpenLog_Click(object sender, EventArgs e) => _openLog?.Invoke(this, new EventArgs());

        private void ErrorHandlerBox_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e) => HelpAction?.Invoke(Error);

        private void LblExpand_Click(object sender, EventArgs e)
        {
            bool expand = this.lblExpand.Text == "展开";
            this.Height = expand ? 560 : 345;
            this.lblExpand.Text = expand ? "隐藏" : "展开";
        }

        private void LblCopyMsg_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.lblMessage.Text);
            this.lblCopyMsg.Text = "已复制";
            new Thread(() =>
            {
                Thread.Sleep(3000);
                if (!this.IsDisposed) Invoke(new Action(() => this.lblCopyMsg.Text = "复制(&C)"));
            }).Start();
        }
    }
}
