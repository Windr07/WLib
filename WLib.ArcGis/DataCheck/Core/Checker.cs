/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2020
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.ArcGis.GeoDatabase.Table;
using WLib.ExtProgress;

namespace WLib.ArcGis.DataCheck.Core
{
    /// <summary>
    /// 表示一项数据检查操作
    /// </summary>
    [Serializable]
    public abstract class Checker : ProLogOperation<Checker, List<CheckRecord>>, IChecker
    {
        /// <summary>
        /// 要素类路径和对应要素类的键值对
        /// </summary>
        private Dictionary<string, IFeatureClass> _pathFeatureClasses { get; set; } = new Dictionary<string, IFeatureClass>();
        /// <summary>
        /// 表格路径和对应表格的键值对
        /// </summary>
        private Dictionary<string, ITable> _pathTables { get; set; } = new Dictionary<string, ITable>();

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
            if (!name.EndsWith("检查"))
                name += "检查";
            base.Name = Name = name;
            InputData = this;
            ResultData = new List<CheckRecord>();
        }
        /// <summary>
        /// 数据检查基类
        /// </summary>
        /// <param name="name">检查名称</param>
        /// <param name="description">规则说明</param>
        protected Checker(string name, string description) : this(name) => Description = description;


        /// <summary>
        /// 从指定路径中获取要素类，返回值代表是否获取成功
        /// </summary>
        /// <param name="layerPath">要素类路径</param>
        /// <param name="featureClass">获取的要素类</param>
        /// <returns></returns>
        protected bool GetFeatureClass(string layerPath, out IFeatureClass featureClass)
        {
            if (_pathFeatureClasses.ContainsKey(layerPath))
                featureClass = _pathFeatureClasses[layerPath];
            else
            {
                try
                {
                    featureClass = FeatureClassEx.FromPath(layerPath);
                }
                catch { featureClass = null; }
                if (featureClass == null)
                {
                    OnDataOutput(new CheckRecord("找不到图层", layerPath, EErrorLevel.异常));
                    return false;
                }
                _pathFeatureClasses.Add(layerPath, featureClass);
            }
            return true;
        }

        /// <summary>
        /// 从指定路径中获取要素类，返回值代表是否获取成功
        /// </summary>
        /// <param name="tablePath">要素类路径</param>
        /// <param name="featureClass">获取的要素类</param>
        /// <returns></returns>
        protected bool GetTable(string tablePath, out ITable table)
        {
            if (_pathTables.ContainsKey(tablePath))
                table = _pathTables[tablePath];
            else
            {
                try
                {
                    table = TableEx.FromPath(tablePath);
                }
                catch { table = null; }
                if (table == null)
                {
                    OnDataOutput(new CheckRecord("找不表格", tablePath, EErrorLevel.异常));
                    return false;
                }
                _pathTables.Add(tablePath, table);
            }
            return true;
        }
    }
}
