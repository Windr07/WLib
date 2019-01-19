using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace GISsys
{
    /// <summary>
    /// 表示要素和要素所在的图层（要素类）
    /// </summary>
    public class FeatureAttributeClass
    {
        private IFeature _feature;
        private int _layerIndex;
        private string _layerName;
        public IFeature Feature { get { return this._feature; } set { this._feature = value; } }
        public Int32 LayerIndex { get { return this._layerIndex; } set { this._layerIndex = value; } }
        public string LayerName { get { return this._layerName; } set { this._layerName = value; } }


        public FeatureAttributeClass(int layerIndex)
        {
            this._layerIndex = layerIndex;
        }

        public FeatureAttributeClass(int layerIndex,string layerName, IFeature feature)
        {
            this._feature = feature;
            this._layerIndex = layerIndex;
            this._layerName = layerName;
        }

        /// <summary>
        /// 获得要素的记录，返回以字符串“字段名(字段值)”的形式组合形成的字符串链表
        /// </summary>
        /// <returns></returns>
        public List<string> getAttribute()
        {
            List<string> attributes =new List<string> ();
            for (int i = 0; i < _feature.Fields.FieldCount; i++)
            {
                attributes.Add( _feature.Fields.get_Field(i).AliasName +
                    "(" + _feature.get_Value(i).ToString() + ")");
            }
            return attributes;
        }

    }
}
