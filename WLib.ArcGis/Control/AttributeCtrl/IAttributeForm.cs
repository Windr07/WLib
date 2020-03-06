using System.Windows.Forms;

namespace WLib.ArcGis.Control.AttributeCtrl
{
    /// <summary>
    /// 显示属性表的窗体
    /// </summary>
    public interface IAttributeForm
    {
        IAttributeCtrl AttributeCtrl { get; }


        #region 控件本身的属性和方法
        /// <summary>
        /// 
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        void Activate();
        /// <summary>
        /// 
        /// </summary>
        void Show(IWin32Window win32Window);
        /// <summary>
        /// 
        /// </summary>
        event FormClosingEventHandler FormClosing;
        #endregion
    }
}
