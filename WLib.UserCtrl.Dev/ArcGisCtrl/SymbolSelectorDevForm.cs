using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using WLib.Envir.ArcGIS;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 符号选择器(Symbology)窗体
    /// </summary>
    public partial class SymbolSelectorDevForm : DevExpress.XtraEditors.XtraForm
    {
        private IStyleGalleryItem pStyleGalleryItem;//图形目录、图库
        private ILegendClass pLegendClass;//图例类
        private ILayer pLayer;
        public ISymbol pSymbol;
        public Image pSymbolImage;
        bool contextMenuMoreSymbolInitiated = false;//判断菜单是否已经初始化
        /// <summary>
        /// 符号选择器(Symbology)窗体
        /// （注意Show窗体前先调用LoadSymbolSelector方法）
        /// </summary>
        public SymbolSelectorDevForm(ILegendClass tempLegendClass, ILayer tempLayer)
        {
            InitializeComponent();

            this.pLegendClass = tempLegendClass;
            this.pLayer = tempLayer;
        }
        /// <summary>
        /// 初始化SymbologyControl的StyleClass,
        /// 如果图层已有符号,则把符号添加到SymbologyControl中的第一个符号,并选中
        /// </summary>
        /// <param name="symbologyStyleClass"></param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass symbologyStyleClass)
        {
            this.axSymbologyControl1.StyleClass = symbologyStyleClass;
            ISymbologyStyleClass pSymbologyStyleClass =
                this.axSymbologyControl1.GetStyleClass(symbologyStyleClass);
            if (this.pLegendClass != null)
            {
                IStyleGalleryItem currentStyleGalleryItem = new ServerStyleGalleryItem();
                currentStyleGalleryItem.Name = "当前符号";
                currentStyleGalleryItem.Item = pLegendClass.Symbol;
                pSymbologyStyleClass.AddItem(currentStyleGalleryItem, 0);
                this.pStyleGalleryItem = currentStyleGalleryItem;
            }
            pSymbologyStyleClass.SelectItem(0);
        }
        /// <summary>
        /// 把选中并设置好的符号在picturebox控件中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = this.axSymbologyControl1.
                GetStyleClass(this.axSymbologyControl1.StyleClass).
                PreviewItem(pStyleGalleryItem, this.ptbPreview.Width, this.ptbPreview.Height);
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new System.IntPtr(picture.Handle));
            this.ptbPreview.Image = image;
        }
        /// <summary> 
        /// 将ArcGIS Engine中的IRgbColor接口转换至.NET中的Color结构
        /// </summary>
        /// <param name="pRgbColor">IRgbColor</param>
        /// <returns>.NET中的System.Drawing.Color结构表示ARGB颜色</returns>
        public Color ConvertIRgbColorToColor(IRgbColor pRgbColor)
        {
            return ColorTranslator.FromOle(pRgbColor.RGB);
        }
        /// <summary> 
        /// 将.NET中的Color结构转换至于ArcGIS Engine中的IColor接口
        /// </summary>
        /// <param name="color">.NET中的System.Drawing.Color结构表示ARGB颜色</param>
        /// <returns>IColor</returns>
        public IColor ConvertColorToIColor(Color color)
        {
            IColor pColor = new RgbColorClass();
            pColor.RGB = color.B * 65536 + color.G * 256 + color.R;
            return pColor;
        }


        /// <summary>
        /// 根据图层的几何类型，加载符号并确定显示哪些控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolSelectorForm_Load(object sender, EventArgs e)
        {
            //取得ArcGIS安装路径，载入ESRI.ServerStyle文件到SymbologyControl
            CheckArcGIS10Install checkArcGISInstall = new CheckArcGIS10Install();
            string sInstall = checkArcGISInstall.GetDesktopPath();
            this.axSymbologyControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            this.axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            //确定图层的类型(点线面),设置好SymbologyControl的StyleClass,设置好各控件的可见性(visible)
            IGeoFeatureLayer pGeoFeatureLayer = (IGeoFeatureLayer)pLayer;
            switch (((IFeatureLayer)pLayer).FeatureClass.ShapeType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    this.lblAngle.Visible = true;
                    this.nudAngle.Visible = true;
                    this.lblSize.Visible = true;
                    this.nudSize.Visible = true;
                    this.lblWidth.Visible = false;
                    this.nudWidth.Visible = false;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = false;
                    this.btnOutlineColor.Visible = false;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = true;
                    this.btnOutlineColor.Visible = true;
                    break;
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultiPatch:
                    this.SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    this.lblAngle.Visible = false;
                    this.nudAngle.Visible = false;
                    this.lblSize.Visible = false;
                    this.nudSize.Visible = false;
                    this.lblWidth.Visible = true;
                    this.nudWidth.Visible = true;
                    this.lblOutlineColor.Visible = true;
                    this.btnOutlineColor.Visible = true;
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        /// <summary>
        /// 双击符号同单击确定按钮，关闭符号选择器。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            this.btnOK.PerformClick();
        }

        /// <summary>
        /// 当样式（Style）改变时，重新设置符号类型和控件的可视性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnStyleClassChanged(object sender, ISymbologyControlEvents_OnStyleClassChangedEvent e)
        {
            try
            {
                switch ((esriSymbologyStyleClass)(e.symbologyStyleClass))
                {
                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                        this.lblAngle.Visible = true;
                        this.nudAngle.Visible = true;
                        this.lblSize.Visible = true;
                        this.nudSize.Visible = true;
                        this.lblWidth.Visible = false;
                        this.nudWidth.Visible = false;
                        this.lblOutlineColor.Visible = false;
                        this.btnOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        this.lblAngle.Visible = false;
                        this.nudAngle.Visible = false;
                        this.lblSize.Visible = false;
                        this.nudSize.Visible = false;
                        this.lblWidth.Visible = true;
                        this.nudWidth.Visible = true;
                        this.lblOutlineColor.Visible = false;
                        this.btnOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        this.lblAngle.Visible = false;
                        this.nudAngle.Visible = false;
                        this.lblSize.Visible = false;
                        this.nudSize.Visible = false;
                        this.lblWidth.Visible = true;
                        this.nudWidth.Visible = true;
                        this.lblOutlineColor.Visible = true;
                        this.btnOutlineColor.Visible = true;
                        break;
                }
            }
            catch { }
        }

        /// <summary>
        /// 选中符号时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            pStyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            Color color;
            switch (this.axSymbologyControl1.StyleClass)
            {
                //点符号
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    color = this.ConvertIRgbColorToColor(((IMarkerSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    //设置点符号角度和大小初始值
                    this.nudAngle.Value = (decimal)((IMarkerSymbol)this.pStyleGalleryItem.Item).Angle;
                    this.nudSize.Value = (decimal)((IMarkerSymbol)this.pStyleGalleryItem.Item).Size;
                    break;
                //线符号
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    color = this.ConvertIRgbColorToColor(((ILineSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    //设置线宽初始值
                    this.nudWidth.Value = (decimal)((ILineSymbol)this.pStyleGalleryItem.Item).Width;
                    break;
                //面符号
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    color = this.ConvertIRgbColorToColor(((IFillSymbol)pStyleGalleryItem.Item).Color as IRgbColor);
                    ILineSymbol outline = ((IFillSymbol)pStyleGalleryItem.Item).Outline;
                    this.btnOutlineColor.Color = this.ConvertIRgbColorToColor(outline.Color as IRgbColor);
                    this.nudWidth.Value = (decimal)outline.Width;//设置外框线宽度初始值
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            //设置按钮背景色
            this.btnColor.Color = color;
            //预览符号
            this.PreviewImage();

        }

        /// <summary>
        /// 调整符号大小-点符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)this.pStyleGalleryItem.Item).Size =
                (double)this.nudSize.Value;
            this.PreviewImage();

        }

        /// <summary>
        /// 调整符号宽度-限于线符号和面符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            switch (this.axSymbologyControl1.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    ((ILineSymbol)this.pStyleGalleryItem.Item).Width = Convert.ToDouble(this.nudWidth.Value);
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    //取得面符号的轮廓线符号
                    ILineSymbol pLineSymbol = ((IFillSymbol)this.pStyleGalleryItem.Item).Outline;
                    pLineSymbol.Width = Convert.ToDouble(this.nudWidth.Value);
                    ((IFillSymbol)this.pStyleGalleryItem.Item).Outline = pLineSymbol;
                    break;
            }
            this.PreviewImage();
        }

        /// <summary>
        /// 调整符号角度-点符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)this.pStyleGalleryItem.Item).Angle = (double)this.nudAngle.Value;
            this.PreviewImage();
        }

        /// <summary>
        /// 颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_EditValueChanged(object sender, EventArgs e)
        {
            //设置符号颜色为用户选定的颜色
            IColor color = this.ConvertColorToIColor(this.btnColor.Color);
            switch (this.axSymbologyControl1.StyleClass)
            {
                //点符号
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    ((IMarkerSymbol)this.pStyleGalleryItem.Item).Color = color;
                    break;
                //线符号
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    ((ILineSymbol)this.pStyleGalleryItem.Item).Color = color;
                    break;
                //面符号
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    ((IFillSymbol)this.pStyleGalleryItem.Item).Color = color;
                    break;
            }
            //更新符号预览
            this.PreviewImage();
        }

        /// <summary>
        /// 外框颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutlineColor_EditValueChanged(object sender, EventArgs e)
        {
            //取得面符号中的外框线符号
            ILineSymbol pLineSymbol = ((IFillSymbol)this.pStyleGalleryItem.Item).Outline;
            //设置外框线颜色
            pLineSymbol.Color = this.ConvertColorToIColor(this.btnOutlineColor.Color);
            //重新设置面符号中的外框线符号
            ((IFillSymbol)this.pStyleGalleryItem.Item).Outline = pLineSymbol;
            //更新符号预览
            this.PreviewImage();
        }


        /// <summary>
        /// 单击更多符号按钮，弹出上下文菜单列出其它符号菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoreSymbols_Click(object sender, EventArgs e)
        {
            if (this.contextMenuMoreSymbolInitiated == false)
            {
                CheckArcGIS10Install checkArcGISInstall = new CheckArcGIS10Install();
                string sInstall = checkArcGISInstall.GetDesktopPath();
                string path = sInstall + "\\Styles";
                //取得菜单项数量
                string[] styleNames = System.IO.Directory.GetFiles(path, "*.ServerStyle");
                ToolStripMenuItem[] symbolContextMenuItem = new ToolStripMenuItem[styleNames.Length + 1];
                //循环添加其它符号菜单项到菜单
                for (int i = 0; i < styleNames.Length; i++)
                {
                    symbolContextMenuItem[i] = new ToolStripMenuItem();
                    symbolContextMenuItem[i].CheckOnClick = true;
                    symbolContextMenuItem[i].Text = System.IO.Path.GetFileNameWithoutExtension(styleNames[i]);
                    if (symbolContextMenuItem[i].Text == "ESRI")
                    {
                        symbolContextMenuItem[i].Checked = true;
                    }
                    symbolContextMenuItem[i].Name = styleNames[i];
                }
                //添加“更多符号”菜单项到菜单最后一项
                symbolContextMenuItem[styleNames.Length] = new ToolStripMenuItem();
                symbolContextMenuItem[styleNames.Length].Text = "添加符号";
                symbolContextMenuItem[styleNames.Length].Name = "AddMoreSymbol";

                //添加所有的菜单项到菜单
                this.contextMenuStripMoreSymbol.Items.AddRange(symbolContextMenuItem);
                this.contextMenuMoreSymbolInitiated = true;
            }
            //显示菜单
            this.contextMenuStripMoreSymbol.Show(this.btnMoreSymbols.Location);
        }

        /// <summary>
        /// 单击更多符号按钮的上下文菜单后，将新符号加入到符号选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripMoreSymbol_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem pToolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
            //如果单击的是“添加符号”
            if (pToolStripMenuItem.Name == "AddMoreSymbol")
            {
                //弹出打开文件对话框
                if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //导入style file到SymbologyControl
                    this.axSymbologyControl1.LoadStyleFile(this.openFileDialog.FileName);
                    //刷新axSymbologyControl控件
                    this.axSymbologyControl1.Refresh();
                }
            }
            else//如果是其它选项
            {
                if (pToolStripMenuItem.Checked == false)
                {
                    this.axSymbologyControl1.LoadStyleFile(pToolStripMenuItem.Name);
                    this.axSymbologyControl1.Refresh();
                }
                else
                {
                    this.axSymbologyControl1.RemoveFile(pToolStripMenuItem.Name);
                    this.axSymbologyControl1.Refresh();
                }
            }
        }

        /// <summary>
        /// 单击确定按钮，更新符号并关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //取得选定的符号
            this.pSymbol = (ISymbol)pStyleGalleryItem.Item;
            //更新预览图像
            this.pSymbolImage = this.ptbPreview.Image;
            //关闭窗体
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 单击取消按钮，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
