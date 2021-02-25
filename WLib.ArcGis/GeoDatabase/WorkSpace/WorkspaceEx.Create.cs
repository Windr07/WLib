/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using WLib.Attributes.Description;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// 提供对工作空间的扩展操作
    /// <para>包括工作空间的增删改查、打开、编辑、要素类/数据集/表格查询创建删除等</para>
    /// </summary>
    public static partial class WorkspaceEx
    {
        /// <summary>
        /// 创建内存工作空间
        /// </summary>
        /// <param name="strWorkspaceName">内存工作空间名称</param>
        /// <returns></returns>
        public static IWorkspace CreateInMemoryWorkspace(string strWorkspaceName)
        {
            IWorkspaceFactory workspaceFactory = new InMemoryWorkspaceFactoryClass();
            IWorkspaceName workspaceName = workspaceFactory.Create("", strWorkspaceName, null, 0);
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            Marshal.ReleaseComObject(workspaceFactory);
            return workspace;
        }

        /// <summary>
        /// 创建工作空间（地理数据库）
        /// </summary>
        /// <param name="parentDirectory">
        /// 工作空间所在的父目录，具体为：
        /// <para>①shp、dwg、txt、xls、xlsx、栅格文件所在文件夹再上一层目录，e.g. c:\gis；</para>
        /// <para>②mdb文件所在文件夹，e.g. c:\gis；</para>
        /// <para>③gdb文件夹的上一层目录，e.g. c:\gis；</para>
        /// <para>④内存工作空间位置，不需要指定，e.g. ""</para>
        /// </param>
        /// <param name="strWorkspaceName">
        /// 工作空间名称，具体为：
        ///  <para>①shp、dwg、txt、xls、xlsx、栅格文件所在文件夹，e.g. testFolder；</para>
        ///  <para>②mdb文件名，e.g. testMdbFile(.mdb)；</para>
        ///  <para>③gdb文件夹名，e.g. testGdb(.gdb)；</para>
        /// <para>④内存工作空间名，e.g. "InMemoryWorkspace"</para>
        /// </param>
        /// <returns></returns>
        public static IWorkspace CreateWorkspace(string parentDirectory, string strWorkspaceName)
            => CreateWorkspace(EWorkspaceType.Default, parentDirectory, strWorkspaceName);
        /// <summary>
        /// 创建工作空间（地理数据库）
        /// </summary>
        /// <param name="eType">工作空间类别</param>
        /// <param name="parentDirectory">
        /// 工作空间所在的父目录，具体为：
        /// <para>①shp、dwg、txt、xls、xlsx、栅格文件所在文件夹再上一层目录，e.g. c:\gis；</para>
        /// <para>②mdb文件所在文件夹，e.g. c:\gis；</para>
        /// <para>③gdb文件夹的上一层目录，e.g. c:\gis；</para>
        /// </param>
        /// <param name="strWorkspaceName">
        /// 工作空间名称，具体为：
        ///  <para>①shp、dwg、txt、xls、xlsx、栅格文件所在文件夹，e.g. testFolder；</para>
        ///  <para>②mdb文件名，e.g. testMdbFile(.mdb)；</para>
        ///  <para>③gdb文件夹名，e.g. testGdb(.gdb)；</para>
        /// </param>
        /// <returns></returns>
        public static IWorkspace CreateWorkspace(EWorkspaceType eType, string parentDirectory, string strWorkspaceName)
        {
            if (eType == EWorkspaceType.Default)
                eType = GetDefaultWorkspaceType(strWorkspaceName);

            var extension = Path.GetExtension(strWorkspaceName);
            if (!string.IsNullOrEmpty(extension)) strWorkspaceName = strWorkspaceName.Replace(extension, "");
            switch (eType)
            {
                case EWorkspaceType.ShapeFile: return CreateWorkspace(new ShapefileWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.FileGDB: return CreateWorkspace(new FileGDBWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.Access: return CreateWorkspace(new AccessWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.CAD: return CreateWorkspace(new CadWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.InMemory: return CreateWorkspace(new InMemoryWorkspaceFactoryClass(), "", strWorkspaceName);
                case EWorkspaceType.Sde: return CreateWorkspace(new SdeWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.Excel: return CreateWorkspace(new ExcelWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.TextFile: return CreateWorkspace(new TextFileWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.OleDb: return CreateWorkspace(new OLEDBWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.Raster: return CreateWorkspace(new RasterWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.Sql: return CreateWorkspace(new SqlWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                default: throw new ArgumentException($"未实现创建{eType.GetDescriptionEx()}的操作，请联系系统管理员");
            }
            
        }
        /// <summary>
        /// 创建工作空间（地理数据库）
        /// </summary>
        /// <param name="workspaceFactory">工作空间工厂</param>
        /// <param name="parentDirectory">工作空间所在目录</param>
        /// <param name="strWorkspaceName">工作空间名称</param>
        /// <returns></returns>
        public static IWorkspace CreateWorkspace(IWorkspaceFactory workspaceFactory, string parentDirectory, string strWorkspaceName)
        {
            IWorkspaceName workspaceName = workspaceFactory.Create(parentDirectory, strWorkspaceName, null, 0);
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            Marshal.ReleaseComObject(workspaceFactory);
            return workspace;
        }
    }
}
