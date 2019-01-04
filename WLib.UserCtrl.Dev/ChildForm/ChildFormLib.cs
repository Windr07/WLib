using System.Linq;
using System.Windows.Forms;

namespace WLib.UserCtrls.Dev.ChildForm
{
    /// <summary>
    /// 子窗体管理类
    /// </summary>
    public class ChildFormLib
    {
        /// <summary>
        /// 打开子窗体（已存在同类窗体时，不再重复打开，而是显示已有的窗体）
        /// </summary>
        /// <param name="from">已经实例化的子窗体</param>
        public static void ShowForm(ChildForm Newform)
        {
            if (Newform == null) return;
            if (Newform.HasShow)
            {
                Newform.Close();
                Newform.Dispose();
            }
            else
            {
                Newform.Show();
            }
        }

        /// <summary>
        /// 打开子窗体（已存在同类窗体时，不再重复打开，而是显示已有的窗体）
        /// </summary>
        /// <param name="from">已经实例化的子窗体</param>
        /// <param name="openDifTextForm">是否允许打开标题不同的同一类型窗体</param>
        public static void ShowSizableForm(ChildSizableForm Newform, bool openDifTextForm = false)
        {
            if (Newform == null) return;
            if (Newform.HasShow)
            {
                var forms = Application.OpenForms.Cast<Form>().Where(v => v.GetType().Equals(Newform.GetType()));

                //已show过同类窗体，并且允许比较窗体标题且标题不相同：建立新窗体
                if (openDifTextForm && !forms.Any(v => v.Text.Equals(Newform.Text)))
                {
                    Newform.Show();
                }
                else//销毁新窗体，显示旧窗体
                {
                    Newform.Close();
                    Newform.Dispose();

                    Form form = forms.FirstOrDefault(v => v.Text.Equals(Newform.Text));
                    if (form != null && form.WindowState == FormWindowState.Minimized)
                    {
                        form.WindowState = FormWindowState.Normal;
                        form.BringToFront();
                    }
                }
            }
            else//未显示过同类型窗体时，显示新窗体
            {
                Newform.Show();
        } 
        }


        /// <summary>
        /// 打开子窗体（已存在同类窗体时，仍然打开新窗体）
        /// </summary>
        /// <param name="Newform">已经实例化的子窗体</param>
        /// <param name="openDifTextForm">是否允许打开标题不同的同一类型窗体</param>
        public static void ShowNewSizableForm(ChildSizableForm Newform, bool openDifTextForm = false)
        {
            if (Newform == null) return;
            if (Newform.HasShow)
            {
                var forms = Application.OpenForms.Cast<Form>().Where(v => v.GetType().Equals(Newform.GetType()));
                if (openDifTextForm)
                {
                    forms = forms.Where(v => v.Text.Equals(Newform.Text));
                }
                ////销毁旧窗体
                //foreach (var form in forms)
                //{
                //    form.Close(); form.Dispose();
                //}
            }
            Newform.Show();
        }

    }

    /// <summary>
    /// 表示鼠标的拖动方向
    /// </summary>
    internal enum MouseDirection
    {
        /// <summary>
        /// 水平方向拖动，只改变窗体的宽度
        /// </summary>
        Herizontal,
        /// <summary>
        /// 垂直方向拖动，只改变窗体的高度 
        /// </summary>
        Vertical,
        /// <summary>
        /// 倾斜方向，同时改变窗体的宽度和高度 
        /// </summary>
        Declining,
        /// <summary>
        /// 不做标志，即不拖动窗体改变大小 
        /// </summary>
        None
    }


}
