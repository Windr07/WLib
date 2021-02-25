/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/8
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WLib.Attributes;
using WLib.Reflection;

namespace WLib.WinCtrls.Extension
{
    /// <summary>
    /// 提供与TreeNode相关的扩展方法
    /// </summary>
    public static class TreeNodeOpt
    {
        /// <summary>
        /// 递归TreeNode及其各级子节点Tag值，转成指定类型的对象
        /// <para>递归TreeNode子节点，转成对象可枚举属性（继承自IEnumerable）的值</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static IEnumerable<T> EnumerableValueToObject<T>(this TreeNodeCollection nodes)
        {
            return nodes.Cast<TreeNode>().Select(node => node.EnumerableValueToObject<T>());
        }
        /// <summary>
        /// 递归TreeNode及其各级子节点Tag值，转成指定类型的对象
        /// <para>递归TreeNode子节点，转成对象可枚举属性（继承自IEnumerable）的值</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static T EnumerableValueToObject<T>(this TreeNode parentNode)
        {
            if (!(parentNode.Tag is T))
                return default;
            T t = (T)parentNode.Tag;
            var objType = t.GetType();

            var properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType.IsGenericType && typeof(IList<>).IsAssignableFrom(propertyType.GetGenericTypeDefinition()))
                {
                    var propValue = new object();
                    if (property.CanRead)
                        propValue = property.GetValue(t, null);

                    if (propValue == null)
                        propValue = Activator.CreateInstance(propertyType);

                    var methods = propertyType.GetMethodInfoByBaseTypes("Clear", "Add");//获取Clear和Add方法
                    methods[0].Invoke(propValue, new object[] { });//调用Clear方法

                    var genericArs = propertyType.GetGenericArguments();
                    var tType = genericArs.Length > 0 ? genericArs[0] : null;
                    foreach (TreeNode treeNode in parentNode.Nodes)
                    {
                        if (tType == null || (treeNode.Tag != null && tType.IsAssignableFrom(treeNode.Tag.GetType())))
                        {
                            var obj = EnumerableValueToObject<object>(treeNode);
                            methods[1].Invoke(propValue, new object[] { treeNode.Tag });//调用Add方法
                        }
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// 将对象及其包含的数组数据转成TreeNode
        /// <para>递归对象属性，获取其中的可枚举属性（继承自IEnumerable），转成子节点数组</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TreeNode EnumerableValueToTreeNode<T>(this T obj)
        {
            var objType = obj.GetType();
            var node = new TreeNode { Name = objType.Name, Text = obj.ToString(), Tag = obj };
            EnumerableValueToTreeNode(obj, node);
            return node;
        }
        /// <summary>
        /// 将对象包含的数组数据转成TreeNode
        /// <para>递归对象属性，获取其中的可枚举属性（继承自IEnumerable），转成子节点数组</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="parentNode"></param>
        public static void EnumerableValueToTreeNode<T>(this T obj, TreeNode parentNode)
        {
            if (obj == null) return;
            var objType = obj.GetType();
            if (typeof(IEnumerable).IsAssignableFrom(objType) && !objType.IsExtSimpleType())
            {
                foreach (object child in (IEnumerable)obj)
                {
                    var subType = child.GetType();
                    var node = parentNode.AddSubNode(subType.Name, child.ToString(), child);
                    EnumerableValueToTreeNode(child, node);
                }
            }
            else
            {
                var properties = objType.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var propValue = new object();
                    if (property.CanRead)
                        propValue = property.GetValue(obj, null);

                    if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && !property.PropertyType.IsExtSimpleType())
                        EnumerableValueToTreeNode(propValue, parentNode);
                }
            }
        }


