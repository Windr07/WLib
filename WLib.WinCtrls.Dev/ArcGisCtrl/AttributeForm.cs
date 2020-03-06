/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using WLib.ArcGis.Control.AttributeCtrl;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 显示表格/图层属性表的窗口
    /// </summary>
    public partial class AttributeForm : XtraForm, IAttributeForm
    {
        /// <summary>
        /// 显示属性表的控件
        /// </summary>
        public IAttributeCtrl AttributeCtrl { get => this.attributeCtrl1; }

        /// <summary>
        /// 显示表格/图层属性表的窗口
        /// </summary>
        public AttributeForm()
        {
            InitializeComponent();
        }
    }
}
