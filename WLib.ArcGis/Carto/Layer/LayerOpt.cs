/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDatabase.WorkSpace;
using WLib.Attributes;

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
        public static string GetWorkspacePath(this ILayer layer)
        {
            IDataLayer dataLayer = (IDataLayer)layer;
            IDatasetName datasetName = dataLayer.DataSourceName as IDatasetName;
            IWorkspaceName workspaceName = datasetName?.WorkspaceName;
            return workspaceName?.PathName;
        }
        /// <summary>
        /// 获取图层数据源完整路径
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static string GetSourcePath(this ILayer layer)
        {
            switch (layer)
            {
                case IFeatureLayer featureLayer:
                    if (featureLayer is IDataLayer dataLayer)
                    {
                        IDatasetName datasetName = (IDatasetName)dataLayer.DataSourceName;
                        var dataSourceType = featureLayer.DataSourceType.ToLower();
                        var extension = string.Empty;
                        if (dataSourceType.Contains("shapefile"))
                            extension = ".shp";
                        else if (dataSourceType.Contains("cad"))
                            extension = ".dwg";
                        return Path.Combine(datasetName.WorkspaceName.PathName, datasetName.Name + extension);
                    }
                    break;
                case IRasterLayer rasterLayer:
                    return rasterLayer.FilePath;
                case IGroupLayer groupLayer:
                    throw new Exception("该图层对象为图层组(IGroupLayer)，无法获取图层组的数据源，请指定到具体的图层");
                default:
                    throw new NotImplementedException("图层不是要素图层(IFeatureLayer)，也不是栅格图层(IRasterLayer)，未实现其他类型图层的数据源获取");
            }
            return null;
        }


        /// <summary>
        /// 设置图层数据源
        /// </summary>
        /// <param name="layer">设置数据源的图层</param>
        /// <param name="sourcePath">
        /// 数据源完整路径，可以是以下一种情况：
        /// ①shp文件路径；
        /// ②mdb文件路径\[数据集名]\要素类或栅格数据集名；
        /// ③gdb目录\[数据集名]\要素类或栅格数据集名；
        /// ④dwg数据集文件路径\要素类名；
        /// ⑤栅格数据集文件路径
        /// </param>
        public static void SetSourcePath(this ILayer layer, string sourcePath)
        {
            string workspacePath = null, datasetName = null, className = null;
            if (File.Exists(sourcePath))
            {
                //&& Path.GetExtension(sourcePath) == ".shp" //.jpg.tiff.bmp.emf.png.gif.pdf.eps.ai.svg
                workspacePath = Path.GetDirectoryName(sourcePath);
                className = Path.GetFileNameWithoutExtension(sourcePath);
            }
            else if (sourcePath.Contains(".dwg"))
            {
                var datasetPath = sourcePath.Substring(0, sourcePath.LastIndexOf(".dwg", StringComparison.OrdinalIgnoreCase) + 4);
                workspacePath = Path.GetDirectoryName(datasetPath);
                datasetName = Path.GetFileNameWithoutExtension(sourcePath);
                className = sourcePath.Replace(datasetPath, "");
            }
            else
            {
                int index;
                if ((index = sourcePath.LastIndexOf(".gdb", StringComparison.OrdinalIgnoreCase)) > -1)
                    workspacePath = sourcePath.Substring(0, index + 4);
                else if ((index = sourcePath.LastIndexOf(".mdb", StringComparison.OrdinalIgnoreCase)) > -1)
                    workspacePath = sourcePath.Substring(0, index + 4);

                //按照"\"或者"/"分割子路径，获得要素集名称、要素类名称
                var subPath = sourcePath.Replace(workspacePath, "");
                var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1)
                    className = names[0];
                if (names.Length == 2)
                {
                    datasetName = names[0];
                    className = names[1];
                }
            }
            SetSourcePath(layer, workspacePath, datasetName, className);
        }
        /// <summary>
        /// 设置图层数据源
        /// </summary>
        /// <param name="layer">设置数据源的图层</param>
        /// <param name="workspacePath">工作空间路径</param>
        /// <param name="dataSetName">数据集名称，数据源不在数据集中则应为null</param>
        /// <param name="objectName">图层关联对象的名称，即要素类名称或栅格数据集名称</param>
        public static void SetSourcePath(this ILayer layer, string workspacePath, string dataSetName, string objectName)
        {
            if (!GetWorkspace.IsWorkspacePath(workspacePath) &&
                !GetWorkspace.IsConnectionString(workspacePath))
                throw new ArgumentException($"找不到数据源({workspacePath})，请指定正确的数据源！");

            switch (layer)
            {
                case IFeatureLayer featureLayer:
                    if (featureLayer is IDataLayer dataLayer)
                    {
                        IDatasetName datasetName = (IDatasetName)dataLayer.DataSourceName;
                        EWorkspaceType eWorkspaceType = GetWorkspace.GetDefaultWorkspaceType(workspacePath);
                        datasetName.WorkspaceName.WorkspaceFactoryProgID = eWorkspaceType.GetDescription(1);
                        datasetName.WorkspaceName.PathName = workspacePath;
                        //TODO:
                        if (!string.IsNullOrWhiteSpace(dataSetName)) datasetName.Category = dataSetName;
                        if (!string.IsNullOrWhiteSpace(objectName)) datasetName.Name = objectName;
                    }
                    break;
                case IRasterLayer rasterLayer:
                    objectName = string.IsNullOrWhiteSpace(objectName)
                        ? ((rasterLayer as IDataLayer)?.DataSourceName as IDatasetName)?.Name
                        : objectName;
                    if (objectName == null)
                        throw new Exception($"指定数据源名称（参数{nameof(objectName)}）不能为空！");
                    rasterLayer.CreateFromFilePath(!string.IsNullOrWhiteSpace(dataSetName)
                        ? Path.Combine(workspacePath, dataSetName, objectName)
                        : Path.Combine(workspacePath, objectName));
                    break;
                case IGroupLayer groupLayer:
                    throw new Exception("该图层对象为图层组(IGroupLayer)，无法设置图层组的数据源，请指定到具体的图层");
                default:
                    throw new NotImplementedException(
                        "图层不是要素图层(IFeatureLayer)，也不是栅格图层(IRasterLayer)，未实现其他类型图层的数据源设置");
            }
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
