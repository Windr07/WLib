using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Controls;

namespace GISsys
{
    public partial class frmQueryFeat : Form
    {
        private List<FeatureClassInfo> _fInfoList = new List<FeatureClassInfo>();
        private FeatureClassInfo _fInfo = null;

        public frmQueryFeat(FeatureClassInfo[] featClsesInfo)
        {
            InitializeComponent();
            foreach (var f in featClsesInfo)
            {
                this.featCls_comboBox.Items.Add(f.LayerName);
                _fInfoList.Add(f);
            }
            if (featClsesInfo.Length > 0)
            {
                _fInfo = _fInfoList[0];
                this.featCls_comboBox.SelectedIndex = 0;
                sql_textBox.Focus();
                //_featureClass = (IFeatureClass)this.featCls_comboBox.SelectedItem;
            }
        }


        #region 各类函数

        /// <summary>
        /// 返回通过属性查询要素的SQL语句
        /// </summary>
        /// <returns></returns>
        public string SQL()
        {
           return this.sql_textBox.Text.Trim();
        }
        /// <summary>
        /// 返回要执行查询的图层索引
        /// </summary>
        /// <returns></returns>
        public int LayerIndex()
        {
            return this.featCls_comboBox.SelectedIndex;
        }
        /// <summary>
        /// 选中查找所得的要素，放入选择集，遍历要素并闪烁显示
        /// </summary>
        /// <param name="sqlfilter">SQL语句</param>
        /// <param name="pFeatureLayer">查找的图层</param>
        private void searchSelection(string sqlfilter, IFeatureLayer pFeatureLayer)
        {
            
        }


        #endregion


        #region 符号按钮事件

        private void button1_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " = ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " <> ";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " > ";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " >= ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " < ";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " <= ";
        }


        private void button7_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " AND ";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " OR ";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " NOT ";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sql_textBox.Text += " LIKE ";
        }

        #endregion


        private void featCls_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.field_listBox.Items.Clear();
            this.uniqueValue_listBox.Items.Clear();
            int index = featCls_comboBox.SelectedIndex;
            _fInfo = _fInfoList[index];

            int flds_cnt = _fInfo.FeatureClass .Fields.FieldCount;

            for (int i = 0; i < flds_cnt; i++)
            {
                IField field = _fInfo.FeatureClass.Fields.get_Field(i);
                //if (field.Editable)
                //{
                //    _Valuefields.Add(new ValueField(field));
                //}
                field_listBox.Items.Add(field.Name);
            }

        }

        private void UniqueValue_button_Click(object sender, EventArgs e)
        {
            int index = this.featCls_comboBox.SelectedIndex;
            FeatureClassInfo featClsInfo = _fInfoList[index];
            ArrayList list = new ArrayList();
            if (this.field_listBox.SelectedIndex != -1)
            {
                UniqueValueClass uniqueVaueCls = new UniqueValueClass();
                list = uniqueVaueCls.GetLayerUniqueFieldValueByDataStatistics(
                    featClsInfo.FeatureClass, this.field_listBox.SelectedItem.ToString());
                this.uniqueValue_listBox.Items.Clear();
                foreach (var f in list)
                {
                    this.uniqueValue_listBox.Items.Add(f);
                }
            }
        }

        private void field_DoubleClick(object sender, EventArgs e)
        {
            sql_textBox.Text += field_listBox.SelectedItem.ToString();
        }

        private void uniqueValue_DoubleClick(object sender, EventArgs e)
        {
            IField field = _fInfo.FeatureClass.Fields.get_Field(field_listBox.SelectedIndex);
            if (field.Type == esriFieldType.esriFieldTypeString)
            {
                sql_textBox.Text += "'"+uniqueValue_listBox.SelectedItem.ToString()+"' ";
            }
            else
            {
                sql_textBox.Text += uniqueValue_listBox.SelectedItem.ToString();
            }
        }

        
        private void clear_button_Click(object sender, EventArgs e)
        {
            sql_textBox.Text = "";
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
            MainForm form = (MainForm)Application.OpenForms[0];
            int lyrindex = LayerIndex();//要查询的图层
            string sql = SQL();//查询语句
            //axMapControl1.Map.get_Layer(lyrindex) as IFeatureLayer;
            AxMapControl mapControl = form.getMapControl();
            IFeatureLayer featureLyr = mapControl.
                Map.get_Layer(LayerIndex()) as IFeatureLayer;

            #region  执行查询(searchSelection)
            //定义查询接口
            IQueryFilter pFilter = new QueryFilterClass();
            pFilter.WhereClause = sql;
            //要素选择接口
            IFeatureSelection pFeatureSelection = featureLyr as IFeatureSelection;
            try
            {
                //通过SQL语句查询并选择查询结果（SQL语句，选择结果集=新建集合，             仅有一个结果=false）;
                pFeatureSelection.SelectFeatures(pFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                //获取选择集
                ISelectionSet pFeatSet = pFeatureSelection.SelectionSet;

                //获取指针，遍历选择集
                ICursor pCursor;
                pFeatSet.Search(null, true, out pCursor);//out pCursor获取指针
                IFeatureCursor pFeatCursor = pCursor as IFeatureCursor;
                IFeature pFeat = pFeatCursor.NextFeature();
                while (pFeat != null)//遍历要素使其闪烁显示
                {
                    ISimpleFillSymbol pFillsyl2 = new SimpleFillSymbolClass();
                    pFillsyl2.Color = form.getRGB(220, 60, 60);
                    mapControl.FlashShape(pFeat.Shape, 3, 100, pFillsyl2);//闪烁显示要素
                    pFeat = pFeatCursor.NextFeature();
                    mapControl.ActiveView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败，" + ex.ToString());
            }
            #endregion
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
