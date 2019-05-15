/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/12 11:13:21
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using stdole;
using WLib.ArcGis.Display;

namespace WLib.ArcGis.Carto.LabelAnno
{
    /// <summary>
    /// 提供设置、显示、隐藏标注的方法
    /// </summary>
    public static class LabelOpt
    {
        /// <summary>
        /// 设置标注大小
        /// </summary>
        /// <param name="geoLayer">要设置标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        /// <param name="size">标注的大小</param>
        public static void SetLabelSize(this IGeoFeatureLayer geoLayer, int size)
        {
            IAnnotateLayerPropertiesCollection annotateLyrProColl = geoLayer.AnnotationProperties;
            IAnnotateLayerProperties annoPros = null;
            IElementCollection placeElements = null;
            IElementCollection unplaceElements = null;
            for (int i = 0; i < annotateLyrProColl.Count; i++)
            {
                annotateLyrProColl.QueryItem(i, out annoPros, out placeElements, out unplaceElements);
                if (annoPros is ILabelEngineLayerProperties labelEngine)
                {
                    labelEngine.Symbol.Size = size;
                    break;
                }
            }
        }
        /// <summary>
        /// 显示指定图层指定字段的标注
        /// </summary>
        /// <param name="geoLayer">要设置注标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        /// <param name="fieldName">显示标注的字段</param>
        /// <param name="fontName">标注的字体</param>
        /// <param name="size">标注的大小</param>
        public static void ShowLabel(this IGeoFeatureLayer geoLayer, string fieldName, string fontName = "宋体", int size = 12)
        {
            //标注属性集
            IAnnotateLayerPropertiesCollection annotateLyrProColl = geoLayer.AnnotationProperties;
            annotateLyrProColl.Clear();

            //普通标准属性（另一个是Maplex标准属性）  
            ILabelEngineLayerProperties labelEngine = new LabelEngineLayerPropertiesClass();
            labelEngine.Expression = $"[{fieldName}]";

            //字体
            IFontDisp fontDisp = new StdFont() { Name = fontName, Bold = false } as IFontDisp;

            //标注符号
            ITextSymbol textSymbol = new TextSymbolClass();
            textSymbol.Color = ColorCreate.GetIColor(0, 0, 0);
            textSymbol.Font = fontDisp;
            textSymbol.Size = size;
            labelEngine.Symbol = textSymbol;

            //设置同名标注：默认为移除同名标注，应设为每个要素放置一个标注
            IBasicOverposterLayerProperties basicOverpLyrPro = labelEngine.BasicOverposterLayerProperties as IBasicOverposterLayerProperties;
            basicOverpLyrPro.NumLabelsOption = esriBasicNumLabelsOption.esriOneLabelPerShape;//每个要素放置一个标注

            annotateLyrProColl.Add(labelEngine as IAnnotateLayerProperties);
            geoLayer.DisplayAnnotation = true;
        }
        /// <summary>
        /// 隐藏标注
        /// </summary>
        /// <param name="geoLayer">要设置标注的图层(IFeatureLayer as IGeoFeatureLayer)</param>
        public static void HideLabel(this IGeoFeatureLayer geoLayer)
        {
            geoLayer.DisplayAnnotation = false;
        }
    }
}
