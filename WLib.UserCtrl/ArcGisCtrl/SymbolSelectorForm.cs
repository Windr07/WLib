/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Display;
using WLib.Envir.ArcGIS;

namespace WLib.UserCtrls.ArcGisCtrl
{
    public partial class SymbolSelectorForm : Form
    {
        /// <summary>
        /// 要设置样式的图层
        /// </summary>
        protected ILayer Layer { get; }
        /// <summary>
        /// 当前所选的图层图例（包含符号(Symbol)及其标注与描述等）
        /// </summary>
        protected ILegendClass LegendClass { get; }
        /// <summary>
        /// 当前所选的符号样式项（包含符号或元素(Symbol/Element)及其名称与分类等）
        /// </summary>
        protected IStyleGalleryItem StyleGalleryItem { get; set; }
        /// <summary>
        /// 当前所选的样式
        /// </summary>
        public ISymbol Symbol { get; protected set; }
        /// <summary>
        /// 添加更多符号
        /// </summary>
        public static string StrAddMoreSymbol = "添加更多符号";


        /// <summary>
        /// 符号选择器(Symbology)窗体（注意Show窗体前先调用LoadSymbolSelector方法）
        /// </summary>
        /// <param name="legendClass">当前所选的图层图例（包含符号(Symbol)及其标注与描述等）</param>
        /// <param name="layer">要设置样式的图层</param>
        public SymbolSelectorForm(ILegendClass legendClass, ILayer layer)
        {
            InitializeComponent();

            LegendClass = legendClass;
            Layer = layer;
        }
        /// <summary>
        /// 根据符号样式类别初始化SymbologyControl，如果图层已有符号，则把符号作为SymbologyControl的第一个符号并选中
        /// </summary>
        /// <param name="eSymbologyStyleClass">符号样式类别枚举（点/线/面/标注/文本/指北针/比例尺等样式类别）</param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass eSymbologyStyleClass)
        {
            //获取指定类别的符号样式库，即当前是点/线/面/标注/文本/指北针/比例尺等符号库的哪一个符号库
            axSymbologyControl1.StyleClass = eSymbologyStyleClass;
            var symbologyStyleClass = axSymbologyControl1.GetStyleClass(eSymbologyStyleClass);
            if (LegendClass != null)
            {
                StyleGalleryItem = new ServerStyleGalleryItem { Name = "当前符号", Item = LegendClass.Symbol };
                symbologyStyleClass.AddItem(StyleGalleryItem, 0);
            }
            symbologyStyleClass.SelectItem(0);
        }
        /// <summary>
        /// 根据符号样式类别显示不同的控件
        /// </summary>
        /// <param name="eSymbologyStyleClass">符号样式类别枚举（点/线/面/标注/文本/指北针/比例尺等样式类别）</param>
        private void SetControlsVisible(esriSymbologyStyleClass eSymbologyStyleClass)
        {
            try
            {
                switch (eSymbologyStyleClass)
                {
                    case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                        nudAngle.Visible = lblAngle.Visible = true;
                        nudSize.Visible = lblSize.Visible = true;
                        nudWidth.Visible = lblWidth.Visible = false;
                        btnOutlineColor.Visible = lblOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                        nudAngle.Visible = lblAngle.Visible = false;
                        nudSize.Visible = lblSize.Visible = false;
                        nudWidth.Visible = lblWidth.Visible = true;
                        btnOutlineColor.Visible = lblOutlineColor.Visible = false;
                        break;
                    case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                        nudAngle.Visible = lblAngle.Visible = false;
                        nudSize.Visible = lblSize.Visible = false;
                        nudWidth.Visible = lblWidth.Visible = true;
                        btnOutlineColor.Visible = lblOutlineColor.Visible = true;
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 把选中并设置好的符号在picturebox控件中预览
        /// </summary>
        private void PreviewImage()
        {
            stdole.IPictureDisp picture = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass).
                PreviewItem(StyleGalleryItem, ptbPreview.Width, ptbPreview.Height);
            ptbPreview.Image = Image.FromHbitmap(new IntPtr(picture.Handle));
        }


        /// <summary>
        /// 根据图层的几何类型，加载符号并确定显示哪些控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolSelectorForm_Load(object sender, EventArgs e)
        {
            //取得ArcGIS安装路径，载入ESRI.ServerStyle文件到SymbologyControl
            axSymbologyControl1.LoadStyleFile(new CheckArcGIS10Install().GetDesktopPath() + "\\Styles\\ESRI.ServerStyle");

            //确定图层的类型(点线面)
            esriSymbologyStyleClass eType;
            var shapeType = ((IFeatureLayer)Layer).FeatureClass.ShapeType;
            if (shapeType == esriGeometryType.esriGeometryPoint) eType = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            else if (shapeType == esriGeometryType.esriGeometryPolyline) eType = esriSymbologyStyleClass.esriStyleClassLineSymbols;
            else if (shapeType == esriGeometryType.esriGeometryPolygon) eType = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            else if (shapeType == esriGeometryType.esriGeometryMultiPatch) eType = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            else return;

            SetFeatureClassStyle(eType);//设置好SymbologyControl的StyleClass
            SetControlsVisible(eType);//设置好各控件的可见性(visible)
        }
        /// <summary>
        /// 单击更多符号按钮，弹出上下文菜单列出其它符号菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMoreSymbols_Click(object sender, EventArgs e)
        {
            if (btnMoreSymbols.Tag == null)//Tag值表示是否已加载过其它符号菜单
            {
                var dir = new CheckArcGIS10Install().GetDesktopPath() + "\\Styles";
                var filePaths = System.IO.Directory.GetFiles(dir, "*.ServerStyle");//取得菜单项数量

                //循环添加其它符号菜单项到菜单
                ToolStripMenuItem[] toolStripMenuItems = new ToolStripMenuItem[filePaths.Length + 1];
                for (int i = 0; i < filePaths.Length; i++)
                {
                    toolStripMenuItems[i] = new ToolStripMenuItem
                    {
                        CheckOnClick = true,
                        Text = System.IO.Path.GetFileNameWithoutExtension(filePaths[i])
                    };
                    if (toolStripMenuItems[i].Text == "ESRI")
                        toolStripMenuItems[i].Checked = true;

                    toolStripMenuItems[i].Name = filePaths[i];
                }
                //添加“更多符号”菜单项到菜单最后一项
                toolStripMenuItems[filePaths.Length] = new ToolStripMenuItem { Text = StrAddMoreSymbol, Name = StrAddMoreSymbol };

                //添加所有的菜单项到菜单
                contextMenuStripMoreSymbol.Items.AddRange(toolStripMenuItems);
                btnMoreSymbols.Tag = true;//Tag值表示是否已加载过其它符号菜单
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
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
            if (toolStripMenuItem.Name == StrAddMoreSymbol)//如果单击的是“添加更多符号”
            {
                if (this.openFileDialog.ShowDialog() == DialogResult.OK) //弹出打开文件对话框
                    this.axSymbologyControl1.LoadStyleFile(this.openFileDialog.FileName);
            }
            else//如果是其它选项
            {
                if (toolStripMenuItem.Checked == false)
                    this.axSymbologyControl1.LoadStyleFile(toolStripMenuItem.Name);
                else
                    this.axSymbologyControl1.RemoveFile(toolStripMenuItem.Name);
            }
            this.axSymbologyControl1.Refresh();
        }
        /// <summary>
        /// 单击确定按钮，更新符号并关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            Symbol = (ISymbol)StyleGalleryItem.Item; //取得选定的符号
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


        #region 符号选择器事件
        /// <summary>
        /// 双击符号相当于单击确定按钮，关闭符号选择器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnDoubleClick(object sender, ISymbologyControlEvents_OnDoubleClickEvent e)
        {
            btnOK.PerformClick();
        }
        /// <summary>
        /// 当符号样式类别改变时，重新设置符号类型和控件的可视性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnStyleClassChanged(object sender, ISymbologyControlEvents_OnStyleClassChangedEvent e)
        {
            SetControlsVisible((esriSymbologyStyleClass)e.symbologyStyleClass);
        }
        /// <summary>
        /// 选中符号时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            StyleGalleryItem = (IStyleGalleryItem)e.styleGalleryItem;
            object objSymbol = StyleGalleryItem.Item;
            switch (axSymbologyControl1.StyleClass)
            {
                //点符号
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols:
                    btnColor.BackColor = ((IMarkerSymbol)objSymbol).Color.ToColor();
                    nudAngle.Value = (decimal)((IMarkerSymbol)objSymbol).Angle;//设置点符号角度
                    nudSize.Value = (decimal)((IMarkerSymbol)objSymbol).Size;//设置点符号大小
                    break;
                //线符号
                case esriSymbologyStyleClass.esriStyleClassLineSymbols:
                    btnColor.BackColor = ((ILineSymbol)objSymbol).Color.ToColor();
                    nudWidth.Value = (decimal)((ILineSymbol)objSymbol).Width;//设置线宽初始值
                    break;
                //面符号
                case esriSymbologyStyleClass.esriStyleClassFillSymbols:
                    btnColor.BackColor = ((IFillSymbol)objSymbol).Color.ToColor();
                    ILineSymbol outline = ((IFillSymbol)objSymbol).Outline;
                    btnOutlineColor.BackColor = outline.Color.ToColor();
                    nudWidth.Value = (decimal)outline.Width;//设置外框线宽度初始值
                    break;
                default:
                    btnColor.BackColor = Color.Black;
                    break;
            }
            PreviewImage(); //预览符号
        }
        #endregion


        #region 符号样式设置事件
        /// <summary>
        /// 调整符号大小-点符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSize_ValueChanged(object sender, EventArgs e)
        {
            ((IMarkerSymbol)StyleGalleryItem.Item).Size = (double)nudSize.Value;
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
                    ((ILineSymbol)StyleGalleryItem.Item).Width = (double)nudWidth.Value;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols://取得面符号的轮廓线符号
                    ILineSymbol lineSymbol = ((IFillSymbol)StyleGalleryItem.Item).Outline;
                    lineSymbol.Width = Convert.ToDouble(nudWidth.Value);
                    ((IFillSymbol)StyleGalleryItem.Item).Outline = lineSymbol;
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
            ((IMarkerSymbol)StyleGalleryItem.Item).Angle = (double)nudAngle.Value;
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
            IColor color = btnColor.BackColor.ToIColor();
            switch (axSymbologyControl1.StyleClass)
            {
                case esriSymbologyStyleClass.esriStyleClassMarkerSymbols://点符号
                    ((IMarkerSymbol)StyleGalleryItem.Item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassLineSymbols: //线符号
                    ((ILineSymbol)StyleGalleryItem.Item).Color = color;
                    break;
                case esriSymbologyStyleClass.esriStyleClassFillSymbols://面符号
                    ((IFillSymbol)StyleGalleryItem.Item).Color = color;
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
            ILineSymbol lineSymbol = ((IFillSymbol)StyleGalleryItem.Item).Outline;//取得面符号中的外框线符号
            lineSymbol.Color = btnOutlineColor.BackColor.ToIColor();//设置外框线颜色
            ((IFillSymbol)StyleGalleryItem.Item).Outline = lineSymbol; //重新设置面符号中的外框线符号
            PreviewImage();//更新符号预览
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
        #endregion
    }
}
