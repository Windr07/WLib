/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Control.AttributeCtrl;
using WLib.ArcGis.GeoDatabase.Fields;

namespace WLib.WinCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 按属性查询的窗体
    /// </summary>
    public partial class AttributeQueryForm : DevExpress.XtraEditors.XtraForm, IAttributeQueryCtrl
    {
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler Query;
        /// <summary>
        /// 构造的查询语句
        /// </summary>
        public string WhereClause { get => txtWhereClause.Text.Trim(); set => txtWhereClause.Text = value; }


        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        public AttributeQueryForm()
        {
            InitializeComponent();

            const string findFieldTips = @"按名称/别名查找字段";
            const string findValueTips = @"查找字段值";
            txtSearchFields.Text = findFieldTips;
            txtSearchValues.Text = findValueTips;
            txtSearchFields.MouseDown += delegate { if (txtSearchFields.Text == findFieldTips) txtSearchFields.Text = string.Empty; };
            txtSearchValues.MouseDown += delegate { if (txtSearchValues.Text == findValueTips) txtSearchValues.Text = string.Empty; };
            清空DToolStripMenuItem.Click += delegate { txtWhereClause.Text = string.Empty; };
            sBtnClear.Click += delegate { txtWhereClause.Text = string.Empty; };
            sBtnClose.Click += delegate { Close(); };
        }
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTable">要查询的表格</param>
        /// <param name="isQueryAaliasName">确定查询的是字段别名还是字段名</param>
        /// <param name="queryFieldNames">显示在属性查询窗口的，要查询的字段</param>
        /// <param name="queryHandler">查询事件处理</param>
        public void LoadQueryInfo(ITable searchTable, bool isQueryAaliasName = false, string[] queryFieldNames = null)
        {
            cmbTables.Properties.Items.Clear();
            cmbTables.Properties.Items.Add(new AttributeQuery(searchTable, isQueryAaliasName, queryFieldNames));
        }

        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTables">要查询的表格</param>
        /// <param name="queryHandler">查询事件处理</param>
        public void LoadQueryInfo(IEnumerable<ITable> searchTables)
        {
            cmbTables.Properties.Items.Clear();
            cmbTables.Properties.Items.AddRange(searchTables.Select(v => new AttributeQuery(v, false, null)).ToArray());
        }
        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="tableAttributeQueries">要查询的表格</param>
        /// <param name="queryHandler">查询事件处理</param>
        public void LoadQueryInfo(IEnumerable<AttributeQuery> tableAttributeQueries)
        {
            cmbTables.Properties.Items.Clear();
            cmbTables.Properties.Items.AddRange(tableAttributeQueries.ToArray());
        }


        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            var attrQuery = (AttributeQuery)cmbTables.SelectedItem;
            listBoxFields.Items.AddRange(attrQuery.FieldItems);
            Text = $@"按属性查询 - {attrQuery.TableName}";
        }

        private void symbolButton_Click(object sender, EventArgs e)//各类符号按钮
        {
            var symbol = $"{(sender as Control)?.Tag} ";
            var startIndex = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(startIndex, symbol);
            txtWhereClause.SelectionStart = symbol == "='' " ? startIndex + 2 : startIndex + symbol.Length;
        }

        private void txtSearchFields_EditValueChanged(object sender, EventArgs e)//查找字段
        {
            if(cmbTables.SelectedItem!=null)
            listBoxFields.SelectedItem = ((AttributeQuery)cmbTables.SelectedItem).QueryField(txtSearchFields.Text);
        }

        private void txtSearchValues_EditValueChanged(object sender, EventArgs e)//查找唯一值
        {
            listBoxValues.SelectedItem = listBoxValues.Items.Cast<string>().FirstOrDefault(v => v.StartsWith(txtSearchValues.Text));
        }

        private void sBtnGetUniqueValue_Click(object sender, EventArgs e)//获取唯一值
        {
            if (listBoxFields.SelectedIndex < 0) return;
            listBoxValues.Items.Clear();
            listBoxValues.Items.AddRange(((AttributeQuery)cmbTables.SelectedItem).GetUnqiueValues((GFieldItem)listBoxFields.SelectedItem));
        }

        private void listBoxFields_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段，将字段名加入到查询语句中
        {
            if (listBoxFields.SelectedIndex < 0) return;

            var fieldItem = (GFieldItem)listBoxFields.SelectedItem;
            var fieldName = ((AttributeQuery)cmbTables.SelectedItem).IsQueryAaliasName ? fieldItem.AliasName : fieldItem.Name;
            var value = $"{fieldName} ";
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxValues_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段值，将字段值加入到查询语句中
        {
            if (listBoxValues.SelectedIndex < 0 && listBoxFields.SelectedIndex < 0)
                return;
            var fieldType = ((GFieldItem)listBoxFields.SelectedItem).eFieldType;
            var format = fieldType == esriFieldType.esriFieldTypeString ? "'{0}' " : "{0} ";
            var value = string.Format(format, listBoxValues.SelectedItem);
            var index = txtWhereClause.SelectionStart;
            txtWhereClause.Text = txtWhereClause.Text.Insert(index, value);
            txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxFields_MouseUp(object sender, MouseEventArgs e)//提起鼠标时，在指针位置弹出右键菜单
        {
            listBoxFields.SelectedIndex = listBoxFields.IndexFromPoint(new Point(e.X, e.Y));
            if (listBoxFields.SelectedIndex > -1 && e.Button == MouseButtons.Right)
            {
                等于EToolStripMenuItem.Tag = ((GFieldItem)listBoxFields.SelectedItem).eFieldType == esriFieldType.esriFieldTypeString ? "=''" : "=";
                cMenuStripFields.Show(listBoxFields, e.X, e.Y);
            }
        }

        private void sBtnApply_Click(object sender, EventArgs e)//应用
        {
            try
            {
                Query?.Invoke(this, new EventArgs());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
