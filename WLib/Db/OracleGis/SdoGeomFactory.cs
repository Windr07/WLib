/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Oracle.DataAccess.Types;

namespace WLib.Db.OracleGis
{
    [OracleCustomTypeMapping("MDSYS.SDO_GEOMETRY")]
    public class SdoGeomFactory : IOracleCustomTypeFactory
    {
        public virtual IOracleCustomType CreateObject()
        {
            SdoGeomType o = new SdoGeomType();
            return o;
        }
    }
}
