using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace WLib.Samples.WinForm.View
{
    public partial class frmQueryFeat : Form
    {
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
          
        }

        private void UniqueValue_button_Click(object sender, EventArgs e)
        {
        }

        private void field_DoubleClick(object sender, EventArgs e)
        {
        }

        private void uniqueValue_DoubleClick(object sender, EventArgs e)
        {
            
        }

        
        private void clear_button_Click(object sender, EventArgs e)
        {
            sql_textBox.Text = "";
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
