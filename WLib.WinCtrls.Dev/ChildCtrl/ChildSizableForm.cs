/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WLib.WindowsAPI;

namespace WLib.WinCtrls.Dev.ChildCtrl
{
    /// <summary>
    /// 自定义的子窗体，包含重复打开的控制
    /// </summary>
    public partial class ChildSizableForm : DevExpress.XtraEditors.XtraForm
    {
        private Form _parentForm;
        private readonly Type _formType;
        /// <summary>
        /// 父窗体中是否已经Show过了此类型的窗口（设置父窗体后此属性才有效）
        /// </summary>
        public bool HasShow { get; set; }
        /// <summary>
        /// 类型记录列表，用于防止打开同类型的窗体
        /// </summary>
        internal static List<Type> ListFormType = new List<Type>();
        /// <summary>
        /// 自定义的父窗口
        /// </summary>
        public Form FatherForm { get => _parentForm; private set { _parentForm = value; SetMidChildForm(); } }
        /// <summary>
        /// 只能是无边框窗体，因为边框是自定义的
        /// </summary>
        public new FormBorderStyle FormBorderStyle { get => FormBorderStyle.None; set => base.FormBorderStyle = FormBorderStyle.None; }


        /// <summary>
        /// 自定义的子窗体，包含重复打开的控制
        /// </summary>
        public ChildSizableForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        /// <summary>
        /// 自定义的子窗体，包含重复打开的控制
        /// </summary>
        /// <param name="parentForm"></param>
        public ChildSizableForm(Form parentForm)
        {
            Type formType = this.GetType();
            Type t = ListFormType.Find(p => p == formType);
            if (t != null)
            {
                this.Visible = false;
                HasShow = true;
            }
            else
            {
                InitializeComponent();
                _formType = formType;
                ListFormType.Add(formType);
                FatherForm = parentForm;
            }
        }
        /// <summary>
        /// 设置窗体成为主窗体的子窗体
        /// </summary> 
        private void SetMidChildForm()
        {
            this.FormClosed += ChildForm_FormClosed;
            this.Disposed += ChildForm_Disposed;
            this.Icon = _parentForm.Icon;
            WinApi.SetParent((int)this.Handle, (int)_parentForm.Handle);
        }


        private void ChildForm_Disposed(object sender, EventArgs e)
        {
            if (_formType != null)
            {
                ListFormType.Remove(_formType);
                _parentForm.Focus();
            }
        }

        private void ChildForm_FormClosed(object sender, EventArgs e)
        {
            if (_formType != null)
            {
                ListFormType.Remove(_formType);
                _parentForm.Focus();
            }
        }

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
