/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/8 17:16:51
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// WinForm窗口或控件相关操作
    /// </summary>
    public static class WinFormOpt
    {
        /// <summary>
        /// 从控件集合中查找指定窗体类型（Form.GetType()）相同的窗体
        /// </summary>
        /// <param name="controls">控件集</param>
        /// <param name="subFormType">要查找的子窗体的类型</param>
        /// <param name="subFormText">窗体标题，值为null表示不比较窗体标题；值不为null则比较窗体标题(Form.Text)，返回类型相同且窗体标题相同的窗体</param>
        /// <returns></returns>
        public static Form QueryForm(this IEnumerable<Control> controls, Type subFormType, string subFormText = null)
        {
            if (!IsType(subFormType, typeof(Form)))
                throw new Exception($"参数{nameof(subFormType)}的类型不是窗体类型！");

            return subFormText == null ?
                controls.OfType<Form>().FirstOrDefault(v => v.GetType() == subFormType.GetType() && v.Text.Equals(subFormText)) :
                controls.OfType<Form>().FirstOrDefault(v => v.GetType() == subFormType.GetType());
        }
        /// <summary>
        /// 显示MDI子窗体，防止重复打开同一窗体
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="subFormType">要查找的子窗体的类型</param>
        /// <param name="subFormText">窗体标题，值为null表示不比较窗体标题；值不为null则比较窗体标题(Form.Text)，返回类型相同且窗体标题相同的窗体</param>
        /// <param name="args">窗体构造函数的参数</param>
        public static Form ShowMdiForm(this Form parentForm, Type subFormType, string subFormText = null, object[] args = null)
        {
            var openedForm = QueryForm(parentForm.MdiChildren, subFormType, subFormText);
            if (openedForm == null)
                openedForm = (Form)Activator.CreateInstance(subFormType, args);

            openedForm.MdiParent = parentForm;
            openedForm.Show();
            openedForm.WindowState = FormWindowState.Maximized;
            openedForm.BringToFront();
            return openedForm;
        }
        /// <summary>
        /// 显示独立子窗体，防止重复打开同一窗体
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="subForm">子窗体</param>
        /// <param name="subFormText">是否比较窗体标题，True表示窗体标题(Form.Text)不同则不是同一窗体，False表示只比较窗体类型(Form.GetType())</param>
        public static Form ShowIndependentForm(this Form parentForm, Type subFormType, string subFormText = null, object[] args = null)
        {
            var openedForm = QueryForm(Application.OpenForms.Cast<Form>(), subFormType, subFormText);
            if (openedForm == null)
                openedForm = (Form)Activator.CreateInstance(subFormType, args);

            if (openedForm.Visible == false)
                openedForm.Show(parentForm);

            openedForm.WindowState = FormWindowState.Normal;
            openedForm.BringToFront();

            return openedForm;
        }
        /// <summary>
        /// 向控件添加和显示窗体，防止重复打开同一窗体
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="formType">要显示的窗体类型，若容器的控件集已存在同类型窗体则直接打开窗体，否则在容器控件集中创建并加入该类型新窗体再显示新窗体</param>
        /// <param name="args"></param>
        /// <param name="resize">自动调整窗口大小适配控件的大小的选项</param>
        /// <returns></returns>
        public static Form ShowSubForm(this Control parentControl, Type formType, string subFormText = null, object[] args = null, EResize resize = EResize.Auto)
        {
            var openedForm = QueryForm(parentControl.Controls.OfType<Form>(), formType, subFormText);
            if (openedForm == null)
                openedForm = (Form)Activator.CreateInstance(formType, args);

            return parentControl.ShowSubForm(openedForm, resize);
        }


        /// <summary>
        /// 向控件添加和显示窗体，隐藏窗体标题栏
        /// </summary>
        /// <param name="parentControl">容器控件</param>
        /// <param name="form">显示在容器控件内的窗体</param>
        /// <param name="resize">自动调整窗口大小适配控件的大小的选项</param>
        /// <returns></returns>
        public static Form ShowSubForm(this Control parentControl, Form form, EResize resize = EResize.Auto)
        {
            //if (_reisze == null)
            //{
            //    _reisze = (sender, e) =>
            //    {
            //        if (form != null && !form.IsDisposed) parentControl.ResizeForm(form, resize);
            //    };
            //}
            //parentControl.Resize -= _reisze;
            //parentControl.Resize += _reisze;

            form.TopLevel = false; //窗体设置成非顶级控件
            form.FormBorderStyle = FormBorderStyle.None;//去掉窗体边框
            form.Parent = parentControl;//指定子窗体显示的容器
            parentControl.ResizeForm(form, resize);
            form.Refresh();
            form.Show();
            form.BringToFront();
            return form;
        }

        /// <summary>
        /// 按照包含窗口的容器控件和调整选项，调整窗口大小
        /// </summary>
        /// <param name="parentCtrl">包含窗口的容器控件</param>
        /// <param name="form">要调整大小的窗口</param>
        /// <param name="resize">窗体自动调整大小的选项</param>
        public static void ResizeForm(this Control parentCtrl, Form form, EResize resize)
        {
            var scrollWidth = parentCtrl.GetScrollWidth(ScrollBars.Vertical);
            var scrollHeight = parentCtrl.GetScrollWidth(ScrollBars.Horizontal);

            if (resize == EResize.Auto)
            {
                form.Width = parentCtrl.ClientSize.Width - scrollWidth;
                form.Height = parentCtrl.ClientSize.Height - scrollHeight;
            }
            else if (resize == EResize.AutoWidth) form.Width = parentCtrl.ClientSize.Width - scrollWidth;
            else if (resize == EResize.AutoHeight) form.Height = parentCtrl.ClientSize.Height - scrollHeight;
            //form.Refresh();
            //parentCtrl.Refresh();
        }

        /// <summary>
        /// 获取控件滚动条的宽度，没有滚动条则返回0
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        public static int GetScrollWidth(this Control ctrl, ScrollBars scrollBars)
        {
            if (ctrl is ScrollableControl sCtrl)
            {
                if (scrollBars == ScrollBars.Vertical)
                    return sCtrl.Size.Height - sCtrl.ClientSize.Height;
                else if (scrollBars == ScrollBars.Horizontal)
                    return sCtrl.Size.Width - sCtrl.ClientSize.Width;
            }
            return 0;
        }
        /// <summary>
        /// 容器控件的控件大小调整事件
        /// </summary>
        private static EventHandler _reisze = null;
        /// <summary>
        /// 判断类型是否为指定类型或继承自指定类型
        /// </summary>
        /// <param name="type">被判断的类型</param>
        /// <param name="compareType">指定类型，表示被判断的类型是否继承自该类型</param>
        /// <returns></returns>
        public static bool IsType(Type type, Type compareType)
        {
            if (type == compareType)
                return true;
            else if (type == null)
                return false;
            else
                return IsType(type.BaseType, compareType);
        }
    }
}
