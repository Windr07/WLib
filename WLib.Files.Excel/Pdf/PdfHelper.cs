/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/6/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;

namespace WLib.Files.Pdf
{
    /// <summary>
    /// 提供PDF文件合并、转换jpg等方法
    /// </summary>
    public static class PdfHelper
    {
        /// <summary>
        /// 创建一个包含指定文本的pdf文件
        /// </summary>
        /// <param name="pdfFilePath">pdf文件保存路径</param>
        /// <param name="content">pdf文本内容</param>
        public static void CreatePdf(string pdfFilePath, string content)
        {
            var document = new Document();
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();
            var paragraph = new Paragraph();
            document.Add(paragraph);
            document.Close();
        }
        /// <summary>
        /// 创建一个包含指定文本的pdf文件，并设置页面大小、作者、标题等相关信息设置
        /// </summary>
        /// <param name="pdfFilePath">pdf文件保存路径</param>
        /// <param name="content">pdf文本内容</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="title">标题</param>
        /// <param name="subject">主题</param>
        /// <param name="keywords">关键字</param>
        /// <param name="creator">创建者</param>
        /// <param name="author">作者</param>
        public static void CreatePdfSetInfo(string pdfFilePath, string content, Rectangle pageSize = null,
            string title = null, string subject = null, string keywords = null, string creator = null, string author = null)
        {
            //设置页面大小
            if (pageSize == null)
                pageSize = new Rectangle(216f, 716f);
            pageSize.BackgroundColor = new BaseColor(0xFF, 0xFF, 0xDE);

            //设置边界
            Document document = new Document(pageSize, 36f, 72f, 108f, 180f);
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));

            // 添加文档信息
            document.AddTitle(title);
            document.AddSubject(subject);
            document.AddKeywords(keywords);
            document.AddCreator(creator);
            document.AddAuthor(author);
            document.Open();

