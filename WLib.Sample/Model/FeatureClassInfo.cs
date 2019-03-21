using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace GISsys
{
    /// <summary>
    /// 表示要素类及其所在图层的图层名、图层索引
    /// </summary>
    public class FeatureClassInfo
    {
        private int _lyrIndex;
        private string _lyrName;
        private IFeatureClass _featCls;

        public Int32 LayerIndex { get { return _lyrIndex; } }
        public String LayerName { get { return _lyrName; } }
        public IFeatureClass FeatureClass { get { return _featCls; } }

        public FeatureClassInfo(int lyrIndex,string lyrName,IFeatureClass featCls)
        {
            this._lyrIndex = lyrIndex;
            this._lyrName = lyrName;
            this._featCls = featCls;
        }
    }
}
