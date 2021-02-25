/*---------------------------------------------------------------- 
// auth： Windragon
//        Windr07
// date： 2020/12
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

namespace WLib.Database.TableInfo
{
    /// <summary>
    /// 表示调用CheckValueOnType方法检查字段值不符合要求时返回的提示信息
    /// </summary>
    public class DefaultInpuTips
    {
        #region  类型不符合提示
        /// <summary>
        /// 默认值为：请输入整型数值
        /// </summary>
        public string IntMsg = "请输入整型数值";
        /// <summary>
        /// 默认值为：请输入非负整型数值
        /// </summary>
        public string UintMsg = "请输入非负整型数值";
        /// <summary>
        /// 默认值为：请输入短整型数值
        /// </summary>
        public string ShortMsg = "请输入短整型数值";
        /// <summary>
        /// 默认值为：请输入非负短整型数值
        /// </summary>
        public string UshortMsg = "请输入非负短整型数值";
        /// <summary>
        /// 默认值为：请输入小于255的非负整型数值
        /// </summary>
        public string ByteMsg = "请输入小于255的非负整型数值";
        /// <summary>
        /// 默认值为：请输入小于127的整型数值
        /// </summary>
        public string SbyteMsg = "请输入小于127的整型数值";
        /// <summary>
        /// 默认值为：请输入整型数值
        /// </summary>
        public string LongMsg = "请输入整型数值";
        /// <summary>
        /// 默认值为：请输入非负整型数值
        /// </summary>
        public string UlongMsg = "请输入非负整型数值";
        /// <summary>
        /// 默认值为：请输入数值
        /// </summary>
        public string FloatMsg = "请输入数值";
        /// <summary>
        /// 默认值为：请输入数值
        /// </summary>
        public string DoubleMsg = "请输入数值";
        /// <summary>
        /// 默认值为：请输入True或False
        /// </summary>
        public string BoolMsg = "请输入True或False";
        /// <summary>
        /// 默认值为：请输入数值
        /// </summary>
        public string DecimalMsg = "请输入数值";
        /// <summary>
        /// 默认值为：请输入单个字符
        /// </summary>
        public string CharMsg = "请输入单个字符";
        /// <summary>
        /// 默认值为：请输入符合格式的正确日期
        /// </summary>
        public string DateTimeMsg = "请输入符合格式的正确日期";
        #endregion


        /// <summary>
        ///  默认值为：此项不能为空
        /// </summary>
        public string NotNullMsg = "此项不能为空";
        /// <summary>
        ///  默认值为：超出字段长度要求
        /// </summary>
        public string OutOfLength = "超出字段长度要求";
        /// <summary>
        ///  默认值为：超出要求的值域范围
        /// </summary>
        public string OutOfValueRange = "超出要求的值域范围";
        /// <summary>
        ///  默认值为：小数位数超出范围
        /// </summary>
        public string OutOfDigits = "小数位数超出范围";
    }
}
