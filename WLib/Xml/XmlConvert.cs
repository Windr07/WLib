/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace WLib.Xml
{
    /// <summary>
    /// XML序列化，包括XML与对象互转， XML与DataTable互转
    /// </summary>
    public class XmlConvert
    {
        /// <summary>
        /// 将XML文本转换为T类型的对象（反序列化）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="xml">xml文本</param>
        /// <returns></returns>
        public static T XmlToModel<T>(string xml)
        {
            StringReader xmlReader = new StringReader(xml);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return (T)xmlSerializer.Deserialize(xmlReader);
        }
        /// <summary>
        /// 将T类型的对象转换为XML文本（序列化）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="model">T类型的对象</param>
        /// <returns></returns>
        public static string ModelToXml<T>(T model)
        {
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(memoryStream, model);

            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }
        /// <summary>
        /// 提取XML文本的数据转为DataSet
        /// </summary>
        /// <param name="xml">xml文本</param>
        /// <returns></returns>
        public static System.Data.DataSet XmlToDataset(string xml)
        {
            StringReader xmlReader = new StringReader(xml);
            DataSet dataset = new DataSet();
            dataset.ReadXml(xmlReader);
            return dataset;
        }
        /// <summary>
        /// 提取XML文本的数据转为DataSet，返回第一个DataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static System.Data.DataTable XmlToTable(string xml)
        {
            return XmlToDataset(xml).Tables[0];
        }
    }
}
