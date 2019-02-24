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
    public class SdoPointType : IOracleCustomType, INullable
    {
        #region 属性
        [OracleObjectMapping("X")]
        public double? X { get; set; }
        [OracleObjectMapping("Y")]
        public double? Y { get; set; }
        [OracleObjectMapping("Z")]
        public double? Z { get; set; }
        public bool IsNull
        {
            get
            {
                if (this.X != null && this.Y != null && this.Z != null)
                    return false;
                return true;
            }
        }
        #endregion

        public void FromCustomObject(global::Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            if (!this.IsNull)
            {
                OracleUdt.SetValue(con, pUdt, "X", this.X);
                OracleUdt.SetValue(con, pUdt, "Y", this.Y);
                OracleUdt.SetValue(con, pUdt, "Z", this.Z);
            }
        }

        public void ToCustomObject(global::Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            this.X = OracleUdt.IsDBNull(con, pUdt, "X") ? null : (double?)(OracleUdt.GetValue(con, pUdt, "X"));
            this.Y = OracleUdt.IsDBNull(con, pUdt, "Y") ? null : (double?)(OracleUdt.GetValue(con, pUdt, "Y"));
            this.Z = OracleUdt.IsDBNull(con, pUdt, "Z") ? null : (double?)(OracleUdt.GetValue(con, pUdt, "Z"));
        }
    }
}
