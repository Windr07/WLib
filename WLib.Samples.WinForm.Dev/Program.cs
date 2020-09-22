using ESRI.ArcGIS.esriSystem;
using System;
using System.Windows.Forms;
using WLib.ArcGis;

namespace WLib.Samples.WinForm.Dev
{
    static class Program
    {
        //private static readonly LicenseInitializer licenseInitializer = new LicenseInitializer();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //licenseInitializer.InitializeApplication(
            //   new[] {
            //        esriLicenseProductCode.esriLicenseProductCodeAdvanced
            //   },
            //   new[] {
            //        esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork,
            //        esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst,
            //   });
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            Application.Run(new MainForm());
            //licenseInitializer.ShutdownApplication();

        }
    }
}
