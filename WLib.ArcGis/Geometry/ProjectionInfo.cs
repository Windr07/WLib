/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geometry;
using System;
using System.Linq;

namespace WLib.ArcGis.Geometry
{
    /// <summary>
    /// 投影坐标系信息
    /// <para>包括：坐标系别名、坐标系名称、坐标系WKID、投影带号等</para>
    /// </summary>
    public class ProjectionInfo
    {
        /// <summary>
        /// 坐标系别名，一般为中文显示名称
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 坐标系名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 坐标系WKID
        /// </summary>
        public int Wkid { get; set; }
        /// <summary>
        /// 获取投影带号
        /// </summary>
        public int ProjectZone { get; set; }
        /// <summary>
        /// 获取坐标系
        /// </summary>
        public ISpatialReference SpatialRef => SpatialRefOpt.CreateSpatialRef(Wkid, ESrType.Projected);


        /// <summary>
        /// 投影坐标系信息
        /// <para>包括：坐标系别名、坐标系名称、坐标系枚举、投影带号等</para>
        /// </summary>
        public ProjectionInfo() { }
        /// <summary>
        /// 投影坐标系信息
        /// <para>包括：坐标系别名、坐标系名称、坐标系枚举、投影带号等</para>
        /// </summary>
        /// <param name="eType"></param>
        public ProjectionInfo(esriSRProjCS4Type eType)
        {
            Wkid = (int)eType;
            AliasName = Name = eType.ToString().Replace("esriSRProjCS4Type.esriSRProjCS_", "").Replace('_', ' ');
            var splitNameAraay = Name.Split(new[] { "Zone " }, StringSplitOptions.RemoveEmptyEntries);
            ProjectZone = splitNameAraay.Length == 2 ? Convert.ToInt32(splitNameAraay[1]) : 0;
        }
        /// <summary>
        /// 投影坐标系信息
        /// <para>包括：坐标系别名、坐标系名称、坐标系枚举、投影带号等</para>
        /// </summary>
        /// <param name="wkid"></param>
        /// <param name="name"></param>
        /// <param name="aliasName"></param>
        /// <param name="projectZone"></param>
        public ProjectionInfo(int wkid, string name, string aliasName, int projectZone)
        {
            Wkid = wkid;
            Name = name;
            AliasName = aliasName;
            ProjectZone = projectZone;
        }
        /// <summary>
        /// 输出坐标系别名
        /// </summary>
        /// <returns></returns>
        public override string ToString() => AliasName;


        /// <summary>
        ///  获取“西安1980高斯克吕格3度分带含带号”的投影坐标系的信息
        /// </summary>
        /// <param name="startWkid">开始WKID值</param>
        /// <param name="endWkid">结束的WKID值</param>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_Xian1980(int startWkid, int endWkid)
        {
            var wkids = Enumerable.Range(startWkid, endWkid - startWkid + 1).ToArray();
            return wkids.Select(wkid =>
            {
                var dh = wkid - 2324;
                return new ProjectionInfo(wkid, $"Xian1980 3 Degree GK Zone {dh}", $"西安1980坐标系3度分带{dh}度带", dh);
            }).ToArray();
        }
        /// <summary>
        ///  获取全部“西安1980高斯克吕格3度分带含带号”的投影坐标系的信息
        /// <para>WKID值范围[2349, 2390]</para>
        /// </summary>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_Xian1980() => GetPrjRef_Xian1980(2349, 2390);
        /// <summary>
        ///  获取广东省范围的“西安1980高斯克吕格3度分带含带号”的投影坐标系的信息
        /// <para>WKID值范围[2361, 2363]</para>
        /// </summary>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_Xian1980_GuangDong() => GetPrjRef_Xian1980(2361, 2363);


        /// <summary>
        ///  获取指定WKID范围的“国家2000高斯克吕格3度分带含带号”的投影坐标系的信息
        /// </summary>
        /// <param name="startWkid">开始WKID值</param>
        /// <param name="endWkid">结束的WKID值</param>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_CGCS2000(int startWkid, int endWkid)
        {
            var wkids = Enumerable.Range(startWkid, endWkid - startWkid + 1).ToArray();
            return wkids.Select(wkid =>
            {
                var dh = wkid - 4513 + 25;
                return new ProjectionInfo(wkid, $"CGCS2000 3 Degree GK Zone {dh}", $"国家2000坐标系3度分带{dh}度带", dh);
            }).ToArray();
        }
        /// <summary>
        /// 获取全部“国家2000高斯克吕格3度分带含带号”的投影坐标系的信息
        /// <para>WKID值范围[4513, 4533]</para>
        /// </summary>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_CGCS2000() => GetPrjRef_CGCS2000(4513, 4533);
        /// <summary>
        /// 获取广东省范围的“国家2000高斯克吕格3度分带含带号”的投影坐标系的信息
        /// <para>WKID值范围[4525, 4527]</para>
        /// </summary>
        /// <returns></returns>
        public static ProjectionInfo[] GetPrjRef_CGCS2000_GuangDong() => GetPrjRef_CGCS2000(4525, 4527);
    }
}
