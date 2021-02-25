using Dapper;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WLib.Attributes.Table;
using WLib.Database;
using WLib.Model;
using WLib.WinCtrls.MessageCtrl;

namespace WLib.WinCtrls.Dev.GridViewCtrl
{
    /// <summary>
    /// 可对实体或表格数据进行增、删、改、查、保存提交到数据库的控件
    /// <para>提示：①在实体类的属性上设置<see cref="KeyAttribute"/>表示主键；②设置<see cref="ReadOnlyAttribute"/>表示只读；③设置<see cref="HiddenAttribute"/>则不显示该字段</para>
    /// <para>注意：①内部使用了ORM组件Dapper；  ②需要设置主键字段<see cref="KeyField"/>，且要求其为整型自增字段</para>
    /// </summary>
    public partial class EditGridView : XtraUserControl
    {
        /// <summary>
        /// 表中的自增ID的最大值
        /// </summary>
        protected int MaxId = -1;
        /// <summary>
        /// 表的主键字段
        /// </summary>
        protected string KeyField { get; set; }
        /// <summary>
        /// 加载的实体数据的类型
        /// </summary>
        protected Type ModelType { get; set; }
        /// <summary>
        /// 加载的实体数据
        /// </summary>
        protected List<object> Models { get; set; } = new List<object>();
        /// <summary>
        /// 经过新增、删除、修改的记录/实体
        /// </summary>
        public List<EntityModify> Modifys { get; } = new List<EntityModify>();
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection { get; set; }
        /// <summary>
        /// 数据保存到数据库后的事件
        /// </summary>
        public EventHandler AfterSaveToDb;


        /// <summary>
        /// 可对实体或表格数据进行增、删、改、查、保存提交到数据库的控件
        /// <para>提示：①在实体类的属性上设置<see cref="KeyAttribute"/>表示主键；②设置<see cref="ReadOnlyAttribute"/>表示只读；③设置<see cref="HiddenAttribute"/>则不显示该字段</para>
        /// <para>注意：①内部使用了ORM组件Dapper；  ②需要设置主键字段<see cref="KeyField"/>，且要求其为整型自增字段</para>
        /// </summary>
        public EditGridView() => InitializeComponent();
        /// <summary>
        /// 可对实体或表格数据进行增、删、改、查、保存提交到数据库的控件
        /// <para>提示：①在实体类的属性上设置<see cref="KeyAttribute"/>表示主键；②设置<see cref="ReadOnlyAttribute"/>表示只读；③设置<see cref="HiddenAttribute"/>则不显示该字段</para>
        /// <para>注意：①内部使用了ORM组件Dapper；  ②需要设置主键字段<see cref="KeyField"/>，且要求其为整型自增字段</para>
        /// </summary>
        /// <param name="connection">数据库连接</param>
        public EditGridView(IDbConnection connection) : this() => this.Connection = connection;
        /// <summary>
        /// 查询和显示指定类型（表格）的数据
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="saveChangedCheck">是否判断并且向用户询问保存之前的修改</param>
        public void ShowModel(Type modelType, bool saveChangedCheck)
        {
            if (saveChangedCheck) SaveChanged();
            Modifys.Clear();
            Models.Clear();
            ModelType = modelType;
            Models = Connection?.SimpleQuery(ModelType).ToList();
            MaxId = -1;
            ReBingding(Models);
        }
        /// <summary>
        /// 窗口关闭前判断是否保存修改
        /// </summary>
        private void SaveChanged()
        {
            if (Modifys.Count == 0) return;
            if (MessageBox.Show("数据已修改但未保存，是否保存？", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SBtnSave_Click(null, null);
        }
        /// <summary>
        /// 重新绑定指定类型（表格）数据
        /// </summary>
        /// <param name="models"></param>
        private void ReBingding(List<object> models)
        {
            try
            {
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                gridControl1.DataSource = new BindingList<object>(models);

                if (models.Count == 0) return;
                var type = models[0].GetType();
                KeyField = type.GetKeyProperty();//获取主键字段

                //拥有只读特性的字段，设置不可编辑
                type.GetReadOnlyProperties().ToList().ForEach(f => gridView1.Columns[f].OptionsColumn.AllowEdit = false);

                //拥有隐藏特性的字段，设置隐藏
                type.GetHiddenProperties().ToList().ForEach(f => gridView1.Columns[f].Visible = false);

                //拥有别名特性的字段，设置别名
                type.GetNameAndAliasName().ToList().ForEach(f => gridView1.Columns[f.Key].Caption = f.Value);

                //拥有外键特性的字段获取外键数据，作为下拉框的选项
                foreach (var foreignInfo in type.GetForeignKeyInfos())
                {
                    try
                    {
                        var values = Connection.QueryValue<object>(foreignInfo.ForeignTable, foreignInfo.ForeignField).ToArray();
                        var cmb = new RepositoryItemComboBox() { TextEditStyle = TextEditStyles.Standard };//创建下拉框
                        cmb.Items.AddRange(values);
                        gridView1.Columns[foreignInfo.CurrentField].ColumnEdit = cmb;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"获取表“{type.Name}”的字段“{foreignInfo.CurrentField}”的外键数据失败：" +
                            $"{ex.Message}\r\n 外键关联至“{foreignInfo.ForeignTable}”表的“{ foreignInfo.ForeignField}”字段", ex);
                    }
                }
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)//被修改的单元格颜色显示为粉红色，不能修改的为灰色
        {
            if (!e.Column.OptionsColumn.AllowEdit)
                e.Appearance.BackColor = Color.LightGray;
            else
            {
                if (KeyField == null)
                    return;
                var strId = gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[KeyField]).ToString();
                int.TryParse(strId, out var id);
                var modify = Modifys.Find(v => v.Id == id && v.FieldNames.Contains(e.Column.Name));
                if (modify != null)
                    e.Appearance.BackColor = Color.LightPink;
            }
        }

        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)//记录被修改的单元格信息
        {
            var sourceIndex = gridView1.GetDataSourceRowIndex(e.RowHandle);
            var model = Models[sourceIndex];
            var strId = ModelType.GetProperty(KeyField).GetValue(model).ToString();
            int.TryParse(strId, out var id);
            var modify = Modifys.Find(v => v.Id == id);
            if (modify == null)
                Modifys.Add(new EntityModify(id, e.Column.Name, EModifyType.Update, model));
            else if (!modify.FieldNames.Contains(e.Column.Name))
                modify.FieldNames.Add(e.Column.Name);
        }

