using System;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using WLib.ArcGis;
using WLib.Samples.WinForm.SubForm;

namespace WLib.Samples.WinForm
{
    static class Program
    {

        private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            m_AOLicenseInitializer.InitializeApplication(
                new[] {
                    esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB,
                    esriLicenseProductCode.esriLicenseProductCodeEngine,
                    esriLicenseProductCode.esriLicenseProductCodeAdvanced
                },
                new[] {
                    esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork,
                    esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst,
                });


            //ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            //Application.Run(new OleDbTestForm());
            Application.Run(new MainForm());
            m_AOLicenseInitializer.ShutdownApplication();
        }
    }
}
