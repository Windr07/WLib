using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace WLib.Samples.WinForm.View
{
    public partial class frmAddmdb : Form
    {
        //private IEnumDataset _enumDataset;
        private List<IFeatureLayer> _featureLayers = null;

        public frmAddmdb()
        {
            InitializeComponent();
        }

        public List<IFeatureLayer> GetFeatureLayers() 
        {
            return this._featureLayers;
        }

      
        private void OpenMDB_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "*.mdb|*.mdb";
            op.Title = "打开Access数据库";
            if (op.ShowDialog() == DialogResult.OK)
            {


            }
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
