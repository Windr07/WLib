/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Carto.Map
{
    /// <summary>
    /// 地图设置操作（包括设置比例尺等）
    /// </summary>
    public class MapOption
    {
        /// <summary>
        /// 设置地图为固定比例尺模式，并设定其比例尺
        /// </summary>
        public static void SetMapScale(IMap map, double scale)
        {
            #region ArcGIS 10.0及以上版本使用的代码
            IMapAutoExtentOptions mapAutoExtentOptions = map as IMapAutoExtentOptions;
            mapAutoExtentOptions.AutoExtentType = esriExtentTypeEnum.esriExtentScale;//使用固定比例尺模式
            mapAutoExtentOptions.AutoExtentScale = scale;
            #endregion
        }

        /// <summary>
        /// 从比例尺列表中，选择能够展示指定要素范围的最合适比例尺
        /// </summary>
        /// <param name="pageLayout">页面布局</param>
        /// <param name="mapFrame">要设定显示范围及比例尺的地图数据框</param>
        /// <param name="featureEnv">地图要素显示范围</param>
        /// <param name="scaleList">比例尺列表，将从中选取最合适的比例尺</param>
        /// <param name="bufferDistance">要素与图廓的缓冲距离(厘米)，即地图实际显示范围是featEnvelope + 2 * bufferDistance</param>
        /// <returns></returns>
        public static double SetMapScale(IPageLayout pageLayout, IMapFrame mapFrame,
            IEnvelope featureEnv, double[] scaleList, double bufferDistance = 2)
        {
            esriUnits pageUnit = pageLayout.Page.Units;
            esriUnits mapUnit = mapFrame.Map.DistanceUnits;
            esriUnits cmUnit = esriUnits.esriCentimeters; //厘米

            IEnvelope mapFrameEnv = (mapFrame as IElement)?.Geometry.Envelope;
            IUnitConverter unitConverter = new UnitConverterClass();
            double wScale = unitConverter.ConvertUnits(featureEnv.Width, mapUnit, cmUnit) / (unitConverter.ConvertUnits(mapFrameEnv.Width, pageUnit, cmUnit) - 2 * bufferDistance);
            double hScale = unitConverter.ConvertUnits(featureEnv.Height, mapUnit, cmUnit) / (unitConverter.ConvertUnits(mapFrameEnv.Height, pageUnit, cmUnit) - 2 * bufferDistance);
            double dScale = wScale > hScale ? wScale : hScale;

            int maxIndex = 0;
            while (maxIndex < scaleList.Length && dScale > scaleList[maxIndex])
                maxIndex++;//寻找比要素全图廓显示所确定的比例尺小一级的比例尺

            if (maxIndex == scaleList.Length)
                throw new Exception("需要更小的比例尺！");

            return scaleList[maxIndex];
        }

        /// <summary>
        /// 从比例尺列表中，选择能够展示指定要素范围的最合适比例尺，对地图设定固定比例尺
        /// </summary>
        /// <param name="pageLayout">页面布局</param>
        /// <param name="mapFrame">要设定显示范围及比例尺的地图数据框</param>
        /// <param name="featureEnv">地图要素显示范围</param>
        /// <param name="scaleList">比例尺列表，将从中选取最合适的比例尺</param>
        /// <param name="bufferDistance">要素与图廓的缓冲距离(厘米)，即地图实际显示范围是featEnvelope + 2 * bufferDistance</param>
        /// <param name="isFixedScale">是否设定固定比例尺</param>
        /// <returns></returns>
        public static double SetMapScale(IPageLayout pageLayout, IMapFrame mapFrame,
            IEnvelope featureEnv, double[] scaleList, double bufferDistance = 2, bool isFixedScale = true)
        {
            double scale = SetMapScale(pageLayout, mapFrame, featureEnv, scaleList, bufferDistance);

            ((IActiveView)mapFrame.Map).Extent = featureEnv;
            if (isFixedScale)
                SetMapScale(mapFrame, scale, featureEnv);

            return scale;
        }

        /// <summary>
        /// 设置地图为固定比例尺模式，并设定其比例尺（及显示范围和坐标系）
        /// </summary>
        /// <param name="mapFrame">地图数据框</param>
        /// <param name="nScale">比例尺</param>
        /// <param name="featureEnv">显示范围，值为null时不修改显示范围</param>
        /// <param name="spatialRef">坐标系，值为null是不修改坐标系</param>
        public static void SetMapScale(IMapFrame mapFrame, double nScale,
            IEnvelope featureEnv = null, ISpatialReference spatialRef = null)
        {
            #region ArcGIS 9.3版本使用的代码
            //if (spatialRef != null)
            //    mapFrame.Map.SpatialReference = spatialRef;
            //if (featureEnv != null)
            //    (mapFrame.Map as IActiveView).Extent = featureEnv;
            //mapFrame.ExtentType = esriExtentTypeEnum.esriExtentScale;
            //mapFrame.MapScale = nScale;
            #endregion

            #region ArcGIS 10.0及以上版本使用的代码
            if (spatialRef != null)
                mapFrame.Map.SpatialReference = spatialRef;
            if (featureEnv != null)
                ((IActiveView) mapFrame.Map).Extent = featureEnv;
            IMapAutoExtentOptions mapAutoExtentOptions = mapFrame.Map as IMapAutoExtentOptions;
            mapAutoExtentOptions.AutoExtentType = esriExtentTypeEnum.esriExtentScale;//使用固定比例尺模式
            if (nScale != 0.0)
                mapAutoExtentOptions.AutoExtentScale = nScale;
            #endregion
        }
    }
}