            // 添加文档内容
            document.Add(new Paragraph(content));
            document.Close();
        }
        /// <summary>
        /// 创建表格
        /// </summary>
        public static void CreateTable(this Document document, DataTable dataTable)
        {
            string fileName = string.Empty;
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            document.Open();

            var table = new PdfPTable(dataTable.Columns.Count);
            if (!string.IsNullOrWhiteSpace(dataTable.TableName))
                document.Add(new Paragraph(dataTable.TableName));

            //添加表头（列名）
            var columnCells = new List<PdfPCell>();
            foreach (DataColumn column in dataTable.Columns)
            {
                columnCells.Add(new PdfPCell(new Phrase(column.Caption.ToString())));
            }
            table.Rows.Add(new PdfPRow(columnCells.ToArray()));

            //添加行
            foreach (DataRow row in dataTable.Rows)
            {
                var cells = new List<PdfPCell>();
                foreach (var value in row.ItemArray)
                {
                    cells.Add(new PdfPCell(new Phrase(value.ToString())));
                }
                table.Rows.Add(new PdfPRow(cells.ToArray()));
            }

            document.Add(table);
            document.Close();
        }


        /// <summary>
        /// 将JPG文件转为PDF文件
        /// </summary>
        /// <param name="jpgFilePath">jpg文件路径</param>
        /// <param name="pdfPath">转换后的pdf文件保存路径</param>
        public static void ConvertJpg2Pdf(string jpgFilePath, string pdfPath)
        {
            var document = new Document(PageSize.A4, 25, 25, 25, 25);
            using (var stream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();
                using (var imageStream = new FileStream(jpgFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var image = Image.GetInstance(imageStream);
                    if (image.Height > PageSize.A4.Height - 25)
                        image.ScaleToFit(PageSize.A4.Width - 25, PageSize.A4.Height - 25);
                    else if (image.Width > PageSize.A4.Width - 25)
                        image.ScaleToFit(PageSize.A4.Width - 25, PageSize.A4.Height - 25);

                    image.Alignment = Element.ALIGN_MIDDLE;
                    document.Add(image);
                }

                document.Close();
            }
        }
        /// <summary>
        /// 合并jpg和pdf文件到新pdf文件中
        /// </summary>
        ///  <param name="sourcePdfPath"></param>
        /// <param name="jpgFilePath"></param>
        /// <param name="resultFilePath"></param>
        public static void JpgToPdfFile(string sourcePdfPath, string jpgFilePath, string resultFilePath)
        {
            if (File.Exists(resultFilePath))
                File.Delete(resultFilePath);

            //复制源pdf内容到输出pdf中
            PdfReader reader = new PdfReader(sourcePdfPath);
            Rectangle psize = reader.GetPageSize(1);
            Document document = new Document(psize);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(resultFilePath, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            reader = new PdfReader(sourcePdfPath);
            int iPageNum = reader.NumberOfPages;
            for (int j = 1; j <= iPageNum; j++)
            {
                document.NewPage();
                PdfImportedPage newPage = writer.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);
            }

            using (var imageStream = new FileStream(jpgFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var image = Image.GetInstance(imageStream);
                //image.Rotation = 90f;
                //h1/w2 = w1/h2 = k,   h2 = w1/k 
                float k = psize.Height / image.Width;
                float newWidth = psize.Height;
                float newHeight = image.Height * k;
                document.SetPageSize(new Rectangle(0, 0, psize.Height, newHeight));
                //document.SetPageSize(new Rectangle(0, 0, image.Width, image.Height));
                document.SetMargins(0, 0, 0, 0);
                document.NewPage();
                if (image.Height > newHeight)
                {
                    image.ScaleToFit(newWidth, newHeight);
                }
                else if (image.Width > psize.Width)
                {
                    image.ScaleToFit(newWidth, newHeight);
                }

                image.Alignment = Element.ALIGN_MIDDLE;
                document.Add(image);
            }
            document.Close();
        }
        /// <summary>
        /// 合并jpg和pdf文件到新pdf文件中，在pdf每一页之间插入一页jpg图片
        /// </summary>
        /// <param name="sourcePdfPath"></param>
        /// <param name="jpgFilePath"></param>
        /// <param name="resultFilePath"></param>
        public static void InsertJpgToPdfFile(string sourcePdfPath, string jpgFilePath, string resultFilePath)
        {
            if (File.Exists(resultFilePath))
                File.Delete(resultFilePath);

            //生成结果pdf文件
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(resultFilePath, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            //获取源pdf页面的大小
            PdfReader reader = new PdfReader(sourcePdfPath);
            Rectangle size = reader.GetPageSize(1);
            int iPageNum = reader.NumberOfPages;

            //获取JPG图片页面的大小
            var imageStream = new FileStream(jpgFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var image = Image.GetInstance(imageStream);
            float k = size.Height / image.Width;
            float newWidth = size.Height;
            float newHeight = image.Height * k;

            for (int j = 1; j <= iPageNum; j++)
            {
                //插入原pdf页面
                document.SetPageSize(new Rectangle(0, 0, size.Width, size.Height));
                document.NewPage();
                PdfImportedPage newPage = writer.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);

                //插入图片页面
                document.SetPageSize(new Rectangle(0, 0, size.Height, newHeight));
                document.SetMargins(0, 0, 0, 0);
                document.NewPage();
                //if (image.Height > newHeight)
                image.ScaleToFit(newWidth, newHeight);
                //else if (image.Width > psize.Width)
                //image.ScaleToFit(newWidth, newHeight);
                image.Alignment = Element.ALIGN_MIDDLE;
                document.Add(image);
            }

            imageStream.Close();
            document.Close();
        }
        /// <summary>
        /// 将多个jpg文件合并生成pdf文件
        /// </summary>
        /// <param name="jpgPaths"></param>
        /// <param name="outMergePdfFilePath"></param>
        public static void MergeJpgsToPdf(string[] jpgPaths, string outMergePdfFilePath)
        {
            if (File.Exists(outMergePdfFilePath))
                File.Delete(outMergePdfFilePath);

            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergePdfFilePath, FileMode.Create));
            document.Open();

            foreach (var jpgPath in jpgPaths)
            {
                //读jpg文件
                var imageStream = new FileStream(jpgPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var image = Image.GetInstance(imageStream);

                //获取JPG图片页面的大小，插入图片页面
                document.SetPageSize(new Rectangle(0, 0, image.Width, image.Height));
                document.SetMargins(0, 0, 0, 0);
                document.NewPage();
                image.Alignment = Element.ALIGN_MIDDLE;
                document.Add(image);

                imageStream.Close();
            }
            document.Close();
        }


        /// <summary> 
        /// 合并pdf文档
        /// </summary>
        /// <param name="pdfFilePaths"></param>
        /// <param name="outMergeFilePath"></param>
        public static void MergePDFFiles(string[] pdfFilePaths, string outMergeFilePath)
        {
            PdfReader reader = new PdfReader(pdfFilePaths[0]);
            Rectangle psize = reader.GetPageSize(1);
            //厘米/位置单位 = 0.03527462121212121212121212121212
            //iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(951.987f, 660.985f);
            Document document = new Document(psize);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFilePath, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage newPage;
            for (int i = 0; i < pdfFilePaths.Length; i++)
            {
                reader = new PdfReader(pdfFilePaths[i]);
                int iPageNum = reader.NumberOfPages;
                for (int j = 1; j <= iPageNum; j++)
                {
                    document.NewPage();
                    newPage = writer.GetImportedPage(reader, j);
                    cb.AddTemplate(newPage, 0, 0);
                }
            }
            document.Close();
        }
        /// <summary>
        /// 合并pdf文档，第二份pdf文件的第一页插入到第一份pdf文件的每一页之间
        /// </summary>
        /// <param name="pdfFilePath1"></param>
        /// <param name="pdfFilePath2"></param>
        /// <param name="outMeregeFilePath"></param>
        public static void InsertPdfToPdfFile(string pdfFilePath1, string pdfFilePath2, string resultFilePath)
        {
            if (File.Exists(resultFilePath))
                File.Delete(resultFilePath);
            Thread.Sleep(200);

            //生成结果pdf文件
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(resultFilePath, FileMode.Create));
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            //获取源pdf页面的大小
            PdfReader reader = new PdfReader(pdfFilePath1);
            Rectangle size = reader.GetPageSize(1);
            int iPageNum = reader.NumberOfPages;

            //获取JPG图片页面的大小
            PdfReader reader2 = new PdfReader(pdfFilePath2);
            PdfImportedPage pdfImagePage = writer.GetImportedPage(reader2, 1);
            var image = Image.GetInstance(pdfImagePage);
            float k = size.Height / image.Width;
            float newWidth = size.Height;
            float newHeight = image.Height * k;

            for (int j = 1; j <= iPageNum; j++)
            {
                //插入原pdf页面
                document.SetPageSize(new Rectangle(0, 0, size.Width, size.Height));
                document.NewPage();
                PdfImportedPage newPage = writer.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);

                //插入图片页面
                document.SetPageSize(new Rectangle(0, 0, size.Height, newHeight));
                document.SetMargins(0, 0, 0, 0);
                document.NewPage();
                //if (image.Height > newHeight)
                image.ScaleToFit(newWidth, newHeight);
                //else if (image.Width > psize.Width)
                //image.ScaleToFit(newWidth, newHeight);
                image.Alignment = Element.ALIGN_MIDDLE;
                document.Add(image);
            }

            document.Close();
        }
    }
}
