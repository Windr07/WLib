/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Model;

namespace WLib.ArcGis.GeoDatabase.FeatClass
{
    /// <summary>
    /// 提供对要素类数据的获取、输出、复制、创建、增、删、改、查、筛选、检查、重命名等方法
    /// </summary>
    public static partial class FeatureClassEx
    {
        /// <summary>
        /// 获取指定路径或连接字符串对应数据库下，以数据库（工作空间）名为根节点，要素集或要素类为子节点的树结构信息
        /// <para>在返回的<see cref="TreeNode"/>各级节点中，Tag属性为1代表要素类，0为要素数据集，-1为工作空间</para>
        /// </summary>
        /// <param name="strConnOrPath">路径或连接字符串，可以是gdb文件夹路径、shp所在目录、mdb文件路径或sde数据库连接字符串</param>
        /// <returns></returns>
        public static TreeNode DbToTreeNodes(string strConnOrPath)
        {
            SplitPath(strConnOrPath, out var workspacePath, out _, out _);
            var workspaceName = Directory.Exists(workspacePath) ? new DirectoryInfo(workspacePath).Name : Path.GetFileNameWithoutExtension(workspacePath);
            var rootNode = new TreeNode(workspaceName) { Tag = -1 };
            var workspace = WorkspaceEx.GetWorkSpace(strConnOrPath);

            //不放到dataset的要素数据
            var dataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            IFeatureClass featureClass;
            while ((featureClass = dataset.Next() as IFeatureClass) != null)
            {
                rootNode.Nodes.Add(new TreeNode(featureClass.AliasName) { Tag = 1 });
            }

            //放到dataset里的要素
            var enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IFeatureDataset featureDataset;
            while ((featureDataset = enumDataset.Next() as IFeatureDataset) != null)//遍历数据集
            {
                var dataSetNode = new TreeNode(featureDataset.Name) { Tag = 0 };
                var enumFeatureClass = ((IFeatureClassContainer)featureDataset).Classes;
                while ((featureClass = enumFeatureClass.Next()) != null)//在每一个数据集中遍历数据层IFeatureClass
                {
                    dataSetNode.Nodes.Add(new TreeNode(featureClass.AliasName) { Tag = 1 });
                }
                rootNode.Nodes.Add(dataSetNode);
            }
            return rootNode;
        }

        /// <summary>
        /// 获取指定路径或连接字符串对应数据库下，以数据库（工作空间）名为根节点，要素集或要素类为子节点的树结构信息
        /// <para>在返回的<see cref="NodeObject"/>对象中，NodeTag属性为1代表要素类，0为要素数据集，-1为工作空间</para>
        /// </summary>
        /// <param name="strConnOrPath">路径或连接字符串，可以是gdb文件夹路径、shp所在目录、mdb文件路径或sde数据库连接字符串</param>
        /// <param name="getTopNode">是否获取顶层节点，即工作空间节点</param>
        /// <returns></returns>
        public static List<NodeObject> DbToNodeObjects(string strConnOrPath, bool getTopNode = true)
        {
            var nodes = new List<NodeObject>();

            int id = 0;
            if (getTopNode)
            {
                SplitPath(strConnOrPath, out var workspacePath, out _, out _);
                var workspaceName = Directory.Exists(workspacePath) ? new DirectoryInfo(workspacePath).Name : Path.GetFileNameWithoutExtension(workspacePath);
                nodes.Add(new NodeObject(id++, -1, workspaceName, 0, -1, workspaceName));
            }
            var workspace = WorkspaceEx.GetWorkSpace(strConnOrPath);

            //不放到dataset的要素数据
            var dataset = workspace.Datasets[esriDatasetType.esriDTFeatureClass];
            IFeatureClass featureClass;
            while ((featureClass = dataset.Next() as IFeatureClass) != null)
            {
                nodes.Add(new NodeObject(id++, 0, featureClass.AliasName, 0, 1, featureClass.GetName()));
            }

            //放到dataset里的要素
            var enumDataset = workspace.Datasets[esriDatasetType.esriDTFeatureDataset];
            IFeatureDataset featureDataset;
            while ((featureDataset = enumDataset.Next() as IFeatureDataset) != null)//遍历数据集
            {
                int datasetId = id++;
                nodes.Add(new NodeObject(datasetId, 0, featureDataset.Name, 0, 0, featureClass.GetName()));
                var enumFeatureClass = ((IFeatureClassContainer)featureDataset).Classes;
                while ((featureClass = enumFeatureClass.Next()) != null)//在每一个数据集中遍历数据层IFeatureClass
                {
                    nodes.Add(new NodeObject(id++, datasetId, featureClass.AliasName, 0, 1, featureClass.GetName()));
                }
            }
            return nodes;
        }
    }
}
