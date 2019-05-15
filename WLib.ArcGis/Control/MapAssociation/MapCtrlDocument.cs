/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

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
    public class MapCtrlDocument: IMapCtrlAssociation
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
        /// 析构函数，关闭地图文档
        /// </summary>
        ~MapCtrlDocument()
        {
            MapDoc?.Close();
        }


        /// <summary>
        /// 打开空白地图文档
        /// </summary>
        /// <returns></returns>
        public void NewEmptyDoc()
        {
            if (MapDoc != null)
                Save();

            MapControl.ClearLayers();
            MapDoc = null;
        }
        /// <summary>
        /// 打开地图文档并加载地图到地图控件中（若当前地图文档非空则先保存地图文档）
        /// </summary>
        /// <returns></returns>
        public IMapDocument OpenDoc()
        {
            var filePath = DialogOpt.ShowOpenFileDialog(@"*.mxd|*.mxd");
            if (filePath != null)
            {
                if (MapDoc != null)
                    Save();

                MapDoc = new MapDocumentClass();
                MapDoc.Open(filePath);
                MapControl.Map = MapDoc.ActiveView.FocusMap;
                MapControl.ActiveView.Refresh();
            }
            return MapDoc;
        }
        /// <summary>
        /// 打开地图文档并加载地图到地图控件中
        /// </summary>
        /// <returns></returns>
        public IMapDocument OpenDoc(string filePath)
        {
            MapDoc = new MapDocumentClass();
            MapDoc.Open(filePath);
            MapControl.Map = MapDoc.ActiveView.FocusMap;
            MapControl.ActiveView.Refresh();
            return MapDoc;
        }
        /// <summary>
        /// 添加数据（调用AE添加数据命令）
        /// </summary>
        public void AddData()
        {
            var addDataCmd = new ControlsAddDataCommandClass();
            addDataCmd.OnCreate(MapControl.Object);
            addDataCmd.OnClick();
        }
        /// <summary>
        /// 保存地图文档（地图文档从未保存时，则先弹出对话框选择保存地址）
        /// </summary>
        public void Save()
        {
            if (MapDoc == null)
                SaveAs();
            else
            {
                MapDoc.ReplaceContents(MapControl.Map as IMxdContents);
                MapDoc.Save();
            }
        }
        /// <summary>
        /// 另存为新地图文档（弹出对话框选择保存地址）
        /// </summary>
        public void SaveAs()
        {
            var filePath = DialogOpt.ShowSaveFileDialog(@"地图文档(*.mxd)|*.mxd", @"保存地图文档", @"地图文档");
            if (filePath != null)
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);

                MapDoc = new MapDocumentClass();
                MapDoc.New(filePath);
                MapDoc.ReplaceContents(MapControl.Map as IMxdContents);
                MapDoc.Save();
            }
        }
    }
}
