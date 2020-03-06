/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/26 16:54:24
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;

namespace WLib.ArcGis.Control.AttributeCtrl
{
    /// <summary>
    /// 显示属性表的控件
    /// </summary>
    public interface IAttributeCtrl : IDisposable
    {
        /// <summary>
        /// 属性查询控件
        /// </summary>
        IAttributeQueryCtrl AtrributeQueryCtrl { get; set; }
        /// <summary>
        /// 要获取属性表的表格
        /// </summary>
        ITable Table { get; }
        /// <summary>
        /// 数据显示的筛选条件，表示当前显示的数据范围（对WhereClause筛选后的数据的进一步筛选）
        /// </summary>
        string Filter { get; }
        /// <summary>
        /// 数据加载的筛选条件，表示从表格(ITable)或图层(IFeatureLayer)加载的数据范围，只能通过LoadAttribute方法指定
        /// </summary>
        string WhereClause { get; }
        /// <summary>
        /// 定位到要素图斑的事件
        /// </summary>
        event EventHandler<FeatureLocationEventArgs> FeatureLocation;


        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="table">要获取属性表的表</param>
        /// <param name="whereClause">筛选条件，表示从表加载的数据范围</param>
        /// <param name="fieldNames">加载和显示的字段</param>
        /// <returns></returns>
        void LoadAttribute(ITable table, string whereClause = null, int layerIndex = -1, EventHandler<FeatureLocationEventArgs> featureLocation = null);
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        void Clear();
    }
}
