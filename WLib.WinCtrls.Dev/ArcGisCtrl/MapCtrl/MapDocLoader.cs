using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 
    /// </summary>
    public class MapDocLoader
    {
        /// <summary>
        /// 地图文档
        /// </summary>
        public IMapDocument MapDoc { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        protected AxMapControl AxMapControlMainMap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected AxPageLayoutControl AxPageLayoutControl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axMapControlMainMap"></param>
        /// <param name="axPageLayoutControl"></param>
        public MapDocLoader(AxMapControl axMapControlMainMap, AxPageLayoutControl axPageLayoutControl)
        {
            AxMapControlMainMap = axMapControlMainMap;
            AxPageLayoutControl = axPageLayoutControl;
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
                AxMapControlMainMap.LoadMxFile(fileName);
            return mapDoc;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        public void AddData()
        {
            ControlsAddDataCommandClass addDataCmd = new ControlsAddDataCommandClass();
            addDataCmd.OnCreate(AxMapControlMainMap.Object);
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
                var dialog = new SaveFileDialog
                {
                    Title = "保存地图文档",
                    FileName = "地图文档",
                    Filter = "地图文档(*.mxd)|*.mxd"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                    SaveAs(dialog.FileName);
            }
            else
            {
                MapDoc.ReplaceContents(AxMapControlMainMap.Map as IMxdContents);
                MapDoc.ReplaceContents(AxPageLayoutControl.PageLayout as IMxdContents);
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
            MapDoc.ReplaceContents(AxMapControlMainMap.Map as IMxdContents);
            MapDoc.ReplaceContents(AxPageLayoutControl.PageLayout as IMxdContents);
            MapDoc.Save();
            MapDoc.Close();
        }
        /// <summary>
        /// 刷新地图
        /// </summary>
        public void RefreshMap()
        {
            AxMapControlMainMap.Refresh();
            AxMapControlMainMap.Update();
        }
        /// <summary>
        /// 清空图层并保存
        /// </summary>
        public void ClearLayers()
        {
            IMapDocument pMapDoc = new MapDocumentClass();
            string strfilename = AxMapControlMainMap.DocumentFilename;
            pMapDoc.Open(strfilename, "");
            pMapDoc.Map[0].ClearLayers();
            pMapDoc.Save();
            pMapDoc.Close();
            AxMapControlMainMap.LoadMxFile(strfilename);
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

            AxMapControlMainMap.LoadMxFile(mxdFilePath);
        }
    }
}
