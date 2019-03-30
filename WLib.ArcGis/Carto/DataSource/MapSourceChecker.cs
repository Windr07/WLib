/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Carto.Map;
using WLib.ArcGis.GeoDb.WorkSpace;

namespace WLib.ArcGis.Carto.DataSource
{
    /// <summary>
    /// 检查或设置出图模板（mxd文档）的图层和表格数据源
    /// </summary>
    public class MapSourceChecker
    {
        /// <summary>
        /// 出图模板Mxd文档路径
        /// </summary>
        private string _mapDocPath;                 
        /// <summary>
        /// 检查信息
        /// </summary>
        private readonly List<string> _messages;    
        /// <summary>
        /// 地图数据框中，图层及其数据源信息
        /// </summary>
        private readonly MapSource[] _mapLyrSources;
        /// <summary>
        /// 地图数据框中，表格及其数据源信息
        /// </summary>
        private readonly MapSource[] _mapTblSources;
        /// <summary>
        /// 地图文档
        /// </summary>
        public IMapDocument MapDoc { get; private set; }
        /// <summary>
        /// 检查信息
        /// </summary>
        public string AllMessage
        {
            get
            {
                var msg = _messages;
                msg.Reverse();
                return msg.Aggregate((a, b) => a + "," + b);
            }
        }


        /// <summary>
        /// 实例化出图文档模板的数据源检查设置类
        /// </summary>
        /// <param name="mapDocPath">mxd文档路径</param>>
        /// <param name="mapLyrSources">地图的图层与数据源信息的对象</param>
        /// <param name="mapTblSources">地图的表格与数据源信息的对象</param>
        public MapSourceChecker(string mapDocPath, MapSource[] mapLyrSources = null, MapSource[] mapTblSources = null)
        {
            _mapDocPath = mapDocPath;
            _messages = new List<string>();

            if (!System.IO.Path.IsPathRooted(_mapDocPath))
                _mapDocPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _mapDocPath);
            _mapLyrSources = mapLyrSources;
            _mapTblSources = mapTblSources;

            MapDoc = new MapDocumentClass();
            MapDoc.Open(_mapDocPath);
            _mapDocPath = MapDoc.DocumentFilename;
        }
        /// <summary>
        /// 实例化出图文档模板的数据源检查设置类
        /// </summary>
        /// <param name="mapDoc">mxd地图文档</param>
        /// <param name="mapLyrSources">地图的图层与数据源信息的对象</param>
        /// <param name="mapTblSources">地图的表格与数据源信息的对象</param>
        public MapSourceChecker(IMapDocument mapDoc, MapSource[] mapLyrSources = null, MapSource[] mapTblSources = null)
        {
            MapDoc = mapDoc;
            _messages = new List<string>();
            _mapLyrSources = mapLyrSources;
            _mapTblSources = mapTblSources;
            _mapDocPath = MapDoc.DocumentFilename;
        }


        /// <summary>
        /// 检查地图文档是否包含指定图层/表，以及图层/表数据源是否正确，数据源出错时尝试正确关联数据源
        /// </summary>
        /// <param name="mapDoc">地图文档</param>
        /// <param name="mapLyrSources">地图的图层与数据源信息的对象</param>
        /// <param name="mapTblSources">地图的表格与数据源信息的对象</param>
        /// <param name="checkMessages">检查结果信息</param>
        /// <returns></returns>
        public static bool CheckSetMapSource(IMapDocument mapDoc, MapSource[] mapLyrSources, MapSource[] mapTblSources, out string checkMessages)
        {
            MapSourceChecker checkMapSource = new MapSourceChecker(mapDoc, mapLyrSources, mapTblSources);
            bool isSuccess = checkMapSource.CheckSetSource();
            checkMessages = checkMapSource.AllMessage;
            return isSuccess;
        }
        /// <summary>
        /// 检查地图文档各图层/表是否已关联数据源
        /// </summary>
        /// <param name="mapDoc">地图文档</param>
        /// <param name="checkMessages">检查结果信息</param>
        /// <returns></returns>
        public static bool HasMapSource(IMapDocument mapDoc, out string checkMessages)
        {
            MapSourceChecker checkMapSource = new MapSourceChecker(mapDoc, null, null);
            bool isSuccess = checkMapSource.HasSource();
            checkMessages = checkMapSource.AllMessage;
            return isSuccess;
        }


