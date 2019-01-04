/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.Carto.Layer
{
    /// <summary>
    /// 图层相关操作
    /// </summary>
    public static class LayerOpt
    {
        /// <summary>
        /// 获取图层数据源的工作空间路径
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static string GetLayerWorkspacePath(this ILayer layer)
        {
            IDataLayer dataLayer = (IDataLayer)layer;
            IDatasetName datasetName = dataLayer.DataSourceName as IDatasetName;
            IWorkspaceName workspaceName = datasetName?.WorkspaceName;
            return workspaceName?.PathName;
        }
        /// <summary>
        /// 获取图层的数据源路径（全路径）
        /// </summary>
        /// <param name="layer">图层</param>
        /// <returns>图层的数据源路径，数据源未设置则返回null</returns>
        public static string GetLayerDataSourcePath(this ILayer layer)
        {
            if (layer is IDataLayer dataLayer && dataLayer.DataSourceName is IDatasetName datasetName)
            {
                if (datasetName.Name != null && datasetName.WorkspaceName.PathName != null)
                    return $"{datasetName.WorkspaceName.PathName}\\{datasetName.Name}";
            }
            return null;
        }



        /// <summary>
        /// 遍历组合图层中的各级子图层，对子图层执行操作
        /// </summary>
        /// <param name="layer">组合图层</param>
        /// <param name="dealWithLayer"></param>
        public static void EnumLayerInGroupLayer(this ILayer layer, Action<ILayer> dealWithLayer)
        {
            if (!(layer is ICompositeLayer groupLayer))
                return;

            for (int i = 0; i < groupLayer.Count; i++)
            {
                ILayer tmpLayer = groupLayer.get_Layer(i);
                if (tmpLayer is IGroupLayer)
                    EnumLayerInGroupLayer(tmpLayer, dealWithLayer);
                else
                    dealWithLayer(tmpLayer);
            }
        }
    }
}
