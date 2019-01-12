using AppLibrary.WriteExcel;

namespace WLib.Files.Excel.AppLibExcel.StyleHelper
{
    /// <summary>
    /// Excel单元格样式
    /// </summary>
    public class CellStyle
    {
        /// <summary>
        /// 创建一个表格单元格的样式，fontSize=0表示使用默认大小11,默认居中
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="cellLine">是否使用边框线</param>
        /// <param name="underline">字体是下划线样式，默认没有下划线</param> 
        /// <param name="fontSize">字体大小，0表示11</param> 
        /// <returns></returns>
        public static AppLibrary.WriteExcel.XF NewXf(XlsDocument doc, CellLine cellLine, UnderlineTypes underline = UnderlineTypes.None, int fontSize = 0)
        {
            int size = fontSize;
            if (fontSize == 0)
                size = 10;
            AppLibrary.WriteExcel.XF xf = doc.NewXF(); 
            xf.Font.FontName = "宋体";
            xf.Font.Height = (ushort)(size * 20);
            xf.Font.Underline = underline;
            xf.TextWrapRight = true;
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
            if (cellLine.ShowTop) xf.TopLineStyle = 1;
            if (cellLine.ShowRight) xf.RightLineStyle = 1;
            if (cellLine.ShowBottom) xf.BottomLineStyle = 1;
            if (cellLine.ShowLeft) xf.LeftLineStyle = 1;
            return xf;
        }
    }
}
