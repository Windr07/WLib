/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/19 16:02:07
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace WLib.WinCtrls.ExplorerCtrl.FileFolderCtrl
{
    public class FolderNameEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) => UITypeEditorEditStyle.Modal;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if (value != null)
                browser.SelectedPath = value.ToString();

            if (browser.ShowDialog(null) == DialogResult.OK)
                return browser.SelectedPath;
            return value;
        }
    }
}
