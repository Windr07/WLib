/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/07
// desc： 参考：https://blog.csdn.net/u012110480/article/details/84189611
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WLib.Model
{
    /// <summary>
    /// 提供对象深拷贝的方法
    /// </summary>
    public static class ObjectCopy
    {
        /// <summary>
        /// 通过将对象序列化、反序列二进制流的方式深拷贝对象
        /// （注意对象必须可序列化[Serializable]）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyBySerialize<T>(this T obj)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException( $"指定类型{typeof(T).FullName}不能序列化，"+
                    $"不能通过{nameof(CopyBySerialize)}方法深拷贝对象，请改用{nameof(CopyByReflect)}或其他方法！", nameof(obj));

            if (Object.ReferenceEquals(obj, null))
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
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CopyByReflect<T>(this T obj)
        {
            if (obj is string || obj.GetType().IsValueType)//如果是字符串或值类型则直接返回
                return obj;

            object newObject = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(newObject, CopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)newObject;
        }
    }
}
