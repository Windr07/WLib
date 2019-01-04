using System.Collections.Generic;
using System.Linq;
using ESRI.ArcGIS.Geometry;

namespace WLib.UserCtrls.Dev.ArcGisControl
{
    internal class CommonLib
    {
        /// <summary>
        /// 主地图上鼠标操作产生的Geometry
        /// </summary>
        internal static IGeometry MapGeometry = null;
        //变量
        /// <summary>
        /// 主地图上鼠标操作的动作标识
        /// 0：nothing，默认
        /// 1:系统操作：全屏、放大、缩小、平移、上一视图、下一视图
        /// 8:测距离
        /// 9：测面积        
        /// 11:空间查询-画点
        /// 12:空间查询-画线
        /// 13:空间查询-画多边形
        /// 14:空间查询-画矩形
        /// </summary>
        internal static int MapMouseFlag = 0;
        /// <summary>
        /// 视图页面中的鼠标操作标识
        /// 0:nothing
        /// 1:系统
        /// 20:选择要素
        /// 21:添加文本
        /// 22:修改文本
        /// </summary>
        internal static int PageLayoutMouseFlag = 0;
        /// <summary>
        /// 三维地图上鼠标操作的动作标识
        /// 0：nothing，默认<para/>
        /// 11:导航条-Navigation<para/>
        /// 12:导航条-Fly<para/>
        /// 
        /// 13:导航条-Center on Target<para/>
        /// 14:导航条-Zoom to Target<para/>
        /// 15:导航条-Set Observer<para/>
        /// 
        /// 16:导航条-放大<para/>
        /// 17:导航条-缩小<para/>
        /// 18:导航条-平移<para/>
        /// 19:导航条-全屏<para/>
        /// </summary>
        internal static int MapMouse3DFlag = 0;

        /// <summary>
        /// 空间数据编辑的当前操作状态<para/>
        /// addpoint:增加点<para/>
        /// addline:增加线<para/>
        /// addpolygon:增加面<para/>
        /// deletepoint:删除点<para/>
        /// deleteline:删除线<para/>
        /// deletepolygon:删除面<para/>
        /// movepoint:移动点<para/>
        /// moveline:移动线<para/>
        /// movepolygon:移动面<para/>
        /// copypoint:复制点<para/>
        /// copyline：复制线<para/>
        /// copypolygon:复制面<para/>
        /// </summary>
        internal static string OperatorString = null;
        /// <summary>
        /// 求两个文件名的集合的并集
        /// </summary>
        /// <param name="fileList1"></param>
        /// <param name="fileList2"></param>
        /// <returns></returns>
        internal static List<string> UnionFileNameSet(string[] fileList1, string[] fileList2)
        {
            List<string> d = new List<string>();
            for (int i = 0; i < fileList1.Count(); i++)
                d.Add(fileList1[i].Remove(fileList1[i].LastIndexOf(".")));
            bool test = false;
            for (int i = 0; i < fileList2.Count(); i++)
            {
                test = true;
                string a = fileList2[i].Remove(fileList2[i].LastIndexOf("."));
                for (int j = 0; j < fileList1.Count(); j++)
                {
                    string b = fileList1[j].Remove(fileList1[j].LastIndexOf("."));
                    if (a.Equals(b)) { test = false; break; }
                }
                if (test) d.Add(a);
            }
            return d;
        }
        /// <summary>
        /// 自动修改下拉框的宽度
        /// </summary>
        /// <param name="cbb"></param>
        internal static void AutoFitComboBoxDropDownWidth(System.Windows.Forms.ComboBox cbb)
        {
            int maxWidth = 0;
            for (int i = 0; i < cbb.Items.Count; i++)
                if (maxWidth < cbb.Items[i].ToString().Length)
                    maxWidth = cbb.Items[i].ToString().Length;
            cbb.DropDownWidth = maxWidth * 12 > cbb.DropDownWidth ? maxWidth * 12 : cbb.DropDownWidth;
        }
    }
}

