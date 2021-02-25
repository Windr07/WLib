/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Data;

namespace WLib.WinCtrls.Dev.Extension
{
    /// <summary>
    /// <see cref="ChartControl"/>统计图控件的扩展功能
    /// </summary>
    public static class ChartControlEx
    {
        /// <summary>
        /// 创建统计图上的一组值
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型(柱状图、折线图、饼状图)</param>
        /// <param name="fieldValueDict">键值对，以柱状图为例，键为柱状图每一个柱状条的数值说明(X)，值为柱状值(Y)</param>
        /// <returns></returns>
        public static Series CreateSeries(string caption, ViewType viewType, Dictionary<string, double> fieldValueDict)
        {
            Series series = new Series(caption, viewType);
            foreach (var pair in fieldValueDict)
            {
                series.Points.Add(new SeriesPoint(pair.Key, pair.Value));
            }
            return series;
        }
        /// <summary>
        /// 创建统计图上的一组值
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型(柱状图、折线图、饼状图)</param>
        /// <param name="fieldValueDict">键值对，以柱状图为例，键为柱状图每一个柱状条的数值说明(X)，值为柱状值(Y)</param>
        /// <returns></returns>
        public static Series CreateSeries(string caption, ViewType viewType, Dictionary<string, double[]> fieldValueDict)
        {
            Series series = new Series(caption, viewType);
            foreach (var pair in fieldValueDict)
            {
                series.Points.Add(new SeriesPoint(pair.Key, pair.Value));
            }
            return series;
        }
        /// <summary>
        /// 创建统计图上的一组值
        /// </summary>
        /// <param name="caption">图形标题</param>
        /// <param name="viewType">图形类型(柱状图、折线图、饼状图)</param>
        /// <param name="row">数据行，以柱状图为例，
        /// 该行的第一个字段值为数据说明，第二个字段值开始将作为柱状图上的柱状值进行显示，且字段值应为数值类型</param>
        /// <returns></returns>
        public static Series CreateSeries(string caption, ViewType viewType, DataRow row)
        {
            Series series = new Series(caption, viewType);
            for (int i = 1; i < row.Table.Columns.Count; i++)
            {
                string argument = row.Table.Columns[i].ColumnName;
                decimal value = (decimal)row[i];
                series.Points.Add(new SeriesPoint(argument, value));
            }
            return series;
        }
    }
}
