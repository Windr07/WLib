/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using WLib.Attributes.Description;

namespace WLib.ArcGis.GeoDatabase.WorkSpace
{
    /// <summary>
    /// ESRI ArcGIS常用工作空间（IWorkspace）类别
    /// </summary>
    public enum EWorkspaceType
    {
        /// <summary>
        /// 默认，指ShapeFile,FileGDB,Access三种工作空间之一
        /// </summary>
        [DescriptionEx("shp/gdb，或mdb数据库")]
        [DescriptionEx("default",2)]
        [DescriptionEx("", 1)]
        Default = 0,
        /// <summary>
        /// ShapeFile文件夹
        /// </summary>
        [DescriptionEx("ShapeFile文件夹")]
        [DescriptionEx("shp", 2)]
        [DescriptionEx("esriDataSourcesFile.ShapefileWorkspaceFactory", 1)]
        ShapeFile = 1,
        /// <summary>
        /// 文件地理数据库（*.gdb）
        /// </summary>
        [DescriptionEx("文件地理数据库")]
        [DescriptionEx("gdb", 2)]
        [DescriptionEx("esriDataSourcesGDB.FileGDBWorkspaceFactory", 1)]
        FileGDB = 2,
        /// <summary>
        /// Access数据库
        /// </summary>
        [DescriptionEx("Access数据库")]
        [DescriptionEx("mdb", 2)]
        [DescriptionEx("esriDataSourcesGDB.AccessWorkspaceFactory", 1)]
        Access = 3,
        /// <summary>
        /// SDE数据库
        /// </summary>
        [DescriptionEx("SDE数据库")]
        [DescriptionEx("sde", 2)]
        [DescriptionEx("esriDataSourcesGDB.SdeWorkspaceFactory", 1)]
        Sde = 4,
        /// <summary>
        /// Excel文件
        /// </summary>
        [DescriptionEx("Excel文件")]
        [DescriptionEx("excel", 2)]
        [DescriptionEx("esriDataSourcesOleDB.ExcelWorkspaceFactory", 1)]
        Excel = 5,
        /// <summary>
        /// 文本文件夹
        /// </summary>
        [DescriptionEx("文本文件夹")]
        [DescriptionEx("txt", 2)]
        [DescriptionEx("esriDataSourcesOleDB.TextFileWorkspaceFactory", 1)]
        TextFile = 6,
        /// <summary>
        /// OleDb数据库（Access/Excel/dbf/Oracle/SQLServer等）
        /// </summary>
        [DescriptionEx("OleDb数据库")]
        [DescriptionEx("oledb", 2)]
        [DescriptionEx("esriDataSourcesOleDB.OLEDBWorkspaceFactory", 1)]
        OleDb = 7,
        /// <summary>
        /// 栅格数据
        /// </summary>
        [DescriptionEx("栅格数据文件夹")]
        [DescriptionEx("raster", 2)]
        [DescriptionEx("esriDataSourcesRaster.RasterWorkspaceFactory", 1)]
        Raster = 8,
        /// <summary>
        /// Sql数据库
        /// </summary>
        [DescriptionEx("Sql数据库")]
        [DescriptionEx("sql", 2)]
        [DescriptionEx("esriDataSourcesGDB.SqlWorkspaceFactory", 1)]
        Sql = 10,
        /// <summary>
        /// CAD数据文件夹
        /// </summary>
        [DescriptionEx("CAD数据文件夹")]
        [DescriptionEx("cad", 2)]
        [DescriptionEx("esriDataSourcesFile.CadWorkspaceFactory", 1)]
        CAD = 11,
        /// <summary>
        /// 内存工作空间
        /// </summary>
        [DescriptionEx("内存工作空间")]
        [DescriptionEx("InMemory", 2)]
        [DescriptionEx("esriDataSourcesGDB.InMemoryWorkspaceFactory", 1)]
        InMemory = 12,
        
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
