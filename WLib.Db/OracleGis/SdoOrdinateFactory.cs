/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using Oracle.DataAccess.Types;

namespace WLib.Db.OracleGis
{
    [OracleCustomTypeMapping("MDSYS.SDO_ORDINATE_ARRAY")]
    public class SdoOrdinateFactory : IOracleArrayTypeFactory
    {

        public Array CreateArray(int numElems)
        {
            return new double[numElems];
        }

        public Array CreateStatusArray(int numElems)
        {
            return new OracleUdtStatus[numElems];
        }
    }
}
