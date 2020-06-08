/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WLib.ArcGis.Carto.Element;
using WLib.ArcGis.Carto.Layer;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.Carto.MapExport.Base;
using WLib.ArcGis.Control;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.ArcGis.Geometry;
using WLib.ExtProgress;
using WLib.Files;
using WLib.Model;
using Path = System.IO.Path;

namespace WLib.ArcGis.Carto.MapExport
{
    /// <summary>
    /// 设置地图文档内容并进行出图的操作
    /// </summary>
    public class MapExportHelper : ProLogOperation<MapExportInfo, bool>
    {
        /// <summary>
        /// 要设置并出图的地图文档，若值为null则根据出图参数指定的信息
        /// 复制地图模板到生成目录或临时目录，打开复制后的地图文档进行设置和出图
        /// </summary>
        protected IMapDocument MapDoc { get; set; }
        /// <summary>
        /// 设置地图文档内容并进行出图的操作
        /// </summary>
        /// <param name="cfg">对地图进行各项配置和出图的信息</param>
        /// <param name="mapDoc">要设置并出图的地图文档，若值为null则根据<paramref name="cfg"/>参数指定的信息
        /// 复制地图模板到生成目录或临时目录，打开复制后的地图文档进行设置和出图</param>
        public MapExportHelper(MapExportInfo cfg, IMapDocument mapDoc = null) : base(cfg.CfgName, cfg) => MapDoc = mapDoc;


