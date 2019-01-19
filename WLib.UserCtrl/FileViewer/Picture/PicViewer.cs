/*---------------------------------------------------------------- 
// auth： XiaoJiaMing
// date： 2013/05/31
// desc： 简单的图片查看控件
//        如果放得越大，速度越快
// mdfy:  Windragon
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WLib.UserCtrls.FileViewer.Picture
{
    /// <summary>
    /// 图片简单浏览控件
    /// </summary>
    public partial class PicViewer : UserControl, IFileViewer
    {
        private readonly Timer _timer = new Timer();
        /// <summary>
        /// 图片
        /// </summary>
        private Image _image;
        /// <summary>
        /// 图片控件的操作类型（平移、放大、缩小）
        /// </summary>
        private EPicViewerAction _actionType = EPicViewerAction.Pan;
        /// <summary>
        /// 图片控件的操作类型（平移、放大、缩小）
        /// </summary>
        public EPicViewerAction ActionType
        {
            get => _actionType;
            set
            {
                _actionType = value;
                if (_actionType == EPicViewerAction.ZoomIn)//设置鼠标
                    pictureBox.Cursor = _zoomIn;
                else if (_actionType == EPicViewerAction.ZoomOut)
                    pictureBox.Cursor = _zoomOut;
                else if (_actionType == EPicViewerAction.Pan)
                    pictureBox.Cursor = _pan;
            }
        }
        /// <summary>
        /// 当前要显示的图片路径
        /// </summary>
        public string PicFilePath { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public override Color BackColor { get; set; } = Color.White;


        /// <summary>
        /// 图片框的画图区域
        /// </summary>
        private Rectangle _curDrawRect;
        /// <summary>
        /// 图片的显示区域
        /// </summary>
        private Rectangle _curImgRect;
        /// <summary>
        /// 拉框放大的区域
        /// </summary>
        private Rectangle _autoZoom;
        /// <summary>
        /// 
        /// </summary>
        private Rectangle _trackRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
        /// <summary>
        /// 
        /// </summary>
        private Point _padPoint;
        /// <summary>
        /// 
        /// </summary>
        private Point _zoomPoint;
     

        /// <summary>
        /// 光标
        /// </summary>
        private readonly Cursor _pan;
        private readonly Cursor _mPaning;
        private readonly Cursor _zoomIn;
        private readonly Cursor _zoomOut;

      
        public PicViewer()
        {
            InitializeComponent();

            _pan = Cursors.Default;
            _mPaning = Cursors.Default;
            _zoomIn = Cursors.Default;
            _zoomOut = Cursors.Default;
            BackColor = Color.Transparent;
            pictureBox.MouseWheel += pictureBox1_MouseWheel;
        }

        public void DisposeImage()
        {
            _image?.Dispose();
        }

        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                ZoomIn();
            else
                ZoomOut();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (Width < 200)
                Width = 200;
            if (Height < 200)
                Height = 200;
        }

       
        /// <summary>
        /// 放大操作
        /// </summary>
        public void ZoomIn()
        {
            if (_image == null)
                return;

            //计算放大的范围和画图的坐标
            int newht = (int)(_curDrawRect.Height * 1.2);
            int newwt = (int)(_curDrawRect.Width * 1.2);
            int ox = (newwt - _curDrawRect.Width) / 2;
            int oy = (newht - _curDrawRect.Height) / 2;
            int x = _curDrawRect.X - ox;
            int y = _curDrawRect.Y - oy;

            //如果放大超过实际图片大小的倍数则放弃操作
            if ((newht / _image.Height) > 5 || (newwt / _image.Width) > 5)
                return;

            _curDrawRect = new Rectangle(x, y, newwt, newht);
            pictureBox.Invalidate();
            ShowPercentLabel();
        }
        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomOut()
        {
            if (_image == null)
                return;

            //计算放大的范围和画图的坐标
            int pbh = pictureBox.Height;
            int pbw = pictureBox.Width;
            int newht = (int)(_curDrawRect.Height / 1.2);
            int newwt = (int)(_curDrawRect.Width / 1.2);
            int ox = (_curDrawRect.Width - newwt) / 2;
            int oy = (_curDrawRect.Height - newht) / 2;
            int x = _curDrawRect.X + ox;
            int y = _curDrawRect.Y + oy;

            //判断边缘
            if (newwt > pbw)
            {
                if (x > 0) x = 0;
                if ((x + newwt) < pbw) x = pbw - newwt;
            }
            else
            {
                x = (pbw - newwt) / 2;
            }

            if (newht > pbh)
            {
                if (y > 0) y = 0;
                if ((y + newht) < pbh) y = pbh - newht;
            }
            else
            {
                y = (pbh - newht) / 2;
            }

            //如果缩小小于图片框大小的倍数则放弃操作
            if ((pictureBox.Height / newht) > 5 || (pictureBox.Width / newwt) > 5)
                return;

            _curDrawRect = new Rectangle(x, y, newwt, newht);
            pictureBox.Invalidate();
            ShowPercentLabel();
        }
        /// <summary>
        /// 全图显示
        /// </summary>
        public void Full()
        {
            if (_image == null)
                return;

            int pbh = pictureBox.Height;
            int pbw = pictureBox.Width;

            int ht = _image.Height;
            int wt = _image.Width;

            int newht = pbh;
            int newwt = pbw;
            int x = 0;
            int y = 0;

            //计算画图坐标和显示区域大小
            if (ht < pbh && wt < pbw)//图片高宽均小于图片框
            {
                newht = ht;
                newwt = wt;
                x = (pbw - wt) / 2;
                y = (pbh - ht) / 2;
            }
            else if (ht > pbh && wt <= pbw)//图片高>图片框高,宽<=图片框宽
            {
                newht = pbh;
                newwt = (int)((pbh / (ht * 1.0)) * wt);
                x = (pbw - newwt) / 2;
                y = 0;
            }
            else if (ht <= pbh && wt > pbw)//反过来
            {
                newht = (int)((pbw / (wt * 1.0)) * ht);
                newwt = pbw;
                x = 0;
                y = (pbh - newht) / 2;
            }
            else//图片高宽均大于图片框
            {
                if ((ht / (pbh * 1.0)) > (wt / (pbw * 1.0)))//同比高大于宽
                {
                    newht = pbh;
                    newwt = (int)((pbh / (ht * 1.0)) * wt);
                    x = (pbw - newwt) / 2;
                    y = 0;

                }
                else//同比宽大于高
                {
                    newht = (int)((pbw / (wt * 1.0)) * ht);
                    newwt = pbw;
                    x = 0;
                    y = (pbh - newht) / 2;
                }
            }

            _curDrawRect = new Rectangle(x, y, newwt, newht);
            _curImgRect = new Rectangle(0, 0, wt, ht);
            pictureBox.Invalidate();
            ShowPercentLabel();
        }
        /// <summary>
        /// 原图大小显示
        /// </summary>
        public void Zoom()
        {
            if (_image == null)
                return;

            int pbh = pictureBox.Height;
            int pbw = pictureBox.Width;

            int ht = _image.Height;
            int wt = _image.Width;

            int newht = pbh;
            int newwt = pbw;
            int x = 0;
            int y = 0;

            //计算画图坐标和显示区域大小
            if (ht < pbh && wt < pbw)//图片高宽均小于图片框
            {
                newht = ht;
                newwt = wt;
                x = (pbw - wt) / 2;
                y = (pbh - ht) / 2;
            }
            else if (ht > pbh && wt <= pbw)//图片高>图片框高,宽<=图片框宽
            {
                x = (pbw - wt) / 2;
                y = 0 - (ht - pbh) / 2;
                newht = ht;
                newwt = wt;
            }
            else if (ht <= pbh && wt > pbw)//反过来
            {
                x = 0 - (wt - pbw) / 2;
                y = (pbh - ht) / 2;
                newht = ht;
                newwt = wt;
            }
            else//图片高宽均大于图片框
            {
                x = 0 - (wt - pbw) / 2;
                y = 0 - (ht - pbh) / 2;
                newht = ht;
                newwt = wt;
            }

            _curDrawRect = new Rectangle(x, y, newwt, newht);
            _curImgRect = new Rectangle(0, 0, wt, ht);
            pictureBox.Invalidate();
            ShowPercentLabel();
        }


        /// <summary>
        /// 拉框放大
        /// </summary>
        private void AutoZoomIn()
        {
            if (_image == null)
                return;

            //picturebox控件范围
            double pbh = pictureBox.Height;
            double pbw = pictureBox.Width;

            //图片实际范围
            double drawh = _curDrawRect.Height;
            double draww = _curDrawRect.Width;
            double drawX = _curDrawRect.X;
            double drawY = _curDrawRect.Y;

            //图片原始范围
            double imgH = _image.Height;
            double imgW = _image.Width;

            //zoom框范围
            double zoomh = _autoZoom.Height;
            double zoomw = _autoZoom.Width;
            double zoomX = _autoZoom.X;
            double zoomY = _autoZoom.Y;

            //Zoom矩形不能是一条线或点，也就是width和height要大于0
            if (zoomh <= 0 || zoomw <= 0) return;

            //zoom框宽高比
            double zoomWhScale = zoomw / zoomh;
            //picturebox宽高比
            double picboxWhScale = pbw / pbh;

            //把zoom框以picturebox的范围当做最大范围，进行缩放
            double fillZoomX = 0, fillZoomY = 0, fillZoomW = 0, fillZoomH = 0;
            if (zoomWhScale > picboxWhScale)
            {
                fillZoomX = 0;
                fillZoomW = pbw;
                double zoomInScale = fillZoomW / zoomw;
                fillZoomH = zoomh * zoomInScale;
                fillZoomY = (pbh - fillZoomH) / 2;
            }
            else
            {
                fillZoomY = 0;
                fillZoomH = pbh;
                double zoomInScale = fillZoomH / zoomh;
                fillZoomW = zoomw * zoomInScale;
                fillZoomX = (pbw - fillZoomW) / 2;
            }

            //zoom框与图片实际显示范围的交集
            Rectangle oldZoomDraw = Rectangle.Intersect(_autoZoom, _curDrawRect);

            //矩形交集为空时跳出
            if (oldZoomDraw.Width <= 0 || oldZoomDraw.Height <= 0 || oldZoomDraw.X > pbw || oldZoomDraw.Y > pbh) return;

            //缩放后的zoom框与zoom框的宽高比
            double zoomXScale = fillZoomW / zoomw;
            double zoomYScale = fillZoomH / zoomh;

            //缩放后的zoom框中的图片实际显示范围
            double newZoomDrawX = (oldZoomDraw.X - zoomX) * zoomXScale + fillZoomX;
            double newZoomDrawY = (oldZoomDraw.Y - zoomY) * zoomYScale + fillZoomY;
            double newZoomDrawW = oldZoomDraw.Width * zoomXScale;
            double newZoomDrawH = oldZoomDraw.Height * zoomYScale;

            //图片实际显示与图片原始的宽高比
            double oldZoomImgXScale = draww / imgW;
            double oldZoomImgYScale = drawh / imgH;

            //zoom框中图片的范围（以图片原始大小为单位）
            double zoomImgX = (oldZoomDraw.X - drawX) / oldZoomImgXScale;
            double zoomImgY = (oldZoomDraw.Y - drawY) / oldZoomImgYScale;
            double zoomImgW = oldZoomDraw.Width / oldZoomImgXScale;
            double zoomImgH = oldZoomDraw.Height / oldZoomImgYScale;

            //缩放后图片实际显示与图片原始的宽高比
            double newZoomImgXScale = newZoomDrawW / zoomImgW;
            double newZoomImgYScale = newZoomDrawH / zoomImgH;

            //最终图片的实际显示范围
            double newDrawX = newZoomDrawX - zoomImgX * newZoomImgXScale;
            double newDrawY = newZoomDrawY - zoomImgY * newZoomImgYScale;
            double newDrawW = imgW * newZoomImgXScale;
            double newDrawH = imgH * newZoomImgYScale;

            //如果缩小小于图片框大小的倍数则放弃操作
            if ((pictureBox.Height / newDrawH) < 0.1 || (pictureBox.Width / newDrawW) < 0.1) return;

            _curDrawRect = new Rectangle((int)Math.Truncate(newDrawX), (int)Math.Truncate(newDrawY), (int)Math.Truncate(newDrawW), (int)Math.Truncate(newDrawH));

            pictureBox.Invalidate();
        }
        /// <summary>
        /// 拉框缩小
        /// </summary>
        private void AutoZoomOut()
        {
            if (_image == null)
                return;

            //各个高宽取值
            double pbh = pictureBox.Height;
            double pbw = pictureBox.Width;

            double drawh = _curDrawRect.Height;
            double draww = _curDrawRect.Width;
            double drawX = _curDrawRect.X;
            double drawY = _curDrawRect.Y;

            double imgH = _image.Height;
            double imgW = _image.Width;

            double zoomh = _autoZoom.Height;
            double zoomw = _autoZoom.Width;
            double zoomX = _autoZoom.X;
            double zoomY = _autoZoom.Y;

            //Zoom矩形不能是一条线或点，也就是width和height要大于0
            if (zoomh <= 0 || zoomw <= 0) return;

            double zoomWhScale = zoomw / zoomh;
            double picboxWhScale = pbw / pbh;

            double fillZoomX = 0, fillZoomY = 0, fillZoomW = 0, fillZoomH = 0;

            if (zoomWhScale > picboxWhScale)
            {
                fillZoomX = 0;
                fillZoomW = pbw;
                double zoomInScale = fillZoomW / zoomw;
                fillZoomH = zoomh * zoomInScale;
                fillZoomY = (pbh - fillZoomH) / 2;
            }
            else
            {
                fillZoomY = 0;
                fillZoomH = pbh;
                double zoomInScale = fillZoomH / zoomh;
                fillZoomW = zoomw * zoomInScale;
                fillZoomX = (pbw - fillZoomW) / 2;
            }

            Rectangle fillZoom = new Rectangle((int)Math.Truncate(fillZoomX), (int)Math.Truncate(fillZoomY), (int)Math.Truncate(fillZoomW), (int)Math.Truncate(fillZoomH));

            Rectangle oldZoomDraw = Rectangle.Intersect(fillZoom, _curDrawRect);

            //矩形交集为空时跳出
            if (oldZoomDraw.Width <= 0 || oldZoomDraw.Height <= 0 || oldZoomDraw.X > pbw || oldZoomDraw.Y > pbh) return;

            double zoomXScale = fillZoomW / zoomw;
            double zoomYScale = fillZoomH / zoomh;

            double newZoomDrawX = (oldZoomDraw.X - fillZoomX) / zoomXScale + zoomX;
            double newZoomDrawY = (oldZoomDraw.Y - fillZoomY) / zoomYScale + zoomY;
            double newZoomDrawW = oldZoomDraw.Width / zoomXScale;
            double newZoomDrawH = oldZoomDraw.Height / zoomYScale;

            double oldZoomImgXScale = draww / imgW;
            double oldZoomImgYScale = drawh / imgH;

            double zoomImgX = (oldZoomDraw.X - drawX) / oldZoomImgXScale;
            double zoomImgY = (oldZoomDraw.Y - drawY) / oldZoomImgYScale;
            double zoomImgW = oldZoomDraw.Width / oldZoomImgXScale;
            double zoomImgH = oldZoomDraw.Height / oldZoomImgYScale;

            double newZoomImgXScale = newZoomDrawW / zoomImgW;
            double newZoomImgYScale = newZoomDrawH / zoomImgH;

            double newDrawX = newZoomDrawX - zoomImgX * newZoomImgXScale;
            double newDrawY = newZoomDrawY - zoomImgY * newZoomImgYScale;
            double newDrawW = imgW * newZoomImgXScale;
            double newDrawH = imgH * newZoomImgYScale;

            //如果缩小小于图片框大小的倍数则放弃操作
            if ((pictureBox.Height / newDrawH) > 10 || (pictureBox.Width / newDrawW) > 10) return;

            _curDrawRect = new Rectangle((int)Math.Truncate(newDrawX), (int)Math.Truncate(newDrawY), (int)Math.Truncate(newDrawW), (int)Math.Truncate(newDrawH));

            pictureBox.Invalidate();
        }
        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="toPoint"></param>
        private void Pad(Point toPoint)
        {
            int newht = _curDrawRect.Height;
            int newwt = _curDrawRect.Width;
            int x = _curDrawRect.X + (toPoint.X - _padPoint.X);
            int y = _curDrawRect.Y + (toPoint.Y - _padPoint.Y);

            if (_curDrawRect.X == x && _curDrawRect.Y == y)
                return;

            Rectangle newDrawRect = new Rectangle(x, y, newwt, newht);

            //如果图片完全不在picturebox中则不处理
            Rectangle picBoxRect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);
            if (newDrawRect.IntersectsWith(picBoxRect) == false) return;

            _curDrawRect = newDrawRect;
            pictureBox.Invalidate();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //非左键不操作
            if (e.Button != MouseButtons.Left) return;

            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_autoZoom == Rectangle.Empty)
                {
                    //判断鼠标是否落在图片绘图区
                    if ((_actionType == EPicViewerAction.ZoomIn && _curDrawRect.Contains(e.X, e.Y)) ||
                        (_actionType == EPicViewerAction.ZoomOut && e.X <= pictureBox.Width && e.Y <= pictureBox.Height))
                    {
                        _padPoint = Point.Empty;
                        _zoomPoint = new Point(e.X, e.Y);
                    }
                    else
                    {
                        _padPoint = Point.Empty;
                        _zoomPoint = Point.Empty;
                    }
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                _padPoint = new Point(e.X, e.Y);
                _zoomPoint = Point.Empty;
                Cursor.Current = _mPaning;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_zoomPoint != Point.Empty)//拉框放大,画虚线框
                {
                    ControlPaint.DrawReversibleFrame(_trackRectangle, BackColor, FrameStyle.Dashed);

                    //鼠标不能超过的范围
                    Rectangle standardRect;
                    if (_actionType == EPicViewerAction.ZoomIn)
                        standardRect = _curDrawRect;
                    else
                        standardRect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);

                    Point tmpPoint = new Point(e.X, e.Y);
                    if (!standardRect.Contains(tmpPoint))
                    {
                        if (tmpPoint.X < standardRect.Left)
                            tmpPoint.X = standardRect.Left;
                        if (tmpPoint.X > standardRect.Right)
                            tmpPoint.X = standardRect.Right;
                        if (tmpPoint.Y < standardRect.Top)
                            tmpPoint.Y = standardRect.Top;
                        if (tmpPoint.Y > standardRect.Bottom)
                            tmpPoint.Y = standardRect.Bottom;
                    }

                    Point startPoint = PointToScreen(_zoomPoint);
                    Point endPoint = PointToScreen(tmpPoint);
                    int width = endPoint.X - startPoint.X;
                    int height = endPoint.Y - startPoint.Y;
                    _trackRectangle = new Rectangle(startPoint.X, startPoint.Y, width, height);

                    ControlPaint.DrawReversibleFrame(_trackRectangle, BackColor, FrameStyle.Dashed);
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                Point thisPoint = new Point(e.X, e.Y);
                Rectangle standardRect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);
                //超出picturebox就不处理
                if (_padPoint != Point.Empty && standardRect.Contains(thisPoint))//平移
                {
                    Pad(thisPoint);
                    _padPoint = thisPoint;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Point toPoint = new Point(e.X, e.Y);
            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_zoomPoint != Point.Empty)//拉框放大
                {
                    //鼠标不能超过的范围
                    Rectangle standardRect;
                    if (_actionType == EPicViewerAction.ZoomIn)
                        standardRect = _curDrawRect;
                    else
                        standardRect = new Rectangle(0, 0, pictureBox.Width, pictureBox.Height);

                    Point tmpPoint = new Point(e.X, e.Y);
                    if (!standardRect.Contains(tmpPoint))
                    {
                        if (tmpPoint.X < standardRect.Left)
                            tmpPoint.X = standardRect.Left;
                        if (tmpPoint.X > standardRect.Right)
                            tmpPoint.X = standardRect.Right;
                        if (tmpPoint.Y < standardRect.Top)
                            tmpPoint.Y = standardRect.Top;
                        if (tmpPoint.Y > standardRect.Bottom)
                            tmpPoint.Y = standardRect.Bottom;
                    }

                    int x = _zoomPoint.X;
                    int y = _zoomPoint.Y;
                    int width = Math.Abs(toPoint.X - x);
                    int height = Math.Abs(toPoint.Y - y);

                    if (x > toPoint.X) x = toPoint.X;
                    if (y > toPoint.Y) y = toPoint.Y;
                    _autoZoom = new Rectangle(x, y, width, height);

                    _zoomPoint = Point.Empty;

                    //取消掉图片上的临时框
                    ControlPaint.DrawReversibleFrame(_trackRectangle, BackColor, FrameStyle.Dashed);
                    _trackRectangle = new Rectangle(0, 0, 0, 0);

                    ///放大和缩小
                    if (_actionType == EPicViewerAction.ZoomIn)
                        AutoZoomIn();
                    else if (_actionType == EPicViewerAction.ZoomOut)
                        AutoZoomOut();

                    _autoZoom = Rectangle.Empty;
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                if (_padPoint != Point.Empty)//平移
                {
                    _padPoint = Point.Empty;
                }

                Cursor = _pan;
            }
        }


        /// <summary>
        /// 显示当前图片比例3秒钟
        /// </summary>
        private void ShowPercentLabel()
        {
            label1.Text = (_curDrawRect.Width * 100.0 / _image.Width).ToString("f0") + "%";
            label1.Visible = true;
            Application.DoEvents();
            if (_timer.Enabled) _timer.Stop();
            _timer.Interval = 3000;
            _timer.Tick += m_LabelTicker_Tick;
            _timer.Start();
        }

        void m_LabelTicker_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Tick -= m_LabelTicker_Tick;
            label1.Visible = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //无图片时不执行操作
            if (_image == null)
                return;
            //初始化显示图片
            if (_curDrawRect == Rectangle.Empty || _curImgRect == Rectangle.Empty)
            {
                Full();
                return;
            }

            try
            {
                //判断拉框放大的框是否还存在
                if (_autoZoom != Rectangle.Empty)
                {
                    ControlPaint.DrawReversibleFrame(_trackRectangle, BackColor, FrameStyle.Dashed);
                    _trackRectangle = new Rectangle(0, 0, 0, 0);
                    _autoZoom = Rectangle.Empty;
                }

                //设置高质量,低速度呈现平滑程度
                Graphics grap = e.Graphics;
                //			grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //			grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //			grap.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //清空一下画布
                grap.Clear(BackColor);
                grap.DrawImage(_image, _curDrawRect, _curImgRect, GraphicsUnit.Pixel);
            }
            catch { }

            OnPaint(e);
        }

        public new void Refresh()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            ((Control)this).Refresh();
        }

        /// <summary>
        /// 左键双击放大，右键双击缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ZoomIn();
            else
                ZoomOut();
        }

        /// <summary>
        /// 一旦进入picturebox就捕捉鼠标，以便进行滚轮缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }


        #region IViewer 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return;

            PicFilePath = filePath;
            try
            {
                _image?.Dispose();
                _image = Image.FromFile(PicFilePath);
                ShowPercentLabel();
                Full();
            }
            catch
            {
                if (MessageBox.Show("无法打开指定图片！是否使用浏览器打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var registryKey = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");//记录浏览器地址的注册表（一般为IE）
                    var regValue = registryKey.GetValue("").ToString();
                    var defaultBrowserPath = regValue.Split("\"".ToCharArray())[1];
                    System.Diagnostics.Process.Start(defaultBrowserPath, "file:" + filePath);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            PicFilePath = "";
            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }
            pictureBox.Refresh();
        }
        #endregion
    }
}

