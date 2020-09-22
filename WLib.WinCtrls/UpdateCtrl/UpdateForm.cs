using System;
using System.Threading;
using System.Windows.Forms;
using WLib.Web.Update;

namespace WLib.WinCtrls.UpdateCtrl
{
    /// <summary>
    /// 进行软件联网更新检查和执行软件更新操作的窗体
    /// </summary>
    public partial class UpdateForm : Form
    {
        /// <summary>
        /// 进行软件联网更新检查和执行软件更新的线程
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// 提供软件自动更新操作的对象
        /// </summary>
        public UpdaterClient UpdaterClient { get; set; }

        /// <summary>
        /// 进行软件联网更新检查和执行软件更新操作的窗体
        /// </summary>
        /// <param name="updater">提供软件自动更新操作的对象</param>
        public UpdateForm(UpdaterClient updaterClinet)
        {
            InitializeComponent();
            this.UpdaterClient = updaterClinet;
        }


        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            switch (this.btnCheckUpdate.Text)
            {
                case "检查更新":
                    this.btnCheckUpdate.Text = "取消";
                    _thread = new Thread(() => CheckAndUpdate());
                    _thread.Start();
                    break;
                case "取消":
                    this.btnCheckUpdate.Text = "检查更新";
                    _thread.Abort();
                    this.Close();
                    break;
            }
        }
        /// <summary>
        /// 判断软件是否需要更新，若需要更新则下载更新包，重启软件进行软件文件的替换更新
        /// </summary>
        private void CheckAndUpdate()
        {
            try
            {
                if (UpdaterClient.CheckUpdate(@""))
                {
                    var state = UpdaterClient.DownloadUpdateFiles(@"http://localhost:8080/Update", out var msg);
                    if (state == EDownloadState.Dowloaded)
                    {
                        Invoke(new Action(() =>
                        {
                            if (MessageBox.Show(msg + "\r\n是否立即重启软件完成更新？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                                UpdaterClient.RestartForUpdate("");
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() => MessageBox.Show(msg, this.Text)));
                    }
                }
            }
            catch (ThreadAbortException ex) { }
            catch (Exception ex)
            {
                if (!this.IsDisposed)
                    Invoke(new Action(() => MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)));
            }
        }
    }
}
