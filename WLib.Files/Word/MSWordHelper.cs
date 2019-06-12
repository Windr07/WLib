/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： 使用微软官方Office库，安装MS Office后即可调用，注意版本兼容问题
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace WLib.Files.Word
{
    /// <summary>
    /// Microsoft Word帮助类，提供Word文档操作
    /// </summary>
    public class MsWordHelper
    {
        private object _missing = System.Reflection.Missing.Value;//默认值
        /// <summary>
        /// 当前操作的Word文档
        /// </summary>
        public Document Document { get; private set; }
        /// <summary>
        /// Word程序
        /// </summary>
        public Application AppClass { get; private set; }


        #region 文件操作
        /// <summary>
        /// 打开word
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="isVisible"></param>
        public void Open(string strFileName, bool isVisible = true)
        {
            if (AppClass == null)
                AppClass = new Application();

            object fileName = strFileName;
            object readOnly = false;
            object isVisible2 = isVisible as object;

            Document = AppClass.Documents.Open(ref fileName, ref _missing, ref readOnly,
                ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing,
                ref _missing, ref _missing, ref isVisible2, ref _missing, ref _missing, ref _missing, ref _missing);

            Document.Activate();
        }
        /// <summary>
        /// 退出word
        /// </summary>
        public void Quit()
        {
            AppClass?.Application?.Quit(ref _missing, ref _missing, ref _missing);
        }
        /// <summary>
        /// 关闭当前的文档
        /// </summary>
        public void CloseDocument()
        {
            Document?.Close();
        }
        /// <summary>
        /// 打开Word文档,并且返回对象oDoc
        /// </summary>
        /// <param name="app"></param>
        /// <param name="fileName">完整Word文件路径+名称  </param>
        /// <param name="hideWin"></param>
        /// <returns>返回的Word.Document oDoc对象 </returns>
        public Document CreateWordDocument(string fileName, bool hideWin)
        {
            if (fileName == "") return null;

            AppClass.Visible = hideWin;
            AppClass.Caption = "";
            AppClass.Options.CheckSpellingAsYouType = false;
            AppClass.Options.CheckGrammarAsYouType = false;

            Object filename = fileName;
            Object confirmConversions = false;
            Object readOnly = true;
            Object addToRecentFiles = false;

            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;
            Object xmlTransform = Type.Missing;
            try
            {
                Document wordDoc = AppClass.Documents.Open(ref filename, ref confirmConversions,
                ref readOnly, ref addToRecentFiles, ref passwordDocument, ref passwordTemplate,
                ref revert, ref writePasswordDocument, ref writePasswordTemplate, ref format,
                ref encoding, ref visible, ref openAndRepair, ref documentDirection,
                ref noEncodingDialog, ref xmlTransform);
                return wordDoc;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Word文档“{filename}”失败：{ex.Message}");
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            Document.Save();
        }
        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="oDoc"></param>
        /// <param name="strFileName"></param>
        public void SaveAs(string strFileName)
        {
            object fileName = strFileName;
            Document.SaveAs(ref fileName, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing,
                      ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing, ref _missing);
        }
        #endregion


        #region 标签操作
        /// <summary>
        /// 替换标签内容
        /// </summary>
        /// <param name="marks">所有标签</param>
        /// <param name="marksValue">所有标签对应的值</param>
        /// <param name="outPutPath">输出地址（不含后缀）</param>
        public void ReplaceBookMark(string[] marks, string[] marksValue, object outPutPath)
        {
            Bookmarks bookmarks = Document.Bookmarks;
            foreach (Bookmark bookmark in bookmarks)
            {
                object obDdName = bookmark.Name;
                for (int j = 0; j < marks.Length; j++)
                {
                    if (marks[j] == obDdName.ToString())
                    {
                        bookmark.Range.Text = marksValue[j];
                        break;
                    }
                }
            }
            SaveAs(outPutPath.ToString());
            object doNotSaveChanges = WdSaveOptions.wdDoNotSaveChanges;
            Document.Close(ref doNotSaveChanges, ref _missing, ref _missing);
        }
        /// <summary>
        /// 替换标签内容
        /// </summary>
        /// <param name="marks">所有标签</param>
        /// <param name="marksValue">所有标签对应的值</param>
        public void ReplaceBookMark(string[] marks, string[] marksValue)
        {
            Bookmarks bookmarks = Document.Bookmarks;
            foreach (Bookmark bookmark in bookmarks)
            {
                string obDdName = bookmark.Name;
                for (int j = 0; j < marks.Length; j++)
                {
                    if (marks[j] == obDdName)
                    {
                        bookmark.Range.Text = marksValue[j];
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 替换标签内容
        /// </summary>
        /// <param name="markNameToValue">标签名及对应值的键值对</param>
        public void ReplaceBookMark(Dictionary<string, string> markNameToValue)
        {
            Bookmarks bookmarks = Document.Bookmarks;
            foreach (Bookmark bookmark in bookmarks)
            {
                string obDdName = bookmark.Name;
                if (obDdName == "DJRQ")
                {
                    Console.WriteLine();
                }
                foreach (var mark in markNameToValue)
                {
                    if (mark.Key == "DJRQ")
                    {
                        Console.WriteLine();
                    }
                    if (mark.Key == obDdName)
                    {
                        bookmark.Range.Text = mark.Value;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 填充word表格内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="bookmarkName">书签名</param>
        /// <param name="blanklines">指定在Word表格中的第几行开始插入新行</param>
        public void WriteToTable(System.Data.DataTable dt, string bookmarkName, int blanklines = 0)
        {
            Bookmarks bookmarks = Document.Bookmarks;
            Bookmark bookmark = GetBookmark(bookmarks, bookmarkName);
            object what = WdGoToItem.wdGoToBookmark;
            AppClass.Selection.GoTo(what, _missing, _missing, bookmark.Name);//移动到书签位置

            if (dt.Rows.Count < 1)
                return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i >= blanklines)
                {
                    AppClass.Selection.InsertRows(1);
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Rows[i][j] != null)
                    {
                        AppClass.Selection.TypeText(dt.Rows[i][j].ToString());
                        AppClass.Selection.MoveRight(_missing, 1, _missing);
                    }
                    else
                    {
                        AppClass.Selection.TypeText("");
                        AppClass.Selection.MoveRight(_missing, 1, _missing);
                    }
                }
            }
        }
        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="bookmarkName"></param>
        /// <returns></returns>
        public Bookmark GetBookmark(string bookmarkName)
        {
            Bookmark theResult = null;
            Bookmarks bookmarks = Document.Bookmarks;
            foreach (Bookmark bookmark in bookmarks)
            {
                if (bookmark.Name == bookmarkName)
                {
                    theResult = bookmark;
                    break;
                }
            }
            return theResult;
        }
        /// <summary>
        /// 获取标签
        /// </summary>
        /// <param name="bookmarks"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public Bookmark GetBookmark(Bookmarks bookmarks, string item)
        {
            if (bookmarks.Exists(item))
            {
                foreach (Bookmark bookmark in bookmarks)
                {
                    if (bookmark.Name == item)
                        return bookmark;
                }
            }
            return null;
        }
        /// <summary>
        /// 向上合并单元格
        /// </summary>
        /// <param name="bookmarkName">从此书签处开始合并单元格</param>
        /// <param name="count">合并单元格数</param>
        public void UpMergeCells(string bookmarkName, int count)
        {
            AppClass.ActiveWindow.View.Type = WdViewType.wdWebView;
            Bookmarks bookmarks = Document.Bookmarks;
            Bookmark bookmark = GetBookmark(bookmarks, bookmarkName);
            object what = WdGoToItem.wdGoToBookmark;
            AppClass.Selection.GoTo(what, _missing, _missing, bookmark.Name);

            Document.Bookmarks.DefaultSorting = WdBookmarkSortBy.wdSortByName;
            Document.Bookmarks.ShowHidden = false;
            object extend = WdMovementType.wdExtend;
            object unit = WdUnits.wdLine;
            AppClass.Selection.MoveUp(unit, count, extend);

            AppClass.ActiveWindow.View.Type = WdViewType.wdPrintView;
            MergeCells();
        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        public void MergeCells()
        {
            AppClass.Selection.Cells.Merge();
        }
        #endregion


        #region 其他操作
        /// <summary>
        /// 获得当前位置的行号、列号、页码信息
        /// </summary>
        /// <returns></returns>
        public string GetRowColunmPageNum()
        {
            var selection = Document.Application.Selection;
            object row = selection.get_Information(WdInformation.wdFirstCharacterLineNumber);
            object col = selection.get_Information(WdInformation.wdFirstCharacterColumnNumber);
            object page = selection.get_Information(WdInformation.wdActiveEndAdjustedPageNumber);
            return row + "," + col + "," + page;
        }
        #endregion
    }
}
