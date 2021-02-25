using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using WLib.Model;

namespace WLib.WinCtrls.Dev.GridViewCtrl.EntityEdit
{
    /// <summary>
    /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的窗体
    /// <para>注意：内部使用了ORM组件Dapper</para>
    /// </summary>
    public partial class DbEditGridForm : XtraForm
    {
        /// <summary>
        /// 多个实体数据库
        /// </summary>
        public EntityDb[] Dbs => this.dbEditGridView1.Dbs;
        /// <summary>
        /// 实体数据库
        /// </summary>
        public EntityDb Db => this.dbEditGridView1.Db;
        /// <summary>
        /// 可对实体或表格数据进行增、删、改、查、保存提交到数据库的控件
        /// </summary>
        public EditGridView EditGridView => this.dbEditGridView1.EditGridView;



        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的窗体
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        public DbEditGridForm() => InitializeComponent();
        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的窗体
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        /// <param name="entityDb">实体数据库</param>
        /// <param name="text">窗体的标题</param>
        /// <param name="afterSaveToDb">数据保存到数据库后的事件的处理方法</param>
        public DbEditGridForm(EntityDb entityDb, string text = null, Action afterSaveToDb = null) : this()
        {
            this.dbEditGridView1.Add(entityDb);
            this.Text = text;
            if (afterSaveToDb != null)
                EditGridView.AfterSaveToDb += (sender, e)=> afterSaveToDb();
        }
        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的窗体
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        /// <param name="entityDbs">多个实体数据库</param>
        /// <param name="text">窗体的标题</param>
        /// <param name="afterSaveToDb">数据保存到数据库后的事件的处理方法</param>
        public DbEditGridForm(IEnumerable<EntityDb> entityDbs, string text = null, Action afterSaveToDb = null) : this()
        {
            this.dbEditGridView1.AddRange(entityDbs);
            this.Text = text;
            if (afterSaveToDb != null)
                EditGridView.AfterSaveToDb += (sender, e) => afterSaveToDb();
        }
    }
}
