using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.Data;

namespace WLib.UserCtrls.ArcGisControl
{
    /// <summary>
    /// 图层属性表窗口
    /// </summary>
    public partial class AttributeForm : Form
    {
        /// <summary>
        /// 要获取属性表的图层
        /// </summary>
        public IFeatureLayer Layer { get; set; }
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
        public void LoadAttribute(IFeatureLayer featureLayer, AxMapControl mapControl)
        {
            this.Layer = featureLayer;
            this.MapControl = mapControl;
            this.Text = Layer.Name + " - 属性表";

            try
            {
                IdFieldName = Layer.FeatureClass.OIDFieldName;//ID字段名称，可能为"OID"或"FID"
                this.dataGridView1.DataSource = DataConvert.CreateDataTable(featureLayer.FeatureClass);
            }
            catch (Exception ex) { MessageBox.Show("无法显示属性表！" + ex.Message); }
        }
        /// <summary>
        /// 移除图层，清空属性表
        /// </summary>
        public void Clear()
        {
            this.Text = "属性表";
            this.Layer = null;
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
            IFeature feature = Layer.FeatureClass.GetFeature(id);
            MapControl.Map.SelectFeature(Layer, feature);  //获取属性表中选中行对应的图形要素
            MapControl.Extent = feature.Shape.Envelope;

            MapControl.Refresh();
        }
    }
}
