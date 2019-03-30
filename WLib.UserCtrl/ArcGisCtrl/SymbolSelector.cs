/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/29 13:44:12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;
using System.IO;
using System.Windows.Forms;
using WLib.Envir.ArcGis;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 封装AxSymbologyControl的常用操作的符号选择器
    /// </summary>
    public class SymbolSelector
    {
        /// <summary>
        /// 查看更多符号的右键菜单
        /// </summary>
        private ContextMenuStrip _moreSymbolMenuStrip;
        /// <summary>
        /// 要设置样式的图层
        /// </summary>
        public ILayer Layer { get; }
        /// <summary>
        /// 当前所选的图层图例（包含符号(Symbol)及其标注与描述等）
        /// </summary>
        public ILegendClass LegendClass { get; }
        /// <summary>
        /// 当前所选的符号样式项（包含符号或元素(Symbol/Element)及其名称与分类等）
        /// </summary>
        public IStyleGalleryItem StyleGalleryItem { get; set; }
        /// <summary>
        /// 符号选择器控件
        /// </summary>
        public AxSymbologyControl SymbologyControl { get; }
        /// <summary>
        /// 查看更多符号的右键菜单
        /// </summary>
        public ContextMenuStrip MoreSymbolMenuStrip => _moreSymbolMenuStrip ?? (_moreSymbolMenuStrip = CreateMoreSymbolToMenuStrip());

        /// <summary>
        /// 添加更多符号
        /// </summary>
        public static string StrAddMoreSymbol = "添加更多符号";
        /// <summary>
        /// 当前所选的样式
        /// </summary>
        public ISymbol Symbol => (ISymbol)StyleGalleryItem.Item;
        /// <summary>
        /// 设置好各控件的可见性
        /// </summary>
        public Action<esriSymbologyStyleClass> SetControlsVisible { get; set; }
        /// <summary>
        /// 初始化封装AxSymbologyControl的常用操作的符号选择器，向AxSymbologyControl加载指定图例
        /// </summary>
        /// <param name="symbologyControl"></param>
        /// <param name="layer">要设置样式的图层</param>
        /// <param name="legendClass">当前所选的图层图例（包含符号(Symbol)及其标注与描述等）</param>
        public SymbolSelector(AxSymbologyControl symbologyControl, ILayer layer, ILegendClass legendClass)
        {
            SymbologyControl = symbologyControl;
            LegendClass = legendClass;
            Layer = layer;
            LoadStyle();
        }


        /// <summary>
        /// 根据图层的几何类型，加载符号并确定显示哪些控件
        /// </summary>
        private void LoadStyle()
        {
            SymbologyControl.LoadStyleFile(GetStylesFilePath());//加载ESRI.ServerStyle文件到SymbologyControl
            var eType = GetSymbologyStyleClassType(((IFeatureLayer)Layer).FeatureClass.ShapeType);//根据图层的几何类型，获取需要加载的符号类别
            SetFeatureClassStyle(eType);//设置好SymbologyControl的StyleClass
            SetControlsVisible(eType);//设置好各控件的可见性(visible)
        }
        /// <summary>
        /// 获取ESRI.ServerStyle文件的路径
        /// </summary>
        /// <returns></returns>
        private string GetStylesFilePath()
        {
            var paths = new[]
            {
                AppDomain.CurrentDomain.BaseDirectory + @"\Styles\ESRI.ServerStyle",
                ArcGisEnvironment.GetInstallPath() + @"\Styles\ESRI.ServerStyle",
                RuntimeManager.ActiveRuntime.Path + @"\Styles\ESRI.ServerStyle",
            };
            foreach (var path in paths)
            {
                if (File.Exists(path))
                    return path;
            }
            return null;
        }
        /// <summary>
        /// 根据图层的几何类型，获取需要加载的符号类别
        /// </summary>
        /// <param name="shapeType"></param>
        /// <returns></returns>
        private esriSymbologyStyleClass GetSymbologyStyleClassType(esriGeometryType shapeType)
        {
            esriSymbologyStyleClass eType = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            if (shapeType == esriGeometryType.esriGeometryPoint) eType = esriSymbologyStyleClass.esriStyleClassMarkerSymbols;
            else if (shapeType == esriGeometryType.esriGeometryPolyline) eType = esriSymbologyStyleClass.esriStyleClassLineSymbols;
            else if (shapeType == esriGeometryType.esriGeometryPolygon) eType = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            else if (shapeType == esriGeometryType.esriGeometryMultiPatch) eType = esriSymbologyStyleClass.esriStyleClassFillSymbols;
            return eType;
        }
        /// <summary>
        /// 根据符号样式类别初始化SymbologyControl，如果图层已有符号，则把符号作为SymbologyControl的第一个符号并选中
        /// </summary>
        /// <param name="eSymbologyStyleClass">符号样式类别枚举（点/线/面/标注/文本/指北针/比例尺等样式类别）</param>
        private void SetFeatureClassStyle(esriSymbologyStyleClass eSymbologyStyleClass)
        {
            SymbologyControl.StyleClass = eSymbologyStyleClass;//获取指定类别的符号样式库，即当前是点/线/面/标注/文本/指北针/比例尺等符号库的哪一个符号库
            var symbologyStyleClass = SymbologyControl.GetStyleClass(eSymbologyStyleClass);
            if (LegendClass != null)
            {
                StyleGalleryItem = new ServerStyleGalleryItem { Name = "当前符号", Item = LegendClass.Symbol };
                symbologyStyleClass.AddItem(StyleGalleryItem, 0);
            }
            symbologyStyleClass.SelectItem(0);
        }
        /// <summary>
        /// 创建查看更多符号的右键菜单
        /// </summary>
        /// <returns></returns>
        private ContextMenuStrip CreateMoreSymbolToMenuStrip()
        {
            var contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.ItemClicked += contextMenuStripMoreSymbol_ItemClicked;

            var dir = ArcGisEnvironment.GetInstallPath() + "\\Styles";//var dir = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Path;
            var filePaths = Directory.GetFiles(dir, "*.ServerStyle");//取得菜单项数量
            foreach (var filePath in filePaths)//循环添加其它符号菜单项到菜单
            {
                var text = System.IO.Path.GetFileNameWithoutExtension(filePath);
                contextMenuStrip.Items.Add(new ToolStripMenuItem
                {
                    Name = filePath,
                    CheckOnClick = true,
                    Text = text,
                    Checked = text == "ESRI"
                });
            }

            //添加“更多符号”菜单项到菜单最后一项
            contextMenuStrip.Items.Add(new ToolStripMenuItem { Text = StrAddMoreSymbol, Name = StrAddMoreSymbol });
            return contextMenuStrip;
        }
        /// <summary>
        /// 单击更多符号按钮的上下文菜单后，将新符号加入到符号选择控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripMoreSymbol_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;
            if (toolStripMenuItem.Name == StrAddMoreSymbol)//如果单击的是“添加更多符号”
            {
                var openfileDialog = new OpenFileDialog();
                if (openfileDialog.ShowDialog() == DialogResult.OK) //弹出打开文件对话框
                    SymbologyControl.LoadStyleFile(openfileDialog.FileName);
            }
            else//如果是其它选项
            {
                if (toolStripMenuItem.Checked == false)
                    SymbologyControl.LoadStyleFile(toolStripMenuItem.Name);
                else
                    SymbologyControl.RemoveFile(toolStripMenuItem.Name);
            }
            SymbologyControl.Refresh();
        }
    }
}
