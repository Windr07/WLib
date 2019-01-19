/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Data;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 图层属性表窗口
    /// </summary>
    public partial class AttributeForm : Form
    {
        /// <summary>
        /// 要获取属性表的图层
        /// </summary>
        public IFeatureLayer FeatLayer { get; set; }
        /// <summary>
        /// 图层所在的地图控件
        /// </summary>
        public AxMapControl MapControl { get; set; }
        /// <summary>
        /// 图层ID字段名称
        /// </summary>
        public string IdFieldName { get; private set; }

        /// <summary>
        /// 图层属性表窗口
        /// </summary>
        public AttributeForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 载入属性表
        /// </summary>
        /// <param name="featureLayer"></param>
        /// <param name="mapControl"></param>
        public void LoadAttribute(AxMapControl mapControl, IFeatureLayer featureLayer)
        {
            this.FeatLayer = featureLayer;
            this.MapControl = mapControl;
            this.Text = FeatLayer.Name + " - 属性表";
            try
            {
                IdFieldName = FeatLayer.FeatureClass.OIDFieldName;//ID字段名称，可能为"OID"或"FID"
                this.dataGridView1.DataSource = featureLayer.FeatureClass.CreateDataTable();
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            this.Text = "属性表";
            this.FeatLayer = null;
            this.dataGridView1.DataSource = null;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedRows = this.dataGridView1.SelectedRows;
            if (selectedRows.Count <= 0) return;
            object value = selectedRows[0].Cells[IdFieldName].Value;
            if (value == null || value == DBNull.Value) return;

            MapControl.Map.ClearSelection();
            int id = Convert.ToInt32(value.ToString());
            IFeature feature = FeatLayer.FeatureClass.GetFeature(id);
            MapControl.Map.SelectFeature(FeatLayer, feature);  //获取属性表中选中行对应的图形要素
            MapControl.Extent = feature.Shape.Envelope;

            MapControl.Refresh();
        }
    }
}
