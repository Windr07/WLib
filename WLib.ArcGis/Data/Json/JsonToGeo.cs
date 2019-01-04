//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ESRI.ArcGIS.Geometry;

//namespace YYGISLib.ArcGisHelper.Json
//{
//    class jsonToGeo
//    {
//        /// <summary>
//        /// 将json字符串转成动态对象
//        /// </summary>
//        /// <param name="json"></param>
//        /// <returns></returns>
//        public dynamic convert(string json)
//        {
//            var DynamicObject = JsonConvert.DeserializeObject<dynamic>(json);
//            return DynamicObject;
//        }

//        /// <summary>    
//        /// JSON字符串转成IGeometry    
//        /// </summary>    
//        public ESRI.ArcGIS.Geometry.IGeometry GeometryFromJsonString(string strJson, ESRI.ArcGIS.Geometry.esriGeometryType type)
//        {
//            return GeometryFromJsonString(strJson, type, false, false);
//        }

//        /// <summary>    
//        /// JSON字符串转成IGeometry    
//        /// </summary>    
//        public ESRI.ArcGIS.Geometry.IGeometry GeometryFromJsonString(string strJson, ESRI.ArcGIS.Geometry.esriGeometryType type,
//            bool bHasZ, bool bHasM)
//        {
//            ESRI.ArcGIS.esriSystem.IJSONReader jsonReader = new ESRI.ArcGIS.esriSystem.JSONReaderClass();
//            jsonReader.ReadFromString(strJson);

//            ESRI.ArcGIS.Geometry.JSONConverterGeometryClass jsonCon = new ESRI.ArcGIS.Geometry.JSONConverterGeometryClass();
//            return jsonCon.ReadGeometry(jsonReader, type, bHasZ, bHasM);
//        }
//        /// <summary>  
//        /// IGeometry转成JSON字符串  
//        /// </summary>  
//        public string GeometryToJsonString(ESRI.ArcGIS.Geometry.IGeometry geometry)
//        {
//            ESRI.ArcGIS.esriSystem.IJSONWriter jsonWriter = new ESRI.ArcGIS.esriSystem.JSONWriterClass();
//            jsonWriter.WriteToString();

//            ESRI.ArcGIS.Geometry.JSONConverterGeometryClass jsonCon = new ESRI.ArcGIS.Geometry.JSONConverterGeometryClass();
//            jsonCon.WriteGeometry(jsonWriter, null, geometry, false);

//            return Encoding.UTF8.GetString(jsonWriter.GetStringBuffer());
//        }





//        /// <summary>
//        /// 将JSON字符串转成Shapfile文件
//        /// </summary>
//        /// <param name="json">输入的Json字符串</param>
//        /// <param name="outputFileFolder">输出的文件地址</param>
//        /// <param name="ShapeName">输出的文件名（不带后缀）</param>
//        public static void JsonToShp(string json, string outputFileFolder, string ShapeName)
//        {
//            //将json字符串转成动态对象
//            jsonToGeo jTG = new jsonToGeo();
//            var obj = jTG.convert(json);

//            //获取json中的features集
//            var features = obj["features"];
//            int count = obj["features"].Count;//图形个数
//            //obj["features"][0]["geometry"]表示第一个图形的geometry

//            //坐标系
//            int wkid = (int)obj["spatialReference"]["wkid"];

//            //创建SpatialReferenceEnvironmentClass对象
//            ISpatialReferenceFactory2 pSpaRefFactory = new SpatialReferenceEnvironmentClass();
//            //创建地理坐标系对象
//            //IGeographicCoordinateSystem pNewGeoSys = pSpaRefFactory.CreateGeographicCoordinateSystem(gcsType);//4214代表Beijing1954
//            //创建投影坐标系
//            IProjectedCoordinateSystem pGeoSys = pSpaRefFactory.CreateProjectedCoordinateSystem(wkid);//2437 Beijing_1954_3_Degree_GK_CM_120E 

//            //字段集合
//            var fields = obj["fields"];
//            dynamic shpFields;
//            shpFields = fields;



//            //List<IPoint> points = new List<IPoint>();
//            //List<IPolyline> polylines = new List<IPolyline>();
//            //List<IPolygon> polygons = new List<IPolygon>();

//            List<IGeometry> geometryList = new List<IGeometry>();

//            GeoToFeature gtf = new GeoToFeature();


//            string geometryType = obj["geometryType"].ToString();
//            switch (geometryType)
//            {
//                case "esriGeometryPoint":
//                    for (int i = 0; i < count; i++)
//                    {
//                        IPoint pointLocation = null;
//                        pointLocation = jTG.GeometryFromJsonString(features[i]["geometry"].ToString(), ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint) as ESRI.ArcGIS.Geometry.IPoint;
//                        pointLocation.SpatialReference = pGeoSys;
//                        IGeometry point = pointLocation as IGeometry;
//                        //points.Add(pointLocation);
//                        geometryList.Add(point);
//                    }
//                    //创建点shp，并添加字段
//                    gtf.CreateShapefile(outputFileFolder, ShapeName, esriGeometryType.esriGeometryPoint, pGeoSys, shpFields);

//                    break;
//                case "esriGeometryPolyline":
//                    for (int i = 0; i < count; i++)
//                    {
//                        IPolyline polylineLocation = jTG.GeometryFromJsonString(features[i]["geometry"].ToString(), ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline) as IPolyline;
//                        polylineLocation.SpatialReference = pGeoSys;
//                        IGeometry polyline = polylineLocation as IGeometry;
//                        //polylines.Add(polylineLocation);
//                        geometryList.Add(polyline);
//                    }
//                    //创建线shp，并添加字段
//                    gtf.CreateShapefile(outputFileFolder, ShapeName, esriGeometryType.esriGeometryPolyline, pGeoSys, shpFields);
//                    break;
//                case "esriGeometryPolygon":
//                    for (int i = 0; i < count; i++)
//                    {
//                        IPolygon polygonLocation = jTG.GeometryFromJsonString(features[i]["geometry"].ToString(), ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon) as IPolygon;
//                        polygonLocation.SpatialReference = pGeoSys;
//                        IGeometry polygon = polygonLocation as IGeometry;
//                        //polygons.Add(polygonLocation);
//                        geometryList.Add(polygon);
//                    }
//                    //创建面shp，并添加字段
//                    gtf.CreateShapefile(outputFileFolder, ShapeName, esriGeometryType.esriGeometryPolygon, pGeoSys, shpFields);


//                    break;
//                default: break;
//            }

//            //添加要素
//            gtf.AddFeatureByBuffer(outputFileFolder, ShapeName, geometryList, features);
//        }

//    }
//}
