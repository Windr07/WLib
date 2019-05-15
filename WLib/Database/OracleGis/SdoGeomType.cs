/*---------------------------------------------------------------- 
// auth： Unknown
// date： None
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using Oracle.DataAccess.Types;

namespace WLib.Database.OracleGis
{
    [OracleCustomTypeMapping("MDSYS.SDO_GEOMETRY")]
    public class SdoGeomType : IOracleCustomType, INullable
    {
        #region 属性
        [OracleObjectMapping("SDO_GTYPE")]
        public int? SdoGType
        { get; set; }
        [OracleObjectMapping("SDO_SRID")]
        public int? SdoSRID
        { get; set; }
        [OracleObjectMapping("SDO_POINT")]
        public SdoPointType SdoPoint
        { get; set; }
        [OracleObjectMapping("SDO_ELEM_INFO")]
        public int[] SdoElemInfoArray
        { get; set; }
        [OracleObjectMapping("SDO_ORDINATES")]
        public double[] SdoOrdinateArray
        { get; set; }
        public bool IsNull
        {
            get
            {
                if (this.SdoGType != null || this.SdoSRID != null || !this.SdoPoint.IsNull ||
                    (this.SdoElemInfoArray != null && this.SdoElemInfoArray.Length > 0) ||
                    (this.SdoOrdinateArray != null && this.SdoOrdinateArray.Length > 0))
                    return false;
                return true;
            }
        }
        #endregion

        public void FromCustomObject(global::Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            if (this.SdoGType != null)
                OracleUdt.SetValue(con, pUdt, "SDO_GTYPE", this.SdoGType);
            if (this.SdoSRID != null)
                OracleUdt.SetValue(con, pUdt, "SDO_SRID", this.SdoSRID);
            if (!this.SdoPoint.IsNull)
                OracleUdt.SetValue(con, pUdt, "SDO_POINT", this.SdoPoint);
            if (this.SdoElemInfoArray != null && this.SdoElemInfoArray.Length > 0)
                OracleUdt.SetValue(con, pUdt, "SDO_ELEM_INFO", this.SdoElemInfoArray);
            if (this.SdoOrdinateArray != null && this.SdoOrdinateArray.Length > 0)
                OracleUdt.SetValue(con, pUdt, "SDO_ORDINATES", this.SdoOrdinateArray);
        }

        public void ToCustomObject(global::Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            this.SdoGType = OracleUdt.IsDBNull(con, pUdt, "SDO_GTYPE") ? null : (int?)OracleUdt.GetValue(con, pUdt, "SDO_GTYPE");
            this.SdoSRID = OracleUdt.IsDBNull(con, pUdt, "SDO_SRID") ? null : (int?)OracleUdt.GetValue(con, pUdt, "SDO_SRID");
            this.SdoPoint = OracleUdt.IsDBNull(con, pUdt, "SDO_POINT") ? null : (SdoPointType)OracleUdt.GetValue(con, pUdt, "SDO_POINT");
            this.SdoElemInfoArray = OracleUdt.IsDBNull(con, pUdt, "SDO_POINT") ? null : (int[])(OracleUdt.GetValue(con, pUdt, "SDO_ELEM_INFO"));
            this.SdoOrdinateArray = OracleUdt.IsDBNull(con, pUdt, "SDO_ORDINATES") ? null : (double[])(OracleUdt.GetValue(con, pUdt, "SDO_ORDINATES"));
        }

    }
}
