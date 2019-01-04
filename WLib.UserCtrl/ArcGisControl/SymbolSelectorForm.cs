using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Display;
using WLib.Envir.ArcGIS;

namespace WLib.UserCtrls.ArcGisControl
{
    public partial class SymbolSelectorForm : Form
    {
        private bool _contextMenuMoreSymbolInitiated = false;//判断菜单是否已经初始化
        private IStyleGalleryItem _styleGalleryItem;//图形目录、图库
        private readonly ILegendClass _legendClass;//图例类
        private readonly ILayer _layer;
        public ISymbol Symbol;
        public Image SymbolImage;

        /// <summary>
        /// 符号选择器(Symbology)窗体
        /// （注意Show窗体前先调用LoadSymbolSelector方法）
        /// </summary>
        public SymbolSelectorForm(ILegendClass tempLegendClass, ILayer tempLayer)
        {
            InitializeComponent();

            _legendClass = tempLegendClass;
            _layer = tempLayer;
        }
        /// <summary>
        /// 初始化SymbologyControl的StyleClass,
        /// 如果图层已有符号,则把符号添加到SymbologyControl中的第一个符号,并选中
        /// </summary>
        /// <param name="eSymbologyStyleClass"></param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass eSymbologyStyleClass)
        {
            axSymbologyControl1.StyleClass = eSymbologyStyleClass;
            ISymbologyStyleClass symbologyStyleClass = axSymbologyControl1.GetStyleClass(eSymbologyStyleClass);
            if (_legendClass != null)
            {
                IStyleGalleryItem currentStyleGalleryItem = new ServerStyleGalleryItem();
                currentStyleGalleryItem.Name = "当前符号";
                currentStyleGalleryItem.Item = _legendClass.Symbol;
                symbologyStyleClass.AddItem(currentStyleGalleryItem, 0);
                _styleGalleryItem = currentStyleGalleryItem;
            }
            symbologyStyleClass.SelectItem(0);
        }
        /// <summary>
        /// 把选中并设置好的符号在picturebox控件中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = axSymbologyControl1.
                GetStyleClass(axSymbologyControl1.StyleClass).
                PreviewItem(_styleGalleryItem, ptbPreview.Width, ptbPreview.Height);
            Image image = Image.FromHbitmap(new IntPtr(picture.Handle));
            ptbPreview.Image = image;
        }
      

      
        #region 符号选择器事件
        /// <summary>
        /// 双击符号同单击确定按钮，关闭符号选择器。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btnOK.PerformClick();
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
                        lblAngle.Visible = true;
                        nudAngle.Visible = true;
                        lblSize.Visible = true;
                        nudSize.Visible = true;
                        lblWidth.Visible = false;
                        nudWidth.Visible = false;
                        lblOutlineColor.Visible = false;
                        btnOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        lblAngle.Visible = false;
                        nudAngle.Visible = false;
                        lblSize.Visible = false;
                        nudSize.Visible = false;
                        lblWidth.Visible = true;
                        nudWidth.Visible = true;
                        lblOutlineColor.Visible = false;
                        btnOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        lblAngle.Visible = false;
                        nudAngle.Visible = false;
                        lblSize.Visible = false;
                        nudSize.Visible = false;
                        lblWidth.Visible = true;
                        nudWidth.Visible = true;
                        lblOutlineColor.Visible = true;
                        btnOutlineColor.Visible = true;
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
            _styleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            Color color;
            switch (axSymbologyControl1.StyleClass)
            {
                //点符号
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    color = ColorConvert.ConvertIRgbColorToColor(((IMarkerSymbol)_styleGalleryItem.Item).Color as IRgbColor);
                    //设置点符号角度和大小初始值
                    nudAngle.Value = (decimal)((IMarkerSymbol)_styleGalleryItem.Item).Angle;
                    nudSize.Value = (decimal)((IMarkerSymbol)_styleGalleryItem.Item).Size;
                    break;
                //线符号
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    color = ColorConvert.ConvertIRgbColorToColor(((ILineSymbol)_styleGalleryItem.Item).Color as IRgbColor);
                    //设置线宽初始值
                    nudWidth.Value = (decimal)((ILineSymbol)_styleGalleryItem.Item).Width;
                    break;
                //面符号
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    color = ColorConvert.ConvertIRgbColorToColor(((IFillSymbol)_styleGalleryItem.Item).Color as IRgbColor);
                    ILineSymbol outline = ((IFillSymbol)_styleGalleryItem.Item).Outline;
                    btnOutlineColor.BackColor = ColorConvert.ConvertIRgbColorToColor(outline.Color as IRgbColor);
                    nudWidth.Value = (decimal)outline.Width;//设置外框线宽度初始值
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            //设置按钮背景色
            btnColor.BackColor = color;
            //预览符号
            PreviewImage();

        }
        #endregion

