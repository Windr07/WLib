/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using WLib.ArcGis.Control.AttributeCtrl;

namespace WLib.WinCtrls.ArcGisCtrl
{
    /// <summary>
    /// 显示表格/图层属性表的窗口
    /// </summary>
    public partial class AttributeForm : Form, IAttributeForm
    {
        public IAttributeCtrl AttributeCtrl => this.attributeCtrl1;
        /// <summary>
        /// 图层属性表窗口
        /// </summary>
        public AttributeForm() => InitializeComponent();

        private void SBtnClose_Click(object sender, EventArgs e) => this.Close();
    }
}
