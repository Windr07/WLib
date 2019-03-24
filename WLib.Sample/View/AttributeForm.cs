using System;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.Samples.WinForm.View
{
    public partial class frmAttribute : Form
    {
        private ILayer layer;

        public frmAttribute(ILayer lyr)
        {
            InitializeComponent();
            layer = lyr;
            this.Text = "\"" + layer.Name + "\" 属性表";  
        }

        private void frmAttribute_Load(object sender, EventArgs e)
        {
            try
            {
                
                ITable lyrtable = (ITable)layer;//将图层转为表
                DataTable table = new DataTable();//创建数据表
                IField field;
                //将图层字段作为数据表的列
                for (int i = 0; i < lyrtable.Fields.FieldCount; i++)
                {
                    field = lyrtable.Fields.get_Field(i);
                    table.Columns.Add(field.Name);
                }
                
                object[] values = new object[lyrtable.Fields.FieldCount];
                IQueryFilter queryFilter = new QueryFilterClass();
                ICursor cursor = lyrtable.Search(queryFilter, true);
                IRow row;
                //读取每一行，获取属性值
                 while ((row = cursor.NextRow()) != null)
                {
                    for (int j = 0; j < lyrtable.Fields.FieldCount; j++)
                    {
                        object ob = row.get_Value(j);
                        values[j] = ob;
                    }
                    table.Rows.Add(values);
                }
                this.dataGridView1.DataSource = table;
            }
            catch
            {
                MessageBox.Show("无法显示属性表！");
                this.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //DataGridViewSelectedRowCollection SelRows = this.dataGridView1.SelectedRows;
            //DataGridViewRow row;
            //MainForm form = (MainForm)Application.OpenForms[0];
            //IMap m = form.getMapControl().Map;
            //m.ClearSelection();
            //for (int i = 0; i < SelRows.Count; i++)
            //{
            //    row = SelRows[i];
            //    int ID = Convert.ToInt32(row.Cells["FID"].Value.ToString());
            //    IFeatureLayer flyr = (IFeatureLayer)layer;
            //    IFeatureClass featurecls = flyr.FeatureClass;
            //    IFeature feature = featurecls.GetFeature(ID);
            //    m.SelectFeature(layer, feature);  //获取属性表中选中行对应的图形要素
            //}
            //form.getMapControl().Refresh();
        }
    }
}
