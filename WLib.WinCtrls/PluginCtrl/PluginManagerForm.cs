using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WLib.Events;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.Plugins.Model;
using WLib.WinCtrls.InputCtrl;
using WLib.WinCtrls.Extension;
using static WLib.Plugins.PluginHelper;
using WLib.Files;
using WLib.Plugins.Enum;

namespace WLib.WinCtrls.PluginCtrl
{
    /// <summary>
    /// 插件管理窗口
    /// </summary>
    public partial class PluginManagerForm : Form
    {
        /// <summary>
        /// 修改插件信息的窗口
        /// </summary>
        private PluginInfoCtrl _pluginInfoForm = new PluginInfoCtrl();
        /// <summary>
        /// 修改插件容器信息的窗口
        /// </summary>
        private PluginContainerInfoCtrl _pluginContainerInfoForm = new PluginContainerInfoCtrl();

        /// <summary>
        /// 保存全部方案事件
        /// </summary>
        public event EventHandler SavePlans;
        /// <summary>
        /// 插件目录
        /// </summary>
        public string PluginDir { get; private set; }
        /// <summary>
        /// 插件程序集文件过滤条件，只加载满足此文件过滤条件的插件文件
        /// </summary>
        public string AssemblyFilter { get; private set; }
        /// <summary>
        /// 应用软件信息及拥有的插件方案
        /// </summary>
        public IPluginPlanSystem PlanSystem { get; private set; }
        /// <summary>
        /// 应用软件目录
        /// </summary>
        public string AppDir { get; private set; }
        /// <summary>
        /// 配置文件目录，未指定时为应用软件目录
        /// </summary>
        public string CfgDir { get; private set; }

        /// <summary>
        /// 实例化插件管理窗口，对指定程序进行插件管理
        /// </summary>
        /// <param name="pluginPlanSystems">要管理的应用软件插件方案系统</param>
        /// <param name="appDir">应用软件目录（在后台管理中是应用软件的配置目录）</param>
        /// <param name="pluginDir">插件命令所在目录</param>
        /// <param name="assemblyFilter">插件程序集文件过滤条件</param>
        /// <param name="cfgDir">配置文件所在目录</param>
        public PluginManagerForm(List<IPluginPlanSystem> pluginPlanSystems = null, string appDir = null, string pluginDir = null, string assemblyFilter = null, string cfgDir = null)
        {
            InitializeComponent();
            InitSubFormEvents();
            InitPluginLibrary(pluginDir, assemblyFilter); //加载dll和exe文件的命令仓库

            AppDir = appDir ?? AppDomain.CurrentDomain.BaseDirectory;
            CfgDir = cfgDir ?? AppDir;
            pluginPlanSystems = pluginPlanSystems ?? CreatePluginPlanSystems(AppDir).ToList();
            cmbSubSystem.DataSource = new BindingList<IPluginPlanSystem>(pluginPlanSystems);
            if (cmbSubSystem.Items.Count > 0)
                cmbSubSystem.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置子窗体事件处理
        /// </summary>
        private void InitSubFormEvents()
        {
            _pluginInfoForm.ValueChanged += (sender, e) => treeViewMenus.SelectedNode.Text = _pluginInfoForm.Plugin.Text;
            _pluginContainerInfoForm.ValueChanged += (sender, e) => treeViewMenus.SelectedNode.Text = _pluginContainerInfoForm.PluginContainer.Text;
            _pluginInfoForm.Dock = DockStyle.Fill;
            _pluginContainerInfoForm.Dock = DockStyle.Fill;
            splitContainerMenus.Panel2.Controls.Add(_pluginInfoForm);
            splitContainerMenus.Panel2.Controls.Add(_pluginContainerInfoForm);
        }
        /// <summary>
        /// 从目录中读取dll和exe中的插件，加载到插件仓库TreeView控件中
        /// </summary>
        /// <param name="pluginDir">需要管理插件的程序所在的目录</param>
        /// <param name="assemblyFileFilter">插件程序集文件过滤条件，只加载满足此文件过滤条件的插件文件</param>
        private void InitPluginLibrary(string pluginDir = null, string assemblyFileFilter = null)
        {
            PluginDir = string.IsNullOrWhiteSpace(pluginDir) ? AppDomain.CurrentDomain.BaseDirectory : pluginDir;
            AssemblyFilter = assemblyFileFilter;
            treeViewCmds.ReloadPluginLib(PluginDir, AssemblyFilter);
            toolTip1.SetToolTip(lblCmdLib, pluginDir);
        }
        /// <summary>
        /// 显示插件方案列表
        /// </summary>
        /// <param name="plans">插件方案列表</param>
        private void ReloadPluginPlans(IList<IPluginPlan> plans)
        {
            listBoxPlans.RemoveEvent(nameof(listBoxPlans.DeleteItem));
            listBoxPlans.DeleteItem += (sender, e) => { plans.Remove(plans[e.Index]); ReBindingPlans(plans); };
            listBoxPlans.ItemPropertyNames.Add(nameof(IPluginPlan.Id));
            listBoxPlans.DefaultImageIndex = 8;

            if (plans == null || plans.Count == 0) return;
            var selectedIndex = plans.IndexOf(plans.First(v => v.Selected));
            listBoxPlans.SetItemInfo(selectedIndex, 12, false);//设置选用的方案的图标
            ReBindingPlans(plans);
            listBoxPlans.SelectedIndex = selectedIndex;
        }
        /// <summary>
        /// 重新绑定listBoxPlans控件的数据源（插件方案）
        /// </summary>
        private void ReBindingPlans(IList<IPluginPlan> plans)
        {
            listBoxPlans.DataSource = null;
            listBoxPlans.DataSource = new BindingList<IPluginPlan>(plans);
        }
        /// <summary>
        /// 将<see cref="treeViewMenus"/>中对插件方案的修改，同步到<see cref="PluginPlans"/>对象中
        /// </summary>
        private void RefreshPluginPlanFormTreeView()
        {
            var oldPlan = PlanSystem.Plans.FirstOrDefault(v => v.Id == groupBoxPlan.Tag?.ToString());
            if (oldPlan != null)
                oldPlan.Views = treeViewMenus.Nodes.EnumerableValueToObject<IPluginView>().ToList();
        }


        private void CmbSubSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PlanSystem != null)
            {
                RefreshPluginPlanFormTreeView();
                SavePlans?.Invoke(this, new EventArgs());
            }
            PlanSystem = (IPluginPlanSystem)cmbSubSystem.SelectedItem;
            Text = "插件管理 - " + (lblSystemName.Text = PlanSystem.SysInfo.Text);
            ReloadPluginPlans(PlanSystem.Plans);  //加载插件方案列表
        }

