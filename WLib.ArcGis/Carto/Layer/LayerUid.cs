/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.esriSystem;

namespace WLib.ArcGis.Carto.Layer
{
    //此类用于YYGISLib.ArcGisHelper.Map.Query.GetLayersByUID和GetLayerNamesByUID方法提供参数调用

    /// <summary>
    /// 各类图层的UID定义
    /// </summary>
    public class LayerUid
    {
        public static UID IACFeatureLayer { get { UID theResult = new UIDClass(); theResult.Value = "{AD88322D-533D-4E36-A5C9-1B109AF7A346}"; return theResult; } }
        public static UID IACLayer { get { UID theResult = new UIDClass(); theResult.Value = "{74E45211-DFE6-11D3-9FF7-00C04F6BC6A5}"; return theResult; } }
        public static UID IACImageLayer { get { UID theResult = new UIDClass(); theResult.Value = "{495C0E2C-D51D-4ED4-9FC1-FA04AB93568D}"; return theResult; } }
        public static UID IACAcetateLayer { get { UID theResult = new UIDClass(); theResult.Value = "{65BD02AC-1CAD-462A-A524-3F17E9D85432}"; return theResult; } }
        public static UID IAnnotationLayer { get { UID theResult = new UIDClass(); theResult.Value = "{4AEDC069-B599-424B-A374-49602ABAD308}"; return theResult; } }
        public static UID IAnnotationSublayer { get { UID theResult = new UIDClass(); theResult.Value = "{DBCA59AC-6771-4408-8F48-C7D53389440C}"; return theResult; } }
        public static UID ICadLayer { get { UID theResult = new UIDClass(); theResult.Value = "{E299ADBC-A5C3-11D2-9B10-00C04FA33299}"; return theResult; } }
        public static UID ICadastralFabricLayer { get { UID theResult = new UIDClass(); theResult.Value = "{7F1AB670-5CA9-44D1-B42D-12AA868FC757}"; return theResult; } }
        public static UID ICompositeLayer { get { UID theResult = new UIDClass(); theResult.Value = "{BA119BC4-939A-11D2-A2F4-080009B6F22B}"; return theResult; } }
        public static UID ICompositeGraphicsLayer { get { UID theResult = new UIDClass(); theResult.Value = "{9646BB82-9512-11D2-A2F6-080009B6F22B}"; return theResult; } }
        public static UID ICoverageAnnotationLayer { get { UID theResult = new UIDClass(); theResult.Value = "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}"; return theResult; } }
        public static UID IDataLayer { get { UID theResult = new UIDClass(); theResult.Value = "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}"; return theResult; } }
        public static UID IDimensionLayer { get { UID theResult = new UIDClass(); theResult.Value = "{0737082E-958E-11D4-80ED-00C04F601565}"; return theResult; } }
        public static UID IFDOGraphicsLayer { get { UID theResult = new UIDClass(); theResult.Value = "{48E56B3F-EC3A-11D2-9F5C-00C04F6BC6A5}"; return theResult; } }
        public static UID IFeatureLayer { get { UID theResult = new UIDClass(); theResult.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}"; return theResult; } }
        public static UID IGdbRasterCatalogLayer { get { UID theResult = new UIDClass(); theResult.Value = "{605BC37A-15E9-40A0-90FB-DE4CC376838C}"; return theResult; } }
        public static UID IGeoFeatureLayer { get { UID theResult = new UIDClass(); theResult.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"; return theResult; } }
        public static UID IGraphicsLayer { get { UID theResult = new UIDClass(); theResult.Value = "{34B2EF81-F4AC-11D1-A245-080009B6F22B}"; return theResult; } }
        public static UID IGroupLayer { get { UID theResult = new UIDClass(); theResult.Value = "{EDAD6644-1810-11D1-86AE-0000F8751720}"; return theResult; } }
        public static UID IIMSSubLayer { get { UID theResult = new UIDClass(); theResult.Value = "{D090AA89-C2F1-11D3-9FEF-00C04F6BC6A5}"; return theResult; } }
        public static UID IIMAMapLayer { get { UID theResult = new UIDClass(); theResult.Value = "{DC8505FF-D521-11D3-9FF4-00C04F6BC6A5}"; return theResult; } }
        public static UID ILayer { get { UID theResult = new UIDClass(); theResult.Value = "{34C20002-4D3C-11D0-92D8-00805F7C28B0}"; return theResult; } }
        public static UID IMapServerLayer { get { UID theResult = new UIDClass(); theResult.Value = "{E9B56157-7EB7-4DB3-9958-AFBF3B5E1470}"; return theResult; } }
        public static UID IMapServerSublayer { get { UID theResult = new UIDClass(); theResult.Value = "{B059B902-5C7A-4287-982E-EF0BC77C6AAB}"; return theResult; } }
        public static UID INetworkLayer { get { UID theResult = new UIDClass(); theResult.Value = "{82870538-E09E-42C0-9228-CBCB244B91BA}"; return theResult; } }
        public static UID IRasterLayer { get { UID theResult = new UIDClass(); theResult.Value = "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}"; return theResult; } }
        public static UID IRasterCatalogLayer { get { UID theResult = new UIDClass(); theResult.Value = "{AF9930F0-F61E-11D3-8D6C-00C04F5B87B2}"; return theResult; } }
        public static UID ITemporaryLayer { get { UID theResult = new UIDClass(); theResult.Value = "{FCEFF094-8E6A-4972-9BB4-429C71B07289}"; return theResult; } }
        public static UID ITerrainLayer { get { UID theResult = new UIDClass(); theResult.Value = "{5A0F220D-614F-4C72-AFF2-7EA0BE2C8513}"; return theResult; } }
        public static UID ITinLayer { get { UID theResult = new UIDClass(); theResult.Value = "{FE308F36-BDCA-11D1-A523-0000F8774F0F}"; return theResult; } }
        public static UID ITopologyLayer { get { UID theResult = new UIDClass(); theResult.Value = "{FB6337E3-610A-4BC2-9142-760D954C22EB}"; return theResult; } }
        public static UID IWMSLayer { get { UID theResult = new UIDClass(); theResult.Value = "{005F592A-327B-44A4-AEEB-409D2F866F47}"; return theResult; } }
        public static UID IWMSGroupLayer { get { UID theResult = new UIDClass(); theResult.Value = "{D43D9A73-FF6C-4A19-B36A-D7ECBE61962A}"; return theResult; } }
        public static UID IWMSMapLayer { get { UID theResult = new UIDClass(); theResult.Value = "{8C19B114-1168-41A3-9E14-FC30CA5A4E9D}"; return theResult; } }
    }
}
