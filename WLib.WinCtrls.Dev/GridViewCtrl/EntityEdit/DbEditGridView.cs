using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WLib.Database;
using WLib.Model;

namespace WLib.WinCtrls.Dev.GridViewCtrl
{
    /// <summary>
    /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的控件
    /// <para>注意：内部使用了ORM组件Dapper</para>
    /// </summary>
    public partial class DbEditGridView : XtraUserControl
    {
        /// <summary>
        /// 多个实体数据库
        /// </summary>
        public EntityDb[] Dbs => this.imgListBoxPlanColl.Items.Cast<ImageListBoxItem>().Select(v => v.Value as EntityDb).ToArray();
        /// <summary>
        /// 实体数据库
        /// </summary>
        public EntityDb Db => this.imgListBoxPlanColl.SelectedItem.CastTo<ImageListBoxItem>()?.Value as EntityDb;
        /// <summary>
        /// 可对实体或表格数据进行增、删、改、查、保存提交到数据库的控件
        /// </summary>
        public EditGridView EditGridView => this.editGridView1;


        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的控件
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        public DbEditGridView() => InitializeComponent();
        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的控件
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        /// <param name="entityDb">实体数据库</param>
        public DbEditGridView(EntityDb entityDb) : this() => Add(entityDb);
        /// <summary>
        /// 可对一个数据库中的实体或表格数据进行增、删、改、查、保存的控件
        /// <para>注意：内部使用了ORM组件Dapper</para>
        /// </summary>
        /// <param name="entityDb">多个实体数据库</param>
        public DbEditGridView(IEnumerable<EntityDb> entityDbs) : this() => AddRange(entityDbs);


        /// <summary>
        /// 添加实体数据库
        /// </summary>
        /// <param name="db"></param>
        public void Add(EntityDb db)
        {
            this.imgListBoxPlanColl.Items.Add(db);
            if (this.imgListBoxPlanColl.SelectedIndex < 0) this.imgListBoxPlanColl.SelectedIndex = 0;
        }
        /// <summary>
        /// 添加实体数据库
        /// </summary>
        /// <param name="db"></param>
        public void AddRange(IEnumerable<EntityDb> dbs)
        {
            this.splitContainerControl1.Collapsed = false;
            this.imgListBoxPlanColl.Items.AddRange(dbs.ToArray());
            if (this.imgListBoxPlanColl.SelectedIndex < 0) this.imgListBoxPlanColl.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置参数方案/参数内容NavBarControl列表项的图标
        /// </summary>
        /// <param name="navBarCtrl"></param>
        /// <param name="selectedLink">当前选中项</param>
        private void ResetImageOfItemsLinks(NavBarControl navBarCtrl, NavBarItemLink selectedLink)
        {
            foreach (NavBarGroup barGroup in navBarCtrl.Groups)
            {
                barGroup.SmallImageIndex = -1;
                foreach (NavBarItemLink itemLink in barGroup.ItemLinks)
                    itemLink.Item.SmallImageIndex = 0;
            }
            selectedLink.Item.SmallImageIndex = selectedLink.Group.SmallImageIndex = 1;
        }


        private void imgListBoxPlanColl_SelectedIndexChanged(object sender, EventArgs e)//加载数据库数据
        {
            if (this.imgListBoxPlanColl.SelectedIndex < 0) return;

            var db = Db;
            if (db == null) throw new NullReferenceException($"实体数据库对象{nameof(Db)}不能为Null");
            this.editGridView1.Connection = DbHelper.GetDbHelper(db.ConnectionString, db.DbType);

            this.navBarCtrl.Groups.Clear();
            this.navBarCtrl.Items.Clear();
            try
            {
                db.ForEach(enityGroup =>
                {
                    var navBarGroup = new NavBarGroup(enityGroup.GroupName);
                    foreach (var entityInfo in enityGroup)
                    {
                        var navBarItem = new NavBarItem { Caption = entityInfo.Name };
                        navBarItem.LinkClicked += navBarItem_LinkClicked;
                        navBarGroup.ItemLinks.Add(navBarItem);
                    }
                    this.navBarCtrl.Groups.Add(navBarGroup);
                });
                this.navBarCtrl.SelectedLink = this.navBarCtrl.Groups[0].ItemLinks[0];
                this.navBarCtrl.SelectedLink.PerformClick();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void navBarItem_LinkClicked(object sender, NavBarLinkEventArgs e)//单击分组，打开对应的分组
        {
            ResetImageOfItemsLinks(this.navBarCtrl, e.Link);
            var group = Db.FirstOrDefault(v => v.GroupName == e.Link.Group.Caption);
            var enityInfo = group.FirstOrDefault(v => v.Name == e.Link.Caption);
            this.labelControlTips.Text = $"{e.Link.Caption}：{enityInfo.Description}";
            editGridView1.ShowModel(enityInfo.EntityType, true);
        }

        private void NavBarCtrl_MouseDown(object sender, MouseEventArgs e)//单击NavBarCtrl分组，加载相应图标
        {
            if (e.Button != MouseButtons.Left) return;

            var navBarCtrl = sender as NavBarControl;
            var hitInfo = navBarCtrl.CalcHitInfo(new Point(e.X, e.Y));
            if (hitInfo.InGroupCaption && !hitInfo.InGroupButton)
            {
                hitInfo.Group.Expanded = !hitInfo.Group.Expanded;
                if (hitInfo.Group.ItemLinks.Count > 0)
                {
                    ResetImageOfItemsLinks(navBarCtrl, hitInfo.Group.ItemLinks[0]);
                    hitInfo.Group.ItemLinks[0].PerformClick();
                }
            }
        }
    }
}
