/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/9
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using WLib.Attributes.Description;
using WLib.Web.Update;

namespace WLib.WinCtrls.UpdateCtrl
{
    /// <summary>
    /// 进行软件联网更新检查和执行软件更新操作的窗体
    /// </summary>
    public partial class UpdateForm : Form
    {
        /// <summary>
        /// 进行软件联网更新检查和执行软件更新操作的窗体
        /// </summary>
        /// <param name="updaterClient">提供软件自动更新操作的对象</param>
        public UpdateForm(UpdaterClient updaterClient)
        {
            InitializeComponent();
            this.btnCancel.Click += (sender, e) => this.Close();
            this.Shown += async (sender, e) =>
            {
                try
                {   
                    if (await updaterClient.CheckUpdateAsync())//判断软件是否需要更新
                    {
                        this.lblTips.Text = "已发现新版本，正在下载信息...";
                        var state = await updaterClient.DownloadFilesAsync();//若需要更新则下载更新包
                        var msg = state.GetDescriptionEx();
                        if (state == EDownloadState.Dowloaded)
                        {
                            if (MessageBox.Show(msg + "\r\n是否立即重启软件完成更新？", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.lblTips.Text = "正在更新软件...";
                                await updaterClient.RestartForUpdateAsync();//重启软件，解压更新包文件并且替换现有程序文件
                            }
                        }
                        else
                            MessageBox.Show(msg);
                    }
                    else
                        MessageBox.Show("软件已是最新版本！");
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                this.Close();
            };
        }
    }
}
