using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using WLib.WinCtrls.ListCtrl;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 图片列表控件<see cref="DevExpress.XtraEditors.ImageListBoxControl"/>的扩展控件，实现<see cref="IListControl"/>
    /// </summary>
    public partial class ImageListBoxControlEx : DevExpress.XtraEditors.ImageListBoxControl, IListControl
    {
        /// <summary>
        /// 图片列表控件<see cref="DevExpress.XtraEditors.ImageListBoxControl"/>的扩展控件，实现<see cref="IListControl"/>
        /// </summary>
        public ImageListBoxControlEx()
        {
            InitializeComponent();
        }

        public new IList SelectedItems => base.SelectedItems.Cast<object>().ToList();
        public new IList Items => base.Items;
        public void AddItems(IEnumerable<object> items) => base.Items.AddRange(items.ToArray());
    }
}
