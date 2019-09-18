/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： 自定义的DataGridView进度条列，
          设置该列的单元格值则改变进度条的值（e.g. dataGridView.Rows[0].Cells["colProgressBar"].Value = 100）
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WLib.WinCtrls.GridViewCtrl
{
    /// <summary>
    /// 承载一个 <see cref="DataGridViewProgressCell"/> 对象集合。
    /// </summary>
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        /// <summary>
        /// 承载一个 <see cref="DataGridViewProgressCell"/> 对象集合。
        /// </summary>
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }
    /// <summary>
    /// 显示 <see cref="DataGridView"/> 控件中的进度条
    /// </summary>
    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(Graphics g, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            float progressVal;
            if (value != null)
                progressVal = Convert.ToSingle(value);
            else
                progressVal = 1;


            float percentage = progressVal / 100.0f; // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds,
             rowIndex, cellState, value, formattedValue, errorText,
             cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            StringFormat StringFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
            RectangleF Rectangle = new RectangleF(cellBounds.X, cellBounds.Y, cellBounds.Width, cellBounds.Height);
            if (percentage >= 0.0)
            {
                // Draw the progress bar and the text
                g.FillRectangle(new SolidBrush(Color.FromArgb(163, 189, 242)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32(percentage * cellBounds.Width - 4), cellBounds.Height - 4);
                g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, Rectangle, StringFormat);
            }
            else
            {
                if (DataGridView.CurrentRow.Index == rowIndex)
                    g.DrawString(progressVal.ToString("#0.0") + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), Rectangle, StringFormat);
                else
                    g.DrawString(progressVal.ToString("#0.0") + "%", cellStyle.Font, foreColorBrush, Rectangle, StringFormat);
            }
        }
    }
}
