using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WLib.UserCtrls.ListCtrl
{
    /// <summary>
    /// 列表控件<see cref="ListBox"/>的扩展控件，在每一列表项的左侧显示图标，实现<see cref="IListControl"/>
    /// </summary>
    public partial class ImageListBox : ListBox, IListControl
    {
        /// <summary>
        /// 在每一列表项的左侧显示的图标
        /// </summary>
        public Image ItemImage { get; set; } = Properties.Resources.table;
        /// <summary>
        /// 列表控件<see cref="ListBox"/>的扩展控件，在每一列表项的左侧显示图标，实现<see cref="IListControl"/>
        /// </summary>
        public ImageListBox()
        {
            InitializeComponent();
            this.DrawItem += ListBox_DrawItem;
        }
        /// <summary>
        /// 重绘ListBox的Item显示，加上图标显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush myBrush = Brushes.Black;
            Color RowBackColorSel = Color.FromArgb(150, 200, 250);//选择项目颜色
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                myBrush = new SolidBrush(RowBackColorSel);
            else
                myBrush = new SolidBrush(Color.White);

            e.Graphics.FillRectangle(myBrush, e.Bounds);
            e.DrawFocusRectangle();//焦点框

            //绘制图标
            Image image = ItemImage;
            Rectangle bound = e.Bounds;
            Rectangle imgRec = new Rectangle(
                bound.X,
                bound.Y,
                bound.Height,
                bound.Height);
            Rectangle textRec = new Rectangle(
                imgRec.Right,
                bound.Y,
                bound.Width - imgRec.Right,
                bound.Height);
            if (image != null)
            {
                e.Graphics.DrawImage(
                    image,
                    imgRec,
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel);
                //绘制字体
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), textRec, stringFormat);
            }
        }


        #region 实现IListControl成员
        public new IList SelectedItems => base.SelectedItems;
        public new IList Items => base.Items;
        public void AddItems(IEnumerable<object> items) => base.Items.AddRange(items.ToArray());
        #endregion
    }
}
