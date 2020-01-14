using System;
using System.Windows.Forms;
using ESRI.ArcGIS;

namespace WLib.ArcGis
{
    /** LicenseInitializer使用说明：
     * private static LicenseInitializer licenseInitializer = new LicenseInitializer();
     *   //Main Method：
     *   licenseInitializer.InitializeApplication(
     *   new[]
     *   {
     *       // 此处绑定Advanced后不要再绑定其他权限， 如果绑定esriLicenseProductCodeEngine权限有些GP工具无法使用
     *       // ArcGIS10.1以上版本为esriLicenseProductCodeAdvanced，ArcGIS10.0应改为esriLicenseProductCodeDesktop
     *       esriLicenseProductCode.esriLicenseProductCodeAdvanced
     *   },
     *   new[]
     *   {
     *       esriLicenseExtensionCode.esriLicenseExtensionCodeSpatialAnalyst,
     *       esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork
     *   });
     *   Application.Run(new MainForm());
     *   licenseInitializer.ShutdownApplication();
     */

    /// <summary>
    /// 初始化ARCGIS许可
    /// </summary>
    public partial class LicenseInitializer
    {
        /// <summary>
        /// 初始化ARCGIS许可
        /// </summary>
        public LicenseInitializer()
        {
            ResolveBindingEvent += BindingArcGISRuntime;
        }

        public void InitializeApplication(object[] p1, object[] p2)
        {
            throw new NotImplementedException();
        }

        private void BindingArcGISRuntime(object sender, EventArgs e)
        {
            ProductCode[] supportedRuntimes = { ProductCode.Engine, ProductCode.Desktop };
            foreach (ProductCode productCode in supportedRuntimes)
            {
                if (RuntimeManager.Bind(productCode))
                    return;
            }
            MessageBox.Show("ArcGIS软件许可错误！", "提示", MessageBoxButtons.OK);
            Environment.Exit(0);
        }
    }
}