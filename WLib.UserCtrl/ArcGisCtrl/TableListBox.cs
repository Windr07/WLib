/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WLib.ArcGis.Control.AttributeCtrl;
using WLib.WinCtrls.ListCtrl;

namespace WLib.WinCtrls.ArcGisCtrl
{
    /// <summary>
    /// 表格(ArcGIS ITable)列表控件
    /// </summary>
    public partial class TableListBox : UserControl
    {
        /// <summary>
        /// 显示表格列表的控件
        /// </summary>
        private IListControl _listCtrl = new ImageListBox();
        /// <summary>
        /// 显示表格列表的控件
        /// （此属性值对象类型，除实现<see cref="IListControl"/>接口外，还必须继承<see cref="Control"/>）
        /// </summary>
        public IListControl ListCtrl
        {
            get => _listCtrl;
            set
            {
                if (!(value is Control))
                    throw new Exception($"参数{nameof(ListCtrl)}必须为控件对象，即继承{typeof(Control)}类型的实例！");
                _listCtrl = value;
                _listCtrl.Dock = DockStyle.Fill;
            }
        }
        /// <summary>
        /// 加载在表格列表中的表格
        /// </summary>
        public List<ITable> Tables { get; private set; }
        /// <summary>
        /// 属性表窗口
        /// </summary>
        public IAttributeCtrl AttributeForm { get; set; }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            Control control = (Control)ListCtrl;
            control.ContextMenuStrip = this.cMenuStripTableList;
            this.Controls.Add(control);

            ListCtrl.Dock = DockStyle.Fill;
            ListCtrl.SelectedIndexChanged += listCtrl_SelectedIndexChanged;
            Tables = new List<ITable>();
        }
        /// <summary>
        /// 表格(ArcGIS ITable)列表控件
        /// </summary>
        public TableListBox()
        {
            InitializeComponent();
            InitControl();
        }
        /// <summary>
        /// 表格(ArcGIS ITable)列表控件
        /// </summary>
        /// <param name="attributeForm">属性表窗口</param>
        /// <param name="listControl"></param>
        public TableListBox(IAttributeCtrl attributeForm, IListControl listControl)
        {
            InitializeComponent();
            InitControl();
            AttributeForm = attributeForm;
            ListCtrl = listControl;
            AttributeForm.FormClosing += (sender, e) => { e.Cancel = true; AttributeForm.Visible = false; };
        }


        /// <summary>
        /// 添加表格
        /// </summary>
        /// <param name="table"></param>
        public void AddTable(ITable table)
        {
            Tables.Add(table);
            var tableName = table is IObjectClass objectClass ? objectClass.AliasName : ((IDataset)table).Name;
            ListCtrl.Items.Add(tableName);
        }
        /// <summary>
        /// 添加多个表格
        /// </summary>
        /// <param name="tables"></param>
        public void AddTables(IEnumerable<ITable> tables)
        {
            Tables.AddRange(tables);
            ListCtrl.Items.Add(tables.Select(v => ((IObjectClass)v).AliasName).ToArray());
        }
        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="table"></param>
        public void RemoveTable(ITable table)
        {
            ListCtrl.Items.Remove(((IObjectClass)table).AliasName);
            Tables.Remove(table);
        }
        /// <summary>
        /// 移除指定表格
        /// </summary>
        /// <param name="tableName"></param>
        public void RemoveTable(string tableName)
        {
            var table = Tables.FirstOrDefault(v => ((IObjectClass)v).AliasName == tableName);
            if (table != null)
                RemoveTable(table);
        }
        /// <summary>
        /// 清空表格列表
        /// </summary>
        public void ClearTable()
        {
            Tables.Clear();
            ListCtrl.Items.Clear();
        }


        private void 打开表格属性表TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListCtrl.SelectedItems.Count < 1)
                return;
            string tableName = ListCtrl.SelectedItems[0].ToString();
            var table = Tables.FirstOrDefault(v => ((IObjectClass)v).AliasName == tableName);
            if (table != null)
            {
                AttributeForm.Activate();//之前已打开，则给予焦点，置顶。
                AttributeForm.Show(this);
                AttributeForm.LoadAttribute(table);
            }
        }

        private void 移除表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListCtrl.SelectedItems.Count > 0)
                RemoveTable(ListCtrl.SelectedItems[0].ToString());
        }

        private void listCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enable = ListCtrl.SelectedItems.Count > 0;
            打开表格属性表TToolStripMenuItem.Enabled = enable;
            移除表格RToolStripMenuItem.Enabled = enable;
        }

        private void tableListBox_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //    tableBox.SelectedIndex = tableBox.IndexFromPoint(e.Location);
        }

        private void tableListBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ListCtrl.SelectedItems.Count > 0)
                cMenuStripTableList.Show((Control)ListCtrl, e.Location);
        }
    }
}