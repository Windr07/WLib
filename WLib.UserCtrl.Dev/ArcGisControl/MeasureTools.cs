using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace WLib.UserCtrls.Dev.ArcGisControl
{
    /// <summary>
    /// 测量工具
    /// </summary>
    internal class MeasureTools
    {
        private AxMapControl m_pMap;						//地图对象
        private IPointCollection m_pPnts;					//点集合
        private INewPolygonFeedback m_pPolygonFeedback;		//新建多边形对象
        private INewLineFeedback m_pLineFeedback;			//新建线对象
        private int m_iSurveyType;							//0-空操作；1-测距离； 2-测面积； 3-测角度

        private bool m_bBusy;						//是否正在测量
        private double m_dTotalLength = 0;			//线总长度
        private double m_dCurrentLength = 0;		//当前线段长度
        private double m_dArea = 0;				//多边形面积
        private double m_dAngle = 0;				//当前线段与前一线段的夹角
        private double m_dDirection = 0;			//与正北方向的夹角,即方位角

        /// <summary>
        /// 构造函数
        /// </summary>
        internal MeasureTools()
        {
            m_pMap = null;
            m_pPnts = null;
            m_pPolygonFeedback = null;
            m_pLineFeedback = null;
            m_iSurveyType = 0;
            m_bBusy = false;
        }

        #region set、get
        /// <summary>
        /// Map对象，只写
        /// </summary>
        public AxMapControl Map
        {
            set => m_pMap = value;
        }

        /// <summary>
        /// 判断是否正在测量，只读
        /// </summary>
        public bool IsSurveying => m_bBusy;

        /// <summary>
        /// 折线总长度，只读 
        /// </summary>
        public double totalLength => m_dTotalLength;

        /// <summary>
        /// 当前线段长度，只读
        /// </summary>
        public double currentLength => m_dCurrentLength;

        /// <summary>
        /// 多边形面积，只读
        /// </summary>
        public double Area => m_dArea;

        /// <summary>
        /// 当前线段与前一线段的夹角，只读
        /// </summary>
        public double Angle => m_dAngle;

        /// <summary>
        /// 当前线段的方向角，只读
        /// </summary>
        public double Direction => m_dDirection;

        #endregion

        /// <summary>
        /// 启动距离测量,pPnt为测量起点，测量结果为
        /// totalLength:线总长度
        /// currentLength:当前线段长度
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="pPnt">测量起点</param>
        public void LengthStart(IPoint pPnt)
        {
            m_iSurveyType = 1;

            m_pLineFeedback = new NewLineFeedbackClass();
            m_pLineFeedback.Display = m_pMap.ActiveView.ScreenDisplay;
            m_pLineFeedback.Start(pPnt);
            m_bBusy = true;

            m_pPnts = new PolylineClass();
            object ep = System.Reflection.Missing.Value;
            m_pPnts.AddPoint(pPnt, ref ep, ref ep);
        }

        /// <summary>
        /// 启动面积测量,pPnt为多边形起点,测量结果为
        /// Area:多边形面积
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="pPnt">测量起点</param>
        public void AreaStart(IPoint pPnt)
        {
            m_iSurveyType = 2;
            m_pPolygonFeedback = new NewPolygonFeedbackClass();
            m_pPolygonFeedback.Display = m_pMap.ActiveView.ScreenDisplay;
            m_pPolygonFeedback.Start(pPnt);
            m_bBusy = true;

            m_pPnts = new PolygonClass();
            object ep = System.Reflection.Missing.Value;
            m_pPnts.AddPoint(pPnt, ref ep, ref ep);
        }

        /// <summary>
        /// 开始测角度,pPnt为折线起点,测量结果为
        /// Direction:当前线段的方向角，即与正北方向的夹角
        /// Angle:当前线段与前一线段的夹角
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="pPnt">折线起点</param>
        public void AngleStart(IPoint pPnt)
        {
            m_iSurveyType = 3;

            m_pLineFeedback = new NewLineFeedbackClass();
            m_pLineFeedback.Display = m_pMap.ActiveView.ScreenDisplay;
            m_pLineFeedback.Start(pPnt);
            m_bBusy = true;

            m_pPnts = new PolylineClass();
            object ep = System.Reflection.Missing.Value;
            m_pPnts.AddPoint(pPnt, ref ep, ref ep);
        }

        /// <summary>
        /// 向折线或多边形上添加节点
        /// 在Map的MouseDown事件中调用本方法，将当前光标位置作为节点
        /// 添加到折线或多边形上
        /// </summary>
        /// <param name="pPnt">拐点</param>
        public void AddPoint(IPoint pPnt)
        {
            if (m_iSurveyType == 1 || m_iSurveyType == 3)
            {
                m_pLineFeedback.AddPoint(pPnt);
            }
            else if (m_iSurveyType == 2)
            {
                m_pPolygonFeedback.AddPoint(pPnt);
            }

            object ep = System.Reflection.Missing.Value;
            m_pPnts.AddPoint(pPnt, ref ep, ref ep);
        }

        /// <summary>
        /// 移动鼠标位置，动态改变折线或多边形最后节点的位置，
        /// 重新计算各个测量值，建议在Map的MouseMove事件中调用，
        /// 并提取当前的测量结果进行显示
        /// </summary>
        /// <param name="pPnt">终点</param>
        public void MoveTo(IPoint pPnt)
        {
            if (m_iSurveyType == 1 || m_iSurveyType == 3)
            {
                m_pLineFeedback.MoveTo(pPnt);
                CalculateLength(pPnt);
                CalculateDirection(pPnt);
                CalculateAngle(pPnt);
            }
            else if (m_iSurveyType == 2)
            {
                m_pPolygonFeedback.MoveTo(pPnt);
                CalculateArea(pPnt);
            }
        }

        /// <summary>
        /// 结束当前的测量,返回进行量算时在地图上绘的图形对象
        /// 建议在Map的DoubleClick事件或MouseDown(右键)事件中调用本方法
        /// 将返回对象用map.DrawShape方法绘在地图临时层上
        /// </summary>
        /// <param name="pPnt">终点</param>
        /// <returns>地图上绘的图形对象</returns>
        public IGeometry SurveyEnd(IPoint pPnt)
        {
            IGeometry pGeometry = null;

            if (m_iSurveyType == 1 || m_iSurveyType == 3)
            {
                if (m_pLineFeedback != null)
                {
                    m_pLineFeedback.AddPoint(pPnt);
                    pGeometry = (IGeometry)m_pLineFeedback.Stop();
                    m_pLineFeedback = null;
                }
            }
            else if (m_iSurveyType == 2)
            {
                if (m_pPolygonFeedback != null)
                {
                    m_pPolygonFeedback.AddPoint(pPnt);
                    pGeometry = (IGeometry)m_pPolygonFeedback.Stop();
                    m_pPolygonFeedback = null;
                }
            }

            m_pPnts = null;
            m_bBusy = false;
            if (pGeometry != null)
                return pGeometry;
            else
                return null;
        }

        /// <summary>
        /// 计算测量线段长度，结果分别存贮在：
        /// totalLength:线总长度
        /// currentLength:当前线段长度
        /// </summary>
        /// <param name="pPnt">计算时的终点</param>
        private void CalculateLength(IPoint pPnt)
        {
            try
            {
                IPointCollection pPs = new PolylineClass();
                double dL = 0;

                pPs.AddPointCollection(m_pPnts);

                IPolyline pLine;
                if (pPs.PointCount > 1)
                {
                    pLine = (IPolyline)pPs;
                    dL = pLine.Length;
                }

                object ep = System.Reflection.Missing.Value;
                pPs.AddPoint(pPnt, ref ep, ref ep);

                pLine = (IPolyline)pPs;
                m_dTotalLength = pLine.Length;
                m_dCurrentLength = m_dTotalLength - dL;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 计算多边形面积,结果存贮在Area中
        /// </summary>
        /// <param name="pPnt">计算时的终点</param>
        private void CalculateArea(IPoint pPnt)
        {
            try
            {
                IPointCollection pPs = new PolygonClass();

                pPs.AddPointCollection(m_pPnts);
                object ep = System.Reflection.Missing.Value;
                pPs.AddPoint(pPnt, ref ep, ref ep);

                if (pPs.PointCount > 2)
                {
                    IPolygon pPolygon = (IPolygon)pPs;
                    IArea pArea = (IArea)pPolygon;
                    m_dArea = pArea.Area;
                    m_dArea = System.Math.Abs(m_dArea);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        /// <summary>
        /// 计算当前线段的方向角,结果存贮为Direction中
        /// 正北方向为0度，顺时针为正，值域为0--360度
        /// </summary>
        /// <param name="pPnt">计算时的终点</param>
        private void CalculateDirection(IPoint pPnt)
        {
            double dx, dy, da;
            IPoint p1, p2;

            p1 = m_pPnts.get_Point(m_pPnts.PointCount - 1);
            p2 = pPnt;

            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;

            if (dx == 0)
            {
                if (dy > 0)
                {
                    m_dDirection = 0;
                }
                else
                {
                    m_dDirection = 180;
                }
            }
            else if (dx > 0)
            {
                if (dy == 0)
                {
                    m_dDirection = 90;
                }
                else if (dy > 0)
                {
                    da = System.Math.Abs(dx / dy);
                    m_dDirection = System.Math.Atan(da) * 180 / 3.14159265;
                }
                else if (dy < 0)
                {
                    da = System.Math.Abs(dx / dy);
                    m_dDirection = System.Math.Atan(da) * 180 / 3.14159265;
                    m_dDirection = 180 - m_dDirection;
                }
            }
            else
            {
                if (dy == 0)
                {
                    m_dDirection = 270;
                }
                else if (dy >= 0)
                {
                    da = System.Math.Abs(dx / dy);
                    m_dDirection = System.Math.Atan(da) * 180 / 3.14159265;
                    m_dDirection = 360 - m_dDirection;
                }
                else
                {
                    da = System.Math.Abs(dx / dy);
                    m_dDirection = System.Math.Atan(da) * 180 / 3.14159265;
                    m_dDirection = 180 + m_dDirection;
                }
            }
        }

        /// <summary>
        /// 计算当前线段与前一线段的夹角,结果存贮为Angle中
        /// 算法采用余弦定理，结果值域为0--180度. 
        /// </summary>
        /// <param name="pPnt">计算时的终点</param>
        private void CalculateAngle(IPoint pPnt)
        {
            int iCount = m_pPnts.PointCount;
            if (iCount < 2) return;

            double a, aa, b, bb, cc;
            IPoint Pnt1, Pnt2, Pnt3;
            try
            {
                Pnt1 = pPnt; Pnt2 = m_pPnts.get_Point(iCount - 1); Pnt3 = m_pPnts.get_Point(iCount - 2);

                aa = (Pnt1.X - Pnt2.X) * (Pnt1.X - Pnt2.X) + (Pnt1.Y - Pnt2.Y) * (Pnt1.Y - Pnt2.Y);
                a = Math.Sqrt(aa);

                bb = (Pnt3.X - Pnt2.X) * (Pnt3.X - Pnt2.X) + (Pnt3.Y - Pnt2.Y) * (Pnt3.Y - Pnt2.Y);
                b = Math.Sqrt(bb);

                cc = (Pnt1.X - Pnt3.X) * (Pnt1.X - Pnt3.X) + (Pnt1.Y - Pnt3.Y) * (Pnt1.Y - Pnt3.Y);

                m_dAngle = Math.Acos((aa + bb - cc) / 2 / a / b) * 180 / 3.14159265;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}

