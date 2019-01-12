using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.Fields;
using WLib.ArcGis.GeoDb.Table;

namespace WLib.UserCtrls.Dev.ArcGisCtrl
{
    /// <summary>
    /// 按属性查询的窗体
    /// </summary>
    public partial class QueryDevForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 查询的表(或图层)
        /// </summary>
        public ITable SearchTable { get; set; }
        /// <summary>
        /// 构造的查询语句
        /// </summary>
        public string WhereClause { get => this.txtWhereClause.Text.Trim(); set => this.txtWhereClause.Text = value; }
        /// <summary>
        /// 查询事件
        /// </summary>
        public event EventHandler Query;
        /// <summary>
        /// 确定查询的是字段别名还是字段名
        /// </summary>
        public bool IsQueryAaliasName { get; set; }
        /// <summary>
        /// 要查询的字段（名称或别名均可，此值为null时则查询所有字段）
        /// </summary>
        public string[] QueryFieldNames { get; set; }


        /// <summary>
        /// 按属性查询的窗体
        /// </summary>
        ///  <param name="searchTable">要查询的表格</param>
        /// <param name="isQueryAaliasName">确定查询的是字段别名还是字段名</param>
        /// <param name="queryFieldNames">要查询的字段</param>
        public QueryDevForm(ITable searchTable, bool isQueryAaliasName = false, string[] queryFieldNames = null)
        {
            InitializeComponent();

            this.SearchTable = searchTable;
            this.Text = "按属性查询 - " + (this.SearchTable as IDataset).Name;
            this.IsQueryAaliasName = isQueryAaliasName;
            this.QueryFieldNames = queryFieldNames;
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        public void OnQuery()
        {
            Query?.Invoke(this, new EventArgs());
        }

        private void QueryForm_Load(object sender, EventArgs e)//加载窗体时，加载字段列表
        {
            this.listBoxCtrlFields.Items.AddRange(SearchTable.Fields.GetFieldItems(QueryFieldNames).ToArray());
        }

        private void symbolButton_Click(object sender, EventArgs e)//各类符号按钮
        {
            //各类符号（like,or,and,not,>,<,=等）存放在按钮的Tag属性里面
            SimpleButton button = sender as SimpleButton;
            int startIndex = this.txtWhereClause.SelectionStart;
            this.txtWhereClause.Text = this.txtWhereClause.Text.Insert(startIndex, $"{button.Tag.ToString()} ");
            this.txtWhereClause.SelectionStart = startIndex + button.Tag.ToString().Length;
            if (button.Tag.ToString() == "=''")
                this.txtWhereClause.SelectionStart = startIndex + 2;
        }

        private void txtSearchFields_EditValueChanged(object sender, EventArgs e)//查找字段
        {
            string value = this.txtSearchFields.Text.ToUpper();
            foreach (var item in this.listBoxCtrlFields.Items)
            {
                var fieldClass = (FieldItem)item;
                if (fieldClass.Name.ToUpper().StartsWith(value) || fieldClass.AliasName.ToUpper().StartsWith(value))
                {
                    this.listBoxCtrlFields.SelectedItem = item;
                    break;
                }
            }
        }

        private void txtSearchValues_EditValueChanged(object sender, EventArgs e)//查找唯一值
        {
            string value = this.txtSearchValues.Text;
            foreach (var item in this.listBoxCtrlValues.Items)
            {
                if (item.ToString().StartsWith(value))
                {
                    this.listBoxCtrlValues.SelectedItem = item;
                    break;
                }
            }
        }

        private void sBtnGetUniqueValue_Click(object sender, EventArgs e)//获取唯一值
        {
            if (this.listBoxCtrlFields.SelectedIndex < 0)
                return;
            string fieldName = ((FieldItem)this.listBoxCtrlFields.SelectedItem).Name;
            var values = TableOpt.GetUniqueValues(SearchTable, fieldName).ToArray();
            this.listBoxCtrlValues.Items.Clear();
            this.listBoxCtrlValues.Items.AddRange(values);
        }

        private void listBoxCtrlFields_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段，将字段名加入到查询语句中
        {
            if (this.listBoxCtrlFields.SelectedIndex < 0)
                return;

            var fieldItem = (FieldItem)this.listBoxCtrlFields.SelectedItem;
            var fieldName = IsQueryAaliasName ? fieldItem.AliasName : fieldItem.Name;
            string value = $"[{fieldName}] ";
            int index = this.txtWhereClause.SelectionStart;
            this.txtWhereClause.Text = this.txtWhereClause.Text.Insert(index, value);
            this.txtWhereClause.SelectionStart = index + value.Length;
        }

        private void listBoxCtrlValues_MouseDoubleClick(object sender, MouseEventArgs e)//双击字段值，将字段值加入到查询语句中
        {
            if (this.listBoxCtrlValues.SelectedIndex < 0 || this.listBoxCtrlFields.SelectedIndex < 0)
                return;
            var fieldType = ((FieldItem)this.listBoxCtrlFields.SelectedItem).FieldType;
            string format = fieldType == esriFieldType.esriFieldTypeString ? "'{0}' " : "{0} ";
            string value = string.Format(format, this.listBoxCtrlValues.SelectedItem.ToString());
            int index = this.txtWhereClause.SelectionStart;
            this.txtWhereClause.Text = this.txtWhereClause.Text.Insert(index, value);
            this.txtWhereClause.SelectionStart = index + value.Length;
        }

        private void sBtnClear_Click(object sender, EventArgs e)//清空
        {
            this.txtWhereClause.Text = "";
        }

        private void sBtnClose_Click(object sender, EventArgs e)//关闭
        {
            this.Close();
        }

        private void sBtnApply_Click(object sender, EventArgs e)//应用
        {
            try
            {
                OnQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void 等于EToolStripMenuItem_Click(object sender, EventArgs e)//右键菜单：等于，自动判断是“=”，还是“=''”
        {
            object item = this.listBoxCtrlFields.SelectedItem;
            if (item != null)
            {
                int startIndex = this.txtWhereClause.SelectionStart;
                if (((FieldItem)item).FieldType == esriFieldType.esriFieldTypeString)
                {
                    this.txtWhereClause.Text = this.txtWhereClause.Text.Insert(startIndex, "=''");
                    this.txtWhereClause.SelectionStart = startIndex + 2;
                }
                else
                {
                    this.txtWhereClause.Text = this.txtWhereClause.Text.Insert(startIndex, "= ");
                    this.txtWhereClause.SelectionStart = startIndex + "= ".Length;
                }
            }
        }

        private void listBoxCtrlFields_MouseUp(object sender, MouseEventArgs e)//提起鼠标时，在指针位置弹出右键菜单
        {
            this.listBoxCtrlFields.SelectedIndex = this.listBoxCtrlFields.IndexFromPoint(new System.Drawing.Point(e.X, e.Y));
            if (this.listBoxCtrlFields.SelectedIndex > -1 && e.Button == MouseButtons.Right)
            {
                this.cMenuStripFields.Show(this.listBoxCtrlFields, e.X, e.Y);
            }
        }
    }
}
