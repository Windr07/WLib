/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace WLib.Database.TableInfo.FieldDomain
{
    /// <summary>
    /// 通过大于小于等于的方式限制的数值类型字段的取值范围
    /// </summary>
    public class FieldRangeDomain : IFieldDomain
    {
        /// <summary>
        /// 字段值域类型（无限制、范围、字典表）
        /// </summary>
        public EFieldDomainType FieldDomianType { get; }

        /// <summary>
        /// 取值范围限制与限制值（eg:key为大于,value为0，则表示大于0）
        /// </summary>
        public Dictionary<EFieldValueRanage, double> Range;

        /// <summary>
        /// 数值类型字段的取值范围
        /// </summary>
        public FieldRangeDomain()
        {
            FieldDomianType = EFieldDomainType.Range;
            Range = new Dictionary<EFieldValueRanage, double>();
        }
        /// <summary>
        /// 添加取值范围限制
        /// </summary>
        /// <param name="range"></param>
        /// <param name="value"></param>
        public void Add(EFieldValueRanage range, double value)
        {
            Range.Add(range, value);
        }

        /// <summary>
        /// 判断输入值是否符合值域要求
        /// </summary>
        /// <param name="objValue">输入值</param>
        /// <param name="message">判断结果信息</param>
        /// <returns></returns>
        public bool CheckValue(object objValue, out string message)
        {
            double value = Convert.ToDouble(objValue);
            bool result = true;
            foreach (var pair in Range)
            {
                switch (pair.Key)
                {
                    case EFieldValueRanage.大于:
                        result = value > pair.Value;
                        break;
                    case EFieldValueRanage.大于等于:
                        result = value >= pair.Value;
                        break;
                    case EFieldValueRanage.小于:
                        result = value < pair.Value;
                        break;
                    case EFieldValueRanage.小于等于:
                        result = value <= pair.Value;
                        break;
                }
                if (!result)
                    break;
            }
            message = result ? "" : ErrorString();
            return result;
        }

        /// <summary>
        /// 判断结果信息
        /// </summary>
        /// <returns></returns>
        public string ErrorString()
        {
            List<string> result = new List<string>();
            foreach (var pair in Range)
            {
                switch (pair.Key)
                {
                    case EFieldValueRanage.大于:
                        result.Add("大于" + pair.Value);
                        break;
                    case EFieldValueRanage.大于等于:
                        result.Add("大于等于" + pair.Value);
                        break;
                    case EFieldValueRanage.小于:
                        result.Add("小于" + pair.Value);
                        break;
                    case EFieldValueRanage.小于等于:
                        result.Add("小于等于" + pair.Value);
                        break;
                }
            }
            if (result.Count > 0)
                return "输入的数值应该" + string.Join(",", result.ToArray());
            else
                return string.Empty;
        }
    }
}
