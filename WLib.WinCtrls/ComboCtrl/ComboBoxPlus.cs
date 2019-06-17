using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WLib.WinCtrls.ComboCtrl
{
    /// <summary>
    /// 带有添加新项和删除操作的ComboBox
    /// </summary>
    public class ComboBoxPlus : ComboBox
    {
        /// <summary>
        /// 
        /// </summary>
        private bool init;
        /// <summary>
        /// 
        /// </summary>
        private IntPtr hwnd;
        /// <summary>
        /// 
        /// </summary>
        private int deleteButtonXPos;
        /// <summary>
        /// 
        /// </summary>
        private int deleteButtonLength;
        /// <summary>
        /// 标记是否在下拉框中点击了新增项按钮
        /// </summary>
        private bool addItemClicked;
        /// <summary>
        /// 标记是否在下拉框中点击了删除按钮
        /// </summary>
        private bool deleteButtonClicked;
        /// <summary>
        /// 记录下拉框展开后进行点击操作之前的selectedIndex
        /// </summary>
        private int beforeMouseDownIndex = -1;
        /// <summary>
        /// 
        /// </summary>
        private readonly NativeCombo nativeCombo = new NativeCombo();
        /// <summary>
        /// key值存储删除按钮的坐标Y值，value值存储combobox对应的index值
        /// </summary>
        private readonly Dictionary<int, int> positions = new Dictionary<int, int>();
        /// <summary>
        /// 删除项按钮单击事件
        /// </summary>
        public event EventHandler<DeleteEventArgs> DeleteButtonClick;
        /// <summary>
        /// 添加项按钮单击事件
        /// </summary>
        public event Func<object, EventArgs, bool> AddNewItemClick;
        /// <summary>
        /// 
        /// </summary>
        public string AddItemString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ComboBoxPlus()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.AddItemString = "添加新项";
            this.Items.Add(AddItemString);
            this.HandleCreated += (s, e) =>
            {
                COMBOBOXINFO combo = new COMBOBOXINFO();
                combo.cbSize = Marshal.SizeOf(combo);
                GetComboBoxInfo(this.Handle, out combo);
                hwnd = combo.hwndList;
                init = false;
            };
        }


        /// <summary>
        /// 为combobox添加数据源，建议使用string类型数据作为displayMember，使用int类型数据作为valueMember
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="displayMember"></param>
        /// <param name="valueMember"></param>
        public void SetDataSource(DataTable dataTable, string displayMember, string valueMember)
        {
            DataRow dataRow = dataTable.NewRow();
            dataRow[displayMember] = this.AddItemString;
            dataRow[valueMember] = 0;
            dataTable.Rows.InsertAt(dataRow, 0);

            this.DataSource = null;
            this.DisplayMember = displayMember;
            this.ValueMember = valueMember;
            this.DataSource = dataTable;

            this.SelectedIndex = dataTable.Rows.Count > 1 ? 1 : -1;
        }

        public void AddNewItem(object display, object value)
        {
            var dataTable = this.DataSource as DataTable;
            var dataRow = dataTable.NewRow();
            dataRow[this.DisplayMember] = display;
            dataRow[this.ValueMember] = display;
            dataTable.Rows.Add(dataRow);
        }

        public void Reset()
        {
            this.beforeMouseDownIndex = -1;
            positions.Clear();
        }

        protected override void OnDropDown(EventArgs e)
        {
            if (!init)
            {
                nativeCombo.MouseDown += comboListMouseDown;
                nativeCombo.AssignHandle(hwnd);
                init = true;
            }
            beforeMouseDownIndex = this.SelectedIndex;
            base.OnDropDown(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Rectangle bounds = e.Bounds;
                deleteButtonLength = e.Bounds.Height / 3;
                if (e.Index > 0 && e.Bounds.X == 0)
                {
                    e.DrawBackground();

                    //绘制删除按钮
                    Rectangle deleteButtonRect = new Rectangle(bounds.X + bounds.Width - 2 * deleteButtonLength,
                        bounds.Y + (e.Bounds.Height - deleteButtonLength) / 2, deleteButtonLength, deleteButtonLength);
                    Pen linePen = new Pen(Brushes.Black);
                    e.Graphics.DrawLine(linePen, deleteButtonRect.Location,
                        new Point(deleteButtonRect.X + deleteButtonRect.Width,
                            deleteButtonRect.Y + deleteButtonRect.Height));
                    e.Graphics.DrawLine(linePen,
                        new Point(deleteButtonRect.X + deleteButtonRect.Width, deleteButtonRect.Y),
                        new Point(deleteButtonRect.X, deleteButtonRect.Y + deleteButtonRect.Height));

                    deleteButtonXPos = deleteButtonRect.X;
                    positions[e.Bounds.Y] = e.Index;
                    deleteButtonLength = e.Bounds.Height;
                }

                string itemString = DataSource == null ? this.Items[e.Index].ToString() : (this.DataSource as DataTable)?.Rows[e.Index][DisplayMember].ToString();

                SizeF stringSize = e.Graphics.MeasureString(itemString, e.Font);
                bounds.Y += (int)(bounds.Height - stringSize.Height);
                bounds.Width -= 2 * deleteButtonLength;

                if (e.Index > 0)
                    e.Graphics.DrawString(itemString, e.Font, Brushes.Black, bounds);
                else
                {
                    Font font = new Font(e.Font, FontStyle.Underline);
                    e.Graphics.DrawString(AddItemString, font, Brushes.Blue, bounds);
                }
            }
            e.Graphics.Dispose();
            base.OnDrawItem(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (this.SelectedIndex == 0)
            {
                if (addItemClicked && AddNewItemClick != null)
                {
                    bool result = AddNewItemClick(this, new EventArgs());
                    this.SelectedIndex = result ? this.Items.Count - 1 : beforeMouseDownIndex;
                }
                addItemClicked = false;
                return;
            }
            if (deleteButtonClicked)
            {
                if (DeleteButtonClick != null)
                {
                    deleteButtonClicked = false;
                    int selectIndex = this.SelectedIndex;
                    DeleteEventArgs deleteEventArgs = new DeleteEventArgs(selectIndex);
                    DeleteButtonClick(this, deleteEventArgs);
                    if (deleteEventArgs.IsDelete)
                    {
                        if (selectIndex == 1) this.OnSelectedIndexChanged(null);
                        if (this.Items.Count > 1) this.SelectedIndex = 1;
                        else this.SelectedIndex = -1;
                    }
                    else
                        this.SelectedIndex = beforeMouseDownIndex;
                }
            }

            base.OnSelectedIndexChanged(e);
        }

        private void comboListMouseDown(object sender, MouseEventArgs e)
        {
            //下拉框有效坐标范围
            Rectangle validRect = new Rectangle(0, 0, this.DropDownWidth, this.Items.Count * this.ItemHeight);
            if (!validRect.Contains(e.Location))
                return;

            if (this.SelectedIndex == 0)
            {
                addItemClicked = true;
                return;
            }

            Rectangle rect = new Rectangle(this.deleteButtonXPos, 0, deleteButtonLength, deleteButtonLength);
            foreach (var position in positions)
            {
                rect.Y = position.Key;
                if (rect.Contains(e.Location))
                {
                    deleteButtonClicked = true;
                    return;
                }
            }
        }


        #region 引用user32.dll函数
        [DllImport("user32")]
        private static extern int GetComboBoxInfo(IntPtr hwnd, out COMBOBOXINFO comboInfo);
        struct RECT { public int left, top, right, bottom; }
        struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }
        #endregion
    }
}
