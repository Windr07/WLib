//原出处：http://peterzb.cnblogs.com/
//下载来源: http://blog.csdn.net/xuwei296/article/details/8548121

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace WLib.UserCtrls.Viewer
{
    /// <summary>
    /// DataGridView行合并.请对属性MergeColumnNames 赋值即可
    /// </summary>
    public partial class RowMergeView : DataGridView
    {
        #region 构造函数
        public RowMergeView()
        {
            InitializeComponent();
        }
        #endregion

        #region 重写的事件
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    DrawCell(e);
                }
                else
                {
                    //二维表头
                    if (e.RowIndex == -1)
                    {
                        if (_spanRows.ContainsKey(e.ColumnIndex)) //被合并的列
                        {
                            //画边框
                            Graphics g = e.Graphics;
                            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                            int left = e.CellBounds.Left, top = e.CellBounds.Top + 2,
                            right = e.CellBounds.Right, bottom = e.CellBounds.Bottom;

                            switch (_spanRows[e.ColumnIndex].Position)
                            {
                                case 1:
                                    left += 2;
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    right -= 2;
                                    break;
                            }

                            //画上半部分底色
                            g.FillRectangle(new SolidBrush(this._mergecolumnheaderbackcolor), left, top,
                            right - left, (bottom - top) / 2);

                            //画中线
                            g.DrawLine(new Pen(this.GridColor), left, (top + bottom) / 2,
                            right, (top + bottom) / 2);

                            //写小标题
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            g.DrawString(e.Value + "", e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, (top + bottom) / 2, right - left, (bottom - top) / 2), sf);
                            left = this.GetColumnDisplayRectangle(_spanRows[e.ColumnIndex].Left, true).Left - 2;

                            if (left < 0) left = this.GetCellDisplayRectangle(-1, -1, true).Width;
                            right = this.GetColumnDisplayRectangle(_spanRows[e.ColumnIndex].Right, true).Right - 2;
                            if (right < 0) right = this.Width;

                            g.DrawString(_spanRows[e.ColumnIndex].Text, e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                            e.Handled = true;
                        }
                    }
                }
                base.OnCellPainting(e);
            }
            catch
            { }
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 画单元格
        /// </summary>
        /// <param name="e"></param>
        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(this.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellwidth;
            //上面相同的行数
            int upRows = 0;
            //下面相同的行数
            int downRows = 0;
            //总行数
            int count = 0;
            if (this.MergeColumnNames.Contains(this.Columns[e.ColumnIndex].Name) && e.RowIndex != -1)
            {
                cellwidth = e.CellBounds.Width;
                Pen gridLinePen = new Pen(gridBrush);
                string curValue = e.Value == null ? "" : e.Value.ToString().Trim();
                string curSelected = this.CurrentRow.Cells[e.ColumnIndex].Value == null ? "" : this.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(curValue))
                {
                    #region 获取下面的行数
                    for (int i = e.RowIndex; i < this.Rows.Count; i++)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            downRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    #region 获取上面的行数
                    for (int i = e.RowIndex; i >= 0; i--)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
                            upRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    count = downRows + upRows - 1;
                    if (count < 2)
                    {
                        return;
                    }
                }
                if (this.Rows[e.RowIndex].Selected)
                {
                    backBrush.Color = e.CellStyle.SelectionBackColor;
                    fontBrush.Color = e.CellStyle.SelectionForeColor;
                }
                //以背景色填充
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
                //画字符串
                PaintingFont(e, cellwidth, upRows, downRows, count);
                if (downRows == 1)
                {
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    count = 0;
                }
                // 画右边线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                e.Handled = true;
            }
        }
        /// <summary>
        /// 画字符串
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cellwidth"></param>
        /// <param name="upRows"></param>
        /// <param name="downRows"></param>
        /// <param name="count"></param>
        private void PaintingFont(System.Windows.Forms.DataGridViewCellPaintingEventArgs e, int cellwidth, int upRows, int downRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int fontheight = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int fontwidth = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int cellheight = e.CellBounds.Height;

            if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y + cellheight * downRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * downRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * downRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (upRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (upRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (upRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (upRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (upRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (upRows - 1));
            }
            else
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (upRows - 1) + (cellheight * count - fontheight) / 2);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 设置或获取合并列的集合，该列的单元格相同的值会自动合并
        /// </summary>
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [Description("设置或获取合并列的集合，该列的单元格相同的值会自动合并"), Browsable(true), Category("单元格合并")]
        public List<string> MergeColumnNames
        {
            get => _mergecolumnname;
            set => _mergecolumnname = value;
        }
        private List<string> _mergecolumnname = new List<string>();
        #endregion

        #region 二维表头
        private struct SpanInfo //表头信息
        {
            public SpanInfo(string text, int position, int left, int right)
            {
                this.Text = text;
                this.Position = position;
                this.Left = left;
                this.Right = right;
            }

            public string Text; //列主标题
            public int Position; //位置，1:左，2中，3右
            public int Left; //对应左行
            public int Right; //对应右行
        }
        private Dictionary<int, SpanInfo> _spanRows = new Dictionary<int, SpanInfo>();//需要2维表头的列
        /// <summary>
        /// 合并列
        /// </summary>
        /// <param name="colIndex">列的索引</param>
        /// <param name="colCount">需要合并的列数</param>
        /// <param name="text">合并列后的文本</param>
        public void AddSpanHeader(int colIndex, int colCount, string text)
        {
            if (colCount < 2)
            {
                throw new Exception("行宽应大于等于2，合并1列无意义。");
            }
            //将这些列加入列表
            int right = colIndex + colCount - 1; //同一大标题下的最后一列的索引
            _spanRows[colIndex] = new SpanInfo(text, 1, colIndex, right); //添加标题下的最左列
            _spanRows[right] = new SpanInfo(text, 3, colIndex, right); //添加该标题下的最右列
            for (int i = colIndex + 1; i < right; i++) //中间的列
            {
                _spanRows[i] = new SpanInfo(text, 2, colIndex, right);
            }
        }
        /// <summary>
        /// 清除合并的列
        /// </summary>
        public void ClearSpanInfo()
        {
            _spanRows.Clear();
            //ReDrawHead();
        }
        private void DataGridViewEx_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)// && e.Type == ScrollEventType.EndScroll)
            {
                timer1.Enabled = false; timer1.Enabled = true;
            }
        }
        //刷新显示表头
        public void ReDrawHead()
        {
            foreach (int si in _spanRows.Keys)
            {
                this.Invalidate(this.GetCellDisplayRectangle(si, -1, true));
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ReDrawHead();
        }
        /// <summary>
        /// 二维表头的背景颜色
        /// </summary>
        [Description("二维表头的背景颜色"), Browsable(true), Category("二维表头")]
        public Color MergeColumnHeaderBackColor
        {
            get => this._mergecolumnheaderbackcolor;
            set => this._mergecolumnheaderbackcolor = value;
        }
        private Color _mergecolumnheaderbackcolor = System.Drawing.SystemColors.Control;
        #endregion
    }

}
