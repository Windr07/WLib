using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WLib.WindowsAPI;

namespace WLib.UserCtrls.Dev.ChildForm
{
    /// <summary>
    /// 自定义的子窗体，包含重复打开的控制
    /// </summary>
    public partial class ChildForm : DevExpress.XtraEditors.XtraForm
    {
        private Form _parentForm = null;
        Type FormType = null;
        private bool _hasshow = false;
        /// <summary>
        /// 自定义的父窗口
        /// </summary>
        public Form FatherForm
        {
            get => _parentForm;
            private set
            {
                _parentForm = value;
                SetMidChildForm();
            }
        }
        /// <summary>
        /// 是否已经Show过了此类型的窗口（设置父窗体后此属性才有效）
        /// </summary>
        public bool HasShow => _hasshow;

        /// <summary>
        /// 只能是无边框窗体，因为边框是自定义的
        /// </summary>
        public new FormBorderStyle FormBorderStyle
        {
            get => FormBorderStyle.None;
            set => base.FormBorderStyle = FormBorderStyle.None;
        } 
        [AmbientValue("")]
        [Localizable(true)]
        public new Icon Icon
        {
            get => base.Icon;
            set
            {
                base.Icon = value;
                this.picIcon.BackgroundImage = value.ToBitmap();
            }
        }


        public ChildForm()
        {
            InitializeComponent();
            this.TextChanged += ChildForm_TextChanged;
            this.picIcon.BackgroundImage = this.Icon.ToBitmap();
            this.StartPosition = FormStartPosition.CenterScreen; 
        }
        public ChildForm(Form parentForm)
            : base()
        {
            Type formType = this.GetType();
            Type t = CommonUtility.ListFormType.Find(p =>
            {
                if (p == formType) return true; else return false;
            });
            if (t != null)
            {
                this.Visible = false;
                _hasshow = true;
            }
            else
            {
                InitializeComponent();
                FormType = formType;
                CommonUtility.ListFormType.Add(formType);
                FatherForm = parentForm;
                this.TextChanged += ChildForm_TextChanged;
                this.picIcon.BackgroundImage = this.Icon.ToBitmap();
            } 
        }

        private void ChildForm_TextChanged(object sender, EventArgs e)
        {
            this.lbTitle.Text = this.Text; 
        }


        /// <summary>
        /// 设置窗体成为主窗体的子窗体
        /// </summary> 
        private void SetMidChildForm()
        {
            this.FormClosed += new FormClosedEventHandler(ChildForm_FormClosed);
            this.Disposed += new EventHandler(ChildForm_Disposed);
            this.Icon = _parentForm.Icon;
            WinApi.SetParent((int)this.Handle, (int)_parentForm.Handle);
        } 
        //窗体注销
        private void ChildForm_Disposed(object sender, EventArgs e)
        {
            if (FormType != null)
            {
                CommonUtility.ListFormType.Remove(FormType);
                _parentForm.Focus();
            }
        }
        //窗体关闭
        private void ChildForm_FormClosed(object sender, EventArgs e)
        {
            if (FormType != null)
            {
                CommonUtility.ListFormType.Remove(FormType);
                _parentForm.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void ChildForm_Load(object sender, EventArgs e)
        { 
        } 

        private void topBaner_MouseDown(object sender, MouseEventArgs e)
        {
            WinFormStyle.FormPan(this.Handle);
            this.Refresh();
        }

        private void lbTitle_MouseDown(object sender, MouseEventArgs e)
        {
            WinFormStyle.FormPan(this.Handle);
            this.Refresh();
        }

        /// <summary>
        /// 窗体大小改变时,刷新窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChildForm_Resize(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Refresh();
        }

        #region 收缩窗体
        int oldHeight;//记录下原来窗体的高度
        private void btnExpant_Click(object sender, EventArgs e)
        {
            int topBanerHeight = this.topBaner.Height;
            if (this.Height != topBanerHeight)
            {
                oldHeight = this.Height;
                this.Height = topBanerHeight;
                this.bBorder.Visible = false;
                btnExpant.ImageIndex = 1;
            }
            else
            {
                this.Height = oldHeight;
                btnExpant.ImageIndex = 2;
                this.bBorder.Visible = true;
            }
            this.Refresh();
        }
        #endregion

        

        #region 自定义窗体大小控制
        //表示鼠标当前是否处于按下状态，初始值为否 
        bool isMouseDown = false;
        //表示拖动的方向，起始为None，表示不拖动  
        MouseDirection direction = MouseDirection.None;
        private void rBorder_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标按下 
            isMouseDown = true;  
        } 
        private void rBorder_MouseUp(object sender, MouseEventArgs e)
        {
            // 鼠标弹起， 
            isMouseDown = false;          
            direction = MouseDirection.None;
        }
        private void rBorder_MouseLeave(object sender, EventArgs e)
        { 
            isMouseDown = false;
            direction = MouseDirection.None;
            this.Cursor = Cursors.Default;
        }
        private void rBorder_MouseMove(object sender, MouseEventArgs e)
        {
            //右 
            this.Cursor = Cursors.SizeWE;
            direction = MouseDirection.Herizontal;
            if (e.Location.Y >= rBorder.Height - 5)
            {
                //右下
                this.Cursor = Cursors.SizeNWSE;
                direction = MouseDirection.Declining;
            }
            ResizeWindow();
        }

