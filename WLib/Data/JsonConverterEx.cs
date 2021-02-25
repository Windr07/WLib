/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/7
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WLib.Data
{
    /* 使用示例：
     * 当一个类的某些属性的类型是接口类型时，不能直接通过Newtonsoft.Json.JsonConvert.DeserializeObject(string json)方法对JSON反序列化获得对象，
     * 此时需要指定接口关联的实现类才能成功反序列化。
     * 当前类通过继承Newtonsoft.Json.JsonConverter类的子类，指定接口对应的实现类来辅助反序列，调用示例：
     * 
     // 下述代码中，PluginPlan类包含ICommand接口的属性， Command类继承自ICommand接口
     * var interfaceToClassDict = new Dictionary<Type, Type> {{ typeof(ICommand), typeof(Command) }};
     * var serializer = new JsonSerializer();
     * serializer.Converters.Add(new JsonConverterEx(interfaceToClassDict));
       var pluginPlan = serializer.Deserialize<PluginPlan>(new JsonTextReader(new StringReader(json)));
     */

    /// <summary>
    /// 可指定接口(或抽象类）与对应实现类的JSON序列化转换器
    /// <para>继承自<see cref="JsonConverter"/>，将接口与默认实体类相对应以实现反序列化</para>
    /// </summary>
    public class JsonConverterEx : JsonConverter
    {
        /// <summary>
        /// 接口(或抽象类）与默认实现类的键值对
        /// </summary>
        public Dictionary<Type, Type> InterfaceToClassDict = new Dictionary<Type, Type>();

        /// <summary>
        /// 可指定接口(或抽象类）与对应实现类的JSON序列化转换器
        /// <para>继承自<see cref="JsonConverter"/>，将接口与默认实体类相对应以实现反序列化</para>
        /// </summary>
        public JsonConverterEx() { }
        /// <summary>
        /// 可指定接口(或抽象类）与对应实现类的序列化转换器
        /// <para>继承自<see cref="JsonConverter"/>，将接口与默认实体类相对应以实现反序列化</para>
        /// </summary>
        /// <param name="interfaceToClassDict">接口(或抽象类）与默认实现类的键值对</param>
        public JsonConverterEx(Dictionary<Type, Type> interfaceToClassDict) => InterfaceToClassDict = interfaceToClassDict;
        /// <summary>
        /// 可指定接口(或抽象类）与对应实现类的序列化转换器
        /// <para>继承自<see cref="JsonConverter"/>，将接口与默认实体类相对应以实现反序列化</para>
        /// </summary>
        /// <param name="interfaceType">反序列化对象时，对象包含的接口(或抽象类）属性类型</param>
        /// <param name="implClassType">反序列化对象时，对象包含的接口(或抽象类）属性类型应对应的实现类类型</param>
        public JsonConverterEx(Type interfaceType, Type implClassType) => InterfaceToClassDict.Add(interfaceType, implClassType);


        public override bool CanConvert(Type objectType) => InterfaceToClassDict.Keys.Contains(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return InterfaceToClassDict.ContainsKey(objectType) ?
                serializer.Deserialize(reader, InterfaceToClassDict[objectType]) :
                serializer.Deserialize(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => serializer.Serialize(writer, value);
    }
}
