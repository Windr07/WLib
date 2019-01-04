using System;
using System.Windows.Forms;
using ESRI.ArcGIS;

namespace WLib.ArcGis
{
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

        void BindingArcGISRuntime(object sender, EventArgs e)
        {
            ProductCode[] supportedRuntimes = { ProductCode.Engine, ProductCode.Desktop };
            foreach (ProductCode c in supportedRuntimes)
            {
                if (RuntimeManager.Bind(c))
                    return;
            }
            MessageBox.Show("ArcGIS软件许可错误！", "提示", MessageBoxButtons.OK);
            Environment.Exit(0);
        }
    }
}