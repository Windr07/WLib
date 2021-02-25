using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Analysis.Topology
{
    /// <summary>
    /// 拓扑错误图形的信息
    /// </summary>
    public class TopoErrorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int OriClsID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DesClsID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OriOID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DesOID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IGeometry Shape { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public TopoErrorInfo() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oriClsID"></param>
        /// <param name="desClsID"></param>
        /// <param name="oriOID"></param>
        /// <param name="desOID"></param>
        /// <param name="shape"></param>
        public TopoErrorInfo(int oriClsID, int desClsID, int oriOID, int desOID, IGeometry shape = null)
        {
            OriClsID = oriClsID;
            DesClsID = desClsID;
            OriOID = oriOID;
            DesOID = desOID;
            Shape = shape;
        }
    }
}
