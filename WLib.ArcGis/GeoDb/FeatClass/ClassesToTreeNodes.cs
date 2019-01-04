using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.WorkSpace;

namespace WLib.ArcGis.GeoDb.FeatClass
{
    /// <summary>
    /// 将数据源存储的要素类结果转为TreeNode形式的树结构信息
    /// </summary>
    public class ClassesToTreeNodes
    {
        /// <summary>
        /// 获取指定路径或连接字符串对应数据库下，以数据库名为根节点，要素集或要素类为子节点的树结构信息
        /// （如果节点为要素类则它的Tag属性为1，否则为0）
        /// </summary>
        /// <param name="strConnOrPath">路径或连接字符串，可以是gdb文件夹路径、shp所在目录、mdb文件路径或sde数据库连接字符串</param>
        /// <returns></returns>
        public static TreeNode GetDbSourceTreeNodes(string strConnOrPath)
        {
            var fileName = System.IO.Path.GetFileNameWithoutExtension(strConnOrPath);
            var rootNode = new TreeNode(fileName) { Tag = 0 };
            var workspace = GetWorkspace.GetWorkSpace(strConnOrPath);

            //不放到DataSet的要素数据
            var dataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureClass);
            IFeatureClass featureClass;
            while ((featureClass = dataset.Next() as IFeatureClass) != null)
            {
                rootNode.Nodes.Add(new TreeNode(featureClass.AliasName) { Tag = 1 });
            }

            //放到Dataset里的要素
            var enumDataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
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
    }
}
