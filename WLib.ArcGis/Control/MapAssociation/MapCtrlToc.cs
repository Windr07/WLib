/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Carto.LabelAnno;
using WLib.ArcGis.Carto.Layer;
using WLib.ArcGis.Control.AttributeCtrl;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件与TOC控件的关联操作
    /// </summary>
    public class MapCtrlToc : IMapCtrlAssociation
    {
        private Form _attributeForm;
        /// <summary>
        /// 属性表控件的类型
        /// </summary>
        private readonly Type _attributeFormType;
        /// <summary>
        /// 在TOC控件选择的图层
        /// </summary>
        public ILayer SelectedLayer;
        /// <summary>
        /// 在TOC控件选择的图层的索引
        /// </summary>
        public int LayerIndex;
        /// <summary>
        /// 属性表窗口
        /// </summary>
        public IAttributeForm AttributeForm;
        /// <summary>
        /// TOC控件
        /// </summary>
        public AxTOCControl TocControl { get; }
        /// <summary>
        /// 地图控件
        /// </summary>
        public AxMapControl MapControl { get; }
        /// <summary>
        /// 将当前标签页设为地图页面
        /// </summary>
        public readonly Action<EViewActionType[]> SwitchView;
        /// <summary>
        /// 图层和对应的字段菜单列表
        /// 
        /// </summary>
        public readonly Dictionary<string, ToolStripMenuItem[]> Layer2FieldsMenuItems;


        /// <summary>
        /// 地图控件与TOC控件的关联操作
        /// </summary>
        /// <param name="tocCtrl">TOC控件</param>
        /// <param name="mapCtrl">地图控件</param>
        /// <param name="attributeForm">显示属性表的窗体</param>
        /// <param name="switchView">将当前标签页设为地图页面</param>
        public MapCtrlToc(AxTOCControl tocCtrl, AxMapControl mapCtrl, IAttributeForm attributeForm, Action<EViewActionType[]> switchView = null)
        {
            MapControl = mapCtrl;
            TocControl = tocCtrl;
            TocControl.SetBuddyControl(MapControl);
            TocControl.OnMouseDown += tocCtrl_OnMouseDown;
            SwitchView = switchView;
            AttributeForm = attributeForm;
            _attributeFormType = AttributeForm.GetType();

            Layer2FieldsMenuItems = new Dictionary<string, ToolStripMenuItem[]>();
            InintMenuStrip();
        }


        /// <summary>
        /// 展开/收缩图层的图例
        /// </summary>
        /// <param name="isExpand"></param>
        public void ExpandLegend(bool isExpand)
        {
            IEnumLayer layers = MapControl.Map.Layers[LayerUid.IFeatureLayer.CreateUid(), true];
            layers.Reset();
            ILayer layer;
            while ((layer = layers.Next()) != null)
            {
                ILegendInfo legendInfo = (ILegendInfo)layer;
                for (int i = 0; i < legendInfo.LegendGroupCount; i++)
                {
                    legendInfo.LegendGroup[i].Visible = isExpand;
                }
            }
            TocControl.Update();
        }
        /// <summary>
        /// 显示指定图层指定字段的标注
        /// </summary>
        /// <param name="geoLayer">显示标注的图层</param>
        /// <param name="fieldName">标注的字段名或字段别名</param>
        /// <param name="fontName">标注字体</param>
        /// <param name="size">标注大小</param>
        public void ShowLabels(IGeoFeatureLayer geoLayer, string fieldName, string fontName = "宋体", int size = 12)
        {
            ShowFieldLabelMenuItems(geoLayer, fontName, size);
            if (Layer2FieldsMenuItems.ContainsKey(geoLayer.Name))
            {
                var menuItems = Layer2FieldsMenuItems[geoLayer.Name];
                var menuItem = menuItems.FirstOrDefault(v => v.Tag.ToString().Equals(fieldName) || v.Text.Equals(fieldName));
                menuItem?.PerformClick();
            }
        }
        /// <summary>
        /// 显示指定图层指定字段的标注
        /// </summary>
        /// <param name="layerName">显示标注的图层的图层名</param>
        /// <param name="fieldName">标注的字段名或字段别名</param>
        /// <param name="fontName">标注字体</param>
        /// <param name="size">标注大小</param>
        public void ShowLabels(string layerName, string fieldName, string fontName = "宋体", int size = 12)
        {
            var lyr = this.MapControl.GetLayer(layerName);
            if (lyr != null)
            {
                var geoLayer = lyr as IGeoFeatureLayer;
                ShowLabels(geoLayer, fieldName, fontName, size);
            }
        }
        /// <summary>
        /// 将图层的字段名作为菜单显示，用于设置标注
        /// </summary>
        /// <param name="setFieldLabelLayer">显示标注的图层</param>
        /// <param name="fontName">标注字体</param>
        /// <param name="size">标注大小</param>
        private void ShowFieldLabelMenuItems(ILayer setFieldLabelLayer, string fontName = "宋体", int size = 12)
        {
            if (!(setFieldLabelLayer is ITable table))
                return;

            if (!Layer2FieldsMenuItems.ContainsKey(setFieldLabelLayer.Name))
            {
                var namesDict = table.GetFieldNameAndAliasName();
                if (namesDict.ContainsKey("SHAPE"))
                    namesDict.Remove("SHAPE");
                var menuItems = new List<ToolStripMenuItem>();
                foreach (var namePair in namesDict)
                {
                    var item = new ToolStripMenuItem(namePair.Value) { Tag = namePair.Key };
                    item.Click += (sender2, e2) =>
                    {
                        SwitchView?.Invoke(new[] { EViewActionType.MainMap });
                        item.Checked = !item.Checked;
                        if (setFieldLabelLayer is IGeoFeatureLayer geoFeatureLayer)
                        {
                            if (item.Checked)
                            {
                                geoFeatureLayer.ShowLabel(namePair.Key.ToString(), fontName, size);
                                var subItems = ((ToolStripMenuItem)item.OwnerItem).DropDownItems.OfType<ToolStripMenuItem>();
                                foreach (var subItem in subItems)
                                {
                                    subItem.Checked = false;
                                }
                                item.Checked = true;
                            }
                            else
                                geoFeatureLayer.DisplayAnnotation = false;
                            MapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        }
                    };
                    menuItems.Add(item);
                }
                Layer2FieldsMenuItems.Add(setFieldLabelLayer.Name, menuItems.ToArray());
            }
            //绑定图层的字段列表
            ShowLabelMenuItem.DropDownItems.Clear();
            ShowLabelMenuItem.DropDownItems.AddRange(Layer2FieldsMenuItems[setFieldLabelLayer.Name]);
        }


        /// <summary>
        /// TOCC左键设置图例，右键弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tocCtrl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            var item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            object unk = null, data = null;
            TocControl.HitTest(e.x, e.y, ref item, ref basicMap, ref SelectedLayer, ref unk, ref data);
            if (e.button == 2)
            {
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    ToccMenuStrip.Show(TocControl, new System.Drawing.Point(e.x, e.y));
                    LayerIndex = MapControl.GetLayerIndex(SelectedLayer);
                    ShowFieldLabelMenuItems(SelectedLayer);
                }
            }
        }
        /// <summary>
        /// 定位查询获得的第一个图斑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _attributeForm_FeatureLocation(object sender, FeatureLocationEventArgs e)
        {
            SwitchView?.Invoke(new[] { EViewActionType.MainMap });
            if (e.LayerIndex < MapControl.LayerCount && e.LayerIndex > -1)
            {
                var layer = MapControl.get_Layer(e.LayerIndex);
                if (layer is IFeatureLayer featureLayer && featureLayer.Name == e.LayerName)
                    MapControl.MapZoomTo(featureLayer, e.WhereClause);
            }
        }


        #region TOC控件的右键菜单
        /// <summary>
        /// TOC控件的右键菜单
        /// </summary>
        public ContextMenuStrip ToccMenuStrip;
        /// <summary>
        /// 缩放到图层菜单项
        /// </summary>
        public ToolStripMenuItem ZoomToLayerMenuItem;
        /// <summary>
        /// 打开属性表菜单项
        /// </summary>
        public ToolStripMenuItem OpenAttributeMenuItem;
        /// <summary>
        /// 显示标注菜单项
        /// </summary>
        public ToolStripMenuItem ShowLabelMenuItem;
        /// <summary>
        /// 定义查询菜单项
        /// </summary>
        public ToolStripMenuItem DefinitionMenuItem;
        /// <summary>
        /// 移除定义查询菜单项
        /// </summary>
        public ToolStripMenuItem RemoveDefinitionMenuItem;
        /// <summary>
        /// 移除图层菜单项
        /// </summary>
        public ToolStripMenuItem RemoveLayerMenuItem;
        /// <summary>
        /// 初始化TOC控件的右键菜单
        /// </summary>
        protected virtual void InintMenuStrip()
        {
            ZoomToLayerMenuItem = new ToolStripMenuItem("缩放到图层(&Q)", null, 缩放到图层toolStripMenuItem_Click);
            OpenAttributeMenuItem = new ToolStripMenuItem("打开属性表(&T)", null, 打开属性表ToolStripMenuItem_Click);
            ShowLabelMenuItem = new ToolStripMenuItem("显示标注(&A)");
            DefinitionMenuItem = new ToolStripMenuItem("定义查询(&I)", null, 定义查询IToolStripMenuItem_Click);
            RemoveDefinitionMenuItem = new ToolStripMenuItem("移除定义查询(&C)", null, 移除定义查询CToolStripMenuItem_Click);
            RemoveLayerMenuItem = new ToolStripMenuItem("移除(&D)", null, 移除DToolStripMenuItem_Click);

            ToccMenuStrip = new ContextMenuStrip();
            ToccMenuStrip.Items.AddRange(new ToolStripItem[] {
                ZoomToLayerMenuItem,
                OpenAttributeMenuItem,
                ShowLabelMenuItem,
                DefinitionMenuItem,
                RemoveDefinitionMenuItem,
                RemoveLayerMenuItem });
        }
        protected virtual void 缩放到图层toolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchView?.Invoke(new[] { EViewActionType.MainMap });
            if (SelectedLayer != null)
            {
                var envelope = SelectedLayer.AreaOfInterest;
                MapControl.Extent = envelope;
            }
        }
        protected virtual void 打开属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayer is IFeatureLayer featureLayer && featureLayer.FeatureClass != null)
            {
                if (AttributeForm == null || AttributeForm.IsDisposed)
                    AttributeForm = (IAttributeForm)Activator.CreateInstance(_attributeFormType);
                AttributeForm.Show(MapControl);
                AttributeForm.Activate();//之前已打开，则给予焦点，置顶。
                AttributeForm.AttributeCtrl.LoadAttribute(featureLayer as ITable, ((IFeatureLayerDefinition)featureLayer).DefinitionExpression, LayerIndex, _attributeForm_FeatureLocation);
            }
        }
        protected virtual void 定义查询IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(SelectedLayer is IFeatureLayer featureLayer)) return;

            var queryCtrl = AttributeForm.AttributeCtrl.AtrributeQueryCtrl;
            if (queryCtrl == null || queryCtrl.IsDisposed)
                queryCtrl = (IAttributeQueryCtrl)Activator.CreateInstance(_attributeFormType);
            queryCtrl.Query -= _queryForm_Query;
            queryCtrl.Query += _queryForm_Query;
            queryCtrl.LoadQueryInfo(featureLayer.FeatureClass as ITable);
            queryCtrl.Show(MapControl);
        }
        protected virtual void 移除定义查询CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedLayer is IFeatureLayer featureLayer)
            {
                if (featureLayer is IFeatureLayerDefinition featureLyrDef)
                {
                    featureLyrDef.DefinitionExpression = null;
                    RemoveDefinitionMenuItem.Visible = false;
                }
            }
        }
        protected virtual void 移除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapControl.Map.DeleteLayer(SelectedLayer);
        }
        protected virtual void _queryForm_Query(object sender, EventArgs e)
        {
            if (SelectedLayer is IFeatureLayer featureLayer && featureLayer is IFeatureLayerDefinition featureLyrDef)
            {
                var queryCtrl = AttributeForm.AttributeCtrl.AtrributeQueryCtrl;
                featureLyrDef.DefinitionExpression = queryCtrl.WhereClause;
                RemoveDefinitionMenuItem.Visible = true;
            }
        }
        #endregion
    }
}
