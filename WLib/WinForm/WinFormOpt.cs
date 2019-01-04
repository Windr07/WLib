/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2017/5/8 17:16:51
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Linq;
using System.Windows.Forms;

namespace WLib.WinForm
{
    /// <summary>
    /// 窗口相关操作
    /// </summary>
    public static class WinFormOpt
    {
        /// <summary>
        /// 显示MDI子窗体，防止重复打开同一窗体
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="subForm">子窗体</param>
        public static Form ShowMDIForm(this Form parentForm, Form subForm)
        {
            var openedForm = parentForm.MdiChildren.FirstOrDefault(v => v.Name.Equals(subForm.Name) && v.Text.Equals(subForm.Text));
            if (openedForm == null)
            {
                subForm.MdiParent = parentForm;
                subForm.Show();
                subForm.WindowState = FormWindowState.Maximized;
                return subForm;
            }
            else
            {
                subForm.Close();
                subForm.Dispose();
                openedForm.MdiParent = parentForm;
                openedForm.Show();
                openedForm.WindowState = FormWindowState.Maximized;
                return openedForm;
            }
        }
        /// <summary>
        /// 显示独立子窗体，防止重复打开同一窗体
        /// </summary>
        /// <param name="parentForm"></param>
        /// <param name="subForm">子窗体</param>
        public static void ShowIndependentForm(this Form parentForm, Form subForm)
        {
            var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(v => v.GetType() == subForm.GetType());
            if (openedForm != null)
            {
                if (openedForm.Visible == false)
                    openedForm.Show(parentForm);
                openedForm.WindowState = FormWindowState.Normal;
                openedForm.BringToFront();
                subForm.Dispose();
            }
            else
            {
                subForm.Show(parentForm);
                subForm.BringToFront();
            }
        }
    }
}
