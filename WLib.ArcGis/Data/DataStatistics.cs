/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;
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
        /// <param name="fieldName">需要统计字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <returns></returns>
        public static IStatisticsResults Statistics(this IFeatureClass featureClass, string fieldName, string whereClause)
        {
            QueryFilter queryFilter = new QueryFilter { WhereClause = whereClause };
            IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);
            ICursor cursor = (ICursor)featureCursor;
            IStatisticsResults result = new DataStatisticsClass { Field = fieldName, Cursor = cursor }.Statistics;

            Marshal.ReleaseComObject(cursor);
            return result;
        }
        /// <summary>
        /// 筛选记录并统计
        /// </summary>
        /// <param name="table">需要统计的表</param>
        /// <param name="fieldName">需要统计字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <returns></returns>
        public static IStatisticsResults Statistics(this ITable table, string fieldName, string whereClause)
        {
            QueryFilter queryFilter = new QueryFilter { WhereClause = whereClause };
            ICursor cursor = table.Search(queryFilter, false);
            IStatisticsResults statisticsResults = new DataStatisticsClass { Field = fieldName, Cursor = cursor }.Statistics;

            Marshal.ReleaseComObject(cursor);
            return statisticsResults;
        }


        /// <summary>
        /// 获得字段统计值
        /// </summary>
        /// <param name="featureClass">统计的表格</param>
        /// <param name="fieldName">统计的字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <param name="eType">统计值类型</param>
        /// <returns></returns>
        public static double Statistics(this IFeatureClass featureClass, string fieldName, string whereClause, EStatisticsType eType)
        {
            IStatisticsResults statisticsResults = Statistics(featureClass, fieldName, whereClause);
            return StatisticsByType(statisticsResults, eType);
        }
        /// <summary>
        /// 获得字段统计值
        /// </summary>
        /// <param name="table">统计的表格</param>
        /// <param name="fieldName">统计的字段</param>
        /// <param name="whereClause">统计条件</param>
        /// <param name="eType">统计值类型</param>
        /// <returns></returns>
        public static double Statistics(this ITable table, string fieldName, string whereClause, EStatisticsType eType)
        {
            IStatisticsResults statisticsResults = Statistics(table, fieldName, whereClause);
            return StatisticsByType(statisticsResults, eType);
        }


        /// <summary>
        /// 根据统计类型返回统计值
        /// </summary>
        /// <param name="statisticsResults"></param>
        /// <param name="eType">统计值类型</param>
        /// <returns></returns>
        private static double StatisticsByType(IStatisticsResults statisticsResults, EStatisticsType eType)
        {
            switch (eType)
            {
                case EStatisticsType.Count: return statisticsResults.Count;
                case EStatisticsType.Maximum: return statisticsResults.Maximum;
                case EStatisticsType.Mean: return statisticsResults.Mean;
                case EStatisticsType.Minimum: return statisticsResults.Minimum;
                case EStatisticsType.Sum: return statisticsResults.Sum;
                case EStatisticsType.StandardDeviation: return statisticsResults.StandardDeviation;
                default: throw new NotImplementedException();
            }
        }
    }
}
