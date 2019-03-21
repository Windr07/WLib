using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using WLib.ArcGis.GeoDb.WorkSpace;
using WLib.Attributes;

namespace WLib.ArcGis.Analysis.OnClass
{
    /// <summary>
    /// 提供空间分析相关操作方法
    /// </summary>
    public class SpatialAnalysisOpt
    {
        /// <summary>
        /// 相交分析，返回相交部分的要素（注意输入要素类和叠加要素类不能有空几何等问题）
        /// </summary>
        /// <param name="inClass">输入的要素类</param>
        /// <param name="overlayClass">叠加的要素类</param>
        /// <param name="outPath">保存分析结果的工作空间路径</param>
        /// <param name="outName">保存分析结果的要素类名称</param>
        /// <returns></returns>
        public static IFeatureClass Intersect(IFeatureClass inClass, IFeatureClass overlayClass, string outPath, string outName)
        {
            var workspaceType = GetWorkspace.GetDefaultWorkspaceType(outPath);
            if (workspaceType == EWorkspaceType.Default)
                throw new Exception($"工作空间路径(outPath)不存在！{outPath} 该路径必须是已存在的mdb文件路径，或shp所在文件夹路径，或gdb文件夹路径");

            IFeatureClassName outClassName = new FeatureClassNameClass
            {
                ShapeType = inClass.ShapeType,
                ShapeFieldName = inClass.ShapeFieldName,
                FeatureType = esriFeatureType.esriFTSimple
            };
            IWorkspaceName workspaceName = new WorkspaceNameClass
            {
                WorkspaceFactoryProgID = workspaceType.GetDescription(1),
                PathName = outPath
            };
            IDatasetName datasetName = (IDatasetName)outClassName;
            datasetName.Name = outName;
            datasetName.WorkspaceName = workspaceName;

            return new BasicGeoprocessorClass().Intersect((ITable)inClass, false, (ITable)overlayClass, false, 0.01, outClassName);
        }
    }
}
