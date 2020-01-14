using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WLib.UserCtrls.Dev.ChildForm
{
    /// <summary>
    /// 子窗体管理类
    /// </summary>
    public static class ChildFormLib
    {
        
        /// <summary>
        /// 打开子窗体（已存在同类窗体时，不再重复打开，而是显示已有的窗体）
        /// </summary>
        /// <param name="newform">已经实例化的子窗体</param>
        /// <param name="openDifTextForm">是否允许打开标题不同的同一类型窗体</param>
        public static void ShowSizableForm(ChildSizableForm newform, bool openDifTextForm = false)
        {
            if (newform == null) return;
            if (newform.HasShow)
            {
                var forms = Application.OpenForms.Cast<Form>().Where(v => v.GetType().Equals(newform.GetType()));

                //已show过同类窗体，并且允许比较窗体标题且标题不相同：建立新窗体
                if (openDifTextForm && !forms.Any(v => v.Text.Equals(newform.Text)))
                {
                    newform.Show();
                }
                else//销毁新窗体，显示旧窗体
                {
                    newform.Close();
                    newform.Dispose();

                    Form form = forms.FirstOrDefault(v => v.Text.Equals(newform.Text));
                    if (form != null && form.WindowState == FormWindowState.Minimized)
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.BringToFront();
                    }
                }
            }
            else//未显示过同类型窗体时，显示新窗体
            {
                newform.Show();
        } 
        }
        /// <summary>
        /// 打开子窗体（已存在同类窗体时，仍然打开新窗体）
        /// </summary>
        /// <param name="newform">已经实例化的子窗体</param>
        /// <param name="openDifTextForm">是否允许打开标题不同的同一类型窗体</param>
        public static void ShowNewSizableForm(ChildSizableForm newform, bool openDifTextForm = false)
        {
            if (newform == null) return;
            if (newform.HasShow)
            {
                var forms = Application.OpenForms.Cast<Form>().Where(v => v.GetType().Equals(newform.GetType()));
                if (openDifTextForm)
                {
                    forms = forms.Where(v => v.Text.Equals(newform.Text));
                }
                ////销毁旧窗体
                //foreach (var form in forms)
                //{
                //    form.Close(); form.Dispose();
                //}
            }
            newform.Show();
        }
    }
}
