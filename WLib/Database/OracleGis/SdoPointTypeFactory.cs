/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using Oracle.DataAccess.Types;

namespace WLib.Database.OracleGis
{
    [OracleCustomTypeMapping("MDSYS.SDO_POINT_TYPE")]
    public class SdoPointTypeFactory : IOracleCustomTypeFactory
    {
        public virtual IOracleCustomType CreateObject()
        {
            return new SdoPointType();
        }
    }
}