        private void SBtnReset_Click(object sender, EventArgs e)//重置
        {
            if (MessageBox.Show("重置将丢失修改的数据，确定表格数据吗？", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                ShowModel(ModelType, false);
        }

        private void SBtnAdd_Click(object sender, EventArgs e)//添加
        {
            try
            {
                if (MaxId == -1)
                {
                    var objMaxId = Connection.ExecuteScalar($"select max({KeyField}) from {ModelType.Name}");
                    int.TryParse(objMaxId.ToString(), out MaxId);
                }
                var model = Activator.CreateInstance(ModelType);
                ModelType.GetProperty(KeyField).SetValue(model, ++MaxId);
                Models.Add(model);
                Modifys.Add(new EntityModify(MaxId, null, EModifyType.Add, model));

                ReBingding(Models);
                gridView1.FocusedRowHandle = gridView1.DataRowCount - 1;
                gridView1.Focus();
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }

        private void SBtnDelete_Click(object sender, EventArgs e)//删除
        {
            try
            {
                var selectedRows = gridView1.GetSelectedRows();
                if (selectedRows.Length == 0) return;

                //可同时删除多行，记录被删除的行对应的实体的主键值（ID，编号）
                var models = selectedRows.Select(rowIndex => Models[gridView1.GetDataSourceRowIndex(rowIndex)]).ToArray();//获取行对应的实体
                var keyProperty = ModelType.GetProperty(KeyField);
                var strIds = models.Select(model => keyProperty.GetValue(model).ToString());
                var ids = strIds.Select(strId => Convert.ToInt32(strId)).ToArray();
                if (MessageBox.Show($"确定要删除{KeyField}为{string.Join(",", strIds)}的记录？", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < selectedRows.Length; i++)
                    {
                        var modify = Modifys.FirstOrDefault(v => v.Id == ids[i]);
                        if (modify == null)
                            Modifys.Add(new EntityModify(ids[i], null, EModifyType.Delete, models[i]));
                        else if (modify.ModifyType == EModifyType.Add)
                            Modifys.Remove(modify);
                        else if (modify.ModifyType == EModifyType.Update)
                        {
                            Modifys.Add(new EntityModify(ids[i], null, EModifyType.Delete, models[i]));
                            Modifys.Remove(modify);
                        }
                        gridView1.DeleteRow(selectedRows[i]);
                    }
                }
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }

        private void SBtnSave_Click(object sender, EventArgs e)//保存
        {
            try
            {
                foreach (var modify in Modifys)
                {
                    switch (modify.ModifyType)
                    {
                        case EModifyType.Add: Connection.SimpleInsert(modify.Enitity); break;//新增
                        case EModifyType.Delete: Connection.SimpleDelete(modify.Enitity, KeyField); break;//删除
                        case EModifyType.Update: Connection.SimpleUpdate(modify.Enitity, KeyField); break;//修改
                    }
                }
                Modifys.Clear();
                AfterSaveToDb?.Invoke(this, new EventArgs());
            }
            catch (Exception ex) { MessageBoxEx.ShowError(ex); }
        }
    }
}
