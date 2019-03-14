using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using WLib.ArcGis.Control;
using WLib.ArcGis.Control.MapAssociation;

namespace WLib.UserCtrls.ArcGisCtrl.Viewer
{
    /// <summary>
    /// 地图控件与TOC控件的关联操作
    /// </summary>
    public class MapCtrlTocEx : MapCtrlToc
    {
        /// <summary>
        /// 属性表窗口
        /// </summary>
        private AttributeForm _attributeForm;
        /// <summary>
        /// 按属性查询窗口
        /// </summary>
        private AttributeQueryForm _queryForm;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tocCtrl"></param>
        /// <param name="mapCtrl"></param>
        /// <param name="goToMapView"></param>
        public MapCtrlTocEx(AxTOCControl tocCtrl, AxMapControl mapCtrl, Action goToMapView = null) :
            base(tocCtrl, mapCtrl, goToMapView)
        {
        }

        /// <summary>
        /// 定位查询获得的第一个图斑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _attributeForm_FeatureLocation(object sender, FeatureLocationEventArgs e)
        {
            GoToMapView?.Invoke();
            MapCtrl.MapZoomToAndSelectFirst(e.LocationLayer, e.WhereClause);
        }
        protected override void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayer is ITable table)
            {
                if (_attributeForm == null || _attributeForm.IsDisposed)
                    _attributeForm = new AttributeForm();
                else
                    _attributeForm.Activate();//之前已打开，则给予焦点，置顶。

                _attributeForm.FeatureLocation -= _attributeForm_FeatureLocation;
                _attributeForm.FeatureLocation += _attributeForm_FeatureLocation;
                if (!_attributeForm.Visible)
                    _attributeForm.Show(MapCtrl);

                _attributeForm.LoadAttribute((IFeatureLayer)SelectedLayer, ((IFeatureLayerDefinition)SelectedLayer).DefinitionExpression);
            }
        }
        protected override void 定义查询IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayer is IFeatureLayer featureLayer)
            {
                if (_queryForm == null || _queryForm.IsDisposed)
                {
                    _queryForm = new AttributeQueryForm(featureLayer.FeatureClass as ITable, false, null, (sender2, e2) =>
                    {
                        if (featureLayer is IFeatureLayerDefinition featureLyrDef)
                        {
                            featureLyrDef.DefinitionExpression = _queryForm.WhereClause;
                            RemoveDefinitionMenuItem.Visible = true;
                        }
                    });
                }
                _queryForm.Show(MapCtrl);
            }
        }
    }
}
