/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Drawing;
using System.Windows.Forms;

namespace WLib.WinCtrls.GridViewCtrl
{
    /// <summary>
    /// 提供<see cref="DataGridView"/>相关扩展方法
    /// </summary>
    public static class GridViewOpt
    {
        /// <summary>
        /// <see cref="DataGridView"/>重新显示行号，但不会自动刷新行号
        /// </summary>
        public static void ShowRowNumber(this DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
        }
        /// <summary>
        /// <see cref="DataGridView"/>显示行号，行号自动刷新
        /// </summary>
        public static void ShowRowNumberDynamic(this DataGridView dataGridView)
        {
            dataGridView.RowPostPaint -= DataGridView_RowPostPaint;
            dataGridView.RowPostPaint += DataGridView_RowPostPaint;
        }

        private static void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dataGridView = (DataGridView)sender;
            var rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
              dataGridView.RowHeadersWidth, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView.RowHeadersDefaultCellStyle.Font, rectangle,
                dataGridView.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
