using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace WLib.ArcGis.Server
{
    /// <summary>
    /// 获取服务图层
    /// </summary>
    public class ServerLayerOpt
    {
        /// <summary>
        /// 获取ArcGIS Server发布的地图服务图层
        /// </summary>
        /// <param name="serviceUrl">服务地址，例：http://services.arcgisonline.com/ArcGIS/services </param>
        /// <param name="serviceName">服务名称，例：ESRI_Imagery_World_2D</param>
        /// <param name="isLAN">是否局域网（Local Area Network）</param>
        /// <returns></returns>
        public ILayer GetServerLayer(string serviceUrl, string serviceName, bool isLAN = false)
        {
            IAGSServerObjectName serverObjectName = GetServerObjectName(serviceUrl, serviceName, isLAN);
            IName iName = (IName)serverObjectName;
            IAGSServerObject serverObject = (IAGSServerObject)iName.Open();
            IMapServer mapServer = (IMapServer)serverObject;
            IMapServerLayer mapServerLayer = new MapServerLayerClass();

            //连接地图服务,第一个参数为地图服务名称，第二个参数为数据框架名称（the name of a data frame）
            mapServerLayer.ServerConnect(serverObjectName, mapServer.DefaultMapName);
            return (ILayer)mapServerLayer;
        }
        /// <summary>
        /// 获取REST的地图服务图层
        /// </summary>
        /// <param name="serviceUrl">服务地址，例：http://192.168.1.102:7485/tset/rest/services/GDZL?token=jeGFSsDzTX3U3Fx4phYDhl2lGw3Veip4DC3k43QxMWHfzCRmvbh2eA5CQ-LQcalC </param>
        /// <returns></returns>
        public ILayer GetRESTLayer(string serviceUrl)
        {
            IMapServerRESTLayer restLayer = new MapServerRESTLayerClass();
            restLayer.Connect(serviceUrl);
            return restLayer as ILayer;
        }
        /// <summary>
        /// 获取WMS的地图服务图层
        /// </summary>
        /// <param name="serviceUrl">服务地址</param>
        /// <returns></returns>
        public ILayer GetWMSLayer(string serviceUrl)
        {
            IPropertySet propertyset = new PropertySetClass();
            propertyset.SetProperty("url", serviceUrl);

            IWMSConnectionName wmsConnectionName = new WMSConnectionNameClass();
            wmsConnectionName.ConnectionProperties = propertyset;

            IWMSGroupLayer wmsMapLayer = new WMSMapLayerClass();
            IDataLayer dataLayer = wmsMapLayer as IDataLayer;
            dataLayer.Connect(wmsConnectionName as IName);
            IWMSServiceDescription wmsServiceDesc = wmsMapLayer.WMSServiceDescription;

            ILayer layer = wmsMapLayer as ILayer;
            layer.Name = wmsServiceDesc.WMSTitle;
            layer.Visible = true;
            return layer;
        }
        /// <summary>
        /// 获取WMTS的地图服务图层
        /// </summary>
        /// <param name="serviceUrl">服务地址</param>
        /// <returns></returns>
        public ILayer GetWMTSLayer(string serviceUrl)
        {
            IPropertySet propertyset = new PropertySetClass();
            propertyset.SetProperty("url", serviceUrl);
            IWMTSConnectionFactory wmtsconnectionfactory = new WMTSConnectionFactory();
            IWMTSConnection connection = wmtsconnectionfactory.Open(propertyset, 0, null);
            IWMTSLayer wmtsLayer = new WMTSLayer();
            IName iName = connection.FullName;
            wmtsLayer.Connect(iName);
            ILayer layer = wmtsLayer as ILayer;
            layer.Visible = true;
            return layer;
        }


        /// <summary>
        /// 连接地图服务，获取IAGSServerObjectName对象
        /// </summary>
        /// <param name="hostOrUrl">服务地址，例：http://services.arcgisonline.com/ArcGIS/services</param>
        /// <param name="serviceName">服务名称，例：ESRI_Imagery_World_2D</param>
        /// <param name="isLAN">是否局域网（Local Area Network）</param>
        /// <returns></returns>
        private IAGSServerObjectName GetServerObjectName(string hostOrUrl, string serviceName, bool isLAN = false)
        {
            //设置连接属性
            IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty(isLAN ? "machine" : "url", hostOrUrl);

            //打开连接
            IAGSServerConnectionFactory factory = new AGSServerConnectionFactory();
            IAGSServerConnection connection = factory.Open(propertySet, 0);

            IAGSEnumServerObjectName serverObjectNames = connection.ServerObjectNames;
            serverObjectNames.Reset();
            IAGSServerObjectName serverObjectName;
            while ((serverObjectName = serverObjectNames.Next()) != null)
            {
                if (serverObjectName.Name.ToLower() == serviceName.ToLower() && serverObjectName.Type == "MapServer")
                    break;
            }
            return serverObjectName;
        }
    }
}