        /// <summary>
        /// 检查地图文档是否包含指定图层/表，以及图层/表数据源是否正确，数据源出错时尝试正确关联数据源
        /// </summary>
        /// <returns></returns>
        public bool CheckSetSource()
        {
            if (!CheckPathExists())//检查各项路径
                return false;

            bool checkLayerExists = false;
            try
            {
                if (MapDoc == null)
                {
                    MapDoc = new MapDocumentClass();
                    MapDoc.Open(_mapDocPath); //打开地图文档
                }
                //检查出图模板规范，同时必须确定图层的数据源已设置（正确关联到数据库的要素类）
                checkLayerExists = CheckOrSetMapDataSource(MapDoc);
                MapDoc.Save(); //保存！！！
                _messages.Add(checkLayerExists ? "✔检查成功，出图模板符合要求！" : "✘出图模板不符合要求！");
            }
            catch (Exception ex)
            {
                _messages.Add("✘检查出图模板规范错误：" + ex.Message);
                try { MapDoc.Save(); }
                catch { }
            }
            return checkLayerExists;
        }
        /// <summary>
        /// 检查地图文档各图层/表是否已关联数据源
        /// </summary>
        /// <returns></returns>
        public bool HasSource()
        {
            if (!CheckMxdPathExists())//检查mxd路径是否存在
                return false;

            bool checkLayerExists = true;
            try
            {
                if (MapDoc == null)
                {
                    MapDoc = new MapDocumentClass();
                    MapDoc.Open(_mapDocPath); //打开地图文档
                }

                for (int i = 0; i < MapDoc.MapCount; i++)
                {
                    IMap map = MapDoc.get_Map(0);
                    for (int j = 0; j < map.LayerCount; j++)
                    {
                        ILayer layer = map.get_Layer(j);
                        IDataLayer dataLayer = layer as IDataLayer;
                        if (dataLayer == null)
                        {
                            _messages.Add($"✘数据框[{map.Name}]的图层[{layer.Name}]没有正确关联数据源，或者该图层不是预期的要素图层！");
                            checkLayerExists = false;
                        }
                        IDatasetName datasetName = (dataLayer.DataSourceName as IDatasetName);
                        if (datasetName != null)
                        {
                            string pathName = datasetName.WorkspaceName.PathName;
                            if (pathName == null)
                            {
                                _messages.Add($"✘数据框[{map.Name}]的图层[{layer.Name}]没有正确关联数据源！");
                                checkLayerExists = false;
                            }
                        }
                    }
                }
                _messages.Add(checkLayerExists ? "✔检查成功，出图模板符合要求！" : "✘出图模板不符合要求！");
            }
            catch (Exception ex)
            {
                _messages.Add("✘检查出图模板规范错误：" + ex.Message);
                try { MapDoc.Close(); }
                catch { }
            }
            return checkLayerExists;
        }
        /// <summary>
        /// 检查各项路径是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckPathExists()
        {
            bool isSuccess = CheckMxdPathExists();
            if (_mapLyrSources != null)
            {
                foreach (var s in _mapLyrSources)
                {
                    string workspacePath = s.SourcePath;
                    if (!System.IO.File.Exists(workspacePath) && !System.IO.Directory.Exists(workspacePath))
                    {
                        _messages.Add("✘找不到图层数据源，请先配置数据库位置！");
                        isSuccess = false;
                    }
                }
            }
            if (_mapTblSources != null)
            {
                foreach (var s in _mapTblSources)
                {
                    string workspacePath = s.SourcePath;
                    if (!System.IO.File.Exists(workspacePath) && !System.IO.Directory.Exists(workspacePath))
                    {
                        _messages.Add("✘找不到表格数据源，请先配置数据库位置！");
                        isSuccess = false;
                    }
                }
            }
            return isSuccess;
        }
        /// <summary>
        /// 检查mxd文档路径是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckMxdPathExists()
        {
            if (!System.IO.Path.IsPathRooted(_mapDocPath))
                _mapDocPath = AppDomain.CurrentDomain.BaseDirectory + _mapDocPath;
            if (!System.IO.File.Exists(_mapDocPath))
            {
                _messages.Add("✘找不到出图模板文档（mxd）！");
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 检查出图模板是否符合规范并对图层和表格设置数据源
        /// </summary>
        /// <param name="mapDoc">地图文档</param>
        /// <returns></returns>
        private bool CheckOrSetMapDataSource(IMapDocument mapDoc)
        {
            bool allSuccess = true;

            #region 设置图层数据源
            _messages.Add("●检查并设置各图层的数据源...");
            for (int i = 0; i < _mapLyrSources.Length; i++)
            {
                var workspace = GetWorkspace.GetWorkSpace(_mapLyrSources[i].SourcePath);
                IMap map = MapQuery.GetMapFromMapDocument(mapDoc, _mapLyrSources[i].MapFrameName);
                if (map == null)
                {
                    _messages.Add($"✘找不到[{_mapLyrSources[i].MapFrameName}]地图数据框，请确定出图模板是否正确！");
                    allSuccess = false;
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
                    continue;
                }

                bool resetSpatialRef = false;
                foreach (var keyValue in _mapLyrSources[i].ViewNames2SourceNames)
                {
                    IFeatureLayer featureLayer = map.GetFeatureLayer2(keyValue.Key);
                    if (featureLayer == null)//若图层为空
                    {
                        _messages.Add(
                            $"✘[{_mapLyrSources[i].MapFrameName}].[{keyValue.Key}]缺失或者该图层不是要素图层，请确定出图模板是否正确！\r\n");
                        continue;
                    }
                    if (featureLayer.FeatureClass == null ||
                        CheckDataSourceCorrect(featureLayer, workspace.PathName) == false)//若图层数据源没有正确设置
                    {
                        _messages.Add($"●重设[{_mapLyrSources[i].MapFrameName}].[{keyValue.Key}]的数据源...");
                        IFeatureClass featureClass = workspace.GetFeatureClassByName(keyValue.Value);
                        if (featureClass == null)
                        {
                            _messages.Add(string.Format("✘错误：重设[{0}].[{1}]的数据源时，找不到名为[{0}]的要素类", _mapLyrSources[i].MapFrameName, keyValue.Key, keyValue.Value));
                            continue;
                        }
                        featureLayer.FeatureClass = featureClass;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                    }

                    if (!resetSpatialRef && featureLayer.FeatureClass != null)//若数据框坐标系与数据源不一致，设为数据源的坐标系
                    {
                        if (map.SpatialReference.FactoryCode != (featureLayer.FeatureClass as IGeoDataset).SpatialReference.FactoryCode)
                        {
                            map.SpatialReference = (featureLayer.FeatureClass as IGeoDataset).SpatialReference;
                            _messages.Add($"●重设[{_mapLyrSources[i].MapFrameName}]的坐标系...");
                            resetSpatialRef = true;
                        }
                    }
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
            }
            #endregion

            #region 设置表格数据源
            if (_mapTblSources == null)
                return allSuccess;

            _messages.Add("●检查并设置各表格的数据源...");
            for (int i = 0; i < _mapTblSources.Length; i++)
            {
                IWorkspace workspace = GetWorkspace.GetWorkSpace(_mapTblSources[i].SourcePath);
                IMap map = MapQuery.GetMapFromMapDocument(mapDoc, _mapTblSources[i].MapFrameName);
                if (map == null)
                {
                    _messages.Add($"✘找不到[{_mapTblSources[i].MapFrameName}]地图数据框，请确定出图模板是否正确！");
                    allSuccess = false;
                }

                ITableCollection tableCollection = map as ITableCollection;
                int cnt = tableCollection.TableCount;
                tableCollection.RemoveAllTables(); 
                foreach (var keyValue in _mapTblSources[i].ViewNames2SourceNames)
                {
                    ITable table = workspace.GetITableByName(keyValue.Value);
                    if (table == null)
                        throw new Exception("数据库找不到表名称为：" + keyValue.Value + " 的数据表，请检查！");
                    tableCollection.AddTable(table);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
            }
            #endregion

            return allSuccess;
        }
        /// <summary>
        /// 判断图层数据源非空，且数据源已设置为指定数据源
        /// </summary>
        /// <param name="layer">要判断的图层</param>
        /// <param name="workspacePathName">指定数据源路径（mdb路径）</param>
        /// <returns></returns>
        private bool CheckDataSourceCorrect(ILayer layer, string workspacePathName)
        {
            IDataLayer dataLayer = layer as IDataLayer;
            IDatasetName datasetName = (dataLayer.DataSourceName as IDatasetName);
            if (datasetName != null)
            {
                string pathName = datasetName.WorkspaceName.PathName;
                if (pathName != null && pathName == workspacePathName)
                    return true;
            }
            return false;
        }
    }
}
