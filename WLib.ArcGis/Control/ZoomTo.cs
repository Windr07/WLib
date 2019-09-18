/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using WLib.ArcGis.Analysis.OnShape;
using WLib.ArcGis.Display;
using WLib.ArcGis.GeoDatabase.FeatClass;

namespace WLib.ArcGis.Control
{
    /// <summary>
    /// 缩放显示图斑的操作
    /// </summary>
    public static class ZoomTo
    {
        #region 缩放至图斑
        /// <summary>
        /// 地图缩放至指定点的位置，visualRange是显示范围Evelope的宽度(或高度)
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="point"></param>
        /// <param name="visualRange"></param>
        public static void MapZoomToPoint(this IActiveView activeView, IPoint point, double visualRange)
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
        }
        /// <summary>
        /// 地图缩放至指定图形位置
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        /// <param name="expendRate">实际显示范围与图形范围框的比值，即放大的倍数</param>
        public static void MapZoomTo(this IActiveView activeView, IGeometry geometry, double expendRate = 2)
        {
            if (activeView != null && geometry != null && !geometry.IsEmpty)
            {
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
                    {
                        envelope.Expand(expendRate, expendRate, true);
                    }
                }
                envelope.CenterAt(point);
                activeView.Extent = envelope;
                activeView.Refresh();
            }
        }
        /// <summary>
        /// 地图定跳转到指定图形位置
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometries"></param>
        /// <param name="expendRate">实际显示范围与图形范围框的比值，即放大的倍数</param>
        public static void MapZoomTo(this IActiveView activeView, IEnumerable<IGeometry> geometries, double expendRate = 2)
        {
            var unionGeometry = TopologicalOpt.UnionGeometry(geometries);
            MapZoomTo(activeView, unionGeometry, expendRate);
        }
        #endregion


        #region 缩放至图斑、高亮显示
        /// <summary>
        /// 地图定跳转到指定图形位置(单个图形)，并高亮显示此图形，会清除其它高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="point"></param>
        /// <param name="visualRange"></param>
        public static void MapZoomToPointAndHightLight(this IActiveView activeView, IPoint point, double visualRange)
        {
            activeView.GraphicsContainer.DeleteAllElements();
            MapZoomToPoint(activeView, point, visualRange);
            HightLightGeo(activeView, point);
        }
        /// <summary>
        /// 地图缩放至指定点集的第一个点的位置，visualRange是显示范围Evelope的宽度(或高度)，并高亮显示这些点，会清除其它高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="points"></param>
        /// <param name="visualRange"></param>
        public static void MapZoomToPointAndHightLight(this IActiveView activeView, IPoint[] points, double visualRange)
        {
            activeView.GraphicsContainer.DeleteAllElements();
            MapZoomToPoint(activeView, points[0], visualRange);
            HightLightGeo(activeView, points);
        }
        /// <summary>
        /// 地图缩放至指定图形位置(单个图形)，并高亮显示此图形，会清除其它高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        public static void MapZoomToAndHightLight(this IActiveView activeView, IGeometry geometry)
        {
            activeView.GraphicsContainer.DeleteAllElements();
            MapZoomTo(activeView, geometry);
            HightLightGeo(activeView, geometry);
        }
        /// <summary>
        /// 地图缩放至指定的多个图形，并高亮显示这些图形，会清除其它高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geomtries"></param>
        public static void MapZoomToAndHightLight(this IActiveView activeView, IEnumerable<IGeometry> geomtries)
        {
            activeView.GraphicsContainer.DeleteAllElements();
            var unionGeometry = TopologicalOpt.UnionGeometry(geomtries);
            foreach (IGeometry geometry in geomtries)
                HightLightGeo(activeView, geometry);

            MapZoomTo(activeView, unionGeometry);
        }
        /// <summary>
        /// 地图缩放至查询所得的所有图形，并高亮显示这些图形，会清除其它高亮
        /// </summary>
        /// <param name="activeView">地图控件</param>
        /// <param name="featureLayer">查询图层</param>
        /// <param name="whereClause">查询条件</param>
        public static void MapZoomToAndHightLight(this IActiveView activeView, IFeatureLayer featureLayer, string whereClause)
        {
            var geometries = featureLayer.FeatureClass.QueryGeometries(whereClause);
            MapZoomToAndHightLight(activeView, geometries);
        }

        /// <summary>
        /// 高亮显示指定图形,不会清除原有的高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometry"></param>
        public static void HightLightGeo(this IActiveView activeView, IGeometry geometry)
        {
            IElement element = CreateHightLightElement(geometry);
            if (element != null)
                activeView.GraphicsContainer.AddElement(element, 0);
            activeView.PartialRefresh(esriViewDrawPhase.esriViewForeground, geometry, activeView.Extent);
        }
        /// <summary>
        ///  高亮显示多个图形,不会清除原有的高亮
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="geometries"></param>
        public static void HightLightGeo(this IActiveView activeView, IGeometry[] geometries)
        {
            foreach (var geo in geometries)
            {
                IElement element = CreateHightLightElement(geo);
                if (element != null)
                    activeView.GraphicsContainer.AddElement(element, 0);
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewForeground, geometries, activeView.Extent);
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
        #endregion


        #region 缩放至要素、选中要素
        /// <summary>
        /// 地图缩放并选中至指定要素
        /// </summary>
        /// <param name="axMapControl"></param>
        /// <param name="featureLayer"></param>
        /// <param name="feature"></param>
        public static void MapZoomToAndSelect(this AxMapControl axMapControl, IFeatureLayer featureLayer, IFeature feature)
        {
            if (feature != null)
            {
                axMapControl.Map.ClearSelection();
                axMapControl.Map.SelectFeature(featureLayer, feature);
                MapZoomTo(axMapControl.ActiveView, feature.ShapeCopy);
            }
        }
        /// <summary>
        /// 地图缩放并选中至查询获得的第一个要素
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="featureLayer">查询图层</param>
        /// <param name="whereClause">查询条件</param>
        public static void MapZoomToAndSelectFirst(this AxMapControl axMapControl, IFeatureLayer featureLayer, string whereClause)
        {
            IQueryFilter filter = new QueryFilterClass();
            filter.WhereClause = whereClause;
            IFeatureCursor cusor = featureLayer.FeatureClass.Search(filter, true);
            IFeature feature = cusor.NextFeature();
            if (feature != null)
                MapZoomToAndSelect(axMapControl, featureLayer, feature);

            System.Runtime.InteropServices.Marshal.ReleaseComObject(cusor);
        }
        #endregion


        #region 缩放至图斑、闪烁图斑
        /// <summary>
        /// 地图缩放至指定图形并闪烁
        /// </summary>
        /// <param name="axMapControl">地图控件</param>
        /// <param name="geometry">闪烁的图像</param>
        /// <param name="nFlashes">闪烁次数</param>
        public static void MapZoomToAndFlash(this AxMapControl axMapControl, IGeometry geometry, int nFlashes = 2)
        {
            MapZoomTo(axMapControl.ActiveView, geometry);

            //地图闪烁
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
    }
}