        /// <summary>
        /// 根据地图出图配置，设置地图文档并出图
        /// </summary>
        protected override void MainOperation()
        {
            //是否在完成出图后关闭地图文档，地图文档来自外部传参（mapDoc != null）则不关闭，来自内部打开则应关闭
            bool closeMapDoc = MapDoc == null;
            try
            {
                if (!ValidateConfig(InputData, out var cfgMessage)) throw new Exception(cfgMessage);//验证配置是否正确
                if (!ValidateData(InputData, out var dataMessage)) Warnning = dataMessage;//验证数据是否正确
                if (MapDoc == null) MapDoc = CopyOpenMapDoc(InputData);//复制地图文档至生成目录或临时目录，打开地图文档
                ExportMapMainOperation(InputData, MapDoc);//根据配置设置地图文档，保存并且导出地图
            }
            catch (Exception ex)
            {
                if (closeMapDoc && MapDoc != null)
                {
                    MapDoc.Close();
                    Marshal.ReleaseComObject(MapDoc);
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
            Info = "设置数据源、定义查询、比例尺、显示范围";
            var graphicsContainer = mapDoc.PageLayout as IGraphicsContainer;
            foreach (var mapFrameInfo in cfg.MapFrames)
                SetMapFrame(graphicsContainer, mapFrameInfo);

            Info = "设置元素的值";
            foreach (var elementInfo in cfg.Elements)
            {
                var elements = graphicsContainer.GetElementsByKeyword(elementInfo.Name);
                SetElementsValue(elements, elementInfo.ValueType, elementInfo.Value);
            }

            Info = "保存地图，导出图片";
            mapDoc.Save();
            foreach (var exportPicture in cfg.ExportPictures)
            {
                var outputPath = Path.Combine(cfg.ExportDirectory, cfg.ExportFileName + exportPicture.PicExtension);
                mapDoc.PageLayout.ExportToPicture(outputPath, exportPicture.Dpi);
            }

            Info = "关闭文档，释放资源";
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
        /// 验证出图数据是否正确
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        protected virtual bool ValidateData(MapExportInfo cfg, out string message)
        {
            var sbMsg = new StringBuilder();
            foreach (var mapFrame in cfg.MapFrames)
            {
                var layerPaths = mapFrame.LayerInfos.Select(v => v.DataSource).Distinct();
                var featureClasses = layerPaths.Select(v => FeatureClassEx.FromPath(v));
                //验证坐标系
                if (!SpatialRefOpt.CheckSpatialRef(featureClasses, out var tmpMessage))
                    sbMsg.Append(tmpMessage + Environment.NewLine);
            }

            return string.IsNullOrWhiteSpace(message = sbMsg.ToString());
        }

        /// <summary>
        /// 根据配置复制地图模板到生成目录或临时目录，打开复制后的地图文档
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <returns></returns>
        protected virtual IMapDocument CopyOpenMapDoc(MapExportInfo cfg)
        {
            var newMxdPath = CopyMapDoc(cfg);
            var mapDoc = new MapDocumentClass();
            mapDoc.Open(newMxdPath);
            return mapDoc;
        }
        /// <summary>
        /// 根据配置复制地图模板到生成目录或临时目录，返回复制后的地图文档路径
        /// </summary>
        /// <param name="cfg">导出地图配置</param>
        /// <returns></returns>
        protected virtual string CopyMapDoc(MapExportInfo cfg)
        {
            //若配置要求导出地图文档，则将地图模板复制到生成目录；否则复制到程序目录的临时文件夹下
            string mxdPath;
            if (!File.Exists(cfg.TemplateMxdPath))
                throw new Exception($"找不到出图模板“{cfg.TemplateMxdPath}”");
            if (cfg.IsExportMxd)
            {
                mxdPath = Path.Combine(cfg.ExportDirectory, cfg.ExportFileName + ".mxd");
                File.Copy(cfg.TemplateMxdPath, mxdPath, cfg.Overwrite);//复制地图文档到出图目录
            }
            else
            {
                var tmpDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp");
                Directory.CreateDirectory(tmpDir);
                mxdPath = Path.Combine(tmpDir, cfg.ExportFileName + ".mxd");
                File.Copy(cfg.TemplateMxdPath, mxdPath, true);//复制地图文档到临时目录
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
            var map = string.IsNullOrWhiteSpace(mapFrameInfo.Name) ?
                graphicsContainer.GetMapFrame()?.Map :
                graphicsContainer.GetMapFrame(mapFrameInfo.Name)?.Map;

            if (map == null) throw new Exception($"找不到名称为{mapFrameInfo.Name}的地图数据框！");
            var spatialRef = map.SpatialReference;

            for (int i = 0; i < mapFrameInfo.LayerInfos.Count; i++)
            {
                var layerInfo = mapFrameInfo.LayerInfos[i];
                var layer = SetLayerDataSource(map, layerInfo, spatialRef);//设置数据源
                if (layer is IFeatureLayer featureLayer)//设置定义查询
                {
                    ((IFeatureLayerDefinition)featureLayer).DefinitionExpression = layerInfo.Definition;
                }
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
        protected virtual ILayer SetLayerDataSource(IMap map, LayerInfo layerInfo, ISpatialReference spatialRef = null)
        {
            var layer = layerInfo.Index > -1 ?
                map.get_Layer(layerInfo.Index) :
                map.GetLayer(layerInfo.Name);

            if (layer == null)
            {
                var msg = layerInfo.Index > -1 ? $"地图“{map.Name}”中，找不到索引为{layerInfo.Index}的图层！" : $"地图“{map.Name}”中，找不到名称为{layerInfo.Name}的图层！";
                if (layerInfo.Optional)
                {
                    Info = msg;
                    return null;
                }
                else throw new Exception(msg);
            }
            layer.SpatialReference = spatialRef;

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
                    var featureClass = FeatureClassEx.FromPath(layerInfo.DataSource);
                    var geometries = featureClass.QueryGeometries(layerInfo.Definition).ToList();
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
        /// <summary>
        /// 判断图层坐标系是否与指定坐标系一致，不一致将其坐标系设置成与指定坐标系一致
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="spatialRef"></param>
        protected virtual void ResetSpatialRef(IFeatureLayer featureLayer, ISpatialReference spatialRef)
        {
            var spatialRef2 = featureLayer.GetSpatialRef();
            if (spatialRef2 != null && !SpatialRefOpt.CheckSpatialRef(new[] { spatialRef, spatialRef2 }, out _))
            {
                featureLayer.SpatialReference = spatialRef;
                Info = $"已修改图层{featureLayer.Name}坐标系，将与地图坐标系保持一致";
            }
        }


        /// <summary>
        /// 将<see cref="MapExportInfo"/>中配置的全部图层统一复制到新数据库或文件夹中，返回图层路径指向新数据库的<see cref="MapExportInfo"/>对象
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="newDbName">新数据库名称，也可以是shp文件夹的名称；若为null则使用<see cref="MapExportInfo.ExportFileName"/>作为新数据库名称，且默认为mdb数据库</param>
        /// <param name="newDbDir">新数据库存放目录；若为null则使用<see cref="MapExportInfo.ExportDirectory"/>作为新数据库存放目录</param>
        /// <returns></returns>
        public static MapExportInfo CopyDataToNewDatabase(MapExportInfo cfg, string newDbName = null, string newDbDir = null)
        {
            newDbDir = newDbDir ?? cfg.ExportDirectory;
            newDbName = newDbName ?? cfg.ExportFileName + ".mdb";

            var workspace = WorkspaceEx.NewWorkspace(newDbDir, newDbName);
            var newCfg = ObjectCopy.CopyBySerialize(cfg);
            var paths = new List<string>();//记录图层路径，路径相同时不重复复制图层
            var names = new List<string>();//记录图层名称，名称相同时在名称后加上序号
            foreach (var mapFrame in cfg.MapFrames)
            {
                foreach (var layerInfo in mapFrame.LayerInfos)
                {
                    if (paths.Contains(layerInfo.DataSource))
                        continue;

                    FeatureClassEx.SplitPath(layerInfo.DataSource, out var workspacePath, out var datasetName, out var featureClassName);
                    var targetClassName = FileOpt.ReNameNotRepeate(names, featureClassName);//在名称后加上“_序号”，保证名称不重复
                    var sourceClass = FeatureClassEx.FromPath(layerInfo.DataSource);
                    var newClass = workspace.CreateFeatureClass(sourceClass, targetClassName);
                    newCfg.MapFrames[mapFrame.Name].LayerInfos[layerInfo.Name].DataSource = newClass.GetSourcePath();

                    Marshal.ReleaseComObject(sourceClass);
                    Marshal.ReleaseComObject(newClass);
                    paths.Add(layerInfo.DataSource);
                    names.Add(featureClassName);
                }
            }
            Marshal.ReleaseComObject(workspace);
            return newCfg;
        }

    }
}
