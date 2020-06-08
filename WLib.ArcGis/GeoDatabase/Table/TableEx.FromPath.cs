using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WLib.ArcGis.GeoDatabase.FeatDataset;
using WLib.ArcGis.GeoDatabase.WorkSpace;

namespace WLib.ArcGis.GeoDatabase.Table
{
    /// <summary>
    /// 提供对表格【<see cref="ITable"/>】数据的获取、增、删、改、查、复制、检查、重命名等方法
    /// </summary>
    public static partial class TableEx
    {
        /// <summary>
        /// 从指定路径（或连接字符串）中获取表格
        /// <para>①shp或dbf路径：返回该shp或存储的要素类或表格；</para>
        /// <para>②mdb路径：返回该mdb数据库第一个表格；</para>
        /// <para>③dwg路径：返回该dwg数据集第一个表格；</para>
        /// <para>④shp或dbf目录：返回目录下第一个dbf文件存储的表格；</para>
        /// <para>⑤gdb目录：返回gdb数据库第一个表格；</para>
        /// <para>⑥mdb文件路径[\DatasetName]\objectName：返回mdb数据库中指定名称或别名的表格；</para>
        /// <para>⑦gdb目录[\DatasetName]\objectName：返回gdb数据库中指定名称或别名的表格；</para>
        /// <para>⑧sde或oleDb或sql连接字符串：返回数据库中的第一个表格；</para>
        /// </summary>
        /// <param name="connStrOrPath">路径或连接字符串</param>
        /// <returns></returns>
        public static ITable FromPath(string connStrOrPath)
        {
            if (WorkspaceEx.IsConnectionString(connStrOrPath))
                return FirstFromConnString(connStrOrPath);

            ITable table = null;
            SplitPath(connStrOrPath, out var workspacePath, out var datasetName, out var objectName);
            if (!string.IsNullOrWhiteSpace(workspacePath))
            {
                IWorkspace workspace = WorkspaceEx.GetWorkSpace(workspacePath);
                if (!string.IsNullOrWhiteSpace(datasetName))
                {
                    var dataset = workspace.GetFeatureDataset(datasetName);
                    if (!string.IsNullOrWhiteSpace(datasetName))
                        table = dataset.GetFeatureClassByName(objectName) as ITable;
                    else
                        table = dataset.GetFirstFeatureClass() as ITable;
                    if (dataset != null) Marshal.ReleaseComObject(dataset);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(objectName))
                        table = workspace.GetITableByNameExtent(objectName);
                    else
                        table = workspace.GetFirstTable();
                }
                if (workspace != null) Marshal.ReleaseComObject(workspace);
            }
            return table;
        }

        /// <summary>
        /// 获取指定连接字符串对应数据库存储的第一个表格
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns>返回表格，若工作空间没有表格则返回null</returns>
        private static ITable FirstFromConnString(string connString)
        {
            var workspace = WorkspaceEx.GetWorkSpace(connString);
            var table = workspace.GetFirstTable();
            Marshal.ReleaseComObject(workspace);
            return table;
        }

        /// <summary>
        /// 从完整的表格或表格路径中，获取工作空间路径、数据集名称、表格或表格名称
        /// <para>注意：路径应为表格或表格或工作空间路径，指向数据集的路径(dwg除外)是无法识别的</para>
        /// <para>支持的数据类型包括gdb、mdb、shp、dbf、dwg等</para>
        /// </summary>
        /// <param name="fullPath">格式为“工作空间路径[\要素集名称]\表格或表格名称”的路径</param>
        /// <param name="workspacePath">工作空间路径</param>
        /// <param name="datasetName">数据集名称（可能为空）</param>
        /// <param name="objectName">表格或表格名称，如果数据为shp则包含.shp后缀</param>
        /// <returns></returns>
        public static void SplitPath(string fullPath, out string workspacePath, out string datasetName, out string objectName)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                throw new Exception($"路径“{nameof(fullPath)}”不能为空！");
            workspacePath = null; datasetName = null; objectName = null;
            var tmpFullPath = fullPath.ToLower();
            foreach (var tmpExtension in new[] { ".gdb", ".mdb" })
            {
                int index;
                if ((index = tmpFullPath.LastIndexOf(tmpExtension, StringComparison.OrdinalIgnoreCase)) > -1)
                {
                    workspacePath = fullPath.Substring(0, index + 4);
                    break;
                }
            }
            var extension = Path.GetExtension(tmpFullPath);
            if (extension == ".shp" || extension == ".dbf" || extension == ".dwg")
                workspacePath = Path.GetDirectoryName(fullPath);
            if (!string.IsNullOrWhiteSpace(workspacePath))//按照"\"或者"/"分割子路径，获得要素集名称、表格名称
            {
                var subPath = fullPath.Replace(workspacePath, "");
                var names = subPath.Split(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 1)
                {
                    if (extension == ".dwg") { datasetName = names[0]; objectName = null; }
                    else { datasetName = null; objectName = names[0]; }
                }
                if (names.Length == 2) { datasetName = names[0]; objectName = names[1]; }
            }
            else
            {
                if (Directory.Exists(fullPath))
                    workspacePath = fullPath;
            }
        }
    }
}
