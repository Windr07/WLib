/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes;

namespace WLib.ArcGis.GeoDb.WorkSpace
{
    /// <summary>
    /// ESRI ArcGIS常用工作空间（IWorkspace）类别
    /// </summary>
    public enum EWorkspaceType
    {
        /// <summary>
        /// 默认，指ShapeFile,FileGDB,Access三种工作空间之一
        /// </summary>
        [Description("shp/gdb，或mdb数据库")]
        [Description("default",2)]
        [Description("", 1)]
        Default = 0,
        /// <summary>
        /// ShapeFile文件夹
        /// </summary>
        [Description("ShapeFile文件夹")]
        [Description("shp", 2)]
        [Description("esriDataSourcesFile.ShapefileWorkspaceFactory", 1)]
        ShapeFile = 1,
        /// <summary>
        /// 文件地理数据库（*.gdb）
        /// </summary>
        [Description("文件地理数据库")]
        [Description("gdb", 2)]
        [Description("esriDataSourcesGDB.FileGDBWorkspaceFactory", 1)]
        FileGDB = 2,
        /// <summary>
        /// Access数据库
        /// </summary>
        [Description("Access数据库")]
        [Description("mdb", 2)]
        [Description("esriDataSourcesFile.ShapefileWorkspaceFactory", 1)]
        Access = 3,
        /// <summary>
        /// SDE数据库
        /// </summary>
        [Description("SDE数据库")]
        [Description("sde", 2)]
        [Description("esriDataSourcesGDB.AccessWorkspaceFactory", 1)]
        Sde = 4,
        /// <summary>
        /// Excel文件
        /// </summary>
        [Description("Excel文件")]
        [Description("excel", 2)]
        [Description("esriDataSourcesOleDB.ExcelWorkspaceFactory", 1)]
        Excel = 5,
        /// <summary>
        /// 文本文件夹
        /// </summary>
        [Description("文本文件夹")]
        [Description("txt", 2)]
        [Description("esriDataSourcesOleDB.TextFileWorkspaceFactory", 1)]
        TextFile = 6,
        /// <summary>
        /// OleDb数据库（Access/Excel/dbf/Oracle/SQLServer等）
        /// </summary>
        [Description("OleDb数据库")]
        [Description("oledb", 2)]
        [Description("esriDataSourcesOleDB.OLEDBWorkspaceFactory", 1)]
        OleDb = 7,
        /// <summary>
        /// 栅格数据
        /// </summary>
        [Description("栅格数据文件夹")]
        [Description("raster", 2)]
        [Description("esriDataSourcesRaster.RasterWorkspaceFactory", 1)]
        Raster = 8,
        /// <summary>
        /// Sql数据库
        /// </summary>
        [Description("Sql数据库")]
        [Description("sql", 2)]
        [Description("esriDataSourcesGDB.SqlWorkspaceFactory", 1)]
        Sql = 10,
        /// <summary>
        /// CAD数据文件夹
        /// </summary>
        [Description("CAD数据文件夹")]
        [Description("cad", 2)]
        [Description("esriDataSourcesFile.CadWorkspaceFactory", 1)]
        CAD = 11,
        /// <summary>
        /// 内存工作空间
        /// </summary>
        [Description("内存工作空间")]
        [Description("InMemory", 2)]
        [Description("esriDataSourcesGDB.InMemoryWorkspaceFactory", 1)]
        InMemory = 11,

        //关于UID：https://blog.csdn.net/yulongguiziyao/article/details/16119633
        //关于WorkspaceFactoryProgID：http://edndoc.esri.com/arcobjects/9.2/ComponentHelp/esriGeodatabase/IWorkspaceName_WorkspaceFactoryProgID.htm
        //  esriDataSourcesGDB.AccessWorkspaceFactory  
        //  esriDataSourcesFile.ArcInfoWorkspaceFactory  
        //  esriDataSourcesFile.CadWorkspaceFactory  
        //  esriDataSourcesGDB.FileGDBWorkspaceFactory  
        //  esriDataSourcesOleDB.OLEDBWorkspaceFactory  
        //  esriDataSourcesFile.PCCoverageWorkspaceFactory  
        //  esriDataSourcesRaster.RasterWorkspaceFactory  
        //  esriDataSourcesGDB.SdeWorkspaceFactory  
        //  esriDataSourcesFile.ShapefileWorkspaceFactory  
        //  esriDataSourcesOleDB.TextFileWorkspaceFactory  
        //  esriDataSourcesFile.TinWorkspaceFactory 
        //  esriDataSourcesFile.VpfWorkspaceFactory
    }
}
