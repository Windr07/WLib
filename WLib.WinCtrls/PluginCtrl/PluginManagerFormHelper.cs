using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WLib.Plugins;
using WLib.Plugins.Interface;
using WLib.Reflection;

namespace WLib.WinCtrls.PluginCtrl
{
    public static class PluginManagerFormHelper
    {
        /// <summary>
        /// 根据列表项信息创建节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="imgIndex">图片索引</param>
        /// <returns></returns>
        public static TreeNode CreateNode(this IItemBase item, int imgIndex)
        {
            var text = item is IPluginItem pluginItem && pluginItem.AppendSplit ? item.Text + "（|）" : item.Text;
            return new TreeNode { Name = item.Name, Text = text, Tag = item, ImageIndex = imgIndex };
        }
        /// <summary>
        /// 将程序集文件（dll、exe）和其中实现ICommand接口的类的信息，按照树状结构生成TreeNode节点
        /// </summary>
        /// <param name="filePaths">dll、exe文件路径</param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> CreateCommandAssemblyTreeNodes(IEnumerable<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                var assembly = Assembly.LoadFrom(filePath);
                var assemblyNode = new TreeNode { Text = assembly.GetName().Name, Tag = assembly, ImageIndex = 6 };//程序集节点

                var commands = assembly.GetInterfaceAchieveTypes<ICommand>();
                var cmdGroups = commands.GroupBy(v => v.Category);
                foreach (var cmdGroup in cmdGroups)
                {
                    var groupNode = new TreeNode { Text = cmdGroup.Key, Tag = cmdGroup.Key, ImageIndex = 6 };//命令分组节点
                    foreach (var cmd in cmdGroup)
                        groupNode.Nodes.Add(new TreeNode { Text = cmd.Text, Tag = cmd, ImageIndex = 7 });//命令节点

                    assemblyNode.Nodes.Add(groupNode);
                }
                yield return assemblyNode;
            }
        }
        /// <summary>
        /// 从目录中读取dll和exe中的插件，加载到插件仓库中
        /// </summary>
        /// <param name="pluginDir">需要管理插件的程序所在的目录</param>
        /// <param name="assemblyFileFilter">插件程序集文件过滤条件，只加载满足此文件过滤条件的插件文件</param>
        public static void ReloadPluginLib(this TreeView treeViewCmds, string pluginDir = null, string assemblyFileFilter = null)
        {
            var filePaths = PluginHelper.GetAssemblyFiles(pluginDir, assemblyFileFilter);//获取全部dll和exe文件路径
            var treeNodes = CreateCommandAssemblyTreeNodes(filePaths);//获得dll和exe中的插件信息，生成树状节点
            treeViewCmds.Nodes.Clear();
            treeViewCmds.Nodes.AddRange(treeNodes.ToArray());
            treeViewCmds.ExpandAll();
        }
    }
}
