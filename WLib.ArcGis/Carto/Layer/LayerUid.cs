/*---------------------------------------------------------------- 
// auth： Windragon
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.esriSystem;

namespace WLib.ArcGis.Carto.Layer
{
    /// <summary>
    /// 各类图层的UID定义，
    /// 为<see cref="WLib.ArcGis.Carto.Map.MapEx.GetLayersByUid(ESRI.ArcGIS.Carto.IMap, UID)"/> 等方法提供参数调用
    /// </summary>
    public static class LayerUid
    {
        /// <summary>
        /// 根据UID字符串创建UID对象
        /// </summary>
        /// <param name="value">UID字符串</param>
        /// <returns></returns>
        public static UID CreateUid(this string value) => new UIDClass { Value = value };

        #region UID字符串
        public const string IACFeatureLayer = "{AD88322D-533D-4E36-A5C9-1B109AF7A346}";
        public const string IACLayer = "{74E45211-DFE6-11D3-9FF7-00C04F6BC6A5}";
        public const string IACImageLayer = "{495C0E2C-D51D-4ED4-9FC1-FA04AB93568D}";
        public const string IACAcetateLayer = "{65BD02AC-1CAD-462A-A524-3F17E9D85432}";
        public const string IAnnotationLayer = "{4AEDC069-B599-424B-A374-49602ABAD308}";
        public const string IAnnotationSublayer = "{DBCA59AC-6771-4408-8F48-C7D53389440C}";
        public const string ICadLayer = "{E299ADBC-A5C3-11D2-9B10-00C04FA33299}";
        public const string ICadastralFabricLayer = "{7F1AB670-5CA9-44D1-B42D-12AA868FC757}";
        public const string ICompositeLayer = "{BA119BC4-939A-11D2-A2F4-080009B6F22B}";
        public const string ICompositeGraphicsLayer = "{9646BB82-9512-11D2-A2F6-080009B6F22B}";
        public const string ICoverageAnnotationLayer = "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}";
        public const string IDataLayer = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}";
        public const string IDimensionLayer = "{0737082E-958E-11D4-80ED-00C04F601565}";
        public const string IFDOGraphicsLayer = "{48E56B3F-EC3A-11D2-9F5C-00C04F6BC6A5}";
        public const string IFeatureLayer = "{40A9E885-5533-11D0-98BE-00805F7CED21}";
        public const string IGdbRasterCatalogLayer = "{605BC37A-15E9-40A0-90FB-DE4CC376838C}";
        public const string IGeoFeatureLayer = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}";
        public const string IGraphicsLayer = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}";
        public const string IGroupLayer = "{EDAD6644-1810-11D1-86AE-0000F8751720}";
        public const string IIMSSubLayer = "{D090AA89-C2F1-11D3-9FEF-00C04F6BC6A5}";
        public const string IIMAMapLayer = "{DC8505FF-D521-11D3-9FF4-00C04F6BC6A5}";
        public const string ILayer = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}";
        public const string IMapServerLayer = "{E9B56157-7EB7-4DB3-9958-AFBF3B5E1470}";
        public const string IMapServerSublayer = "{B059B902-5C7A-4287-982E-EF0BC77C6AAB}";
        public const string INetworkLayer = "{82870538-E09E-42C0-9228-CBCB244B91BA}";
        public const string IRasterLayer = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}";
        public const string IRasterCatalogLayer = "{AF9930F0-F61E-11D3-8D6C-00C04F5B87B2}";
        public const string ITemporaryLayer = "{FCEFF094-8E6A-4972-9BB4-429C71B07289}";
        public const string ITerrainLayer = "{5A0F220D-614F-4C72-AFF2-7EA0BE2C8513}";
        public const string ITinLayer = "{FE308F36-BDCA-11D1-A523-0000F8774F0F}";
        public const string ITopologyLayer = "{FB6337E3-610A-4BC2-9142-760D954C22EB}";
        public const string IWMSLayer = "{005F592A-327B-44A4-AEEB-409D2F866F47}";
        public const string IWMSGroupLayer = "{D43D9A73-FF6C-4A19-B36A-D7ECBE61962A}";
        public const string IWMSMapLayer = "{8C19B114-1168-41A3-9E14-FC30CA5A4E9D}";
        #endregion
    }
}