        private void bBorder_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标按下 
            isMouseDown = true;  
        } 
        private void bBorder_MouseUp(object sender, MouseEventArgs e)
        {
            // 鼠标弹起， 
            isMouseDown = false;
            direction = MouseDirection.None;
        }
        private void bBorder_MouseLeave(object sender, EventArgs e)
        { 
            isMouseDown = false;
            direction = MouseDirection.None;
            this.Cursor = Cursors.Default;
        }
        private void bBorder_MouseMove(object sender, MouseEventArgs e)
        {
            //下
            this.Cursor = Cursors.SizeNS;
            direction = MouseDirection.Vertical;
            ResizeWindow();
        }
         
        private void ResizeWindow()
        {
            if (!isMouseDown) return;
            //MousePosition的参考点是屏幕的左上角，表示鼠标当前相对于屏幕左上角的坐标this.left和this.top的参考点也是屏幕，属性MousePosition是该程序的重点  
            if (direction == MouseDirection.Declining)
            {              
                this.Cursor = Cursors.SizeNWSE;
                //下面是改变窗体宽和高的代码
                this.Width = MousePosition.X - this.Left;
                this.Height = MousePosition.Y - this.Top;
            }
            //以下同理 
            else if (direction == MouseDirection.Herizontal)
            {
                this.Cursor = Cursors.SizeWE;
                this.Width = MousePosition.X - this.Left;
            }
            else if (direction == MouseDirection.Vertical)
            {
                this.Cursor = Cursors.SizeNS;
                this.Height = MousePosition.Y - this.Top;
            }
            //即使鼠标按下，但是不在窗口右和下边缘，那么也不能改变窗口大小 
            else
                this.Cursor = Cursors.Default; 
        }
       
#endregion


        #region 自定义窗体边框颜色，标题栏颜色
        Color formBorderColor = Color.LightBlue;
        private void topBaner_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.Width == 0 || e.ClipRectangle.Height == 0) return;
            Color co1 = this.BackColor;
            //byte r = Convert.ToByte(((int)co1.R + 20) >= 255 ? 255 : (int)co1.R + 20);
            //byte g = Convert.ToByte(((int)co1.G + 30) >= 255 ? 255 : (int)co1.G + 30);
            //byte b = Convert.ToByte(((int)co1.B + 20) >= 255 ? 255 : (int)co1.B + 20);
            //Color co2 = Color.FromArgb(r, g, b);
            //Color co1 = Color.LightBlue;
            Color co2 = Color.WhiteSmoke;
            LinearGradientBrush brush = new LinearGradientBrush(e.ClipRectangle, co1, co2, LinearGradientMode.Vertical); e.Graphics.FillRectangle(brush, e.ClipRectangle);
            brush.SetBlendTriangularShape(0.5f); 

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                                formBorderColor, 1, ButtonBorderStyle.Solid,
                                formBorderColor, 1, ButtonBorderStyle.Solid,
                                formBorderColor, 1, ButtonBorderStyle.Solid,
                                Color.LightGray, 1, ButtonBorderStyle.Solid); 

        }

        private void rBorder_Paint(object sender, PaintEventArgs e)
        {
            Color coBorder = Color.Transparent;
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                formBorderColor, 1, ButtonBorderStyle.Solid,
                                formBorderColor, 1, ButtonBorderStyle.Solid); 
        }

        private void bBorder_Paint(object sender, PaintEventArgs e)
        {
            Color coBorder = Color.Transparent;
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                formBorderColor, 1, ButtonBorderStyle.Solid); 
        }

        private void lBorder_Paint(object sender, PaintEventArgs e)
        {
            Color coBorder = Color.Transparent;
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                                formBorderColor, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid,
                                coBorder, 1, ButtonBorderStyle.Solid);
        }
        #endregion


        private void topBaner_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }


    }
        

   
}