        /// <summary>
        /// 根据图层的几何类型，加载符号并确定显示哪些控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolSelectorForm_Load(object sender, EventArgs e)
        {
            //取得ArcGIS安装路径，载入ESRI.ServerStyle文件到SymbologyControl
            ICheckArcGisInstall checkArcGisInstall = new CheckArcGIS10Install();
            string sInstall = checkArcGisInstall.GetDesktopPath();
            axSymbologyControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
            axSymbologyControl1.LoadStyleFile(sInstall + "\\Styles\\ESRI.ServerStyle");

            //确定图层的类型(点线面),设置好SymbologyControl的StyleClass,设置好各控件的可见性(visible)
            switch (((IFeatureLayer)_layer).FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassMarkerSymbols);
                    lblAngle.Visible = true;
                    nudAngle.Visible = true;
                    lblSize.Visible = true;
                    nudSize.Visible = true;
                    lblWidth.Visible = false;
                    nudWidth.Visible = false;
                    lblOutlineColor.Visible = false;
                    btnOutlineColor.Visible = false;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassLineSymbols);
                    lblAngle.Visible = false;
                    nudAngle.Visible = false;
                    lblSize.Visible = false;
                    nudSize.Visible = false;
                    lblWidth.Visible = true;
                    nudWidth.Visible = true;
                    lblOutlineColor.Visible = false;
                    btnOutlineColor.Visible = false;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    lblAngle.Visible = false;
                    nudAngle.Visible = false;
                    lblSize.Visible = false;
                    nudSize.Visible = false;
                    lblWidth.Visible = true;
                    nudWidth.Visible = true;
                    lblOutlineColor.Visible = true;
                    btnOutlineColor.Visible = true;
                    break;
                case esriGeometryType.esriGeometryMultiPatch:
                    SetFeatureClassStyle(esriSymbologyStyleClass.esriStyleClassFillSymbols);
                    lblAngle.Visible = false;
                    nudAngle.Visible = false;
                    lblSize.Visible = false;
                    nudSize.Visible = false;
                    lblWidth.Visible = true;
                    nudWidth.Visible = true;
                    lblOutlineColor.Visible = true;
                    btnOutlineColor.Visible = true;
                    break;
                default:
                    Close();
                    break;
            }
        }

        /// <summary>
        /// 调整符号大小-点符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)_styleGalleryItem.Item).Size =
                (double)nudSize.Value;
            PreviewImage();

        }

        /// <summary>
        /// 调整符号宽度-限于线符号和面符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            switch (axSymbologyControl1.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    ((ILineSymbol)_styleGalleryItem.Item).Width = Convert.ToDouble(nudWidth.Value);
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    //取得面符号的轮廓线符号
                    ILineSymbol pLineSymbol = ((IFillSymbol)_styleGalleryItem.Item).Outline;
                    pLineSymbol.Width = Convert.ToDouble(nudWidth.Value);
                    ((IFillSymbol)_styleGalleryItem.Item).Outline = pLineSymbol;
                    break;
            }
            PreviewImage();
        }

        /// <summary>
        /// 调整符号角度-点符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudAngle_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)_styleGalleryItem.Item).Angle = (double)nudAngle.Value;
            PreviewImage();
        }

        /// <summary>
        /// 颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_EditValueChanged(object sender, EventArgs e)
        {
            //设置符号颜色为用户选定的颜色
            IColor color = ColorConvert.ConvertColorToIColor(btnColor.BackColor);
            switch (axSymbologyControl1.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols://点符号
                    ((IMarkerSymbol)_styleGalleryItem.Item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols: //线符号
                    ((ILineSymbol)_styleGalleryItem.Item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols://面符号
                    ((IFillSymbol)_styleGalleryItem.Item).Color = color;
                    break;
            }
            //更新符号预览
            PreviewImage();
        }

        /// <summary>
        /// 外框颜色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutlineColor_EditValueChanged(object sender, EventArgs e)
        {
            ILineSymbol pLineSymbol = ((IFillSymbol)_styleGalleryItem.Item).Outline;//取得面符号中的外框线符号
            pLineSymbol.Color = ColorConvert.ConvertColorToIColor(btnOutlineColor.BackColor);//设置外框线颜色
            ((IFillSymbol)_styleGalleryItem.Item).Outline = pLineSymbol; //重新设置面符号中的外框线符号
            PreviewImage();//更新符号预览
        }


        /// <summary>
        /// 单击更多符号按钮，弹出上下文菜单列出其它符号菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoreSymbols_Click(object sender, EventArgs e)
        {
            if (_contextMenuMoreSymbolInitiated == false)
            {
                string sInstall = new CheckArcGIS10Install().GetDesktopPath();
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
                contextMenuStripMoreSymbol.Items.AddRange(symbolContextMenuItem);
                _contextMenuMoreSymbolInitiated = true;
            }
            //显示菜单
            contextMenuStripMoreSymbol.Show(btnMoreSymbols.Location);
        }

        /// <summary>
        /// 单击更多符号按钮的上下文菜单后，将新符号加入到符号选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripMoreSymbol_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        /// <summary>
        /// 单击确定按钮，更新符号并关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Symbol = (ISymbol)_styleGalleryItem.Item; //取得选定的符号
            SymbolImage = ptbPreview.Image; //更新预览图像
            DialogResult = DialogResult.OK;//关闭窗体
            Close();
        }

        /// <summary>
        /// 单击取消按钮，关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
                btnColor_EditValueChanged(null, null);
            }
        }

        /// <summary>
        /// 设置外框颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutlineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnOutlineColor.BackColor = colorDialog1.Color;
                btnOutlineColor_EditValueChanged(null, null);
            }
        }
    }
}
