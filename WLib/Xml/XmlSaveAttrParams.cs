/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Xml
{
    /// <summary>
    /// XML配置中，设置并保存属性值的参数
    /// </summary>
    public class XmlSaveAttrParams
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// XML文件保存路径
        /// </summary>
        public string XmlSavePath { get; set; }

        /// <summary>
        /// XML配置中，设置并保存属性值的参数
        /// </summary>
        public XmlSaveAttrParams()
        {

        }
        /// <summary>
        /// XML配置中，设置并保存属性值的参数
        /// <param name="attributeName">属性名称</param>
        /// </summary>
        public XmlSaveAttrParams(string attributeName)
        {
            this.AttributeName = attributeName;
        }
        /// <summary>
        /// XML配置中，设置并保存属性值的参数
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性值</param>
        /// </summary>
        public XmlSaveAttrParams(string attributeName, string value)
        {
            this.AttributeName = attributeName;
            this.Value = value;
        }
        /// <summary>
        /// XML配置中，设置并保存属性值的参数
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="xmlSavePath">XML文件保存路径</param>
        /// </summary>
        public XmlSaveAttrParams(string attributeName, string value, string xmlSavePath)
        {
            this.AttributeName = attributeName;
            this.Value = value;
            this.XmlSavePath = xmlSavePath;
        }
    }
}
