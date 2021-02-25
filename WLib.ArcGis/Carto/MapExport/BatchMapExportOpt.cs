/*---------------------------------------------------------------- 
// auth： Windragon
// date： 2019/4
// desc： None
// mdfy:  None
// sorc:  https://gitee.com/windr07/WLib
//        https://github.com/Windr07/WLib
//----------------------------------------------------------------*/

using System.Collections.Generic;
using WLib.ExtProgress;

namespace WLib.ArcGis.Carto.MapExport
{
    /// <summary>
    /// 根据出图配置，执行批量出图的操作
    /// </summary>
    public class BatchMapExportOpt : ProLogOperation<IEnumerable<MapExportInfo>, object>
    {
        /// <summary>
        /// 根据出图配置，执行批量出图的操作
        /// </summary>
        /// <param name="name">操作的名称</param>
        /// <param name="data">出图配置信息</param>
        public BatchMapExportOpt(string name, IEnumerable<MapExportInfo> data) : base(name, data)
        {
            foreach (var mapExportInfo in data)
                SubProgressOperations.Add(new MapExportHelper(mapExportInfo));
        }

        protected override void MainOperation()
        {
            OnProgressChanged(0, SubProgressOperations.Count);
            foreach (var subOpt in SubProgressOperations)
            {
                var opt = subOpt as MapExportHelper;
                opt.OperationError += (sender, e) => { opt.Info = e.OptException.ToString(); };
                opt.Info = $"正在导出【{opt.Name}】";
                opt.Run();
                opt.Info = "导出完成！";
                opt.Msgs.Info("", false);

                OnProgressAdd();
                if (StopRunning) break;
            }
        }
    }
}
