/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WLib.ArcGis.Analysis.OnShape;
using WLib.ArcGis.Display;
using WLib.ArcGis.GeoDatabase.FeatClass;

namespace WLib.ArcGis.Control
{
    /// <summary>
    /// 缩放显示图斑/要素/图层的操作
    /// </summary>
    public static class ZoomTo
    {
        #region 缩放至图层
        /// <summary>
        /// 缩放至图层
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayerName">要素图层名称，该图层应当存在于地图控件中</param>
        public static void MapZoomToLayer(this AxMapControl axMapControl, string featureLayerName)
        {
            var featureLayer = axMapControl.GetFeatureLayer(featureLayerName);
            if (featureLayer == null)
                throw new Exception($"在地图控件中，找不到名为“{featureLayerName}的要素图层”");
            MapZoomTo(axMapControl.ActiveView, featureLayer.AreaOfInterest);
        }
        #endregion


        #region 缩放至图斑
        /// <summary>
        /// 地图缩放至指定点的位置，visualRange是显示范围Evelope的宽度(或高度)
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="point">地图移动和缩放的中心点</param>
        /// <param name="visualRange">地图的显示范围：以缩放点为中心，上下左右各延伸<paramref name="visualRange"/>的一半作为显示范围</param>
        public static void MapZoomTo(this IActiveView activeView, IPoint point, double visualRange, bool hightLight = false)
        {
            IEnvelope envelope = new EnvelopeClass();
            envelope.SpatialReference = point.SpatialReference;
            double d = visualRange / 2;
            envelope.XMax = point.Envelope.XMax + d;
            envelope.XMin = point.Envelope.XMin - d;
            envelope.YMax = point.Envelope.YMax + d;
            envelope.YMin = point.Envelope.YMin - d;
            envelope.CenterAt(point);
            activeView.Extent = envelope;
            activeView.Refresh();

            if (hightLight) HightLightGeo(activeView, point, true);
        }
        /// <summary>
        /// 地图缩放至指定图形位置
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        /// <param name="expendRate">实际显示范围与图形范围框的比值，即放大的倍数</param>
        public static void MapZoomTo(this IActiveView activeView, IGeometry geometry, double expendRate = 2, bool hightLight = false)
        {
            InnerZoomToGeo(activeView, geometry, expendRate);
            if (hightLight) HightLightGeo(activeView, geometry, true);
        }
        /// <summary>
        /// 地图定跳转到指定图形位置
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometries"></param>
        /// <param name="expendRate">实际显示范围与图形范围框的比值，即放大的倍数</param>
        public static void MapZoomTo(this IActiveView activeView, IEnumerable<IGeometry> geometries, double expendRate = 2, bool hightLight = false)
        {
            var unionGeometry = TopologicalOpt.UnionGeometryEx(geometries);
            MapZoomTo(activeView, unionGeometry, expendRate, hightLight);
        }
        /// <summary>
        /// 地图缩放至查询所得的所有图形，并高亮显示这些图形，会清除其它高亮
        /// </summary>
        /// <param name="activeView">地图控件</param>
        /// <param name="featureLayer">查询图层</param>
        /// <param name="whereClause">查询条件</param>
        public static void MapZoomTo(this IActiveView activeView, IFeatureLayer featureLayer, string whereClause, double expendRate = 2, bool hightLight = false)
        {
            var geometries = featureLayer.FeatureClass.QueryGeometries(whereClause);
            MapZoomTo(activeView, geometries, expendRate, hightLight);
        }


