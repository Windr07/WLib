/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using WLib.Attributes;

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
        public static IWorkspace NewInMemoryWorkspace(string strWorkspaceName)
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
        /// <param name="eType">工作空间类别</param>
        /// <param name="parentDirectory">
        /// 工作空间所在的父目录，具体为：
        /// ①shp文件所在文件夹再上一层目录，e.g. c:\gis；
        /// ②mdb文件所在文件夹，e.g. c:\gis；
        /// ③dwg文件所在文件夹，e.g. c:\gis；
        /// ④gdb文件夹的上一层目录，e.g. c:\gis；
        /// </param>
        /// <param name="strWorkspaceName">
        /// 工作空间名称，具体为：
        /// ①shp文件所在文件夹，e.g. testShpFolder；
        /// ②mdb文件名，e.g. testMdbFile(.mdb)；//去掉扩展名
        /// ③dwg文件所在文件夹，e.g. testCadFolder；
        /// ④gdb文件夹名，e.g. testGdb.gdb；
        /// </param>
        /// <returns></returns>
        public static IWorkspace NewWorkspace(EWorkspaceType eType, string parentDirectory, string strWorkspaceName)
        {
            if (eType == EWorkspaceType.Default)
                eType = GetDefaultWorkspaceType(strWorkspaceName);

            switch (eType)
            {
                case EWorkspaceType.ShapeFile: return NewWorkspace(new ShapefileWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.FileGDB: return NewWorkspace(new FileGDBWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.Access: return NewWorkspace(new AccessWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                case EWorkspaceType.CAD: return NewWorkspace(new CadWorkspaceFactoryClass(), parentDirectory, strWorkspaceName);
                default:
                    throw new ArgumentException($"未实现创建{eType.GetDescription()}的操作，请联系系统管理员");
            }
        }
        /// <summary>
        /// 创建工作空间（地理数据库）
        /// </summary>
        /// <param name="workspaceFactory">工作空间工厂</param>
        /// <param name="parentDirectory">工作空间所在目录</param>
        /// <param name="strWorkspaceName">工作空间名称</param>
        /// <returns></returns>
        public static IWorkspace NewWorkspace(IWorkspaceFactory workspaceFactory, string parentDirectory, string strWorkspaceName)
        {
            IWorkspaceName workspaceName = workspaceFactory.Create(parentDirectory, strWorkspaceName, null, 0);
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            Marshal.ReleaseComObject(workspaceFactory);
            return workspace;
        }
    }
}
