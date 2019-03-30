/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/3/27 19:09:13
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;

namespace WLib.ArcGis.Control.MapAssociation
{
    /// <summary>
    /// 地图控件与其他控件或接口的关联操作
    /// </summary>
    public interface IMapCtrlAssociation
    {
        /// <summary>
        /// 地图控件
        /// </summary>
        AxMapControl MapControl { get; }
    }
}
