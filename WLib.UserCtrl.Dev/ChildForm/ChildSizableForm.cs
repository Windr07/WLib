using System;
using System.Windows.Forms;
using WLib.WindowsAPI;

namespace WLib.UserCtrls.Dev.ChildForm
{
    /// <summary>
    /// 自定义的子窗体，包含重复打开的控制
    /// </summary>
    public partial class ChildSizableForm : DevExpress.XtraEditors.XtraForm
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
        /// 父窗体中是否已经Show过了此类型的窗口（设置父窗体后此属性才有效）
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


        public ChildSizableForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public ChildSizableForm(Form parentForm)
            : base()
        {
            Type formType = this.GetType();
            Type t = CommonUtility.ListFormType.Find(p => p == formType);
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
            }
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

        private void ChildForm_Load(object sender, EventArgs e)
        {
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

    }



}
