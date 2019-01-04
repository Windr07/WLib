/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Xml;

namespace WLib.Xml
{
    /// <summary>
    /// 提供读取XML配置信息的方法
    /// </summary>
    public static class XmlConfig
    {
        /// <summary>
        /// 获取路径配置（要求路径保存在顶层结点的下一层结点的path属性中）
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName">保存路径的结点</param>
        /// <returns></returns>
        public static string GetPath(this XmlDocument xmlDoc, string nodeName)
        {
            var pathNode = xmlDoc.ChildNodes[0].SelectSingleNode(nodeName);
            var path = pathNode.Attributes["path"].Value;
            if (!string.IsNullOrEmpty(path) && !System.IO.Path.IsPathRooted(path))
                path = AppDomain.CurrentDomain.BaseDirectory + path;
            return path;
        }
        /// <summary>
        /// 保存路径配置（要求路径保存在顶层结点的下一层结点的path属性中）
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="attrParams">包括属性名、属性值、XML保存路径的参数对象</param>
        public static void SetPath(this XmlDocument xmlDoc, XmlSaveAttrParams attrParams)
        {
            var pathNode = xmlDoc.ChildNodes[0].SelectSingleNode(attrParams.AttributeName);
            pathNode.Attributes["path"].Value = attrParams.Value;
            xmlDoc.Save(attrParams.XmlSavePath);
        }

        /// <summary>
        /// 获取指定XML结点的指定属性值
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="childNodes">从顶层至属性所在的层的所有层级结点</param>
        /// <returns></returns>
        public static string GetAttributeValue(this XmlDocument xmlDoc, string attributeName, params string[] childNodes)
        {
            var node = xmlDoc.SelectSingleNode(childNodes[0]);
            for (int i = 1; i < childNodes.Length; i++)
            {
                node = node.SelectSingleNode(childNodes[i]);
            }
            return node.Attributes[attributeName].Value;
        }
        /// <summary>
        ///  获取指定XML结点的指定属性值
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="attrParams">属性名称</param>
        /// <param name="childNodes"></param>
        public static void SetAttributeValue(this XmlDocument xmlDoc, XmlSaveAttrParams attrParams, params string[] childNodes)
        {
            var node = xmlDoc.SelectSingleNode(childNodes[0]);
            for (int i = 1; i < childNodes.Length; i++)
            {
                node = node.SelectSingleNode(childNodes[i]);
            }
            node.Attributes[attrParams.AttributeName].Value = attrParams.Value;
            xmlDoc.Save(attrParams.XmlSavePath);
        }

        /// <summary>
        /// 创建XML结点的属性
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XmlAttribute CreateAttribute(this XmlDocument xmlDoc, XmlNode node, string attributeName, string value)
        {
            XmlAttribute attr = xmlDoc.CreateAttribute(attributeName);
            attr.Value = value;
            node.Attributes.SetNamedItem(attr);
            return attr;
        }
    }
}
