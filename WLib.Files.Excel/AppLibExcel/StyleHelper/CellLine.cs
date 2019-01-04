namespace WLib.Files.Excel.AppLibExcel.StyleHelper
{
    /// <summary>
    /// 单元格是否显示边线，默认不显示
    /// </summary>
    public class CellLine
    {
        /// <summary>
        /// 显示左边线
        /// </summary>
        public bool ShowLeft { get; set; }
        /// <summary>
        /// 显示上边线
        /// </summary>
        public bool ShowTop { get; set; }
        /// <summary>
        /// 显示右边线
        /// </summary>
        public bool ShowRight { get; set; }
        /// <summary>
        /// 显示下边线
        /// </summary>
        public bool ShowBottom { get; set; }


        /// <summary>
        /// 设置是否显示边线，默认不显示
        /// </summary>
        public CellLine()
        {

        }
        /// <summary>
        /// 设置是否显示边线（左、上、右、下）
        /// </summary>
        /// <param name="showLeft">显示左边线</param>
        /// <param name="showTop">显示上边线</param>
        /// <param name="showRight">显示右边线</param>
        /// <param name="showBottom">显示下边线</param>
        public CellLine(bool showLeft, bool showTop, bool showRight, bool showBottom)
        {
            this.ShowLeft = showLeft;
            this.ShowTop = showTop;
            this.ShowRight = showRight;
            this.ShowBottom = showBottom;
        }
    }

}
