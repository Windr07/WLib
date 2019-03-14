using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.GISClient;

namespace WLib.ArcGis.Server
{
    public class LoadServerMap
    {
        /// <summary>
        /// 获取ArcGIS Server发布的地图服务图层
        /// </summary>
        /// <param name="serviceUrl">服务地址，例：http://services.arcgisonline.com/ArcGIS/services </param>
        /// <param name="serviceName">服务名称，例：ESRI_Imagery_World_2D</param>
        /// <param name="isLAN">是否局域网（Local Area Network）</param>
        /// <returns></returns>
        private ILayer GetServerLayer(string serviceUrl, string serviceName, bool isLAN = false)
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
