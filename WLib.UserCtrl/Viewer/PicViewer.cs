///////////////////////////////////////////////
//简单的图片查看控件
//如果放得越大，速度越快
//萧嘉明20130531
//////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WLib.UserCtrls.Viewer
{
    /// <summary>
    /// 图片简单浏览控件
    /// </summary>
    public partial class PicViewer : UserControl,IViewer
    {
        private PictureBox _pictureBox1;
        private readonly Timer _mLabelTicker = new Timer();

        /// <summary>
        /// 图片控件的操作类型
        /// </summary>
        private EPicViewerAction _actionType = 0;
        /// <summary>
        /// 图片控件的操作类型
        /// </summary>
        public EPicViewerAction ActionType
        {
            get => _actionType;
            set
            {
                _actionType = value;

                //设置鼠标
                if (_actionType == EPicViewerAction.ZoomIn)
                {
                    _pictureBox1.Cursor = _mZoomin;
                }
                else if (_actionType == EPicViewerAction.ZoomOut)
                {
                    _pictureBox1.Cursor = _mZoomout;
                }
                else if (_actionType == EPicViewerAction.Pan)
                {
                    _pictureBox1.Cursor = _mPan;
                }
            }
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        private string _mPicfile;
        private Image _mImage;
        /// <summary>
        /// 当前要显示的图片路径
        /// </summary>
        public string PicFile
        {
            set
            {
                if (value != "" && System.IO.File.Exists(value))
                {
                    try
                    {
                        _mPicfile = value;
                        _mImage?.Dispose();
                        _mImage = Image.FromFile(_mPicfile);
                        //this.zoomTrackBarControl1.Value = (int)(this.Width * 1.0 / this.m_image.Width);
                        ShowPercentLabel();
                        Full();
                    }
                    catch (Exception ex)
                    {
                        ////COM.LIB.LOG.LogOperator.WriteLog(ex.ToString());
                        if (MessageBox.Show("无法打开指定图片！是否使用浏览器打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\"); 
                            string s = key.GetValue("").ToString();
                            string[] tt = s.Split("\"".ToCharArray());
                            System.Diagnostics.Process.Start(tt[1], "file:" + value);
                        }
                    }
                }
                else
                {
                    _mPicfile = "";
                    if (_mImage != null)
                    {
                        _mImage.Dispose();
                        _mImage = null;
                    }
                    _pictureBox1.Refresh();
                }
            }
            get => _mPicfile;
        }

        private bool CheckChrome()
        {
            RegistryKey uninstallNode = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                if (subKeyName.Equals("chrome.exe"))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        public System.IO.Stream BackImage
        {
            set => _pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        /// <summary>
        /// 图片框的画图区域
        /// </summary>
        private Rectangle _mCurDrawRect;
        /// <summary>
        /// 图片的显示区域
        /// </summary>
        private Rectangle _mCurImgRect;
        /// <summary>
        /// 拉框放大的区域
        /// </summary>
        private Rectangle _mAutoZoom;
        private Point _mPadPoint;
        private Point _mZoomPoint;

        private Rectangle _mTrackRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));

        /// <summary>
        /// 光标
        /// </summary>
        private readonly Cursor _mPan;
        private readonly Cursor _mPaning;
        private readonly Cursor _mZoomin;
        private readonly Cursor _mZoomout;

        private Color _mBackColor = Color.White;
        /// <summary>
        /// 背景色
        /// </summary>
        public override Color BackColor
        {
            get => _mBackColor;

            set => _mBackColor = value;
        }

        public PicViewer()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            _mPan = Cursors.Default;
            _mPaning = Cursors.Default;
            _mZoomin = Cursors.Default;
            _mZoomout = Cursors.Default;

            //m_pan = new Cursor(GetType().Assembly.GetManifestResourceStream("CommandToolSample.PlugManage.AdvancedTools.Cursor.Pan.cur"));
            //m_paning = new Cursor(GetType().Assembly.GetManifestResourceStream("CommandToolSample.PlugManage.AdvancedTools.Cursor.Paning.cur"));
            //m_zoomin = new Cursor(GetType().Assembly.GetManifestResourceStream("CommandToolSample.PlugManage.AdvancedTools.Cursor.ZoomIn.cur"));
            //m_zoomout = new Cursor(GetType().Assembly.GetManifestResourceStream("CommandToolSample.PlugManage.AdvancedTools.Cursor.ZoomOut.cur"));

            //m_backColor = Color.White;
            _mBackColor = Color.Transparent;

            _pictureBox1.MouseWheel += new MouseEventHandler(pictureBox1_MouseWheel);
            // TODO: 在 InitializeComponent 调用后添加任何初始化      
        }

        void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
        
        public void DisposeImage()
        {
            if (_mImage != null)
                _mImage.Dispose();
        }

        private void picViewer_Load(object sender, EventArgs e)
        {
            //			this.Full();

            ///默认的操方式为平移
            ActionType = EPicViewerAction.Pan;
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (Width < 200)
            {
                Width = 200;
            }
            if (Height < 200)
            {
                Height = 200;
            }

            if (_mImage == null)
                return;

            //			this.pictureBox1.Refresh();
            //this.Full();
        }

        #region  对外功能

        /// <summary>
        /// 放大操作
        /// </summary>
        public void ZoomIn()
        {
            if (_mImage == null)
                return;

            //计算放大的范围和画图的坐标
            int newht = (int)(_mCurDrawRect.Height * 1.2);
            int newwt = (int)(_mCurDrawRect.Width * 1.2);
            int ox = (int)((newwt - _mCurDrawRect.Width) / 2);
            int oy = (int)((newht - _mCurDrawRect.Height) / 2);
            int x = _mCurDrawRect.X - ox;
            int y = _mCurDrawRect.Y - oy;

            //如果放大超过实际图片大小的倍数则放弃操作
            if ((newht / _mImage.Height) > 5 || (newwt / _mImage.Width) > 5)
                return;

            _mCurDrawRect = new Rectangle(x, y, newwt, newht);
            _pictureBox1.Invalidate();
            ShowPercentLabel();
        }

        /// <summary>
        /// 缩小
        /// </summary>
        public void ZoomOut()
        {
            if (_mImage == null)
                return;

            //计算放大的范围和画图的坐标
            int pbh = _pictureBox1.Height;
            int pbw = _pictureBox1.Width;
            int newht = (int)(_mCurDrawRect.Height / 1.2);
            int newwt = (int)(_mCurDrawRect.Width / 1.2);
            int ox = (int)((_mCurDrawRect.Width - newwt) / 2);
            int oy = (int)((_mCurDrawRect.Height - newht) / 2);
            int x = _mCurDrawRect.X + ox;
            int y = _mCurDrawRect.Y + oy;

            //判断边缘
            if (newwt > pbw)
            {
                if (x > 0) x = 0;
                if ((x + newwt) < pbw) x = pbw - newwt;
            }
            else
            {
                x = (int)((pbw - newwt) / 2);
            }

            if (newht > pbh)
            {
                if (y > 0) y = 0;
                if ((y + newht) < pbh) y = pbh - newht;
            }
            else
            {
                y = (int)((pbh - newht) / 2);
            }

            //如果缩小小于图片框大小的倍数则放弃操作
            if ((_pictureBox1.Height / newht) > 5 || (_pictureBox1.Width / newwt) > 5)
                return;

            _mCurDrawRect = new Rectangle(x, y, newwt, newht);
            _pictureBox1.Invalidate();
            ShowPercentLabel();
        }
        /// <summary>
        /// 全图显示
        /// </summary>
        public void Full()
        {
            if (_mImage == null)
                return;

            int pbh = _pictureBox1.Height;
            int pbw = _pictureBox1.Width;

            int ht = _mImage.Height;
            int wt = _mImage.Width;

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

            _mCurDrawRect = new Rectangle(x, y, newwt, newht);
            _mCurImgRect = new Rectangle(0, 0, wt, ht);
            _pictureBox1.Invalidate();
            ShowPercentLabel();
        }

        /// <summary>
        /// 原图大小显示
        /// </summary>
        public void Zoom()
        {
            if (_mImage == null)
                return;

            int pbh = _pictureBox1.Height;
            int pbw = _pictureBox1.Width;

            int ht = _mImage.Height;
            int wt = _mImage.Width;

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

            _mCurDrawRect = new Rectangle(x, y, newwt, newht);
            _mCurImgRect = new Rectangle(0, 0, wt, ht);
            _pictureBox1.Invalidate();
            ShowPercentLabel();
        }

        #endregion

        #region 放大缩小平移具体实现函数

        /// <summary>
        /// 拉框放大
        /// </summary>
        private void AutoZoomIn()
        {
            if (_mImage == null)
                return;

            //picturebox控件范围
            double pbh = _pictureBox1.Height;
            double pbw = _pictureBox1.Width;

            //图片实际范围
            double drawh = _mCurDrawRect.Height;
            double draww = _mCurDrawRect.Width;
            double drawX = _mCurDrawRect.X;
            double drawY = _mCurDrawRect.Y;

            //图片原始范围
            double imgH = _mImage.Height;
            double imgW = _mImage.Width;

            //zoom框范围
            double zoomh = _mAutoZoom.Height;
            double zoomw = _mAutoZoom.Width;
            double zoomX = _mAutoZoom.X;
            double zoomY = _mAutoZoom.Y;

            //Zoom矩形不能是一条线或点，也就是width和height要大于0
            if (zoomh <= 0 || zoomw <= 0) return;

            //zoom框宽高比
            double zoomWhScale = (double)zoomw / (double)zoomh;
            //picturebox宽高比
            double picboxWhScale = (double)pbw / (double)pbh;

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
            Rectangle oldZoomDraw = Rectangle.Intersect(_mAutoZoom, _mCurDrawRect);

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
            if (((double)_pictureBox1.Height / newDrawH) < 0.1 || ((double)_pictureBox1.Width / newDrawW) < 0.1) return;

            _mCurDrawRect = new Rectangle((int)Math.Truncate(newDrawX), (int)Math.Truncate(newDrawY), (int)Math.Truncate(newDrawW), (int)Math.Truncate(newDrawH));

            _pictureBox1.Invalidate();
        }

        /// <summary>
        /// 拉框缩小
        /// </summary>
        private void AutoZoomOut()
        {
            if (_mImage == null)
                return;

            //各个高宽取值
            double pbh = _pictureBox1.Height;
            double pbw = _pictureBox1.Width;

            double drawh = _mCurDrawRect.Height;
            double draww = _mCurDrawRect.Width;
            double drawX = _mCurDrawRect.X;
            double drawY = _mCurDrawRect.Y;

            double imgH = _mImage.Height;
            double imgW = _mImage.Width;

            double zoomh = _mAutoZoom.Height;
            double zoomw = _mAutoZoom.Width;
            double zoomX = _mAutoZoom.X;
            double zoomY = _mAutoZoom.Y;

            //Zoom矩形不能是一条线或点，也就是width和height要大于0
            if (zoomh <= 0 || zoomw <= 0) return;

            double zoomWhScale = (double)zoomw / (double)zoomh;
            double picboxWhScale = (double)pbw / (double)pbh;

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

            Rectangle oldZoomDraw = Rectangle.Intersect(fillZoom, _mCurDrawRect);

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
            if (((double)_pictureBox1.Height / newDrawH) > 10 || ((double)_pictureBox1.Width / newDrawW) > 10) return;

            _mCurDrawRect = new Rectangle((int)Math.Truncate(newDrawX), (int)Math.Truncate(newDrawY), (int)Math.Truncate(newDrawW), (int)Math.Truncate(newDrawH));

            _pictureBox1.Invalidate();
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="toPoint"></param>
        private void Pad(Point toPoint)
        {
            int pbh = _pictureBox1.Height;
            int pbw = _pictureBox1.Width;

            int newht = _mCurDrawRect.Height;
            int newwt = _mCurDrawRect.Width;
            int x = _mCurDrawRect.X + (toPoint.X - _mPadPoint.X);
            int y = _mCurDrawRect.Y + (toPoint.Y - _mPadPoint.Y);

            if (_mCurDrawRect.X == x && _mCurDrawRect.Y == y)
                return;

            Rectangle newDrawRect = new Rectangle(x, y, newwt, newht);

            //如果图片完全不在picturebox中则不处理
            Rectangle picBoxRect = new Rectangle(0, 0, _pictureBox1.Width, _pictureBox1.Height);
            if (newDrawRect.IntersectsWith(picBoxRect) == false) return;

            _mCurDrawRect = newDrawRect;
            _pictureBox1.Invalidate();
        }
        #endregion

        #region MouseDown，MouseMove，MouseUp三大事件函数

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //非左键不操作
            if (e.Button != MouseButtons.Left) return;

            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_mAutoZoom == Rectangle.Empty)
                {
                    //判断鼠标是否落在图片绘图区
                    if ((_actionType == EPicViewerAction.ZoomIn && _mCurDrawRect.Contains(e.X, e.Y)) ||
                        (_actionType == EPicViewerAction.ZoomOut && e.X <= _pictureBox1.Width && e.Y <= _pictureBox1.Height))
                    {
                        _mPadPoint = Point.Empty;
                        _mZoomPoint = new Point(e.X, e.Y);
                    }
                    else
                    {
                        _mPadPoint = Point.Empty;
                        _mZoomPoint = Point.Empty;
                    }
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                _mPadPoint = new Point(e.X, e.Y);
                _mZoomPoint = Point.Empty;
                Cursor.Current = _mPaning;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_mZoomPoint != Point.Empty)//拉框放大,画虚线框
                {
                    ControlPaint.DrawReversibleFrame(_mTrackRectangle, BackColor, FrameStyle.Dashed);

                    //鼠标不能超过的范围
                    Rectangle standardRect;
                    if (_actionType == EPicViewerAction.ZoomIn)
                        standardRect = _mCurDrawRect;
                    else
                        standardRect = new Rectangle(0, 0, _pictureBox1.Width, _pictureBox1.Height);

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

                    Point startPoint = PointToScreen(_mZoomPoint);
                    Point endPoint = PointToScreen(tmpPoint);
                    int width = endPoint.X - startPoint.X;
                    int height = endPoint.Y - startPoint.Y;
                    _mTrackRectangle = new Rectangle(startPoint.X, startPoint.Y, width, height);

                    ControlPaint.DrawReversibleFrame(_mTrackRectangle, BackColor, FrameStyle.Dashed);
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                Point thisPoint = new Point(e.X, e.Y);
                Rectangle standardRect = new Rectangle(0, 0, _pictureBox1.Width, _pictureBox1.Height);
                //超出picturebox就不处理
                if (_mPadPoint != Point.Empty && standardRect.Contains(thisPoint))//平移
                {
                    Pad(thisPoint);
                    _mPadPoint = thisPoint;
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            Point toPoint = new Point(e.X, e.Y);
            if (_actionType == EPicViewerAction.ZoomIn || _actionType == EPicViewerAction.ZoomOut)
            {
                if (_mZoomPoint != Point.Empty)//拉框放大
                {
                    //鼠标不能超过的范围
                    Rectangle standardRect;
                    if (_actionType == EPicViewerAction.ZoomIn)
                        standardRect = _mCurDrawRect;
                    else
                        standardRect = new Rectangle(0, 0, _pictureBox1.Width, _pictureBox1.Height);

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

                    int x = _mZoomPoint.X;
                    int y = _mZoomPoint.Y;
                    int width = Math.Abs(toPoint.X - x);
                    int height = Math.Abs(toPoint.Y - y);

                    if (x > toPoint.X) x = toPoint.X;
                    if (y > toPoint.Y) y = toPoint.Y;
                    _mAutoZoom = new Rectangle(x, y, width, height);

                    _mZoomPoint = Point.Empty;

                    //取消掉图片上的临时框
                    ControlPaint.DrawReversibleFrame(_mTrackRectangle, BackColor, FrameStyle.Dashed);
                    _mTrackRectangle = new Rectangle(0, 0, 0, 0);

                    ///放大和缩小
                    if (_actionType == EPicViewerAction.ZoomIn)
                        AutoZoomIn();
                    else if (_actionType == EPicViewerAction.ZoomOut)
                        AutoZoomOut();

                    _mAutoZoom = Rectangle.Empty;
                }
            }
            else if (_actionType == EPicViewerAction.Pan)
            {
                if (_mPadPoint != Point.Empty)//平移
                {
                    _mPadPoint = Point.Empty;
                }

                Cursor = _mPan;
            }
        }

        #endregion

        /// <summary>
        /// 显示当前图片比例3秒钟
        /// </summary>
        private void ShowPercentLabel()
        {
            label1.Text = (_mCurDrawRect.Width * 100.0 / _mImage.Width).ToString("f0") + "%";
            label1.Visible = true;
            Application.DoEvents();
            if (_mLabelTicker.Enabled) _mLabelTicker.Stop();
            _mLabelTicker.Interval = 3000;
            _mLabelTicker.Tick += new EventHandler(m_LabelTicker_Tick);
            _mLabelTicker.Start();
        }

        void m_LabelTicker_Tick(object sender, EventArgs e)
        {
            _mLabelTicker.Stop();
            _mLabelTicker.Tick -= m_LabelTicker_Tick;
            label1.Visible = false;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //无图片时不执行操作
            if (_mImage == null)
                return;
            //初始化显示图片
            if (_mCurDrawRect == Rectangle.Empty || _mCurImgRect == Rectangle.Empty)
            {
                Full();
                return;
            }

            try
            {
                //判断拉框放大的框是否还存在
                if (_mAutoZoom != Rectangle.Empty)
                {
                    ControlPaint.DrawReversibleFrame(_mTrackRectangle, BackColor, FrameStyle.Dashed);
                    _mTrackRectangle = new Rectangle(0, 0, 0, 0);
                    _mAutoZoom = Rectangle.Empty;
                }

                //设置高质量,低速度呈现平滑程度
                Graphics grap = e.Graphics;
                //			grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //			grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //			grap.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //清空一下画布
                grap.Clear(_mBackColor);
                grap.DrawImage(_mImage, _mCurDrawRect, _mCurImgRect, GraphicsUnit.Pixel);
            }
            catch (Exception ex)
            {

            }

            OnPaint(e);
        }

        public void Refresh()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            ((Control) this).Refresh();
        }

        /// <summary>
        /// 左键双击放大，右键双击缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        /// <summary>
        /// 一旦进入picturebox就捕捉鼠标，以便进行滚轮缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            _pictureBox1.Focus();
        }

        #region IViewer 成员

        public void LoadFile(string file)
        {
            PicFile = file;
        }

        public void Close()
        {
            PicFile = "";
        }

        #endregion
    }
}

