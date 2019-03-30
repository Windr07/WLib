/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;

namespace WLib.ArcGis.Carto
{
    /// <summary>
    /// 导出地图
    /// </summary>
    public static class MapExport
    {
        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="filePath">图片输出的完整路径，路径扩展名应为jpg,tiff,bmp,emf,png,gif,pdf,eps,ai,svg之一</param>
        /// <param name="resolution">图片分辨率（每英寸点数）</param>
        /// <returns></returns>
        public static bool ExportMapExtent(this IActiveView activeView, string filePath, double resolution = 300)
        {
            IExport export = CreateExport(filePath, resolution);
            tagRECT exportRECT = activeView.ExportFrame;
            export.PixelBounds = new EnvelopeClass
            {
                XMin = exportRECT.left,
                YMin = exportRECT.top,
                XMax = exportRECT.right,
                YMax = exportRECT.bottom
            };

            //activeView.Output(int hDC, int dpi, ref tagRECT pixelBounds, ref IEnvelope visibleBounds, ref ITrackCancel trackCancel);
            //hDC - 输出设备
            //dpi - 每英寸点数（分辨率）。如果为0，则默认为hDC的分辨率。如果无法采样，则默认为窗口分辨率
            //pixelBounds - 以像素为单位指定的设备矩形
            //VisibleBounds - 缩放范围。如果传递Null，则默认为当前可见范围
            //如果要将数据框绘制到640x480位图，请指定以下参数值：
            //  dpi - 0
            //  pixelBounds - { 0, 0, 640, 480 }
            //  visible bounds -0
            //如果要创建在300 DPI打印机上打印8.5 x 11的位图，请指定以下参数值：
            //  dpi - 300
            //  pixel bounds - { 0, 0, 8.5 * dpi, 11 * dpi }
            //  visible bounds -0
            //如果要缩放到某个位置并在300x300位图上绘制，请指定以下参数值：
            //  dpi - 0
            //  pixel bounds - { 0, 0, 300, 300 }
            //  visible bounds -  { -45.0, 44.0, -44.0, 45.0 }
            activeView.Output(export.StartExporting(), (int)export.Resolution, ref exportRECT, null, null);

            export.FinishExporting();
            export.Cleanup();
            return true;
        }
        /// <summary>
        /// 输出图片
        /// </summary>
        /// <param name="pageLayout">页面布局对象</param>
        /// <param name="outPath">图片输出的完整路径，路径扩展名应为jpg,tiff,bmp,emf,png,gif,pdf,eps,ai,svg之一</param>
        /// <param name="resolution">图片分辨率（每英寸点数）</param>
        public static void ExportMapExtent(this IPageLayout pageLayout, string outPath, double resolution = 300)
        {
            try
            {
                //参数检查
                IActiveView activeView = pageLayout as IActiveView;
                if (activeView == null) throw new Exception("输入参数（pageLayout）错误！");

                IExport export = CreateExport(outPath, resolution);

                IEnvelope visibleBounds = new EnvelopeClass();
                visibleBounds.PutCoords(0, 0, pageLayout.Page.PrintableBounds.Width, pageLayout.Page.PrintableBounds.Height);

                //输出范围       
                tagRECT pixelBounds = new tagRECT();
                pixelBounds.left = pixelBounds.top = 0;
                pixelBounds.right = (int)(export.Resolution / 2.54 * pageLayout.Page.PrintableBounds.Width);
                pixelBounds.bottom = (int)(export.Resolution / 2.54 * pageLayout.Page.PrintableBounds.Height);

                IEnvelope envelope = new EnvelopeClass();
                envelope.PutCoords(pixelBounds.left, pixelBounds.top, pixelBounds.right, pixelBounds.bottom);
                export.PixelBounds = envelope;

                //可用于取消操作
                ITrackCancel trackCancel = CreateTrackCancel();
                export.TrackCancel = trackCancel;

                //开始转出
                //activeView.Output(int hDC, int Dpi, ref tagRECT pixelBounds, ref IEnvelope VisibleBounds, ref ITrackCancel TrackCancel);
                //hDC - 输出设备
                //dpi - 每英寸点数（分辨率）。如果传递Null，则默认为hDC的分辨率。如果无法采样，则默认为窗口分辨率
                //pixelBounds - 以像素为单位指定的设备矩形
                //VisibleBounds - 缩放范围。如果传递Null，则默认为当前可见范围
                activeView.Output(export.StartExporting(), (int)export.Resolution, ref pixelBounds, visibleBounds, trackCancel);
                bool bContinue = trackCancel.Continue();

                //捕获是否继续
                if (bContinue)
                {
                    export.FinishExporting();
                    export.Cleanup();
                }
                else
                {
                    export.Cleanup();
                }
                trackCancel.Continue();
            }
            catch (Exception ex)
            {
                throw new Exception("输出图片出错：" + ex.Message);
            }
        }


        /// <summary>
        /// 根据文件路径和路径中的扩展名，来决定生成导出不同类型图片的Export对象
        /// </summary>
        /// <param name="picPath">导出的图片的路径</param>
        /// <param name="resolution"></param>
        /// <returns></returns>
        private static IExport CreateExport(string picPath, double resolution)
        {
            IExport export;
            string extension = System.IO.Path.GetExtension(picPath)?.ToLower();
            switch (extension)
            {
                case ".jpg": export = new ExportJPEGClass(); break;
                case ".tiff": export = new ExportTIFFClass(); break;
                case ".bmp": export = new ExportBMPClass(); break;
                case ".emf": export = new ExportEMFClass(); break;
                case ".png": export = new ExportPNGClass(); break;
                case ".gif": export = new ExportGIFClass(); break;
                case ".pdf": export = new ExportPDFClass(); break;
                case ".eps": export = new ExportPSClass(); break;
                case ".ai": export = new ExportAIClass(); break;
                case ".svg": export = new ExportSVGClass(); break;
                default: throw new Exception("不支持的图片输出格式！（程序支持的图片格式包括：jpg,tiff,bmp,emf,png,gif,pdf,eps,ai,svg）");
            }

            export.ExportFileName = picPath;
            export.Resolution = resolution;
            return export;
        }
        /// <summary>
        /// 创建取消操作
        /// </summary>
        /// <returns></returns>
        private static ITrackCancel CreateTrackCancel()
        {
            ITrackCancel trackCancel = new CancelTrackerClass();
            trackCancel.Reset();
            trackCancel.CancelOnKeyPress = true; //点击ESC键时，中止转出
            trackCancel.CancelOnClick = false;
            trackCancel.ProcessMessages = true;
            return trackCancel;
        }
    }
}
