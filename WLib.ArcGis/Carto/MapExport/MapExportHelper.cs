/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/4 16:22:13
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Carto.Layer;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.Carto.MapExport.Base;
using WLib.ArcGis.Control;
using WLib.ArcGis.GeoDatabase.FeatClass;
using Path = System.IO.Path;

namespace WLib.ArcGis.Carto.MapExport
{
    /// <summary>
    /// 设置地图文档内容并进行出图的操作
    /// </summary>
    public class MapExportHelper
    {
        /// <summary>
        /// 设置地图文档内容并进行出图的操作
        /// </summary>
        public MapExportHelper() { }
        /// <summary>
        /// 根据地图出图配置，设置地图文档并出图
        /// </summary>
        /// <param name="cfg">对地图进行各项配置和出图的信息</param>
        /// <param name="mapDoc">要设置并出图的地图文档，若值为null则根据<see cref="cfg"/>参数指定的信息
        /// 复制地图模板到生成目录或临时目录，打开复制后的地图文档进行设置和出图</param>
        public virtual void ExportMap(MapExportInfo cfg, IMapDocument mapDoc = null)
        {
            //是否在完成出图后关闭地图文档，地图文档来自外部传参（mapDoc != null）则不关闭，来自内部打开则应关闭
            bool closeMapDoc = mapDoc == null;
            try
            {
                if (!ValidateConfig(cfg, out var message)) throw new Exception(message);
                if (mapDoc == null) mapDoc = GetMapDocument(cfg);
                ExportMapMainOperation(cfg, mapDoc);
            }
            catch (Exception ex)
            {
                if (closeMapDoc && mapDoc != null)
                {
                    mapDoc.Close();
                    Marshal.ReleaseComObject(mapDoc);
                }
                throw ex;
            }
        }


