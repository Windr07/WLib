/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WLib.Reflection
{
    /// <summary>
    /// 反射操作，包括类型判断、获取、实例创建等
    /// </summary>
    public static class ReflectionOpt
    {
        /// <summary>
        /// C#默认的13种简单类型（包括数值类型、bool、char）
        /// </summary>
        public static Type[] DefaultSimpleType = new Type[]
        {
             typeof(sbyte),
             typeof(byte),
             typeof(short),
             typeof(ushort),
             typeof(int),
             typeof(uint),
             typeof(long),
             typeof(ulong),
             typeof(float),
             typeof(double),
             typeof(decimal),
             typeof(char),
             typeof(bool),
        };
        /// <summary>
        /// C#默认的11种数值类型
        /// </summary>
        public static Type[] DefaultNumericType = new Type[]
        {
             typeof(sbyte),
             typeof(byte),
             typeof(short),
             typeof(ushort),
             typeof(int),
             typeof(uint),
             typeof(long),
             typeof(ulong),
             typeof(float),
             typeof(double),
             typeof(decimal),
        };
        /// <summary>
        /// C#默认的8种整数类型
        /// </summary>
        public static Type[] DefaultIntegerType = new Type[]
        {
             typeof(sbyte),
             typeof(byte),
             typeof(short),
             typeof(ushort),
             typeof(int),
             typeof(uint),
             typeof(long),
             typeof(ulong),
        };
        /// <summary>
        /// C#默认的3种浮点类型（float、double、decimal）
        /// </summary>
        public static Type[] DefaultFloatType = new Type[]
        {
             typeof(float),
             typeof(double),
             typeof(decimal),
        };


        /// <summary>
        /// 判断类型是否为指定类型或继承自指定类型（类、抽象类、接口）
        /// </summary>
        /// <param name="type">被判断的类型</param>
        /// <param name="compareType">指定类型，表示被判断的类型是否继承自该类型（类、抽象类、接口）</param>
        /// <returns></returns>
        public static bool IsType(this Type type, Type compareType)
        {
            if (type == compareType)
                return true;
            else if (type == null)
                return false;
            else
                return IsType(type.BaseType, compareType) || type.GetInterfaces().Contains(compareType);
        }
        /// <summary>
        /// 判断类型是否为扩展意义上的简单类型
        /// <para>包括：数值类型、bool、char、string、IntPtr、UIntPtr、String、DateTime、DateTimeOffset、TimeSpan、Guid等</para>
        /// <para>这些类型通常用于直接数据编辑而不必进行结构细分</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsExtSimpleType(this Type type)
        {
            return
                type.IsSimpleType() ||
                type.IsPrimitive ||
                new Type[]
                {
                    typeof(String),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
        /// <summary>
        /// 判断类型是否为数值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(this Type type) => DefaultNumericType.Contains(type);
        /// <summary>
        /// 判断类型是否为C#默认的简单类型
        /// <para>13种简单类型：包括数值类型、bool、char</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type) => DefaultSimpleType.Contains(type);
        /// <summary>
        ///  判断类型是否为C#默认的整数类型
        /// <para>8种整数类型：sbyte、byte、short、ushort、int、uint、long、ulong</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsIntegerType(this Type type) => DefaultIntegerType.Contains(type);
        /// <summary>
        ///  判断类型是否为C#默认的浮点类型
        /// <para>3种浮点类型：float、double、decimal</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsFloatType(this Type type) => DefaultFloatType.Contains(type);
        /// <summary>
        /// 判断类型是否为基元类型
        /// <para>14种基元类型：包括除了decimal外的数值类型、bool、char、IntPtr、UIntPtr</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Obsolete("请直接使用.NET默认的属性Type.IsPrimitive")]
        public static bool IsPrimitiveType(this Type type) => type.IsPrimitive;
        /// <summary>
        /// 获取数值类型的最大值（<see cref="decimal"/>类型除外）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static double GetMaxValue(this Type type)
        {
            if (type == typeof(sbyte)) return sbyte.MaxValue;
            else if (type == typeof(byte)) return byte.MaxValue;
            else if (type == typeof(short)) return short.MaxValue;
            else if (type == typeof(ushort)) return ushort.MaxValue;
            else if (type == typeof(int)) return int.MaxValue;
            else if (type == typeof(uint)) return uint.MaxValue;
            else if (type == typeof(long)) return long.MaxValue;
            else if (type == typeof(ulong)) return ulong.MaxValue;
            else if (type == typeof(float)) return float.MaxValue;
            else if (type == typeof(double)) return double.MaxValue;
            else throw new InvalidCastException($"类型{type.Name}不是数值类型！");
        }
        /// <summary>
        /// 获取数值类型的最小值（<see cref="decimal"/>类型除外）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static double GetMinValue(this Type type)
        {
            if (type == typeof(sbyte)) return sbyte.MinValue;
            else if (type == typeof(byte)) return byte.MinValue;
            else if (type == typeof(short)) return short.MinValue;
            else if (type == typeof(ushort)) return ushort.MinValue;
            else if (type == typeof(int)) return int.MinValue;
            else if (type == typeof(uint)) return uint.MinValue;
            else if (type == typeof(long)) return long.MinValue;
            else if (type == typeof(ulong)) return ulong.MinValue;
            else if (type == typeof(float)) return float.MinValue;
            else if (type == typeof(double)) return double.MinValue;
            else throw new InvalidCastException($"类型{type.Name}不是数值类型！");
        }


        /// <summary>
        /// 判断指定的类型 <paramref name="type"/> 是否是指定泛型类型的子类型，或实现了指定泛型接口（例如IList&lt;&gt;）。
        /// </summary>
        /// <remarks>来源：https://www.cnblogs.com/walterlv/p/10236419.html </remarks>
        /// <param name="type">需要测试的类型。</param>
        /// <param name="genericType">泛型接口类型，传入 typeof(IXxx&lt;&gt;)，例如 typeof(IList&lt;&gt;) 或者  typeof(List&lt;&gt;)</param>
        /// <returns>如果是泛型接口的子类型，则返回 true，否则返回 false。</returns>
        public static bool HasImplementedRawGeneric(this Type type, Type genericType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (genericType == null) throw new ArgumentNullException(nameof(genericType));

            // 测试接口
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 测试类型
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            // 没有找到任何匹配的接口或类型。
            return false;

            // 测试某个类型是否是指定的原始接口。
            bool IsTheRawGenericType(Type test)
                => genericType == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }


        /// <summary>
        /// 递归获得类型继承的各级父类和接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="containSelfType">返回结果中是否包含当前类型本身</param>
        public static Type[] GetAllBaseTypes(this Type type, bool containSelfType = false)
        {
            var baseTyeps = new List<Type>();
            if (containSelfType)
                baseTyeps.Add(type);
            GetAllBaseTypes(type, ref baseTyeps);
            return baseTyeps.ToArray();
        }
        /// <summary>
        /// 递归获得类型继承的各级父类和接口
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseTypes"></param>
        private static void GetAllBaseTypes(this Type type, ref List<Type> baseTypes)
        {
            if (type.BaseType != null)
            {
                baseTypes.Add(type.BaseType);
                GetAllBaseTypes(type.BaseType, ref baseTypes);
            }

            var interfaces = type.GetInterfaces();
            if (interfaces.Length > 0)
                baseTypes = baseTypes.Union(interfaces).ToList();
        }
        /// <summary>
        /// 递归类型及其继承的各级父类和接口，获取类型中指定名称的方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodNames"></param>
        /// <returns></returns>
        public static MethodInfo[] GetMethodInfoByBaseTypes(this Type type, params string[] methodNames)
        {
            var baseTypes = type.GetAllBaseTypes(true);
            MethodInfo[] methods = new MethodInfo[methodNames.Length];
            foreach (var baseType in baseTypes)
            {
                for (int i = 0; i < methodNames.Length; i++)
                {
                    if (methods[i] == null)
                    {
                        var method = baseType.GetMethod(methodNames[i], BindingFlags.Instance | BindingFlags.Public);
                        if (method != null)
                            methods[i] = method;
                    }
                }
            }
            return methods;
        }


        /// <summary>
        /// 获取程序集下实现指定接口的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="achieveClass">是否只获取具体实现类，即：值为False则结果可能包含接口和抽象类，值为Ture时不包含接口和抽象类</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetInterfaceAchieveTypes(this Assembly assembly, Type interfaceType, bool achieveClass = true)
        {
            if (!interfaceType.IsInterface)
                throw new Exception($"类型{interfaceType.FullName}不是接口类型！");


            var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(interfaceType));
            if (achieveClass)
                types = types.Where(v => v.IsClass && !v.IsAbstract);
            return types;
        }
        /// <summary>
        /// 获取程序集下实现指定接口的类的实例，即使用默认构造函数创建的实例
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static IEnumerable<T> GetInterfaceAchieveTypes<T>(this Assembly assembly)
        {
            return assembly.GetInterfaceAchieveTypes(typeof(T)).Select(t => (T)Activator.CreateInstance(t));
        }


        /// <summary>
        /// 获取程序集下实现指定抽象类的类的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="abstractType">抽象类类型</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAbstractAchieveTypes(this Assembly assembly, Type abstractType)
        {
            if (!abstractType.IsAbstract)
                throw new Exception($"类型{abstractType.FullName}不是抽象类类型！");
            return assembly.GetTypes().Where(t => t != abstractType && t.IsType(abstractType));
        }
        /// <summary>
        /// 获取程序集下实现指定抽象类的类的实例，即使用默认构造函数创建的实例
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAbstractAchieveTypes<T>(this Assembly assembly)
        {
            return assembly.GetAbstractAchieveTypes(typeof(T)).Select(t => (T)Activator.CreateInstance(t));
        }

    }
}
