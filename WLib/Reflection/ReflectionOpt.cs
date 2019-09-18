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
        /// 判断类型是否为简单类型
        /// <para>值类型、IntPtr、UIntPtr、String、DateTime、DateTimeOffset、TimeSpan、Guid等均是简单类型</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[]
                {
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }


        /// <summary>
        /// 递归获得类型的继承树，包括类型继承的各级父类和接口
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
