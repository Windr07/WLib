/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/2/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Envir.ArcGis.CheckVersion
{
    /// <summary>
    /// ArcGIS产品类别
    /// </summary>
    public enum EArcGisProductType
    {
        /// <summary>
        /// 任一ArcGIS产品
        /// </summary>
        Any = 0,
        /// <summary>
        /// ArcGIS License
        /// </summary>
        License = 1,
        /// <summary>
        /// ArcGIS Aministrator
        /// </summary>
        Aministrator = 2,
        /// <summary>
        /// ArcGIS Desktop
        /// </summary>
        Desktop = 10,
        /// <summary>
        /// ArcMap
        /// </summary>
        ArcMap = 11,
        /// <summary>
        /// ArcCatlog
        /// </summary>
        ArcCatlog = 12,
        /// <summary>
        /// ArcGIS Server
        /// </summary>
        Server = 100,
        /// <summary>
        /// ArcGIS Sde
        /// </summary>
        Sde = 1000,
        /// <summary>
        /// ArcEngine SDK
        /// </summary>
        ArcEngineSdk = 10001,
        /// <summary>
        /// ArcEngine Runtime
        /// </summary>
        ArcEngineRuntime = 10002,
    }
}
