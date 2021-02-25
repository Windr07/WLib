/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2016
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WLib.Envir.ArcGis;
using WLib.Envir.ArcGis.CheckVersion;
using WLib.Envir.DotNet;
using WLib.Envir.Windows;
using WLib.WinCtrls.Properties;

namespace WLib.WinCtrls.EnvirCheckCtrl
{
    /* 注意：.NET Framework 4.5 是与版本 1.1、2.0 和 3.5 并行运行的，并且是取代版本 4 的就地更新。 
     * 对于以版本 1.1、2.0 和 3.5 为目标的应用程序，你可以在目标计算机上安装适当的 .NET Framework 版本以在其最佳环境中运行该应用程序。
     * （http://msdn.microsoft.com/zh-cn/library/ff602939.aspx ）  
     * 1、Windows的.net framework安装:
          Windows XP： 默认已安装2.0
          Windows7： 默认已安装3.5（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
          Windows8:  默认已安装4.5（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
          Windows10: 默认已安装4.6（3.5sp1需在“启用或关闭windows功能”中安装，4.0需单独安装）
     *    关于操作系统自带.net版本：http://www.cnblogs.com/tough/p/5103713.html
     *    或者 https://blogs.msdn.microsoft.com/astebner/2007/03/14/mailbag-what-version-of-the-net-framework-is-included-in-what-version-of-the-os/ 
     * 2、Windows8安装.Net Framework3.5有以下三种方法：
          （1）自动联网安装：需要在控制面板→启动或关闭windows功能中设置，勾选.NET Framework3.5(包括.NET 2.0和3.0部分)，等待windows联网安装。
          （2）手动本地安装：使用网上的.net 3.5安装包进行安装，并在控制面板→启动或关闭windows功能中，勾选.NET Framework3.5(包括.NET 2.0和3.0部分)   
          （3）在win8系统ISO文件中安装：载入ISO文件，cmd中，输入dism.exe /online /enable-feature /featurename:NetFX3 /Source:F:\sources\sxs
           下载地址(64位)：http://www.ddooo.com/softdown/25627.htm 
           下载地址(32位)：http://www.bkill.com/download/21488.html 
     */


    /// <summary>
    /// ArcEngine二次开发软件环境检测和安装窗口
    /// </summary>
    public partial class ArcGISEnvirCheckForm : Form
    {
        /// <summary>
        /// ArcEngine二次开发软件环境检测和安装窗口
        /// </summary>
        public ArcGISEnvirCheckForm()
        {
            InitializeComponent();
            this.cmbArcGISVersion.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置运行或结束环境检测时界面控件的状态
        /// </summary>
        /// <param name="started"></param>
        private void ChangeViews(bool started)
        {
            this.lblTips.Visible = started;
            this.progressBar1.Visible = started;
            this.btnCheckEnviroment.Visible = !started;
            this.btnInstallDotNet35.Focus();
            Application.DoEvents();
        }
        /// <summary>
        /// 检查是否安装ArcGIS的指定产品
        /// </summary>
        private bool CheckArcGisProduct(EArcGisProductType productType, string productName, out string message)
        {
            var isInstall = ArcGisEnvironment.CheckArcGisInstall.IsIntall(productType, out var path);
            message = isInstall ? $"{ProductName}：已安装{Environment.NewLine}位置：{path}" : $"{ProductName}：未安装";
            return isInstall;
        }
        /// <summary>
        /// 启动安装文件
        /// </summary>
        /// <param name="filePath"></param>
        private void StartSetupProgram(string filePath)
        {
            try
            {
                var path = string.Format("{0}\\EvirFile\\{1}", AppDomain.CurrentDomain.BaseDirectory, filePath);
                Process.Start(path);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void btnCheckEnviroment_Click(object sender, EventArgs e)//检测系统安装环境
        {
            ChangeViews(true);
            this.txtMessage.Clear();
            try
            {
                //操作系统和.NET Framework信息
                var sb = new StringBuilder($"操作系统版本：{OSEnvironment.OSName}\r\n\r\n");
                sb.AppendLine("已安装的.NET Framework版本：");
                foreach (var info in DotNetEnvironment.GetAllNetFrameworkInfo())
                {
                    sb.AppendLine(info.ToString());
                    if (info.Name.StartsWith("v3.5") && info.Sp == "1") this.btnInstallDotNet35.Image = Resources.ok;
                    else if (info.Name.StartsWith("v4.0")) this.btnInstallDotNet4.Image = Resources.ok;
                }

                //ArcGIS安装情况
                if (CheckArcGisProduct(EArcGisProductType.ArcMap, "ArcMap", out var message1)) this.btnInstallRuntime.Image = Resources.ok;
                sb.AppendLine(Environment.NewLine + message1);

                if (CheckArcGisProduct(EArcGisProductType.ArcEngineRuntime, "Engine Runtime", out var message2)) this.btnInstallRuntime.Image = Resources.ok;
                sb.AppendLine(Environment.NewLine + message2);

                CheckArcGisProduct(EArcGisProductType.Aministrator, "Admin", out var message3);
                sb.AppendLine(Environment.NewLine + message3);

                if (CheckArcGisProduct(EArcGisProductType.License, "License", out var message4)) this.btnInstallLisence.Image = Resources.ok;
                sb.AppendLine(Environment.NewLine + message4);

                this.txtMessage.Text = sb.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { ChangeViews(false); }
        }

        private void btnInstallDotNet35_Click(object sender, EventArgs e)//1、安装.net3.5 sp1
        {
            if (OSEnvironment.WinVersion == EWinVersion.Win8或以上 && OSEnvironment.Is64Bit)
                StartSetupProgram("DotNet\\Win8.Net3.5_x64.exe");
            else if (OSEnvironment.WinVersion == EWinVersion.Win8或以上 && !OSEnvironment.Is64Bit)
                StartSetupProgram("DotNet\\win8.Net3.5_x86.exe");
            else
                StartSetupProgram("DotNet\\dotNetFx35_35+SP1.exe");
        }

        private void btnInstallDotNet4_Click(object sender, EventArgs e)//2、安装.net4.0 sp1
        {
            StartSetupProgram("DotNet\\dotNetFx40_Full_x86_x64.exe");
        }

        private void btnInstallRuntime_Click(object sender, EventArgs e)//3、安装ArcGIS Runtime
        {
            StartSetupProgram("EngineRT 10\\setup.msi");
        }

        private void btnInstallLisence_Click(object sender, EventArgs e)//4、安装ArcGIS Lincese
        {
            StartSetupProgram("EngineRT 10\\License\\setup.msi");
        }

        private void btnInstallSoftware_Click(object sender, EventArgs e)//5、安装软件
        {
            try
            {
                string[] path = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\EvirFile\\Setup");
                Process.Start(path[0]);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void lblOpenLicenseEcp_Click(object sender, EventArgs e)//打开ArcGIS License ecp文件进行授权
        {
            StartSetupProgram("service.ecp");
        }

        private void lblStartupLicenseManager_Click(object sender, EventArgs e)//启动license Manager
        {
            var licensePath = ArcGisEnvironment.GetInstallPath(EArcGisProductType.License);
            if (File.Exists(licensePath)) Process.Start(licensePath);
        }

        private void lblStartupAdministrator_Click(object sender, EventArgs e)//启动ArcGIS Administrator
        {
            var adminPath = ArcGisEnvironment.GetInstallPath(EArcGisProductType.Aministrator);
            if (File.Exists(adminPath)) Process.Start(adminPath);
        }
    }
}
