/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WLib.Attributes.Description;
using WLib.Files;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// 提供根据路径或连接字符串，获取工作空间的方法
    /// （sde的连接可参考 http://blog.csdn.net/mengdong_zy/article/details/8961390）
    /// （参考：http://edndoc.esri.com/arcobjects/9.2/ComponentHelp/esriGeoDatabase/IWorkspaceFactory.htm）
    /// （参考：http://blog.csdn.net/guangliang1102/article/details/51154893）
    /// （连接字符串：https://www.connectionstrings.com）
    /// </summary>
    public static partial class WorkspaceEx
    {
        /// <summary>
        /// 构建SDE连接字符串
        /// <para>示例：SERVER=ditu.test.com;INSTANCE=5151;DATABASE=sde_test;USER=sa;PASSWORD=sa;VERSION=dbo.DEFAULT</para>
        /// </summary>
        /// <param name="server">服务器地址，e.g. ditu.test.com</param>
        /// <param name="database">数据库名称，e.g. sde</param>
        /// <param name="userName">用户名， e.g. sde</param>
        /// <param name="password">密码， e.g. sde</param>
        /// <param name="instance">端口号，一般为5151</param>
        /// <param name="version">版本，一般为dbo.DEFAULT 或 SDE.DEFAULT</param>
        /// <returns></returns>
        public static string Create_SDE_ConnString(string server, string database,
            string userName, string password, string instance ="5151", string version= "dbo.DEFAULT")
        {
            return $"SERVER={server};INSTANCE={instance};DATABASE={database};USER={userName};PASSWORD={password};VERSION={version};";
        }

        /// <summary>
        /// 构建直连SQL Server的连接字符串
        /// <para>示例：server=localhost;uid=sa;pwd=sa;database=myDatabase</para>
        /// </summary>
        /// <param name="server">服务器地址，e.g. ditu.test.com</param>
        /// <param name="database">数据库名称，e.g. sde</param>
        /// <param name="userName">用户名， e.g. sde</param>
        /// <param name="password">密码， e.g. sde</param>
        /// <param name="instance">端口号，一般为5151</param>
        /// <param name="version">版本，一般为dbo.DEFAULT</param>
        /// <returns></returns>
        public static string Create_SQL_ConnString(string server, string database,
            string userName, string password, string instance = "5151", string version = "dbo.DEFAULT")
        {
            return null;
        }


        /// <summary>
        /// 构建直连Oracle的连接字符串
        /// <para>SERVER=127.0.0.1;INSTANCE=sde:oracle11g:orcl;DATABASE=orcl;USER=sde;PASSWORD=sde;VERSION=sde.DEFAULT;</para>
        /// <para>SERVER=127.0.0.1;INSTANCE=esri_sde:orcl;DATABASE=orcl;USER=scott;PASSWORD=tiger;VERSION=sde.DEFAULT;AUTHENTICATION_MODE=DBMS;</para>
        /// </summary>
        /// <param name="server">服务器地址，e.g. 127.0.0.1</param>
        /// <param name="database">数据库名称，e.g. orcl</param>
        /// <param name="userName">用户名， e.g. sde</param>
        /// <param name="password">密码， e.g. sde</param>
        /// <param name="instance">端口号，一般为sde:oracle11g:orcl</param>
        /// <param name="version">版本，一般为SDE.DEFAULT</param>
        /// <returns></returns>
        public static string Create_Oracle_ConnString(string server, string database ,
            string userName, string password, string instance = "sde:oracle11g:orcl", string version = "sde.DEFAULT")
        {
            //IPropertySet propertySet = new PropertySetClass();
            //propertySet.SetProperty("server", "服务器的IP地址");
            //propertySet.SetProperty("instance", "sde:oracle11g:orcl");//这个隐藏的很深
            //propertySet.SetProperty("database", "orcl");//数据库名称
            //propertySet.SetProperty("user", "用户名");
            //propertySet.SetProperty("password", "密码");
            //propertySet.SetProperty("version", "SDE.DEFAULT");
            //IWorkspace workspace = workspaceFactory.Open(propertySet, 0);

            //IPropertySet propertySet = new PropertySetClass();
            //propertySet.SetProperty("SERVER", "cuillin");
            //propertySet.SetProperty("INSTANCE", "esri_sde");
            //propertySet.SetProperty("USER", "scott");
            //propertySet.SetProperty("PASSWORD", "tiger");
            //propertySet.SetProperty("VERSION", "SDE.DEFAULT");
            //propertySet.SetProperty("AUTHENTICATION_MODE", "DBMS");
            //IWorkspace workspace = workspaceFactory.Open(propertySet, 0);

            return $"SERVER={server};INSTANCE={instance};DATABASE={database};USER={userName};PASSWORD={password};VERSION={version};";
        }
    }
}
