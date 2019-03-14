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
using ESRI.ArcGIS.DataSourcesOleDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using WLib.Attributes;
using WLib.ExtException;

namespace WLib.ArcGis.GeoDb.WorkSpace
{
    /// <summary>
    /// 提供根据路径或连接字符串，获取工作空间的方法
    /// （sde的连接可参考 http://blog.csdn.net/mengdong_zy/article/details/8961390）
    /// （参考：http://edndoc.esri.com/arcobjects/9.2/ComponentHelp/esriGeoDatabase/IWorkspaceFactory.htm）
    /// （参考：http://blog.csdn.net/guangliang1102/article/details/51154893）
    /// （连接字符串：https://www.connectionstrings.com）
    /// </summary>
    public class GetWorkspace
    {
        /// <summary>
        /// 判断指定的路径是否为工作空间路径（任意已存在的目录、mdb文件、xls或xlsx文件均认为是工作空间）
        /// </summary>
        /// <param name="path">工作空间路径，任意已存在的目录、mdb文件、xls或xlsx文件均认为是工作空间</param>
        /// <returns></returns>
        public static bool IsWorkspacePath(string path)
        {
            if (!System.IO.Path.IsPathRooted(path))
                path = AppDomain.CurrentDomain.BaseDirectory + path;

            if (System.IO.Directory.Exists(path)) return true;

            if (!System.IO.File.Exists(path)) return false;

            string extension = System.IO.Path.GetExtension(path)?.ToLower();
            return extension == ".mdb" || extension == ".xls" || extension == ".xlsx";
        }
        /// <summary>
        /// 判断指定的字符串是否符合连接字符串规范（sde/sql/oleDb连接字符串）（不判断是否能成功连接）
        /// </summary>
        /// <param name="str">需要判定的字符串</param>
        /// <returns></returns>
        public static bool IsConnectionString(string str)
        {
            str = str.TrimEnd(';');//去掉最后一个分号
            var strConnectArray = str.Split('=', ';');
            return strConnectArray.Length > 0 && strConnectArray.Length % 2 == 0;
        }
        /// <summary>
        /// 根据路径或连接参数，判断工作空间类型，只判断shp/gdb/mdb/sde/xls(xlsx)，当参数为不含后缀的目录时将当成shp工作空间
        /// </summary>
        /// <param name="connStrOrPath">工作空间的路径或连接参数，可以是shp/gdb文件夹路径、mdb文件路径或SDE连接参数</param>
        /// <returns>若strConnOrPath不是连接字符串，且指示的不是gdb,mdb,shp路径或路径不存在，返回null</returns>
        public static EWorkspaceType GetDefaultWorkspaceType(string connStrOrPath)
        {
            var eWorkspaceType = EWorkspaceType.Default;
            if (IsConnectionString(connStrOrPath))
                eWorkspaceType = EWorkspaceType.Sde;
            else if (System.IO.File.Exists(connStrOrPath))
            {
                var extension = System.IO.Path.GetExtension(connStrOrPath);
                if (extension == ".mdb") eWorkspaceType = EWorkspaceType.Access;
                else if (extension == ".xls" || extension == ".xlsx") eWorkspaceType = EWorkspaceType.Excel;
            }
            else if (System.IO.Directory.Exists(connStrOrPath))
                eWorkspaceType = connStrOrPath.EndsWith(".gdb") ? EWorkspaceType.FileGDB : EWorkspaceType.ShapeFile;

            return eWorkspaceType;
        }
        /// <summary>
        /// 解析连接字符串，转成IPropertySet
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <returns></returns>
        public static IPropertySet ConnectStringToPropetySet(string connStr)
        {
            IPropertySet propertySet = new PropertySetClass();
            string[] strConnArray = connStr.Split('=', ';');
            for (int i = 0; i < (strConnArray.Length / 2); i++)
            {
                propertySet.SetProperty(strConnArray[2 * i], strConnArray[2 * i + 1]);
            }
            return propertySet;
        }
        /// <summary>
        /// 已在YYGISLib.ArcGisHelper.WorkSpace.GetWorkSpace实现的工作空间
        /// </summary>
        public static EWorkspaceType[] AllAchieveType => new[]
        {
            EWorkspaceType.ShapeFile,
            EWorkspaceType.FileGDB,
            EWorkspaceType.Access,
            EWorkspaceType.Sde,
            EWorkspaceType.Excel,
            EWorkspaceType.TextFile,
            EWorkspaceType.OleDb,
            EWorkspaceType.Raster,
            EWorkspaceType.Sql
        };


