/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/26 17:23:11
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WLib.ArcGis.Control.AttributeCtrl
{
    /// <summary>
    /// 属性查询控件
    /// </summary>
    public interface IAttributeQueryCtrl
    {
        /// <summary>
        /// 查询事件
        /// </summary>
        event EventHandler Query;
        /// <summary>
        /// 构造的查询语句
        /// </summary>
        string WhereClause { get; set; }


        /// <summary>
        /// 加载查询信息
        /// </summary>
        ///  <param name="searchTable">要查询的表格</param>
        /// <param name="isQueryAaliasName">确定查询的是字段别名还是字段名</param>
        /// <param name="queryFieldNames">显示在属性查询窗口的，要查询的字段</param>
        void LoadQueryInfo(ITable searchTable, bool isQueryAaliasName = false, string[] queryFieldNames = null);
        /// <summary>
        /// 加载查询信息
        /// </summary>
        ///  <param name="searchTables">要查询的表格</param>
        /// <param name="queryHandler">查询事件处理</param>
        void LoadQueryInfo(IEnumerable<ITable> searchTables);
        /// <summary>
        /// 加载查询信息
        /// </summary>
        ///  <param name="tableAttributeQueries">要查询的表格</param>
        void LoadQueryInfo(IEnumerable<AttributeQuery> tableAttributeQueries);


        #region 控件本身的属性和方法
        /// <summary>
        /// 
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="win32Window"></param>
        void Show(IWin32Window win32Window);
        #endregion
    }
}
