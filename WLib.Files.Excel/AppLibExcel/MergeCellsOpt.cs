/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： 使用开源库AppLibrary.dll
// mdfy:  Windragon
//----------------------------------------------------------------*/

namespace WLib.Files.Excel.AppLibExcel
{
    /// <summary>
    /// 合并单元格操作
    /// （此类的方法都将合并范围内的单元格设置统一的边框和样式，随后再合并单元格）
    /// </summary>
    public class MergeCellsOpt
    {
        /// <summary>
        /// 工作簿内的所有单元格的集合
        /// </summary>
        public AppLibrary.WriteExcel.Cells Cells { get; set; }
        /// <summary>
        /// 表示单元格的边框、样式、字体格式等
        /// </summary>
        public AppLibrary.WriteExcel.XF Xf { get; set; }
        /// <summary>
        /// 实例化合并单元格操作类
        /// </summary>
        /// <param name="cells">作簿内的所有单元格的集合</param>
        /// <param name="xf">表示单元格的边框、样式、字体格式等</param>
        public MergeCellsOpt(AppLibrary.WriteExcel.Cells cells, AppLibrary.WriteExcel.XF xf)
        {
            this.Cells = cells;
            this.Xf = xf;
        }


        /// <summary>
        /// 合并单元格（拥有边框和默认样式xf）
        /// </summary>
        /// <param name="rowMin">合并范围的最小行号</param>
        /// <param name="rowMax">合并范围的最大行号</param>
        /// <param name="columnMin">合并范围的最小列号</param>
        /// <param name="columnMax">合并范围的最大列号</param>
        /// <param name="value">单元格的值</param>
        public void MergeCells(int rowMin, int rowMax, int columnMin, int columnMax, object value)
        {
            Cells.Add(rowMin, columnMin, value, Xf);
            for (int r = rowMin + 1; r <= rowMax; r++)
            {
                Cells.Add(r, columnMin, null, Xf);
            }

            for (int r = rowMin; r <= rowMax; r++)
            {
                for (int c = columnMin + 1; c <= columnMax; c++)
                {
                    Cells.Add(r, c, null, Xf);
                }
            }
            Cells.Merge(rowMin, rowMax, columnMin, columnMax);
        }
        /// <summary>
        /// 合并单元格：将指定单元格与其右方、下方、右下的三个单元格合并（拥有边框和默认样式xf）
        /// </summary>
        /// <param name="row">指定单元格的行号</param>
        /// <param name="column">指定单元格的列号</param>
        /// <param name="value">单元格的值</param>
        public void MergeCellsToLowerRight(int row, int column, object value)
        {
            Cells.Add(row, column, value, Xf);
            Cells.Add(row + 1, column, null, Xf);
            Cells.Add(row, column + 1, null, Xf);
            Cells.Add(row + 1, column + 1, null, Xf);
            Cells.Merge(row, row + 1, column, column + 1);
        }
        /// <summary>
        /// 向下合并单元格：将指定单元格与其正下方的一个单元格进行合并（拥有边框和默认样式xf）
        /// </summary>
        /// <param name="row">指定单元格的行</param>
        /// <param name="column">指定单元格的列</param>
        /// <param name="value">单元格的值</param>
        public void MergeCellBelow(int row, int column, object value)
        {
            Cells.Add(row, column, value, Xf);
            Cells.Add(row + 1, column, null, Xf);
            Cells.Merge(row, row + 1, column, column);
        }
        /// <summary>
        /// 向下合并单元格：在指定列中，合并指定行范围的单元格（拥有边框和默认样式xf）
        /// </summary>
        /// <param name="rowMin">起始行</param>
        /// <param name="rowMax">终止行</param>
        /// <param name="column">指定的列</param>
        /// <param name="value">单元格的值</param>
        public void MergeCellBelow(int rowMin, int rowMax, int column, object value)
        {
            Cells.Add(rowMin, column, value, Xf);
            for (int i = rowMin + 1; i <= rowMax; i++)
            {
                Cells.Add(i, column, null, Xf);
            }
            Cells.Merge(rowMin, rowMax, column, column);
        }
        /// <summary>
        /// 向右合并单元格：在指定行中，合并指定列范围的单元格（拥有边框和默认样式xf）
        /// </summary>
        /// <param name="row">指定的行</param>
        /// <param name="columnMin">起始列</param>
        /// <param name="columnMax">终止列</param>
        /// <param name="value">单元格的值</param>
        public void MergeCellRight(int row, int columnMin, int columnMax, object value)
        {
            Cells.Add(row, columnMin, value, Xf);
            for (int i = columnMin + 1; i <= columnMax; i++)
            {
                Cells.Add(row, i, null, Xf);
            }
            Cells.Merge(row, row, columnMin, columnMax);
        }

    }
}
