/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/8
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace WLib.Model
{
    /// <summary>
    /// 动态类型
    /// </summary>
    public class DynamicObjectEx : DynamicObject
    {
        /// <summary>
        /// 属性名和属性值字典
        /// </summary>
        public Dictionary<string, object> Properties = new Dictionary<string, object>();
        /// <summary>
        /// 方法名和方法参数字典（仅用于记录参数便于回溯）
        /// </summary>
        public Dictionary<string, object[]> Parameters = new Dictionary<string, object[]>(); 
        /// <summary>
        /// 方法名和方法体字典
        /// </summary>
        public Dictionary<string, dynamic> Methods = new Dictionary<string, dynamic>();

        /// <summary>
        /// （当找不到函数名称时，进入此方法）
        /// 提供用于调用成员的操作的实现。 类派生自 System.Dynamic.DynamicObject 类可以重写此方法以指定动态行为的操作，如调用方法。
        /// </summary>
        /// <param name="binder"> 提供有关动态操作的信息。 binder.Name 属性提供对其执行动态操作的成员的名称。
        /// 例如，对于该语句 sampleObject.SampleMethod(100),
        /// 其中 sampleObject 是派生自的类的实例 System.Dynamic.DynamicObject 类， binder.Name 返回"SampleMethod"。
        /// binder.IgnoreCase 属性指定的成员名称是否区分大小写。</param>
        /// <param name="args">调用操作期间传递给对象成员的参数。 例如，对于该语句 sampleObject.SampleMethod(100), ，其中 sampleObject 派生自
        /// System.Dynamic.DynamicObject 类， args[0] 等于 100。</param>
        /// <param name="result">该成员的调用的结果</param>
        /// <returns></returns>
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (!Parameters.Keys.Contains(binder.Name))
                Parameters.Add(binder.Name, null);

            if (args != null)
                Parameters[binder.Name] = args;

            result = Methods.ContainsKey(binder.Name) ? (object)Methods[binder.Name](args) : null;

            return true;
        }
        /// <summary>
        /// 设置动态属性的值，因为方法和属性的设置方法可能一样，所以动态方法也可在此设置
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Methods.ContainsKey(binder.Name) && value is Delegate)
                Methods.Add(binder.Name, value);
            else if (!Properties.Keys.Contains(binder.Name))
                Properties.Add(binder.Name, value);

            return true;
        }
        /// <summary>
        /// 获取动态属性的值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result) => Properties.TryGetValue(binder.Name, out result);
    }
}
