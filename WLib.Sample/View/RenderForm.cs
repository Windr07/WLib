using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace GISsys
{
    public partial class frmRender : Form
    {
        private List<FeatureClassInfo> _infoList = new List<FeatureClassInfo>();
        private FeatureClassInfo _info = null;
        private Color _color1 = new Color();
        private Color _color2 = new Color();
        private byte _renderStyle = 0;

        public frmRender(FeatureClassInfo[] infoList,byte style)
        {
            InitializeComponent();
            foreach (var f in infoList)
            {
                this.featCls_comboBox.Items.Add(f.LayerName);
                this.featCls2_comboBox.Items.Add(f.LayerName);
                _infoList.Add(f);
            }
            if (infoList.Length > 0)
            {
                this.featCls_comboBox.SelectedIndex = 0;
                this.featCls2_comboBox.SelectedIndex = 0;
                _info = _infoList[0];
            }
            switch (style)
            {
                case 1:
                    this.Text = "简单渲染(Simple Render)";
                    break ;
                case 2:
                    this.Text = "分级渲染(Class Break Render)";
                    this.classUpDown.Visible = true;
                    this.class_label.Visible = true;
                    this.fillColor_button.Visible = false;
                    this.fillColor_label.Visible = false;
                    break;
                case 3:
                    this.Text = "图表渲染(Chart Render)";
                    this.featCls2_comboBox.Enabled = true;
                    this.field2_comboBox.Enabled = true;
                    this.lineColor_label.Text = "填充颜色";
                    break;
                case 4:
                    this.Text = "唯一值渲染(Unique Value Render)";
                    this.lineColor_button.Visible = false;
                    this.lineColor_label.Visible = false;
                    this.fillColor_button.Visible = false;
                    this.fillColor_label.Visible = false;
                    break;
                default :
                    break;
            }
            _renderStyle = style;
        }

        /// <summary>
        /// 获得专题图中边线的颜色设置
        /// </summary>
        /// <returns></returns>
        public Color getColor1()
        {
            return _color1;
        }
        /// <summary>
        /// 获得专题图中填充颜色设置
        /// </summary>
        /// <returns></returns>
        public Color getColor2()
        {
            return _color2;
        }
        /// <summary>
        /// 获得ClassBreakRender的分级数
        /// </summary>
        /// <returns></returns>
        public byte getClassCount()
        {
            return (byte)this.classUpDown.Value;
        }
        /// <summary>
        /// 获得进行专题着色的图层索引、字段
        /// </summary>
        /// <param name="lyr2"></param>
        /// <returns></returns>
        public string getLyrField(out int lyr)
        {
            int i = this.featCls_comboBox.SelectedIndex;
            lyr = _infoList[i].LayerIndex;
            return this.field_comboBox.SelectedItem.ToString();
        }            
        /// <summary>
        /// 获得进行绘制ChartRender图的第二个图层索引、数字字段
        /// </summary>
        /// <param name="lyr2"></param>
        /// <returns></returns>
        public string getLyr2Field2(out int lyr2)
        {
            int i = this.featCls2_comboBox.SelectedIndex;
            lyr2 = _infoList[i].LayerIndex;
            return this.field2_comboBox.SelectedItem.ToString();
        }
        /// <summary>
        /// 获取唯一值的个数
        /// </summary>
        /// <returns></returns>
        public int getUniqueValueCnt()
        {
            IFeatureClass featCls = 
                _infoList[this.featCls_comboBox.SelectedIndex].FeatureClass;
            string fldName = this.field_comboBox.SelectedItem.ToString();
            UniqueValueClass unique = new UniqueValueClass(featCls, fldName);
            return unique.Count;
        }
        /// <summary>
        /// 返回字段是否为数值型（双精度、整型、浮点、短整型）
        /// </summary>
        /// <param name="filed"></param>
        /// <returns></returns>
        public Boolean RightFieldType(IField filed)
        {
            if (filed.Type == esriFieldType.esriFieldTypeDouble ||
                    filed.Type == esriFieldType.esriFieldTypeInteger ||
                    filed.Type == esriFieldType.esriFieldTypeSingle ||
                    filed.Type == esriFieldType.esriFieldTypeSmallInteger)
            {
                return true;
            }
            else
                return false;


        }


        private void featCls_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.field_comboBox.Items.Clear();
            int index = featCls_comboBox.SelectedIndex;
            _info = _infoList[index];

            int flds_cnt = _info.FeatureClass.Fields.FieldCount;
            for (int i = 0; i < flds_cnt; i++)
            {
                IField field = _info.FeatureClass.Fields.get_Field(i);
                this.field_comboBox.Items.Add(field.Name);
                this.field_comboBox.SelectedIndex = 0;
            }
        }

        private void featCls2_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.field2_comboBox.Items.Clear();
            int index = featCls2_comboBox.SelectedIndex;
            _info = _infoList[index];

            int flds_cnt = _info.FeatureClass.Fields.FieldCount;
            for (int i = 0; i < flds_cnt; i++)
            {
                IField field = _info.FeatureClass.Fields.get_Field(i);
                this.field2_comboBox.Items.Add(field.Name);
                this.field2_comboBox.SelectedIndex = 0;
            }
        }


        private void LineColor_button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _color1 = colorDialog.Color;
                this.lineColor_button.BackColor = _color1;
            }          
        }

        private void FillColor_button_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _color2 = colorDialog.Color;
                this.fillColor_button.BackColor = _color2;
            }
        }
    

        private void OK_button_Click(object sender, EventArgs e)
        {
            //分级渲染或图表渲染时，
            //判断选择的字段是否为数值字段，不符合则不能进行渲染
            if (_renderStyle == 2 || _renderStyle == 3)
            {
                IField field1 = _infoList[this.featCls_comboBox.SelectedIndex].
                    FeatureClass.Fields.Field[this.field_comboBox.SelectedIndex];
                if (!RightFieldType(field1))
                {
                    MessageBox.Show("所选字段不是数值型，不能进行分级渲染或图表渲染",
                        "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (_renderStyle == 3)
                {
                    IField field2 = _infoList[this.featCls2_comboBox.SelectedIndex].
                    FeatureClass.Fields.Field[this.field2_comboBox.SelectedIndex];
                    if (!RightFieldType(field2))
                    {
                        MessageBox.Show("所选字段2不是数值型，不能进行图表渲染",
                            "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }  
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
