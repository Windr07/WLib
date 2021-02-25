/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/07
// desc： 参考：https://blog.csdn.net/u012110480/article/details/84189611
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WLib.Data
{
    /// <summary>
    /// 提供对象深拷贝的方法
    /// </summary>
    public static class ObjectCopy
    {
        /// <summary>
        /// 通过将对象序列化、反序列二进制流的方式深拷贝对象
        /// <para>注意对象必须可序列化（即包含<see cref="SerializableAttribute"/>特性），对象的非基本类型属性也必需可序列化（包含<see cref="SerializableAttribute"/>特性）</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyBySerialize<T>(this T obj)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException($"指定类型{typeof(T).FullName}不能序列化，" +
                    $"不能通过{nameof(CopyBySerialize)}方法深拷贝对象，请改用{nameof(CopyByReflect)}或其他方法！", nameof(obj));

            if (object.ReferenceEquals(obj, null))
                return default;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, obj);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// 通过反射方式深拷贝对象
        /// <para>* 要求对象的属性或字段为基本类型，或包含无参构造函数的类或结构（不包含无参构造函数可能导致出错）</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyByReflect<T>(this T obj)
        {
            //1、如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType)
                return obj;

            //2、如果是数组类型，使用Array.Copy
            var objType = obj.GetType();
            object newObject;
            if (objType.IsArray)
            {
                newObject = Activator.CreateInstance(objType, (obj as Array).Length);
                Array.Copy(obj as Array, newObject as Array, (obj as Array).Length);
                return (T)newObject;
            }

            //3、其他引用类型则递归对象字段进行拷贝
            newObject = Activator.CreateInstance(objType);
            CopyFields(objType, obj, newObject);
            CopyProperties(objType, obj, newObject);
            return (T)newObject;
        }
        /// <summary>
        /// 通过表达式树深拷贝对象
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut CopyByExpTree<TIn, TOut>(this TIn tIn)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindings = new List<MemberBinding>();

            foreach (var outProperty in typeof(TOut).GetProperties())
            {
                if (!outProperty.CanWrite) continue;
                var inProperty = typeof(TIn).GetProperty(outProperty.Name);
                if (inProperty == null)
                    continue;
                MemberExpression property = Expression.Property(parameterExpression, inProperty);
                MemberBinding memberBinding = Expression.Bind(outProperty, property);
                memberBindings.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindings.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            var func = lambda.Compile();
            return func(tIn);
        }


        /// <summary>
        /// 复制对象的字段值到新对象中
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="sourceObject"></param>
        /// <param name="newObject"></param>
        private static void CopyFields(Type objType, object sourceObject, object newObject)
        {
            FieldInfo[] fields = objType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                if (field.IsLiteral) continue;
                var value = field.GetValue(sourceObject);
                if (value == null)
                    field.SetValue(newObject, null);
                else
                {
                    var newFieldValue = CopyByReflect(value);
                    field.SetValue(newObject, newFieldValue);
                }
            }
        }
        /// <summary>
        /// 复制对象的属性值到新对象中
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="sourceObject"></param>
        /// <param name="newObject"></param>
        private static void CopyProperties(Type objType, object sourceObject, object newObject)
        {
            PropertyInfo[] properties = objType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite == false) continue;
                var value = property.GetValue(sourceObject, null);
                if (value == null)
                    property.SetValue(newObject, null, null);
                else
                {
                    var newFieldValue = CopyByReflect(value);
                    property.SetValue(newObject, newFieldValue, null);
                }
            }
        }
    }
}