        private void BtnSave_Click(object sender, EventArgs e)//保存
        {
            RefreshPluginPlanFormTreeView();
            SavePlans?.Invoke(this, new EventArgs());
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e) => Close();//取消



        #region ListBoxPlans事件
        private void ListBoxPlans_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPluginPlanFormTreeView();//将treeViewMenus中对插件方案的修改，同步到PluginPlans对象中

            if (listBoxPlans.SelectedIndex < 0) return;
            var selectePlan = (IPluginPlan)listBoxPlans.SelectedItem;
            var treeNodes = selectePlan.Views.Select(v => v.EnumerableValueToTreeNode()).ToArray();
            treeViewMenus.Nodes.Clear();
            treeViewMenus.Nodes.AddRange(treeNodes);
            treeViewMenus.ExpandAll();
            if (treeViewMenus.Nodes.Count > 0)
                treeViewMenus.SelectedNode = treeViewMenus.Nodes[0];
            foreach (var node in treeNodes) ResetIamge(node, 0);
            groupBoxPlan.Text = selectePlan.Text;
            groupBoxPlan.Tag = selectePlan.Id;
            btnDeletePlan.Enabled = 删除方案DToolStripMenuItem.Enabled = !selectePlan.Selected;
        }

        private void ListBoxPlans_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            重命名方案NToolStripMenuItem_Click(sender, e);
        }

        private void ListBoxPlans_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                cMenuStripPlans.Show(listBoxPlans, e.Location);
        }
        #endregion


        #region TreeViewMenus事件

        private void TreeViewMenus_AfterSelect(object sender, TreeViewEventArgs e)//根据选择项启用按钮和右键菜单
        {
            var tag = treeViewMenus.SelectedNode.Tag;

            btnInsertSplit.Enabled = 插入分隔符MenuItem.Enabled = 移除分隔符MenuItem.Enabled = tag is IPluginItem;//插入移除分隔符
            btnRemoveMenu.Enabled = 移除MenuItem.Enabled = (tag is IPluginContainer container && !container.IsTopContainer()) || tag is IPluginItem;//移除
            btnUp.Enabled = btnDown.Enabled = 上移MenuItem.Enabled = 下移MenuItem.Enabled = !(tag is IPluginView);  //上移、下移、修改信息
            btnAddMenu.Enabled = 添加子菜单AToolStripMenuItem.Enabled = tag is IPluginContainer container1 && !container1.IsItemContainer();  //添加子菜单

            var selectedNode = treeViewMenus.SelectedNode;
            if (selectedNode == null) return;
            if (selectedNode.Tag is IPluginItem plugin)
            {
                _pluginContainerInfoForm.Hide();
                _pluginInfoForm.Show();
                _pluginInfoForm.CfgDir = CfgDir;
                _pluginInfoForm.Plugin = plugin;
            }
            else if (selectedNode.Tag is IPluginContainer pluginContainer)
            {
                _pluginContainerInfoForm.Show();
                _pluginInfoForm.Hide();
                _pluginContainerInfoForm.PluginContainer = pluginContainer;
            }
            else if (selectedNode.Tag is IPluginView)
            {
                _pluginContainerInfoForm.Hide();
                _pluginInfoForm.Hide();
            }
        }

        private void TreeViewMenus_MouseUp(object sender, MouseEventArgs e)//显示右键菜单
        {
            if (e.Button == MouseButtons.Right && treeViewMenus.SelectedNode != null)
                cMenuStripViewMenus.Show(treeViewMenus, e.Location);
        }

        private void TreeViewMenus_DragEnter(object sender, DragEventArgs e)//将对象拖入控件的边界时
        {
            e.Effect = e.Data.GetData(typeof(TreeNode)) != null ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void TreeViewMenus_DragDrop(object sender, DragEventArgs e)//在完成拖放操作时
        {
            var targeTreeView = (TreeView)sender;
            var sourceNode = (TreeNode)e.Data.GetData(typeof(TreeNode));//获得拖动过来的节点
            if (sourceNode == null) return;

            var point = targeTreeView.PointToClient(new Point { X = e.X, Y = e.Y });
            var targetNode = targeTreeView.GetNodeAt(point);//根据鼠标坐标获得目标节点
            if (targetNode == null) return;

            var tag = sourceNode.Tag;
            var pluginItem = tag is ICommand cmd ? PluginItem.FromCommand(cmd, PluginDir) : tag as PluginItem;
            //特殊处理：由于Assembly.LoadFrom方法反射程序集A会被缓存，
            //随后再反射同名但路径不同的程序集B，因缓存原因实际反射的仍是程序集A的奇葩机制，目前未找到解决方案，此处只能强行替代程序集的路径
            pluginItem.AssemblyPath = Path.Combine(PluginDir, Path.GetFileName(pluginItem.AssemblyPath));
            pluginItem.AssemblyPath = PathEx.GetRelativePath(pluginItem.AssemblyPath, PluginDir);

            var insertNode = pluginItem.CreateNode(4);
            AppendToMenu(targetNode, insertNode);

            if (sourceNode.TreeView == targeTreeView)
                sourceNode.Remove();
            targeTreeView.SelectedNode = insertNode;
        }

        private void AppendToMenu(TreeNode targetNode, TreeNode insertNode)
        {
            if (targetNode.Tag is IPluginItem)
                targetNode.Parent.Nodes.Insert(targetNode.Index + 1, insertNode);//目标节点为插件节点, 直接在之后插入
            else if (targetNode.Tag is IPluginContainer container)
            {
                switch (container.Type)
                {
                    case EPluginContainerType.MenuStrip:
                    case EPluginContainerType.ToolBar:
                    case EPluginContainerType.ContextMenu:
                    case EPluginContainerType.RibbonGroup:
                        targetNode.Nodes.Insert(0, insertNode);
                        break;
                    case EPluginContainerType.RibbonMenu:
                        if (targetNode.Nodes.Count == 0)
                            targetNode.Nodes.Add(new PluginContainer(EPluginContainerType.RibbonPage).CreateNode(2));
                        if (targetNode.Nodes[0].Nodes.Count == 0)
                            targetNode.Nodes[0].Nodes.Add(new PluginContainer(EPluginContainerType.RibbonGroup).CreateNode(3));
                        targetNode.Nodes[0].Nodes[0].Nodes.Insert(0, insertNode);
                        break;
                    case EPluginContainerType.RibbonPage:
                        if (targetNode.Nodes.Count == 0)
                            targetNode.Nodes.Add(new PluginContainer(EPluginContainerType.RibbonGroup).CreateNode(3));
                        targetNode.Nodes[0].Nodes.Insert(0, insertNode);
                        break;
                }
            }
            targetNode.ExpandAll();
        }

        private void ResetIamge(TreeNode treeNode, int imageIndex)
        {
            treeNode.ImageIndex = imageIndex;
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Tag is IPluginView) ResetIamge(node, 0);
                else if (node.Tag is IPluginContainer container)
                {
                    if (container.IsTopContainer()) ResetIamge(node, 1);
                    else if (container.Type == EPluginContainerType.RibbonPage) ResetIamge(node, 2);
                    else if (container.Type == EPluginContainerType.RibbonGroup) ResetIamge(node, 3);
                }
                else if (node.Tag is IPluginItem) ResetIamge(node, 4);
            }
            treeViewMenus.Invalidate();
            treeViewMenus.Refresh();
        }
        #endregion


        #region treeViewCmds事件
        private void TreeViewCmds_ItemDrag(object sender, ItemDragEventArgs e)//当用户开始拖动命令节点
        {
            var selectNode = e.Item as TreeNode;
            ((TreeView)sender).SelectedNode = selectNode;
            if (selectNode.Tag is ICommand || selectNode.Tag is IPluginItem)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void TreeViewCmds_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)//点击节点时，显示程序集/分组/命令信息
        {
            var sb = new StringBuilder($"【{e.Node.Text}】\r\n");
            sb.AppendLine(e.Node.Tag?.ToString());
            if (e.Node.Tag is ICommand cmd)
            {
                var cmdType = cmd.GetType();
                sb.AppendLine($"位置：{cmdType.Assembly.Location}");
                foreach (var property in cmdType.GetProperties())
                {
                    var descAttrs = (DescriptionAttribute[])property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (descAttrs.Length > 0)
                        sb.AppendLine($"{descAttrs[0].Description}：{property.GetValue(cmd, null)}");
                }
            }
            else if (e.Node.Tag is Assembly assembly)
                sb.AppendLine($"位置：{assembly.Location}");
            txtPluginInfo.Text = sb.ToString();
        }
        #endregion


        #region 插件方案列表的右键菜单
        private void 新建方案NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var plans = PlanSystem.Plans;
            var lastChars = plans.Select(v => v.Name.Last()).Where(c => int.TryParse(c.ToString(), out _));
            var newPlanName = lastChars.Count() > 0 ? "插件方案" + (lastChars.Select(v => int.Parse(v.ToString())).Max() + 1) : "插件方案1";
            var form = new InputForm("新建插件方案", "请设置一个方案名称", "确定", newPlanName);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var planName = form.KeyWord;
                if (string.IsNullOrWhiteSpace(planName))
                {
                    MessageBox.Show($@"插件方案名称不能为空或者空白字符", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (listBoxPlans.Items.Cast<PluginPlan>().Select(v => v.Name).Contains(planName))
                {
                    MessageBox.Show($@"已存在名为“{form.KeyWord}”的插件方案，请重新填写方案名称！", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                plans.Add(CreatePluginPlan(AppDir, planName, false));
                ReBindingPlans(plans);
            }
        }

        private void 删除方案DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxPlans.SelectedIndex < 1) return;
            var dialogResult = MessageBox.Show("确定要删除此插件方案？", Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                PlanSystem.Plans.Remove((IPluginPlan)listBoxPlans.SelectedItem);
                ReBindingPlans(PlanSystem.Plans);
            }
        }

        private void 复制方案CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlanSystem.Plans.Add(((PluginPlan)listBoxPlans.SelectedItem).Copy());
            ReBindingPlans(PlanSystem.Plans);
        }

        private void 选用此方案SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxPlans.SelectedIndex < 0) return;

            listBoxPlans.Items.Cast<IPluginPlan>().ToList().ForEach(v => v.Selected = false);
            ((IPluginPlan)listBoxPlans.SelectedItem).Selected = true;

            for (int i = 0; i < listBoxPlans.Items.Count; i++)
                listBoxPlans.SetItemInfo(i, 8, true);
            listBoxPlans.SetItemInfo(listBoxPlans.SelectedIndex, 12, false);
            listBoxPlans.Invalidate();
        }

        private void 重命名方案NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxPlans.SelectedIndex < 0) return;
            var plan = (IPluginPlan)listBoxPlans.SelectedItem;
            var form = new InputForm("重命名插件方案", "请修改方案名称", "确定", plan.Name);
            if (form.ShowDialog() == DialogResult.OK)
            {
                plan.Text = form.KeyWord;
                ReBindingPlans(PlanSystem.Plans);
            }
        }
        #endregion


        #region 菜单列表的右键菜单
        private void 上移OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewMenus.SelectedNode;
            if (selectedNode.PrevNode == null || selectedNode.Tag is IPluginView)
                return;
            var targetNode = (TreeNode)selectedNode.Clone();
            selectedNode.Parent.Nodes.Insert(selectedNode.PrevNode.Index, targetNode);
            selectedNode.Remove();
            treeViewMenus.SelectedNode = targetNode;
        }

        private void 下移PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewMenus.SelectedNode;
            if (selectedNode.NextNode == null || selectedNode.Tag is IPluginView)
                return;
            var targetNode = (TreeNode)selectedNode.Clone();
            selectedNode.Parent.Nodes.Insert(selectedNode.NextNode.Index + 1, targetNode);
            selectedNode.Remove();
            treeViewMenus.SelectedNode = targetNode;
        }

        private void 添加子菜单AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = treeViewMenus.SelectedNode;
            var tag = selectedNode.Tag;
            if (tag is IPluginContainer container)
            {
                switch (container.Type)
                {
                    case EPluginContainerType.MenuStrip:
                    case EPluginContainerType.SubMenu:
                        selectedNode.Nodes.Add(new PluginContainer(EPluginContainerType.SubMenu).CreateNode(1));
                        break;
                    case EPluginContainerType.RibbonMenu:
                        selectedNode.Nodes.Add(new PluginContainer(EPluginContainerType.RibbonPage).CreateNode(2));
                        break;
                    case EPluginContainerType.RibbonPage:
                        selectedNode.Nodes.Add(new PluginContainer(EPluginContainerType.RibbonGroup).CreateNode(3));
                        break;
                }
            }
            selectedNode.Expand();
        }

        private void 插入分隔符IToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pluginItem = (IPluginItem)treeViewMenus.SelectedNode?.Tag;
            if (pluginItem != null && !pluginItem.AppendSplit)
            {
                pluginItem.AppendSplit = true;
                treeViewMenus.SelectedNode.Text += "（|）";
            }
        }

        private void 移除分隔符OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((IPluginItem)treeViewMenus.SelectedNode.Tag).AppendSplit = false;
            treeViewMenus.SelectedNode.Text = treeViewMenus.SelectedNode.Text.Replace("（|）", "");
        }

        private void 移除RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(treeViewMenus.SelectedNode.Tag is IPluginView))
                treeViewMenus.SelectedNode.Remove();
        }

        private void 展开全部菜单EToolStripMenuItem_Click(object sender, EventArgs e) => treeViewMenus.ExpandAll();

        private void 折叠全部菜单FToolStripMenuItem_Click(object sender, EventArgs e) => treeViewMenus.CollapseAll();
        #endregion


        #region 命令仓库的右键菜单
        private void 加入到菜单AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuNode = treeViewMenus.SelectedNode;
            var cmdNode = treeViewCmds.SelectedNode;
            if (menuNode == null || cmdNode == null)
                return;

            var cmds = new List<ICommand>();
            if (cmdNode.Tag is Assembly)
                cmds = cmdNode.Nodes.Cast<TreeNode>().SelectMany(node => node.Nodes.Cast<TreeNode>().Select(n => (ICommand)n.Tag)).ToList();
            else if (cmdNode.Tag is string)
                cmds = cmdNode.Nodes.Cast<TreeNode>().Select(n => (ICommand)n.Tag).ToList();
            else if (cmdNode.Tag is ICommand)
                cmds.Add((ICommand)cmdNode.Tag);
            //特殊处理：由于Assembly.LoadFrom方法反射程序集A会被缓存，
            //随后再反射同名但路径不同的程序集B，因缓存原因实际反射的仍是程序集A的奇葩机制，目前未找到解决方案，此处只能强行替代程序集的路径
            var newNodes = cmds.Select(cmd =>
            {
                var plugin = PluginItem.FromCommand(cmd, AppDir);
                plugin.AssemblyPath = Path.Combine(PluginDir, Path.GetFileName(plugin.AssemblyPath));
                return plugin.CreateNode(4);
            });
            foreach (var node in newNodes)
                AppendToMenu(menuNode, node);
        }

        private void 重新加载插件RToolStripMenuItem_Click(object sender, EventArgs e) => InitPluginLibrary(PluginDir, AssemblyFilter);

        private void 全部展开EToolStripMenuItem_Click(object sender, EventArgs e) => treeViewCmds.ExpandAll();

        private void 全部折叠FToolStripMenuItem_Click(object sender, EventArgs e) => treeViewCmds.CollapseAll();
        #endregion
    }
}
