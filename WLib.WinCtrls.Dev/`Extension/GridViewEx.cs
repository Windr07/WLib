using DevExpress.XtraGrid.Views.Grid;

namespace WLib.WinCtrls.Dev._Extension
{
    /// <summary>
    ///  提供<see cref="GridView"/>相关扩展方法
    /// </summary>
    public static class GridViewEx
    {
        /// <summary>
        /// <see cref="GridView"/>显示行号
        /// </summary>
        public static GridView ShowRowNumber(this GridView gridView)
        {
            gridView.IndicatorWidth = 30;
            gridView.CustomDrawRowIndicator -= gridView1_CustomDrawRowIndicator;
            gridView.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            return gridView;
        }
        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }


        /// <summary>
        ///  <see cref="GridView"/>禁用自动调整列宽，根据实际属性值长度调整列宽
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        public static GridView FitColumns(this GridView gridView)
        {
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.BestFitColumns();
            return gridView;
        }
    }
}