        /// <summary>
        /// 将对象及其属性转换为TreeNode
        /// <para>递归筛选包含visibleAttributeType特性的对象属性，作为子节点添加到TreeNode中，其中：</para>
        /// <para>TreeNode.Name = 属性名;</para>
        /// <para>TreeNode.Text = textFormat.Replace("N", 属性名).Replace("V", 属性值)</para>
        /// <para>TreeNode.Tag = 属性值对象</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="textFormat">节点显示的文本格式，"N"代表属性名，"V"代表属性值，值为null或Empty时从visibleAttributeType中查找格式，找不到则使用默认值"N: V"</param>
        /// <param name="visibleAttributeType">筛选特性：值为null时获取全部对象属性；值不为null时，只筛选包含此特性的属性添加到TreeNode中</param>
        public static TreeNode PropertiesToTreeNode<T>(this T obj, string textFormat = "N: V", Type visibleAttributeType = null)
        {
            var objType = obj.GetType();
            var node = new TreeNode { Name = objType.Name, Text = obj.ToString(), Tag = obj };
            PropertiesToTreeNode(obj, node, textFormat, visibleAttributeType);
            return node;
        }
        /// <summary>
        /// 将对象属性转换为TreeNode
        /// <para>递归筛选包含visibleAttributeType特性的对象属性，作为子节点添加到TreeNode中，其中：</para>
        /// <para>TreeNode.Name = 属性名;</para>
        /// <para>TreeNode.Text = textFormat.Replace("N", 属性名).Replace("V", 属性值)</para>
        /// <para>TreeNode.Tag = 属性值对象</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parentNode"></param>
        /// <param name="textFormat">节点显示的文本格式，"N"代表属性名，"V"代表属性值，值为null或Empty时从visibleAttributeType中查找格式，找不到则使用默认值"N: V"</param>
        /// <param name="visibleAttributeType">筛选特性：值为null时获取全部对象属性；值不为null时，只筛选包含此特性的属性添加到TreeNode中</param>
        public static void PropertiesToTreeNode<T>(this T obj, TreeNode parentNode, string textFormat = "N: V", Type visibleAttributeType = null)
        {
            if (obj == null) return;
            var objType = obj.GetType();
            if (typeof(IEnumerable).IsAssignableFrom(objType))
                foreach (object child in (IEnumerable)obj)
                    PropertiesToTreeNode(child, parentNode, textFormat, visibleAttributeType);

            var properties = objType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] attributes = null;
                if (visibleAttributeType != null &&
                    (attributes = property.GetCustomAttributes(visibleAttributeType, true)).Length == 0)
                    continue;

                var propValue = new object();
                if (property.CanRead)
                    propValue = property.GetValue(obj, null);

                if (string.IsNullOrEmpty(textFormat)) textFormat = GetTextFormat(attributes[0]);
                var text = textFormat.Replace("N", property.Name).Replace("V", propValue?.ToString());
                if (property.PropertyType.IsExtSimpleType())
                    parentNode.AddSubNode(property.Name, text, propValue);
                else if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var node = parentNode.AddSubNode(property.Name, text, propValue);
                    foreach (object child in (IEnumerable)propValue)
                        PropertiesToTreeNode(child, node, textFormat, visibleAttributeType);
                }
                else
                {
                    var node = parentNode.AddSubNode(property.Name, text, propValue);
                    PropertiesToTreeNode(propValue, node, textFormat, visibleAttributeType);
                }
            }
        }


        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="treeNode">要添加子节点的节点</param>
        /// <param name="name">节点名称</param>
        /// <param name="text">节点显示的文本</param>
        /// <param name="tag">节点关联的数据</param>
        /// <returns></returns>
        public static TreeNode AddSubNode(this TreeNode treeNode, string name, string text, object tag)
        {
            var node = new TreeNode { Name = name, Text = text, Tag = tag };
            treeNode.Nodes.Add(node);
            return node;
        }
        /// <summary>
        /// 从特性中获取节点显示文本格式，获取失败则返回文本格式为"N: V"
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static string GetTextFormat(object attribute)
        {
            if (attribute != null
                && attribute is NodeAttribute nodeAttribute
                && !string.IsNullOrEmpty(nodeAttribute.TextFormat))
                return nodeAttribute.TextFormat;

            return "N: V";
        }
    }
}

