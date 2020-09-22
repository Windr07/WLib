/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7/25
// desc： 参考：https://www.cnblogs.com/yuefei/p/4062998.html
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace WLib.WinCtrls.ListCtrl
{
    /// <summary>
    /// 扩展的图片列表控件
    /// </summary>
    public partial class ImageListBoxEx : ListBox, IListControl
    {
        private object _mouseItem;
        private int _defaultImageIndex = -1;

        /// <summary>
        /// 列表项的关联信息（显示的图片的索引、是否显示删除按钮）
        /// </summary>
        public List<ItemInfo> ItemInfos { get; } = new List<ItemInfo>();
        /// <summary>
        /// 列表项对象显示在界面上的属性
        /// </summary>
        public List<string> ItemPropertyNames { get; } = new List<string>();
        /// <summary>
        /// 列表项候选图片列表
        /// </summary>
        public ImageList Images { get; set; }
        /// <summary>
        /// 默认列表项显示的图片的索引
        /// </summary>
        public int DefaultImageIndex
        {
            get => _defaultImageIndex;
            set => _defaultImageIndex = Images == null || value >= Images.Images.Count ? -1 : value;
        }
        /// <summary>
        /// 点击删除按钮触发的删除列表项事件
        /// </summary>
        public event EventHandler<MeasureItemEventArgs> DeleteItem;
        /// <summary>
        /// 图片的大小
        /// </summary>
        public int ImageSize { get; set; } = 16;
        /// <summary>
        /// 扩展的图片列表控件
        /// </summary>
        public ImageListBoxEx() : base()
        {
            //Items = new ObjectCollection(this);
            base.DrawMode = DrawMode.OwnerDrawVariable;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲   
            this.SetStyle(ControlStyles.ResizeRedraw, true); // 调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景. 
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); // 开启控件透明
        }


        /// <summary>
        /// 设置列表项显示的图片的索引
        /// </summary>
        /// <param name="index">列表项索引</param>
        /// <param name="imageIndex">列表项显示的图片索引</param>
        public void SetItemInfo(int index, int imageIndex)
        {
            var itemInfo = ItemInfos.FirstOrDefault(v => v.Index == index);
            if (itemInfo == null)
                ItemInfos.Add(new ItemInfo(index, imageIndex, true));
            else
                itemInfo.ImageIndex = imageIndex;
        }
        /// <summary>
        /// 设置列表项显示的图片的索引
        /// </summary>
        /// <param name="index">列表项索引</param>
        /// <param name="imageIndex">列表项显示的图片索引</param>
        public void SetItemInfo(int index, bool showDeleteButton)
        {
            var itemInfo = ItemInfos.FirstOrDefault(v => v.Index == index);
            if (itemInfo == null)
                ItemInfos.Add(new ItemInfo(index, DefaultImageIndex, showDeleteButton));
            else
                itemInfo.ShowDeleteButton = showDeleteButton;
        }
        /// <summary>
        /// 设置列表项的关联信息（显示的图片的索引、是否显示删除按钮）
        /// </summary>
        /// <param name="index">列表项索引</param>
        /// <param name="imageIndex">列表项显示的图片索引</param>
        /// <param name="showDeleteButton">是否显示删除按钮</param>
        public void SetItemInfo(int index, int imageIndex, bool showDeleteButton)
        {
            var itemInfo = ItemInfos.FirstOrDefault(v => v.Index == index);
            if (itemInfo == null)
                ItemInfos.Add(new ItemInfo(index, imageIndex, showDeleteButton));
            else
            { itemInfo.ImageIndex = imageIndex; itemInfo.ShowDeleteButton = showDeleteButton; }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (this.SelectedItem != null)//设置选中项的样式
            {
                Rectangle bounds = this.GetItemRectangle(SelectedIndex);
                using (SolidBrush brush = new SolidBrush(Color.LightSteelBlue))
                    g.FillRectangle(brush, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height));//绘制图片及显示信息区域
            }

            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                Rectangle bounds = this.GetItemRectangle(i);
                if (_mouseItem == item)
                {
                    Color leftColor = Color.FromArgb(192, 224, 248);
                    using (SolidBrush brush = new SolidBrush(leftColor))
                        g.FillRectangle(brush, new Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height));//绘制图片及显示信息区域

                    Color rightColor = Color.FromArgb(252, 233, 161);
                    using (SolidBrush brush = new SolidBrush(rightColor))
                        g.FillRectangle(brush, new Rectangle(bounds.Width - 40, bounds.Y, 40, bounds.Height));//绘制删除图标区域
                }

                int fontLeft = bounds.Left + ImageSize + 15;
                Font font = new Font("微软雅黑", 9);
                g.DrawString(item.ToString(), font, new SolidBrush(this.ForeColor), fontLeft, bounds.Top + 3);//绘制标题
                for (int j = 0; j < ItemPropertyNames.Count; j++)
                {
                    var property = item.GetType().GetProperty(ItemPropertyNames[j]);
                    if (property == null) continue;
                    var value = property.GetValue(item, null);
                    g.DrawString(value?.ToString(), font, new SolidBrush(Color.FromArgb(128, 128, 128)), fontLeft, bounds.Top + 20 + 15 * j);//绘制详细信息
                }

                var itemInfo = ItemInfos.Find(v => v.Index == i);
                if (Images != null && Images.Images.Count > 0)
                {
                    var index = itemInfo == null ? DefaultImageIndex : itemInfo.ImageIndex;
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.DrawImage(Images.Images[index], new Rectangle(bounds.X + 3, bounds.Y + 3, ImageSize, ImageSize));//绘制左侧图标
                }
                if (itemInfo == null || itemInfo.ShowDeleteButton)
                    g.DrawImage(Properties.Resources.close, new Rectangle(bounds.Width - 28, (bounds.Height - 16) / 2 + bounds.Top, 16, 16));//绘制右侧删除图标
            }
            base.OnPaint(e);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (Items.Count > 0)
            {
                e.ItemHeight = ItemPropertyNames == null ? 25 : 25 + 15 * ItemPropertyNames.Count;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            for (int i = 0; i < Items.Count; i++)
            {
                Rectangle bounds = this.GetItemRectangle(i);
                if (bounds.Contains(e.X, e.Y))
                {
                    if (Items[i] != _mouseItem)
                        _mouseItem = Items[i];
                    this.Invalidate();
                    break;
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            for (int i = 0; i < Items.Count; i++)
            {
                Rectangle bounds = this.GetItemRectangle(i);
                Rectangle deleteBounds = new Rectangle(bounds.Width - 28, (bounds.Height - 16) / 2 + bounds.Top, 16, 16);

                if (bounds.Contains(e.X, e.Y))
                {
                    if (Items[i] != _mouseItem)
                        _mouseItem = Items[i];

                    if (_mouseItem != null && deleteBounds.Contains(e.X, e.Y))
                    {
                        object deleteItem = _mouseItem;
                        var itemInfo = ItemInfos.Find(v => v.Index == i);
                        if (itemInfo == null || itemInfo.ShowDeleteButton)
                            if (MessageBox.Show("确定要删除此项？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                if (this.DataSource == null && this.DataBindings.Count < 0)
                                    this.Items.Remove(deleteItem);
                                else
                                    DeleteItem?.Invoke(this, new MeasureItemEventArgs(null, i));
                            }
                    }
                    this.Invalidate();
                    break;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this._mouseItem = null;
            this.Invalidate();
        }


        #region 实现IListControl接口
        public new IList SelectedItems => base.SelectedItems;

        IList IListControl.Items => Items;

        public void AddItems(IEnumerable<object> items) => Items.AddRange(items.ToArray());
        #endregion
    }
}
