/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2018
// desc： None
// mdfy:  None
//----------------------------------------------------------------*/

using System.Collections.Generic;
using ESRI.ArcGIS.Geometry;

namespace WLib.ArcGis.Carto
{
    /// <summary>
    /// 图形的包络线（IEnvelope）相关操作
    /// </summary>
    public class EnvelopeOpt
    {
        /// <summary>
        /// 获得包围框的并集
        /// </summary>
        /// <param name="envelopes"></param>
        /// <seealso cref="http://help.arcgis.com/en/sdk/10.0/arcobjects_net/componenthelp/index.html#//002m00000197000000"/>
        /// <returns></returns>
        public static IEnvelope UnionEnvelope(IEnumerable<IEnvelope> envelopes)
        {
            IEnvelope resultEnv = new EnvelopeClass(); 
            foreach (var envelope in envelopes)
            {
                resultEnv.Union(envelope);
            }
            return resultEnv;
        }
    }
}
