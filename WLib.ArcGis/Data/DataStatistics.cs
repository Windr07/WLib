/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// 提供图层或表的数据统计方法
    /// </summary>
    public static class DataStatistics
    {
        /// <summary>
        /// 筛选记录并统计
        /// </summary>
        /// <param name="featureClass">需要统计的要素类</param>
        /// <param name="statisticField">需要统计字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <returns></returns>
        public static IStatisticsResults Statistics(this IFeatureClass featureClass, string statisticField, string whereClause)
        {
            QueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = whereClause;
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
            ICursor cursor = featureCursor as ICursor;

            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = statisticField;
            dataStatistics.Cursor = cursor;
            IStatisticsResults statisticsResults = dataStatistics.Statistics;
            return statisticsResults;
        }

        /// <summary>
        /// 筛选记录并统计
        /// </summary>
        /// <param name="table">需要统计的表</param>
        /// <param name="statisticField">需要统计字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <returns></returns>
        public static IStatisticsResults Statistics(this ITable table, string statisticField, string whereClause)
        {
            QueryFilter queryFilter = new QueryFilter();
            queryFilter.WhereClause = whereClause;
            ICursor cursor = table.Search(queryFilter, false);

            IDataStatistics dataStatistics = new DataStatisticsClass();
            dataStatistics.Field = statisticField;
            dataStatistics.Cursor = cursor;
            IStatisticsResults statisticsResults = dataStatistics.Statistics;

            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
            return statisticsResults;
        }

        /// <summary>
        /// 筛选记录并求和
        /// </summary>
        /// <param name="featureClass">需要统计的要素类</param>
        /// <param name="statisticField">需要统计字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <returns></returns>
        public static double StatisticsSum(this IFeatureClass featureClass, string statisticField, string whereClause)
        {
            return Statistics(featureClass, statisticField, whereClause).Sum;
        }
    }
}
