/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Runtime.InteropServices;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace WLib.ArcGis.GeoDb.WorkSpace
{
    public class CreateWorkspace
    {
        /// <summary>
        /// 创建工作空间（地理数据库）
        /// </summary>
        /// <param name="workspaceFactory"></param>
        /// <param name="workspacePath"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static IWorkspace NewWorkspace(IWorkspaceFactory workspaceFactory, string workspacePath, string strName)
        {
            IWorkspaceName workspaceName = workspaceFactory.Create(workspacePath, strName, null, 0);
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            Marshal.ReleaseComObject(workspaceFactory);
            return workspace;
        }
        /// <summary>
        /// 创建一个新的个人地理数据库（mdb文件）
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="mdbName">mdb数据库文件名称</param>
        /// <returns></returns>
        public static IWorkspace NewAccessWorkspace(string dir, string mdbName)
        {
            return NewWorkspace(new AccessWorkspaceFactoryClass(), dir, mdbName);
        }
        /// <summary>
        /// 创建一个新的文件地理数据库（gdb文件夹）
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        /// <param name="gdbName">gdb文件夹名称</param>
        /// <returns></returns>
        public static IWorkspace NewGdbWorkspace(string dir, string gdbName)
        {
            return NewWorkspace(new FileGDBWorkspaceFactoryClass(), dir, gdbName);
        }
    }
}
