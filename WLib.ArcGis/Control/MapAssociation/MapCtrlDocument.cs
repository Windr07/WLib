using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using WLib.WinForm;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件与地图文档关联操作
    /// （包括新建、加载、保存、另存地图文档，添加数据、清空图层等）
    /// </summary>
    public class MapCtrlDocument
    {
        /// <summary>
        /// 地图文档
        /// </summary>
        public IMapDocument MapDoc { get; private set; }
        /// <summary>
        /// 地图控件
        /// </summary>
        public AxMapControl MapControl { get; }
        /// <summary>
        /// 地图控件与地图文档关联操作
        /// （包括新建、加载、保存、另存地图文档，添加数据、清空图层等）
        /// </summary>
        /// <param name="mapCtrl">地图控件</param>
        public MapCtrlDocument(AxMapControl mapCtrl)
        {
            MapControl = mapCtrl;
        }


        /// <summary>
        /// 新建地图文档
        /// </summary>
        /// <param name="fileName">地图文档路径</param>
        /// <param name="mapName">地图名称</param>
        /// <param name="loadMapAfterBuilt">是否将地图文档加载到地图控件</param>
        /// <returns></returns>
        public IMapDocument NewMap(string fileName, string mapName = "图层", bool loadMapAfterBuilt = true)
        {
            IMapDocument mapDoc = new MapDocumentClass();
            mapDoc.New(fileName);
            mapDoc.Map[0].Name = mapName;
            if (loadMapAfterBuilt)
                MapControl.LoadMxFile(fileName);
            return mapDoc;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddData()
        {
            var addDataCmd = new ControlsAddDataCommandClass();
            addDataCmd.OnCreate(MapControl.Object);
            addDataCmd.OnClick();
        }
        /// <summary>
        /// 保存地图
        /// </summary>
        public void Save()
        {
            if (MapDoc == null)
            {
                MapDoc = new MapDocumentClass();
                var filePath = DialogOpt.ShowSaveFileDialog(@"地图文档(*.mxd)|*.mxd", @"保存地图文档", @"地图文档");
                if (filePath != null)
                    SaveAs(filePath);
            }
            else
            {
                MapDoc.ReplaceContents(MapControl.Map as IMxdContents);
                MapDoc.Save();
                MapDoc.Close();
            }
        }
        /// <summary>
        /// 另存为新地图文档
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveAs(string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);
            if (MapDoc == null)
                MapDoc = new MapDocumentClass();
            MapDoc.New(fileName);
            MapDoc.ReplaceContents(MapControl.Map as IMxdContents);
            MapDoc.Save();
            MapDoc.Close();
        }
      
        /// <summary>
        /// 清空图层并保存
        /// </summary>
        public void ClearLayers()
        {
            IMapDocument mapDoc = new MapDocumentClass();
            var docFilePath = MapControl.DocumentFilename;
            mapDoc.Open(docFilePath, "");
            mapDoc.Map[0].ClearLayers();
            mapDoc.Save();
            mapDoc.Close();
            MapControl.LoadMxFile(docFilePath);
        }
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="mxdFilePath"></param>
        public void LoadFile(string mxdFilePath)
        {
            if (MapDoc != null && File.Exists(MapDoc.DocumentFilename))
                MapDoc.Close();

            MapDoc = new MapDocumentClass();
            MapDoc.Open(mxdFilePath);

            MapControl.LoadMxFile(mxdFilePath);
        }
    }
}
