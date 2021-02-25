/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.WinCtrls.ListCtrl
{
    /// <summary>
    /// <see cref="ImageListBoxEx"/>的列表项的索引及列表项对应关联信息（图片索引、是否显示删除按钮）
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// 列表项索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 列表项显示的图片的索引
        /// </summary>
        public int ImageIndex { get; set; }
        /// <summary>
        /// 列表项是否显示删除按钮
        /// </summary>
        public bool ShowDeleteButton { get; set; }
        /// <summary>
        ///  <see cref="ImageListBoxEx"/>的列表项的索引及列表项对应关联信息（图片索引、是否显示删除按钮）
        /// </summary>
        /// <param name="index"></param>
        /// <param name="imageIndex"></param>
        /// <param name="showDeleteButton"></param>
        public ItemInfo(int index, int imageIndex, bool showDeleteButton)
        {
            Index = index;
            ImageIndex = imageIndex;
            ShowDeleteButton = showDeleteButton;
        }
    }
}
