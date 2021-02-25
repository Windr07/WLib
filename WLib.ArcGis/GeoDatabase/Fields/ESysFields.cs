using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WLib.ArcGis.GeoDatabase.Fields
{
    /// <summary>
    /// 获取的系统字段类型
    /// </summary>
    public enum ESysFields
    {
        /// <summary>
        /// 不获取系统字段
        /// </summary>
        None = 0,

        /// <summary>
        /// 获取OID字段
        /// </summary>
        OID = 1,
        /// <summary>
        /// 获取Shape字段
        /// </summary>
        Shape = 2,
        /// <summary>
        /// 获取ShapeLength字段
        /// </summary>
        Length = 4,
        /// <summary>
        /// 获取ShapeArea字段
        /// </summary>
        Area = 8,

        /// <summary>
        /// 获取OID、Shape字段
        /// </summary>
        OID_Shape = 3,
        /// <summary>
        /// 获取ShapeLength、ShapeArea字段
        /// </summary>
        Length_Area = 12,
        /// <summary>
        /// 获取OID、ShapeLength、ShapeArea字段
        /// </summary>
        OID_Lenth_Area = 13,
        /// <summary>
        /// 获取Shape、ShapeLength、ShapeArea字段
        /// </summary>
        ShapeAll = 14,

        /// <summary>
        /// 获取全部系统字段
        /// </summary>
        All =15,
    }
}
