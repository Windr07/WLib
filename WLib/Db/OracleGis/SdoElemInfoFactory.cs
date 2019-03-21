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
    [OracleCustomTypeMapping("MDSYS.SDO_ELEM_INFO_ARRAY")]
    public class SdoElemInfoFactory : IOracleArrayTypeFactory
    {

        public Array CreateArray(int numElems)
        {
            return new int[numElems];
        }

        public Array CreateStatusArray(int numElems)
        {
            return new OracleUdtStatus[numElems];
        }
    } 
}
