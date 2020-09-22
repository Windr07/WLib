/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020/06/09
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using System.Collections.Generic;
using System.IO;

namespace WLib.ArcGis.Carto.Map
{
    /// <summary>
    /// 提供地图文档的创建、打开、获取地图等方法
    /// </summary>
    public static class MapDocEx
    {
        /// <summary>
        /// 在指定路径中，打开或创建地图文档
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IMapDocument OpenOrCreateMapDoc(string filePath)
        {
            if (File.Exists(filePath))
                return OpenMapDoc(filePath);
            else
                return CreateMapDoc(filePath);
        }
        /// <summary>
        /// 打开地图文档
        /// </summary>
        /// <param name="filePath">地图文档路径</param>
        /// <returns></returns>
        public static IMapDocument OpenMapDoc(string filePath)
        {
            var mapDoc = new MapDocumentClass();
            mapDoc.Open(filePath);
            return mapDoc;
        }
        /// <summary>
        /// 在指定路径中创建地图文档（mxd）
        /// </summary>
        /// <param name="filePath">要创建的mxd文件路径</param>
        /// <returns></returns>
        public static IMapDocument CreateMapDoc(string filePath)
        {
            var mapDoc = new MapDocumentClass();
            mapDoc.New(filePath);
            return mapDoc;
        }
        /// <summary>
        /// 在指定路径中创建地图文档（mxd）
        /// </summary>
        /// <param name="directory">要创建的mxd文件的目录</param>
        /// <param name="fileName">要创建的mxd文件名，应当包含.mxd后缀</param>
        /// <returns></returns>
        public static IMapDocument CreateMapDoc(string directory, string fileName)
        {
            return CreateMapDoc(Path.Combine(directory, fileName));
        }
      


        /// <summary>
        /// 从地图文档中查找地图
        /// </summary>
        /// <param name="mapDoc"></param>
        /// <param name="mapName">地图名称</param>
        /// <returns></returns>
        public static IMap GetMap(this IMapDocument mapDoc, string mapName)
        {
            for (int i = 0; i < mapDoc.MapCount; i++)
            {
                if (mapDoc.Map[i].Name == mapName)
                    return mapDoc.Map[i];
            }
            return null;
        }
        /// <summary>
        /// 获取地图文档中的所有地图
        /// </summary>
        /// <param name="mapDoc"></param>
        /// <returns></returns>
        public static IEnumerable<IMap> GetMaps(this IMapDocument mapDoc)
        {
            for (int i = 0; i < mapDoc.MapCount; i++)
                yield return mapDoc.Map[i];
        }
    }
}
