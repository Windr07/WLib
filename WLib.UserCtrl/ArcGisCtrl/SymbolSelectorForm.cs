/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using System;
using System.Drawing;
using System.Windows.Forms;
using WLib.ArcGis.Display;

namespace WLib.WinCtrls.ArcGisCtrl
{
    /// <summary>
    /// 符号选择器(Symbology)窗体，
    /// Show窗体前应先调用LoadSymbolSelector方法
    /// </summary>
    public partial class SymbolSelectorForm : Form
    {
        /// <summary>
        /// 封装AxSymbologyControl的常用操作的符号选择器
        /// </summary>
        public SymbolSelector Selector { get; private set; }
        /// <summary>
        /// 符号选择器(Symbology)窗体（注意Show窗体前先调用LoadSymbolSelector方法）
        /// </summary>
        /// <param name="legendClass">当前所选的图层图例（包含符号(Symbol)及其标注与描述等）</param>
        /// <param name="layer">要设置样式的图层</param>
        public SymbolSelectorForm()
        {
            InitializeComponent();

            btnOK.Click += delegate { DialogResult = DialogResult.OK; Close(); };
            btnCancel.Click += delegate { DialogResult = DialogResult.Cancel; Close(); };
            btnMoreSymbols.Click += delegate { Selector.MoreSymbolMenuStrip.Show(btnMoreSymbols.Location); };
        }
        /// <summary>
        /// 向AxSymbologyControl加载指定图例，初始化符号选择器
        /// </summary>
        /// <param name="layer">要设置样式的图层</param>
        /// <param name="legendClass">当前所选的图层图例（包含符号(Symbol)及其标注与描述等）</param>
        public void LoadSymbolSelector(ILayer layer, ILegendClass legendClass)
        {
            Selector = new SymbolSelector(this.axSymbologyControl1, layer, legendClass);
        }


        /// <summary>
        /// 根据符号样式类别显示不同的控件
        /// </summary>
        /// <param name="eSymbologyStyleClass">符号样式类别枚举（点/线/面/标注/文本/指北针/比例尺等样式类别）</param>
        private void SetControlsVisible(esriSymbologyStyleClass eSymbologyStyleClass)
        {
            switch (eSymbologyStyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    panelAngle.Visible = panelSize.Visible = true;
                    panelWidth.Visible = panelOutlineColor.Visible = false;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    panelAngle.Visible = panelSize.Visible = panelOutlineColor.Visible = false;
                    panelWidth.Visible = true;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    panelAngle.Visible = panelSize.Visible = false;
                    panelWidth.Visible = panelOutlineColor.Visible = true;
                    break;
            }
        }
        /// <summary>
        /// 把选中并设置好的符号在picturebox控件中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).
                PreviewItem(Selector.StyleGalleryItem, ptbPreview.Width, ptbPreview.Height);
            ptbPreview.Image = Image.FromHbitmap(new IntPtr(picture.Handle));
        }


        #region 符号选择器事件
        private void axSymbologyControl1_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btnOK.PerformClick();
        }
        
        private void axSymbologyControl1_OnStyleClassChanged(object sender, ISymbologyControlEvents_OnStyleClassChangedEvent e)
        {
            SetControlsVisible((esriSymbologyStyleClass)e.symbologyStyleClass);//当符号样式类别改变时，重新设置符号类型和控件的可视性
        }
       
        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            Selector.StyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            object objSymbol = Selector.StyleGalleryItem.Item;
            switch (axSymbologyControl1.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols://点符号
                    btnColor.BackColor = ((IMarkerSymbol)objSymbol).Color.ToColor();
                    nudAngle.Value = (decimal)((IMarkerSymbol)objSymbol).Angle;
                    nudSize.Value = (decimal)((IMarkerSymbol)objSymbol).Size;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols: //线符号
                    btnColor.BackColor = ((ILineSymbol)objSymbol).Color.ToColor();
                    nudWidth.Value = (decimal)((ILineSymbol)objSymbol).Width;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols: //面符号
                    btnColor.BackColor = ((IFillSymbol)objSymbol).Color.ToColor();
                    ILineSymbol outline = ((IFillSymbol)objSymbol).Outline;
                    btnOutlineColor.BackColor = outline.Color.ToColor();
                    nudWidth.Value = (decimal)outline.Width;
                    break;
                default:
                    btnColor.BackColor = Color.Black;
                    break;
            }
            PreviewImage();
        }
        #endregion


        #region 符号样式设置事件
        private void nudSize_ValueChanged(object sender, EventArgs e)//调整点符号大小
        {
            ((IMarkerSymbol)Selector.StyleGalleryItem.Item).Size = (double)nudSize.Value;
            PreviewImage();
        }
        
        private void nudWidth_ValueChanged(object sender, EventArgs e)//调整线或面符号符号宽度
        {
            var styleClass = axSymbologyControl1.StyleClass;
            if (styleClass == esriSymbologyStyleClass.esriStyleClassLineSymbols)
                ((ILineSymbol)Selector.StyleGalleryItem.Item).Width = (double)nudWidth.Value;
            else if (styleClass == esriSymbologyStyleClass.esriStyleClassFillSymbols)
                ((IFillSymbol)Selector.StyleGalleryItem.Item).Outline.Width = Convert.ToDouble(nudWidth.Value);

            PreviewImage();
        }
        
        private void nudAngle_ValueChanged(object sender, EventArgs e)//调整点符号角度
        {
            ((IMarkerSymbol)Selector.StyleGalleryItem.Item).Angle = (double)nudAngle.Value;
            PreviewImage();
        }
        
        private void btnColor_EditValueChanged(object sender, EventArgs e)//颜色选择
        {
            IColor color = btnColor.BackColor.ToIColor();
            object item = Selector.StyleGalleryItem.Item;
            switch (axSymbologyControl1.StyleClass)//设置符号颜色为用户选定的颜色
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols://点符号
                    ((IMarkerSymbol)item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols: //线符号
                    ((ILineSymbol)item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols://面符号
                    ((IFillSymbol)item).Color = color;
                    break;
            }
            //更新符号预览
            PreviewImage();
        }
        
        private void btnOutlineColor_EditValueChanged(object sender, EventArgs e)//外框颜色选择
        {
            ((IFillSymbol)Selector.StyleGalleryItem.Item).Outline.Color = btnOutlineColor.BackColor.ToIColor(); //重新设置面符号中的外框线符号
            PreviewImage();
        }
       
        private void btnColor_Click(object sender, EventArgs e)//设置颜色
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
                btnColor_EditValueChanged(null, null);
            }
        }
       
        private void btnOutlineColor_Click(object sender, EventArgs e)//设置外框颜色
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnOutlineColor.BackColor = colorDialog1.Color;
                btnOutlineColor_EditValueChanged(null, null);
            }
        }
        #endregion
    }
}
