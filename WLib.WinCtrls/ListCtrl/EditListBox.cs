using System;
using System.Drawing;
using System.Windows.Forms;

namespace WLib.WinCtrls.ListCtrl
{
    /// <summary>
    /// 扩展<see cref="ListBox"/>：双击可编辑<see cref="ListBox"/>的项，右键菜单可增删改指定的项
    /// </summary>
    public class EditListBox : ListBox
    {
        private TextBox txtEdit = new TextBox();
        private ContextMenuStrip _contextMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// 扩展<see cref="ListBox"/>：双击可编辑<see cref="ListBox"/>的项，右键菜单可增删改指定的项
        /// </summary>
        public EditListBox() : base()
        {
            var addMenuItem = _contextMenuStrip.Items.Add("添加(&A)");
            var removeMenuItem = _contextMenuStrip.Items.Add("删除(&D)");
            var editMenuItem = _contextMenuStrip.Items.Add("编辑(&E)");
            _contextMenuStrip.Opening += (sender, e) =>
            {
                int index = this.SelectedIndex;
                editMenuItem.Enabled = removeMenuItem.Enabled = index > -1;
            };

            addMenuItem.Click += (sender, e) =>
            {
                this.Items.Add("");
                this.SelectedIndex = this.Items.Count - 1;
                EditListBox_DoubleClick(null, null);
            };
            removeMenuItem.Click += (sender, e) => this.Items.RemoveAt(this.SelectedIndex);
            editMenuItem.Click += EditListBox_DoubleClick;


            this.DoubleClick += EditListBox_DoubleClick;
            this.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    _contextMenuStrip.Show(this, e.Location);
            };
            this.DrawItem += (sender, e) =>
             {
                 e.DrawBackground();
                 e.DrawFocusRectangle();
                 e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
             };
            this.txtEdit.Leave += (sender, e) =>
            {
                if (this.SelectedIndex > -1 && txtEdit.Visible)
                {
                    this.Items[this.SelectedIndex] = txtEdit.Text;
                    txtEdit.Visible = false;
                }
            };

            this.Click += (sender, e) =>
            {
                txtEdit.Visible = false;
            };
            this.txtEdit.KeyDown += (sender, e) =>
             {
                 if (e.KeyCode == Keys.Enter) //Enter键 更新项并隐藏编辑框
                 {
                     this.Items[this.SelectedIndex] = txtEdit.Text;
                     txtEdit.Visible = false;
                 }
                 if (e.KeyCode == Keys.Escape) //Esc键 直接隐藏编辑框
                     txtEdit.Visible = false;
             };
        }

        private void EditListBox_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedIndex < 0)
            {
                this.Items.Add("");
                this.SelectedIndex = this.Items.Count - 1;
            }
            string itemText = this.Items[this.SelectedIndex].ToString();
            Rectangle rect = this.GetItemRectangle(this.SelectedIndex);
            txtEdit.Parent = this;
            txtEdit.Bounds = rect;
            txtEdit.Multiline = true;
            txtEdit.Visible = true;
            txtEdit.Text = itemText;
            txtEdit.Focus();
            txtEdit.SelectAll();
        }
    }
}