        /// <summary>
        /// 打开工作空间
        /// </summary>
        /// <param name="connStrOrPath">
        /// 工作空间路径或连接字符串：
        /// 1、shp、txt、dwg、栅格文件（GRID、TIFF、ERDAS IMAGE等）所在目录；
        /// 2、gdb文件夹自身路径；
        /// 3、mdb、xls、xlsx文件路径；
        /// 4、sde连接字符串（SERVER=ditu.test.com;INSTANCE=5151;DATABASE=sde_test;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT）；
        /// 5、oleDb连接字符串，包括连接Excel、Access、Oracle、SQLServer等（Provider=Microsoft.Jet.OLEDB.4.0;Data Source=x:\xxx.mdb;User Id=admin;Password=xxx;）；
        /// 6、直连sql连接字符串（server=localhost;uid=sa;pwd=sa;database=myDatabase）
        /// </param>
        /// <param name="eType">
        /// 标识优先将strConnOrPath作为打开哪种工作空间的参数，值为Default时，
        /// 根据strConnOrPath参数自动识别为shp/gdb/mdb/sde的其中一种工作空间</param>
        /// <returns></returns>
        public static IWorkspace GetWorkSpace(string connStrOrPath, EWorkspaceType eType = EWorkspaceType.Default)
        {
            IWorkspace workspace = null;
            if (IsConnectionString(connStrOrPath))//当参数是数据库连接字符串时
            {
                var tmpConnStr = connStrOrPath.ToLower();
                if (tmpConnStr.Contains("provider") && tmpConnStr.Contains("oledb"))
                    eType = EWorkspaceType.OleDb;
                else if (eType == EWorkspaceType.Default)
                    eType = EWorkspaceType.Sde;

                workspace = GetWorksapceFromConnStr(connStrOrPath, eType);
            }
            else if (IsWorkspacePath(connStrOrPath))
            {
                if (!System.IO.Path.IsPathRooted(connStrOrPath))
                    connStrOrPath = AppDomain.CurrentDomain.BaseDirectory + connStrOrPath;

                if (System.IO.Directory.Exists(connStrOrPath)) //当参数是文件夹路径时
                {
                    if (eType == EWorkspaceType.Default)
                        eType = connStrOrPath.ToLower().EndsWith(".gdb") ? EWorkspaceType.FileGDB : EWorkspaceType.ShapeFile;

                    workspace = GetWorkspaceFromFile(connStrOrPath, eType);
                }
                else if (System.IO.File.Exists(connStrOrPath)) //当参数是文件路径时
                {
                    var extension = System.IO.Path.GetExtension(connStrOrPath)?.ToLower();
                    if (extension == ".mdb")
                        eType = EWorkspaceType.Access;
                    else if (extension == ".xls" || extension == ".xlsx")
                        eType = EWorkspaceType.Excel;

                    workspace = GetExcelWorkspace(connStrOrPath);
                }
            }
            return workspace;
        }
        /// <summary>
        /// 通过连接字符串获取工作空间，连接字符串参考：
        /// ①sde： SERVER=ditu.test.com;INSTANCE=5151;DATABASE=sde_test;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT
        /// ②sql： server=localhost;uid=sa;pwd=sa;database=myDatabase
        /// ③oleDb： Provider=Microsoft.Jet.OLEDB.4.0;Data Source=x:\xxx.mdb;User Id=admin;Password=xxx;
        /// </summary>
        /// <param name="eType"></param>
        /// <param name="connnectString"></param>
        /// <returns></returns>
        public static IWorkspace GetWorksapceFromConnStr(string connnectString, EWorkspaceType eType)
        {
            try
            {
                IWorkspaceName workspaceName = new WorkspaceNameClass();
                workspaceName.WorkspaceFactoryProgID = eType.GetDescription(1);
                workspaceName.ConnectionProperties = ConnectStringToPropetySet(connnectString);
                IName iName = (IName)workspaceName;
                return iName.Open() as IWorkspace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开{ eType.GetDescription(2)}工作空间：{connnectString}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 通过文件路径获取工作空间
        /// </summary>
        /// <param name="eType"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IWorkspace GetWorkspaceFromFile(string path, EWorkspaceType eType)
        {
            try
            {
                IWorkspaceName workspaceName = new WorkspaceNameClass();
                workspaceName.WorkspaceFactoryProgID = eType.GetDescription(1);
                workspaceName.PathName = path;
                IName iName = (IName)workspaceName;
                return iName.Open() as IWorkspace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开{ eType.GetDescription(2)}工作空间：{path}出错；{ex.Message}");
            }
        }


        #region 获取各类工作空间的连接字符串
        /// <summary>
        /// 构造连接Access个人地理数据库的连接字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string AccessConnectionString(string path)
        {
            return ($"Provider=ESRI.GeoDB.OLEDB.1;Data Source={path};Extended Properties=workspacetype=esriDataSourcesGDB.AccessWorkspaceFactory.1;Geometry=WKB");
        }
        /// <summary>
        /// 构造连接Shapefile文件的连接字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ShpConnectionString(string path)
        {
            path = System.IO.Path.GetDirectoryName(path);
            return ($"Provider=ESRI.GeoDB.OLEDB.1;Data Source={path};Extended Properties=WorkspaceType=esriDataSourcesFile.ShapefileWorkspaceFactory.1;Geometry=WKB|OBJECT");
        }
        /// <summary>
        /// 构造连接SDE地理数据库的连接字符串
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dataSource"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string SdeConnectionString(string serverName, string dataSource, string userName, string password)
        {
            return ($"Provider=ESRI.GeoDB.OLEDB.1;Location={serverName};Data Source={dataSource};User Id={userName};Password={password};Extended Properties=WorkspaceType=esriDataSourcesGDB.SDEWorkspaceFactory.1;Geometry=WKB|OBJECT;Instance=5151;Version=SDE.DEFAULT");
        }
        #endregion


        #region 获取各类工作空间
        /// <summary>
        /// 获取Access数据库的工作空间
        /// </summary>
        /// <param name="mdbPath">Access数据库路径</param>
        /// <returns></returns>
        private static IWorkspace GetAccessWorkspace(string mdbPath)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new AccessWorkspaceFactoryClass();
                IWorkspace workSpace = workspaceFactory.OpenFromFile(mdbPath, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Access数据库：{mdbPath}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取gdb数据库的工作空间
        /// </summary>
        /// <param name="gdbPath">gdb文件夹路径</param>
        /// <returns></returns>
        private static IWorkspace GetGdbWorkspace(string gdbPath)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspace workSpace = workspaceFactory.OpenFromFile(gdbPath, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开GDB数据库：{gdbPath}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取shp文件夹的工作空间
        /// </summary>
        /// <param name="shpDir">shp文件所在的文件夹路径</param>
        /// <returns></returns>
        private static IWorkspace GetShpWorkspace(string shpDir)
        {
            try
            {
                IWorkspaceFactory workfactory = new ShapefileWorkspaceFactoryClass();
                IWorkspace workspace = workfactory.OpenFromFile(shpDir, 0);
                Marshal.ReleaseComObject(workfactory);
                return workspace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Shapefile工作空间：{shpDir}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取SDE数据库的工作空间
        /// </summary>
        /// <param name="strSdeConn">sde连接字符串（eg:SERVER=ditu.test.com;INSTANCE=5151;DATABASE=sde_test;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT）</param>
        /// <remarks>
        /// 参考：http://blog.csdn.net/mengdong_zy/article/details/8961390
        ///       http://www.cnblogs.com/feilong3540717/archive/2011/07/20/2111882.html
        /// </remarks>
        /// <returns></returns>
        private static IWorkspace GetSdeWorkspace(string strSdeConn)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactoryClass();
                var propertySet = ConnectStringToPropetySet(strSdeConn);
                IWorkspace sdeWorkSpace = workspaceFactory.Open(propertySet, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return sdeWorkSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Shapefile工作空间：{strSdeConn}出错；{ex.Message}");
            }
        }
        /// <summary>
        ///  获取OleDb数据的工作空间（包括连接Excel、Access、Oracle、SQLServer等）
        /// </summary>
        /// <param name="strOleDbConn">连接字符串（eg:Provider=Microsoft.Jet.OLEDB.4.0;Data Source=x:\xxx.mdb;User Id=admin;Password=xxx;）</param>
        /// <returns></returns>
        private static IWorkspace GetOleDbWorkspace(string strOleDbConn)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new OLEDBWorkspaceFactoryClass();
                var propertySet = ConnectStringToPropetySet(strOleDbConn);
                IWorkspace workSpace = workspaceFactory.Open(propertySet, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开OleDb工作空间：{strOleDbConn}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取栅格数据目录的工作空间（包括GRID、TIFF、ERDAS IMAGE等文件所在的目录）
        /// </summary>
        /// <param name="rasterDir">栅格数据文件所在的目录</param>
        /// <returns></returns>
        private static IWorkspace GetRasterWorkspace(string rasterDir)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new RasterWorkspaceFactoryClass();
                IWorkspace rasterWorkspace = workspaceFactory.OpenFromFile(rasterDir, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return rasterWorkspace;

                //若格式为ESRI GRID，那么该方法的参数为栅格要素集的名称；
                //若格式为TIFF格式，则要加上.tif扩展名：OpenRasterDataset("hillshade.tif")
                //IRasterDataset rasterDataset = rasterWorkspace.OpenRasterDataset("ca_hillshade");  
            }
            catch (Exception ex)
            {
                throw new Exception($"打开OleDb工作空间：{rasterDir}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取Excel工作空间
        /// </summary>
        /// <param name="excelPath">Excel文件路径</param>
        /// <returns></returns>
        private static IWorkspace GetExcelWorkspace(string excelPath)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new ExcelWorkspaceFactoryClass();
                IWorkspace workSpace = workspaceFactory.OpenFromFile(excelPath, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Excel工作空间：{excelPath}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取txt工作空间
        /// </summary>
        /// <param name="txtDir">txt文件所在的目录</param>
        /// <returns></returns>
        private static IWorkspace GetTextWorkspace(string txtDir)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new TextFileWorkspaceFactoryClass();
                IWorkspace workSpace = workspaceFactory.OpenFromFile(txtDir, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开txt工作空间：{txtDir}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取cad工作空间
        /// </summary>
        /// <param name="cadDir">cad文件所在的目录</param>
        /// <returns></returns>
        private static IWorkspace GetCadWorkspace(string cadDir)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new CadWorkspaceFactoryClass();
                IWorkspace workSpace = workspaceFactory.OpenFromFile(cadDir, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开CAD工作空间：{cadDir}出错；{ex.Message}");
            }
        }
        /// <summary>
        /// 获取Sql工作空间
        /// </summary>
        /// <param name="strSqlConn">直连SQL Server的连接字符串（eg:server=localhost;uid=sa;pwd=sa;database=myDatabase）</param>
        /// <returns></returns>
        private static IWorkspace GetSqlWorkspace(string strSqlConn)
        {
            try
            {
                IWorkspaceFactory workspaceFactory = new SqlWorkspaceFactory();
                var propertySet = ConnectStringToPropetySet(strSqlConn);
                IWorkspace workSpace = workspaceFactory.Open(propertySet, 0);
                Marshal.ReleaseComObject(workspaceFactory);
                return workSpace;
            }
            catch (Exception ex)
            {
                throw new Exception($"打开Sql工作空间：{strSqlConn}出错；{ex.Message}");
            }
        }
        #endregion
    }
}
