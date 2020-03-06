using System;
using System.Collections.Generic;
using System.ComponentModel;
using WLib.ExtProgress;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 数据检查基类
    /// </summary>
    [Serializable]
    public abstract class Checker : ProLogOperation<Checker, List<ErrorRecord>>, ICheckItem
    {
        /// <summary>
        /// 检查名称
        /// </summary>
        [Category("基本"), DisplayName("检查名称"), Description("检查名称"), ReadOnly(true)]
        public new string Name { get; set; }
        /// <summary>
        /// 规则说明
        /// </summary>
        [Category("基本"), DisplayName("规则说明"), Description("规则说明"), ReadOnly(true), Browsable(false)]
        public new string Description { get; set; }
        /// <summary>
        /// 数据检查项
        /// </summary>
        [Browsable(false)]
        public ICheckGroup CheckGroup { get; set; }


        /// <summary>
        /// 数据检查基类
        /// </summary>
        /// <param name="name">检查名称</param>
        protected Checker(string name)
        {
            base.Name = Name = name;
            InputData = this;
            ResultData = new List<ErrorRecord>();
        }
        /// <summary>
        /// 数据检查基类
        /// </summary>
        /// <param name="checkType">检查类型</param>
        /// <param name="ruleName">检查项名称</param>
        protected Checker(string name, string ruleName) : this(name) => Name = ruleName;
        /// <summary>
        /// 数据检查基类
        /// </summary>
        /// <param name="checkType">检查类型</param>
        /// <param name="ruleName">检查项名称</param>
        /// <param name="description">规则说明</param>
        protected Checker(string name, string ruleName, string description) : this(name, ruleName) => Description = description;
    }
}