        /// <summary>
        /// 地图缩放至指定要素并闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayerName">要素图层名称，该图层应当存在于地图控件中</param>
        /// <param name="whereClause">筛选要素的where条件</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void MapZoomTo(this AxMapControl axMapControl, IFeatureLayer featureLayer, string whereClause, double expendRate = 2, bool hightLight = false, bool selectShape = false)
        {
            var geometries = featureLayer.FeatureClass.QueryGeometries(whereClause);
            MapZoomTo(axMapControl.ActiveView, geometries, expendRate, hightLight);

            if (selectShape) SelectFeatures(axMapControl.Map, featureLayer, whereClause);
        }
        /// <summary>
        /// 地图缩放至指定要素并闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayerName">要素图层名称，该图层应当存在于地图控件中</param>
        /// <param name="whereClause">筛选要素的where条件</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void MapZoomTo(this AxMapControl axMapControl, IFeatureLayer featureLayer, int[] oids, double expendRate = 2, bool hightLight = false, bool selectShape = false)
        {
            var whereClause = $"{featureLayer.FeatureClass.OIDFieldName} In ({string.Join(",", oids.Select(v => v.ToString()))})";
            MapZoomTo(axMapControl, featureLayer, whereClause, expendRate, hightLight, selectShape);
        }
        /// <summary>
        /// 地图缩放至指定要素并闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayerName">要素图层名称，该图层应当存在于地图控件中</param>
        /// <param name="whereClause">筛选要素的where条件</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void MapZoomTo(this AxMapControl axMapControl, string featureLayerName, int[] oids, double expendRate = 2, bool hightLight = false, bool selectShape = false)
        {
            var featureLayer = axMapControl.GetFeatureLayer(featureLayerName);
            if (featureLayer == null) throw new Exception($"在地图控件中，找不到名为“{featureLayerName}的要素图层”");

            MapZoomTo(axMapControl, featureLayer, oids, expendRate, hightLight, selectShape);
        }
        /// <summary>
        /// 地图缩放至指定要素并闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayerName">要素图层名称，该图层应当存在于地图控件中</param>
        /// <param name="whereClause">筛选要素的where条件</param>
        /// <param name="expendRate"></param>
        /// <param name="hightLight"></param>
        public static void MapZoomTo(this AxMapControl axMapControl, string featureLayerName, string whereClause, double expendRate = 2, bool hightLight = false, bool selectShape = false)
        {
            var featureLayer = axMapControl.GetFeatureLayer(featureLayerName);
            if (featureLayer == null) throw new Exception($"在地图控件中，找不到名为“{featureLayerName}的要素图层”");

            MapZoomTo(axMapControl, featureLayer, whereClause, expendRate, hightLight, selectShape);
        }
        /// <summary>
        /// 地图缩放至指定图形并以选中形式闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="control">任意主线程窗体控件（axMapCtontrol除外）</param>
        /// <param name="geometry">闪烁的图像</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void MapZoomToSelectFlash(this AxMapControl axMapControl, System.Windows.Forms.Control control, IGeometry geometry, int nFlashes = 2)
        {
            MapZoomTo(axMapControl.ActiveView, geometry);

            ISelectionEnvironment pSelectionEnvironment = new SelectionEnvironmentClass();
            pSelectionEnvironment.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;

            Thread th = new Thread(delegate (object data)
            {
                for (int i = 0; i < nFlashes; i++)
                {
                    Thread.Sleep(50);
                    control.Invoke(new Action(() =>
                    {
                        axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        axMapControl.Map.SelectByShape(geometry, pSelectionEnvironment, true);
                        axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    }));

                    Thread.Sleep(100);
                    control.Invoke(new Action(() =>
                    {
                        axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                        axMapControl.Map.ClearSelection();
                        axMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
                    }));
                }
                var stopThreadDelegate = (StopThreadDelegate)data;
                stopThreadDelegate(Thread.CurrentThread);//停止线程
            });
            th.Start(new StopThreadDelegate((thread) =>
            {
                thread.Abort();
            }));
        }
        /// <summary>
        /// 停止线程的委托
        /// </summary>
        /// <param name="thread"></param>
        private delegate void StopThreadDelegate(Thread thread);
        #endregion



