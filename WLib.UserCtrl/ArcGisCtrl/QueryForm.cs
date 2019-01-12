/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.Fields;

namespace WLib.UserCtrls.ArcGisCtrl
{
    /// <summary>
    /// 属性查询窗口
    /// </summary>
    public partial class QueryForm : Form
    {
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler Query;
        protected AttributeQueryHelper QueryHelper { get; }
        /// <summary>
        /// 构造的查询语句
        /// </summary>
        public string WhereClause { get => txtWhereClause.Text.Trim(); set => txtWhereClause.Text = value; }
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTable">要查询的表格</param>
        /// <param name="isQueryAaliasName">确定查询的是字段别名还是字段名</param>
        /// <param name="queryFieldNames">显示在属性查询窗口的，要查询的字段</param>
        public QueryForm(ITable searchTable, bool isQueryAaliasName = false, string[] queryFieldNames = null)
        {
            InitializeComponent();

            QueryHelper = new AttributeQueryHelper(searchTable, isQueryAaliasName, queryFieldNames);
            Text = "按属性查询 - " + QueryHelper.TableName;
        }


        private void QueryForm_Load(object sender, EventArgs e)//加载窗体时显示字段列表
        {
            listBoxCtrlFields.Items.AddRange(QueryHelper.FieldItems);
        }

        private void symbolControl_Click(object sender, EventArgs e)//各类符号按钮
        {
            var symbol = $"{(sender as Control)?.Tag} ";
            var startIndex = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(startIndex, symbol);
            txtWhereClause.SelectionStart = symbol == "='' " ? startIndex + 2 : startIndex + symbol.Length;
        }

        private void txtSearchFields_EditValueChanged(object sender, EventArgs e)//查找字段
        {
            listBoxCtrlFields.SelectedItem = QueryHelper.QueryField(txtSearchFields.Text);
        }

        private void txtSearchValues_EditValueChanged(object sender, EventArgs e)//查找唯一值
        {
            listBoxCtrlValues.SelectedItem = listBoxCtrlValues.Items.Cast<string>().FirstOrDefault(v => v.StartsWith(txtSearchValues.Text));
        }

        private void sBtnGetUniqueValue_Click(object sender, EventArgs e)//获取唯一值
        {
            if (listBoxCtrlFields.SelectedIndex < 0) return;
            listBoxCtrlValues.Items.Clear();
            listBoxCtrlValues.Items.AddRange(QueryHelper.GetUnqiueValues((FieldItem)listBoxCtrlFields.SelectedItem));
        }

        private void listBoxCtrlFields_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段，将字段名加入到查询语句中
        {
            if (listBoxCtrlFields.SelectedIndex < 0) return;

            var fieldItem = (FieldItem)listBoxCtrlFields.SelectedItem;
            var fieldName = QueryHelper.IsQueryAaliasName ? fieldItem.AliasName : fieldItem.Name;
            var value = $"{fieldName} ";
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxCtrlValues_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段值，将字段值加入到查询语句中
        {
            if (listBoxCtrlValues.SelectedIndex < 0 && listBoxCtrlFields.SelectedIndex < 0)
                return;
            var fieldType = ((FieldItem)listBoxCtrlFields.SelectedItem).FieldType;
            var format = fieldType == esriFieldType.esriFieldTypeString ? "'{0}' " : "{0} ";
            var value = string.Format(format, listBoxCtrlValues.SelectedItem);
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxCtrlFields_MouseUp(object sender, MouseEventArgs e)//弹出右键菜单
        {
            listBoxCtrlFields.SelectedIndex = listBoxCtrlFields.IndexFromPoint(new Point(e.X, e.Y));
            if (listBoxCtrlFields.SelectedIndex > -1 && e.Button == MouseButtons.Right)
            {
                等于EToolStripMenuItem.Tag = ((FieldItem)listBoxCtrlFields.SelectedItem).FieldType == esriFieldType.esriFieldTypeString ? "=''" : "=";
                cMenuStripFields.Show(listBoxCtrlFields, e.X, e.Y);
            }
        }

        private void sBtnClear_Click(object sender, EventArgs e)//清除查询语句
        {
            txtWhereClause.Text = string.Empty;
        }

        private void sBtnClose_Click(object sender, EventArgs e)//关闭窗体
        {
            Close();
        }

        private void sBtnApply_Click(object sender, EventArgs e)//应用（执行查询语句）
        {
            try
            {
                Query?.Invoke(this, new EventArgs());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
