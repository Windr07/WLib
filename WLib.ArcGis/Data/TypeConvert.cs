/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4/28 17:29:50
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;

namespace WLib.ArcGis.Data
{
    /// <summary>
    /// ArcGIS字段类型和C#数据类型等的互相转换
    /// </summary>
    public static class TypeConvert
    {
        /// <summary>
        /// 获取几何类型字符（eg:"Line","Point","Polygon","Polyline","Null"等等）
        /// </summary>
        /// <param name="geoType"></param>
        /// <returns></returns>
        public static string GetGeoTypeStr(this esriGeometryType geoType)
        {
            return geoType.ToString().Replace("esriGeometry", "");
        }
        
        /// <summary>
        /// 将ArcGIS字段类型，转换成C#相应的数据类型
        /// </summary>
        /// <param name="fieldType">字段类型</param>
        /// <returns></returns>
        public static Type ParseFieldType(this esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return typeof(int);
                case esriFieldType.esriFieldTypeInteger:
                    return typeof(long);
                case esriFieldType.esriFieldTypeOID:
                    return typeof(long);
                case esriFieldType.esriFieldTypeSingle:
                    return typeof(float);
                case esriFieldType.esriFieldTypeDouble:
                    return typeof(double);
                case esriFieldType.esriFieldTypeGeometry:
                    return typeof(IGeometry);
                case esriFieldType.esriFieldTypeDate:
                    return typeof(DateTime);
                case esriFieldType.esriFieldTypeString:
                case esriFieldType.esriFieldTypeGUID:
                case esriFieldType.esriFieldTypeGlobalID:
                case esriFieldType.esriFieldTypeXML:
                default:
                    return typeof(string);
            }
        }
        /// <summary>
        /// 将C#数据类型，转换成ArcGIS相应的字段类型
        /// </summary>
        /// <param name="type">数据类型，一般是long,double,float,int,string,DateTime,IGeometry之一，其他类型将默认当成字符串类型(esriFieldTypeString)</param>
        /// <returns></returns>
        public static esriFieldType GetesriFieldTypeFromType(Type type)
        {
            if (type == typeof(long))
                return esriFieldType.esriFieldTypeInteger;
            if (type == typeof(double))
                return esriFieldType.esriFieldTypeDouble;
            if (type == typeof(float))
                return esriFieldType.esriFieldTypeSingle;
            if (type == typeof(int))
                return esriFieldType.esriFieldTypeSmallInteger;
            if (type == typeof(IGeometry))
                return esriFieldType.esriFieldTypeGeometry;
            if (type == typeof(DateTime))
                return esriFieldType.esriFieldTypeDate;

            return esriFieldType.esriFieldTypeString;
        }
    }
}