        /// <summary>
        /// 根据地图出图配置，设置地图文档并导出地图
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="mapDoc"></param>
        protected virtual void ExportMapMainOperation(MapExportInfo cfg, IMapDocument mapDoc)
        {
            //设置数据源、定义查询、比例尺、显示范围
            var graphicsContainer = mapDoc.PageLayout as IGraphicsContainer;
            foreach (var mapFrameInfo in cfg.MapFrames)
                SetMapFrame(graphicsContainer, mapFrameInfo);

            //设置元素的值
            foreach (var elementInfo in cfg.Elements)
            {
                var elements = graphicsContainer.GetElementsByKeyword(elementInfo.Name);
                SetElementsValue(elements, elementInfo.ValueType, elementInfo.Value);
            }

            //保存地图，导出图片
            mapDoc.Save();
            foreach (var exportPicture in cfg.ExportPictures)
            {
                var outputPath = Path.Combine(cfg.ExportDirectory, cfg.ExportFileName + exportPicture.PicExtension);
                mapDoc.PageLayout.ExportToPicture(outputPath, exportPicture.Dpi);
            }

            //关闭文档，释放资源
            mapDoc.Close();
            Marshal.ReleaseComObject(mapDoc);
        }
        /// <summary>
        /// 验证出图配置是否正确
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        protected virtual bool ValidateConfig(MapExportInfo cfg, out string message)
        {
            var sbMsg = new StringBuilder();
            if (!Directory.Exists(cfg.ExportDirectory))
                sbMsg.AppendLine($"输出目录{cfg.ExportDirectory}不存在，请先选定或创建正确的输出目录！");

            return string.IsNullOrWhiteSpace(message = sbMsg.ToString());
        }
        /// <summary>
        /// 根据配置复制地图模板到生成目录或临时目录，打开复制后的地图文档
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <returns></returns>
        protected virtual IMapDocument GetMapDocument(MapExportInfo cfg)
        {
            var newMxdPath = GetMxdPath(cfg);
            var mapDoc = new MapDocumentClass();
            mapDoc.Open(newMxdPath);
            return mapDoc;
        }
        /// <summary>
        /// 根据配置复制地图模板到生成目录或临时目录，返回复制后的地图文档路径
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <returns></returns>
        protected virtual string GetMxdPath(MapExportInfo cfg)
        {
            //若配置要求导出地图文档，则将地图模板复制到生成目录；否则复制到程序目录的临时文件夹下
            string mxdPath;
            if (cfg.IsExportMxd)
            {
                mxdPath = Path.Combine(cfg.ExportDirectory, cfg.ExportFileName + ".mxd");
                File.Copy(cfg.TemplateMxdPath, mxdPath, cfg.Overwrite);
            }
            else
            {
                var tmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
                Directory.CreateDirectory(tmpDir);
                mxdPath = Path.Combine(tmpDir, cfg.ExportFileName + ".mxd");
                File.Copy(cfg.TemplateMxdPath, mxdPath, true);
            }
            return mxdPath;
        }
        /// <summary>
        /// 设置地图框的图层数据源、图层定义查询、比例尺、地图显示范围等
        /// </summary>
        /// <param name="graphicsContainer">图形容器</param>
        /// <param name="mapFrameInfo">地图框信息</param>
        protected virtual void SetMapFrame(IGraphicsContainer graphicsContainer, MapFrameInfo mapFrameInfo)
        {
            var map = graphicsContainer.GetMapFrame(mapFrameInfo.MapFrameName).Map;
            foreach (var layerInfo in mapFrameInfo.LayerInfos)
            {
                var layer = SetLayerDataSource(map, layerInfo);//设置数据源
                if (layer is IFeatureLayer featureLayer)//设置定义查询
                    ((IFeatureLayerDefinition)featureLayer).DefinitionExpression = layerInfo.Definition;
            }
            if (mapFrameInfo.Scale > 0)//设置比例尺
                map.ReferenceScale = mapFrameInfo.Scale;

            SetMapExtent(map, mapFrameInfo);//设置地图显示范围
        }
        /// <summary>
        /// 从地图中查找图层并设置图层的定义查询
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="layerInfo">图层设置配置</param>
        /// <returns></returns>
        protected virtual ILayer SetLayerDataSource(IMap map, LayerInfo layerInfo)
        {
            var layer = layerInfo.Index > -1 ?
                map.get_Layer(layerInfo.Index) :
                map.GetLayer(layerInfo.Name);

            var sourcePath = layer.GetSourcePath()?.ToLower().Trim();
            var setSourcePath = layerInfo.DataSource?.ToLower().Trim();
            if (setSourcePath != null && sourcePath != setSourcePath)
                layer.SetSourcePath(setSourcePath);

            return layer;
        }
        /// <summary>
        /// 设置地图显示范围
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="mapFrameInfo">地图框信息</param>
        protected virtual void SetMapExtent(IMap map, MapFrameInfo mapFrameInfo)
        {
            //若MapExtent不为null，则地图缩放至MapExtent指定的范围；否则查找LayerInfo.ZoomTo为True的图层来定位缩放
            var activeView = map as IActiveView;
            if (mapFrameInfo.MapExtent != null)
                activeView.MapZoomTo(mapFrameInfo.MapExtent, 1.3);
            else
            {
                var layerInfo = mapFrameInfo.LayerInfos.FirstOrDefault(v => v.ZoomTo);
                if (layerInfo != null)
                {
                    var featureClass = FeatClassFromPath.FromPath(layerInfo.DataSource);
                    var geometries =  featureClass.QueryGeometries(layerInfo.Definition);
                    activeView.MapZoomTo(geometries, 1.3);
                }
            }
        }
        /// <summary>
        /// 设置元素的属性值
        /// </summary>
        /// <param name="elements">需要设置属性值的同一类元素</param>
        /// <param name="valueType">表示设置哪一类元素属性</param>
        /// <param name="value">元素的属性值</param>
        protected virtual void SetElementsValue(IEnumerable<IElement> elements, EElementValueType valueType, object value)
        {
            switch (valueType)
            {
                case EElementValueType.Name:
                    foreach (var element in elements) ((IElementProperties)element).Name = value.ToString();
                    break;
                case EElementValueType.Text:
                    foreach (var element in elements) if (element is ITextElement textElemet) textElemet.Text = value.ToString();
                    break;
                case EElementValueType.Size:
                    throw new NotImplementedException();
                case EElementValueType.Location:
                    if (value is IPoint point)
                        foreach (var element in elements) element.Geometry = point;
                    else if (value is double[] xyPair)
                    {
                        IPoint pt = new PointClass { X = xyPair[0], Y = xyPair[1] };
                        foreach (var element in elements) element.Geometry = pt;
                    }
                    else if (value is System.Drawing.Point sysPoint)
                    {
                        IPoint pt = new PointClass { X = sysPoint.X, Y = sysPoint.Y };
                        foreach (var element in elements) element.Geometry = pt;
                    }
                    break;
                case EElementValueType.Anchor:
                    if (Enum.TryParse<esriAnchorPointEnum>(value.ToString(), out var eValue))
                    {
                        foreach (var element in elements)
                            ((IElementProperties3)element).AnchorPoint = eValue;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }
    }
}