        /// <summary>
        /// 闪烁图斑
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="geometry">闪烁的图斑</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void FlashGeometry(this AxMapControl axMapControl, IGeometry geometry, int nFlashes = 2)
        {
            object symbol = null;
            switch (geometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                    symbol = SymbolCreate.GetSimpleMarkerSymbol("ff0000");
                    break;
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    symbol = SymbolCreate.GetSimpleLineSymbol("ff0000");
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    symbol = SymbolCreate.GetSimpleFillSymbol("99ccff", "ff0000");
                    break;
            }
            if (symbol == null) return;
            axMapControl.FlashShape(geometry, nFlashes, 200, symbol);
        }
        /// <summary>
        /// 高亮显示指定图形,不会清除原有的高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        /// <param name="clearOtherHightLight">是否清除地图上其他高亮显示的图斑</param>
        public static void HightLightGeo(this IActiveView activeView, IGeometry geometry, bool clearOtherHightLight = false)
        {
            if (clearOtherHightLight)
                activeView.GraphicsContainer.DeleteAllElements();

            IElement element = CreateHightLightElement(geometry);
            if (element != null)
                activeView.GraphicsContainer.AddElement(element, 0);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewForeground, geometry, activeView.Extent);
        }
        /// <summary>
        ///  高亮显示多个图形
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometries"></param>
        /// <param name="clearOtherHightLight">是否清除地图上其他高亮显示的图斑</param>
        public static void HightLightGeo(this IActiveView activeView, IEnumerable<IGeometry> geometries, bool clearOtherHightLight = false)
        {
            if (clearOtherHightLight)
                activeView.GraphicsContainer.DeleteAllElements();

            foreach (var geometry in geometries)
            {
                IElement element = CreateHightLightElement(geometry);
                if (element != null)
                    activeView.GraphicsContainer.AddElement(element, 0);
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewForeground, geometries, activeView.Extent);
        }
        /// <summary>
        /// 查询并选中要素
        /// </summary>
        /// <param name="map"></param>
        /// <param name="featureLayer"></param>
        /// <param name="wherClause"></param>
        public static void SelectFeatures(this IMap map, IFeatureLayer featureLayer, string wherClause)
        {
            IQueryFilter queryFilter = new QueryFilterClass() { WhereClause = wherClause };
            IFeatureSelection featureSelection = featureLayer as IFeatureSelection;
            featureSelection.SelectFeatures(queryFilter, esriSelectionResultEnum.esriSelectionResultNew, false);

            featureSelection.SelectionSet.Search(null, true, out var cursor);
            IFeatureCursor featureCursor = cursor as IFeatureCursor;
            IFeature feature;
            while ((feature = featureCursor.NextFeature()) != null)
                map.SelectFeature(featureLayer, feature);
        }
        /// <summary>
        /// 缩放到指定的图斑
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        /// <param name="expendRate"></param>
        /// <returns></returns>
        private static IPoint InnerZoomToGeo(this IActiveView activeView, IGeometry geometry, double expendRate)
        {
            if (activeView == null) throw new ArgumentException("ActiveView不能为空（Null）！", nameof(activeView));
            if (geometry == null) throw new ArgumentException("缩放的图斑不能为空（Null）！", nameof(geometry));
            if (geometry.IsEmpty) throw new ArgumentException("缩放的图斑不能为空（IsEmpty）！", nameof(geometry));

            IEnvelope envelope = new EnvelopeClass();
            envelope.SpatialReference = geometry.SpatialReference;
            envelope.XMax = geometry.Envelope.XMax;
            envelope.XMin = geometry.Envelope.XMin;
            envelope.YMax = geometry.Envelope.YMax;
            envelope.YMin = geometry.Envelope.YMin;
            envelope.Width = geometry.Envelope.Width;
            envelope.Height = geometry.Envelope.Height;
            IPoint point = new PointClass();
            point.SpatialReference = geometry.SpatialReference;
            point.X = (envelope.XMin + envelope.XMax) / 2.0;
            point.Y = (envelope.YMin + envelope.YMax) / 2.0;
            if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                if (envelope.XMax > 360 && envelope.YMax > 360)
                {
                    envelope.Width = 1000;
                    envelope.Height = 1000;
                }
                else
                {
                    envelope.Width = 0.01;
                    envelope.Height = 0.01;
                }
            }
            else
            {
                if (envelope.XMax == 0.0 && envelope.YMax == 0.0)
                {
                    envelope.Width = 10;
                    envelope.Height = 10;
                }
                else
                    envelope.Expand(expendRate, expendRate, true);
            }
            envelope.CenterAt(point);
            activeView.Extent = envelope;
            activeView.Refresh();
            return point;
        }
        /// <summary>
        /// 创建几何图形对应的用于高亮显示的元素
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static IElement CreateHightLightElement(IGeometry geometry)
        {
            IElement element = null;
            IColor redcolor = ColorCreate.GetIColor(255, 0, 0, 50);
            IColor bluecolor = ColorCreate.GetIColor(0, 0, 255);
            switch (geometry.GeometryType)
            {
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    element = new LineElementClass();
                    element.Geometry = geometry;
                    ((ILineElement)element).Symbol = SymbolCreate.GetSimpleLineSymbol(redcolor);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    element = new PolygonElementClass();
                    element.Geometry = geometry;
                    ((PolygonElementClass)element).Symbol = SymbolCreate.GetSimpleFillSymbol(redcolor, bluecolor);
                    break;
                case esriGeometryType.esriGeometryPoint:
                    element = new MarkerElementClass();
                    element.Geometry = geometry;
                    IMarkerSymbol pisymbol = new SimpleMarkerSymbolClass();
                    pisymbol.Color = (IColor)redcolor;
                    pisymbol.Size = 6;
                    ((MarkerElementClass)element).Symbol = pisymbol;
                    break;
            }
            return element;
        }
    }
}
