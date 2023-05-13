/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/3/16 10:13:40
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace ClimateWebApi.Util
{
    //NPOI是一个开源的C#读写Excel的项目（不需要安装MS Office即可读写Excel文档）:https://github.com/tonyqus/npoi
    //使用NPOI时，请注意引用NPOI.dll, NPOI.OOXML.dll, NPOI.OpenXml4Net.dll, NPOI.OpenXmlFormats.dll

    /// <summary>
    /// NPOI操作Excel的帮助类
    /// </summary>
    public static class NPOIHelper
    {
        /// <summary>
        /// 打开Excel工作簿（兼容xls和xlsx）
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="fileAccess"></param>
        /// <returns></returns>
        public static IWorkbook OpenWorkbook(string filePath, FileAccess fileAccess = FileAccess.Read)
        {
            IWorkbook workbook = null;
            string fileExt = Path.GetExtension(filePath);
            using (var file = new FileStream(filePath, FileMode.Open, fileAccess))
            {
                if (fileExt == ".xls")
                    workbook = new HSSFWorkbook(file);
                else if (fileExt == ".xlsx")
                    workbook = new XSSFWorkbook(file);
            }
            return workbook;
        }
        /// <summary>
        /// 保存工作簿到指定目录，文件已存在时则覆盖
        /// </summary>
        /// <param name="workbook">指定要保存的工作簿</param>
        /// <param name="savePath">指定保存工作簿的路径</param>
        public static void SaveWoorkbook(this IWorkbook workbook, string savePath)
        {
            FileStream fileStream = new FileStream(savePath, FileMode.Create);
            workbook.Write(fileStream);
            fileStream.Close();
        }
        /// <summary>
        /// 新建Execl表格并设置单元格为文本格式 
        /// </summary>
        /// <param name="createRowCount">所需创建行数</param>
        /// <param name="createColCount">所需创建列数</param>
        /// <returns></returns>
        public static IWorkbook CreatWorkbook(int createRowCount, int createColCount)
        {
            IWorkbook workbook = new HSSFWorkbook();
            workbook.CreateSheet("Sheet1");
            workbook.CreateSheet("Sheet2");
            workbook.CreateSheet("Sheet3");

            IRow rows;
            ISheet sheet = workbook.GetSheetAt(0);

            //创建CellStyle与DataFormat并加载格式样式  
            IDataFormat dataformat = workbook.CreateDataFormat();
            ICellStyle style = workbook.CreateCellStyle();
            style.DataFormat = dataformat.GetFormat("text");
            for (int i = 0; i < createRowCount; i++)
            {
                rows = sheet.CreateRow(i);
                for (int j = 0; j < createColCount; j++)
                {
                    var cell = rows.CreateCell(j);
                    sheet.GetRow(i).GetCell(j).CellStyle = style;
                    workbook.SetBorderLine(i, j);
                }
            }
            return workbook;
        }


        /// <summary>
        /// 获取Excel指定单元格的字符串值，去除前后空白字符
        /// （单元格cell为空则返回string.Empty，若为日期则返回yyyy/MM/dd格式的日期字符串）
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row">行号，从0开始</param>
        /// <param name="col">列号，从0开始</param>
        /// <returns></returns>
        public static string GetCellValue(this ISheet sheet, int row, int col)
        {
            ICell cell = sheet.GetRow(row).GetCell(col);
            if (cell == null || cell.ToString().Trim() == string.Empty)
                return string.Empty;
            if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                return cell.DateCellValue.ToString("yyyy/MM/dd");

            return cell.ToString().Trim();
        }
        /// <summary>
        /// 复制某一行的单元格格式到目标行中
        /// </summary>
        /// <param name="sourceStyleRow"></param>
        /// <param name="desRow"></param>
        public static void CopyCellStyle(this IRow sourceStyleRow, IRow desRow)
        {
            IRow firstTargetRow = desRow;
            ICell firstSourceCell;
            ICell firstTargetCell;

            for (int m = sourceStyleRow.FirstCellNum; m < sourceStyleRow.LastCellNum; m++)
            {
                firstSourceCell = sourceStyleRow.GetCell(m);
                if (firstSourceCell == null)
                    continue;
                firstTargetCell = firstTargetRow.CreateCell(m);
                //firstTargetCell.Encoding = firstSourceCell.Encoding;
                firstTargetCell.CellStyle = firstSourceCell.CellStyle;
                firstTargetCell.SetCellType(firstSourceCell.CellType);
            }
        }
        /// <summary>
        /// 合并单元格并赋值
        /// </summary>
        /// <param name="workbook">指定工作簿</param>
        /// <param name="sheet">指定工作表</param>
        /// <param name="value">需要单元格的值</param>
        /// <param name="row">开始行号（从0开始）</param>
        /// <param name="col">开始列号（从0开始）</param>
        /// <param name="eRow">结束行号（从0开始）</param>
        /// <param name="eCol">结束列号（从0开始）</param>
        public static void MergeCells(this ISheet sheet, object value, int row, int eRow, int col, int eCol)
        {
            sheet.AddMergedRegion(new CellRangeAddress(row, eRow, col, eCol));
            sheet.GetRow(row).GetCell(col).SetCellValue(value.ToString());
        }
        /// <summary>
        /// 获取合并单元格的值
        /// </summary>
        /// <param name="sheet">指定工作表</param>
        /// <param name="rowIndex">行号（从0开始）</param>
        /// <param name="columnIndex">列号（从0开始）</param>
        /// <param name="firstColumnIndex">返回合并单元格的列号（合并范围的第一列的列号）</param>
        /// <param name="firstRowIndex">返回合并单元格的行号（合并范围的第一行的行号）</param>
        /// <returns></returns>
        public static string GetMergedRegionValue(this ISheet sheet, int rowIndex, int columnIndex, out int firstColumnIndex, out int firstRowIndex)
        {
            for (int i = 0; i < sheet.NumMergedRegions; i++)//遍历Sheet中所有的合并单元格
            {
                CellRangeAddress cellRange = sheet.GetMergedRegion(i);//获取第i个合并单元格
                int firstColumn = cellRange.FirstColumn;
                int lastColumn = cellRange.LastColumn;
                int firstRow = cellRange.FirstRow;
                int lastRow = cellRange.LastRow;
                if (rowIndex >= firstRow && rowIndex <= lastRow &&
                    columnIndex >= firstColumn && columnIndex <= lastColumn)
                {
                    IRow tmpRow = sheet.GetRow(firstRow);
                    ICell tmpCell = tmpRow.GetCell(firstColumn);
                    firstColumnIndex = tmpCell.ColumnIndex;
                    firstRowIndex = tmpCell.RowIndex;
                    tmpCell.SetCellType(CellType.String);
                    return tmpCell.StringCellValue;
                }
            }
            firstColumnIndex = firstRowIndex = -1;
            return null;
        }


        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sheet">指定操作的Sheet</param>
        /// <param name="insertRowIndex">指定在第几行插入（插入行的位置，从0开始算）</param>
        /// <param name="insertRowCount">指定要插入多少行</param>
        /// <param name="sourceStyleRow">源单元格格式的行，注意源格式行应为插入行之前的行（从0开始算）</param>
        public static void InsertRow(this ISheet sheet, int insertRowIndex, int insertRowCount, IRow sourceStyleRow, int lastRowNum = -1)
        {
            if (lastRowNum < 0)
                lastRowNum = sheet.LastRowNum;

            //批量移动行
            sheet.ShiftRows(
                insertRowIndex,                        //--开始行
                lastRowNum,                            //--结束行
                insertRowCount,                        //--移动大小(行数)--往下移动
                true,                                  //是否复制行高
                false//,                               //是否重置行高
                     //true                                 //是否移动批注
            );

            //对批量移动后空出的空行插，创建相应的行，并以插入行的上一行为格式源(即：插入行-1的那一行)
            for (int i = insertRowIndex; i < insertRowIndex + insertRowCount - 1; i++)
            {
                sourceStyleRow.CopyCellStyle(sheet.CreateRow(i + 1));
            }

            sourceStyleRow.CopyCellStyle(sheet.GetRow(insertRowIndex));
        }


        /// <summary>
        /// 设置边框
        /// </summary>
        /// <param name="workbook">工作表</param>
        /// <param name="row">设置行（从0开始算）</param>
        /// <param name="col">设置列（从0开始算）</param>
        public static void SetBorderLine(this IWorkbook workbook, int row, int col)
        {
            ISheet sheet = workbook.GetSheetAt(0);
            ICell cell = sheet.GetRow(row).GetCell(col);
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            cell.CellStyle = style;
        }
        /// <summary>
        /// 设置单元格为无边框
        /// </summary>
        /// <param name="workbook">工作表</param>
        /// <param name="row">开始行（从0开始算）</param>
        /// <param name="col">开始列（从0开始算）</param>
        /// <param name="endRow">结束行（从0开始算）</param>
        /// <param name="endCol">结束列（从0开始算）</param>
        public static void SetBorderLine(this IWorkbook workbook, int row, int col, int endRow, int endCol)
        {
            ISheet sheet = workbook.GetSheetAt(0);
            for (int i = row; i <= endRow; i++)
            {
                for (int j = col; j <= endCol; j++)
                {
                    ICell cell = sheet.GetRow(i).GetCell(j);
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    style.BorderTop = BorderStyle.None;
                    style.BorderLeft = BorderStyle.None;
                    style.BorderRight = BorderStyle.None;
                    style.BorderBottom = BorderStyle.None;
                    cell.CellStyle = style;
                }

            }

        }



        /// <summary>
        /// 设置字符串的字体
        /// </summary>
        /// <param name="workbook">需要应用字体的工作簿</param>
        /// <param name="richText">单元格上的字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称</param>
        public static IFont ApplyFont(this IWorkbook workbook, IRichTextString richText, short fontSize, string fontName = "宋体")
        {
            IFont font = workbook.CreateFont();
            font.Underline = FontUnderlineType.None;
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            richText.ApplyFont(0, richText.Length, font);
            return font;
        }
        /// <summary>
        /// 设置字符串的字体
        /// </summary>
        /// <param name="sheet">需要应用字体的工作表</param>
        /// <param name="richText">单元格上的字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="fontName">字体名称</param>
        public static IFont ApplyFont(this ISheet sheet, IRichTextString richText, short fontSize, string fontName = "宋体")
        {
            return sheet.Workbook.ApplyFont(richText, fontSize, fontName);
        }
        /// <summary>
        /// 在字符串的指定起止位置设置下划线
        /// </summary>
        /// <param name="workbook">需要添加下划线的工作簿</param>
        /// <param name="richText">单元格上的字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="startIndex">添加下划线的起始字符索引</param>
        /// <param name="endIndex">添加下划线的末尾字符索引</param>
        /// <param name="fontName">字体名称</param>
        public static IFont SetUnderline(this IWorkbook workbook, IRichTextString richText, short fontSize, int startIndex, int endIndex, string fontName = "宋体")
        {
            IFont font = workbook.CreateFont();
            font.Underline = FontUnderlineType.Single;
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            richText.ApplyFont(startIndex, endIndex, font);
            return font;
        }
        /// <summary>
        /// 在字符串的指定起止位置设置下划线
        /// </summary>
        /// <param name="sheet">需要添加下划线的工作表</param>
        /// <param name="richText">单元格上的字符串</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="startIndex">添加下划线的起始字符索引</param>
        /// <param name="endIndex">添加下划线的末尾字符索引</param>
        /// <param name="fontName">字体名称</param>
        public static IFont SetUnderline(this ISheet sheet, IRichTextString richText, short fontSize, int startIndex, int endIndex, string fontName = "宋体")
        {
            return sheet.Workbook.SetUnderline(richText, fontSize, startIndex, endIndex, fontName);
        }
        /// <summary>
        /// 对单元格赋值，设置字符串的下划线
        /// </summary>
        /// <param name="sheet">需要添加下划线的工作表</param>
        /// <param name="row">单元格行号（从0开始算）</param>
        /// <param name="col">单元格列号（从0开始算）</param>
        /// <param name="vaule">需要单元格所需传入的值</param>
        /// <param name="startIndex">需要添加下划线的起始位置</param>
        /// <param name="endIndex">需要添加下划线的末尾位置</param>
        public static void SetUnderline(this ISheet sheet, int row, int col, string vaule, int startIndex, int endIndex)
        {
            HSSFRichTextString richtext = new HSSFRichTextString(vaule);
            sheet.ApplyFont(richtext, 11);
            sheet.SetUnderline(richtext, 11, startIndex, endIndex);
            sheet.GetRow(row).GetCell(col).SetCellValue(richtext);
        }
        /// <summary>
        /// 对单元格赋值，赋值格式为“x年x月x日至x年x月x日”，同时设置时间下划线
        /// </summary>
        /// <param name="workbook">需要添加下划线的工作簿</param>
        /// <param name="row">单元格行号（从0开始算）</param>
        /// <param name="cell">单元格列号（从0开始算）</param>
        /// <param name="vaule"></param>
        /// <param name="startTime">起始日期</param>
        /// <param name="endTime">截止日期</param>
        public static void SetDateTimeUnderline(this IWorkbook workbook, int row, int cell, DateTime startTime, DateTime endTime)
        {
            string startYear = startTime.Year.ToString();
            string startMonth = startTime.Month.ToString();
            string startDay = startTime.Day.ToString();
            string endYear = endTime.Year.ToString();
            string endMonth = endTime.Month.ToString();
            string endDay = endTime.Day.ToString();

            string strValue = $"{startYear}年{startMonth}月{startDay}日至{endYear}年{endMonth}月{endDay}日";

            HSSFRichTextString richtext = new HSSFRichTextString(strValue);
            workbook.ApplyFont(richtext, 11);

            var font2 = workbook.SetUnderline(richtext, 11, 0, 4);
            int monthEndIndex = 5 + startMonth.Length;
            richtext.ApplyFont(5, monthEndIndex, font2);
            richtext.ApplyFont(monthEndIndex + 1, monthEndIndex + 1 + startDay.Length, font2);
            richtext.ApplyFont(monthEndIndex + 1 + startDay.Length + 2, monthEndIndex + 1 + startDay.Length + 6, font2);

            int monthEndIndex2 = monthEndIndex + 1 + startDay.Length + 7;
            richtext.ApplyFont(monthEndIndex2, monthEndIndex2 + endMonth.Length, font2);
            richtext.ApplyFont(monthEndIndex2 + endMonth.Length + 1, monthEndIndex2 + endMonth.Length + 1 + endDay.Length, font2);

            ISheet sheet = workbook.GetSheetAt(0);
            sheet.GetRow(row).GetCell(cell).SetCellValue(richtext);
        }
    }
}
