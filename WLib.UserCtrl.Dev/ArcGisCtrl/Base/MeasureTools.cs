using System;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace WLib.UserCtrls.Dev.ArcGisCtrl.Base
{
    /// <summary>
    /// 测量工具
    /// </summary>
    internal class MeasureTools
    {
        /// <summary>
        /// 点集合
        /// </summary>
        private IPointCollection _pointCollection;
        /// <summary>
        /// 新建多边形对象
        /// </summary>
        private INewPolygonFeedback _newPolygonFeedback;
        /// <summary>
        /// 新建线对象
        /// </summary>
        private INewLineFeedback _newLineFeedback;
        /// <summary>
        /// 0-空操作；1-测距离； 2-测面积； 3-测角度
        /// </summary>
        private EMeasureType _eMeasureType;

        /// <summary>
        /// 测量工具
        /// </summary>
        internal MeasureTools(AxMapControl mapCtrl)
        {
            MapCtrl = mapCtrl;
            _pointCollection = null;
            _newPolygonFeedback = null;
            _newLineFeedback = null;
            _eMeasureType = 0;
            IsSurveying = false;
        }

        /// <summary>
        /// Map对象
        /// </summary>
        public AxMapControl MapCtrl { get; set; }
        /// <summary>
        /// 判断是否正在测量，只读
        /// </summary>
        public bool IsSurveying { get; private set; }
        /// <summary>
        /// 折线总长度，只读 
        /// </summary>
        public double TotalLength { get; private set; }
        /// <summary>
        /// 当前线段长度，只读
        /// </summary>
        public double CurrentLength { get; private set; }
        /// <summary>
        /// 多边形面积，只读
        /// </summary>
        public double Area { get; private set; }
        /// <summary>
        /// 当前线段与前一线段的夹角，只读
        /// </summary>
        public double Angle { get; private set; }
        /// <summary>
        /// 当前线段的方向角，只读
        /// </summary>
        public double Direction { get; private set; }

        /// <summary>
        /// 开始测角度,point为折线起点,测量结果为
        /// Direction:当前线段的方向角，即与正北方向的夹角
        /// Angle:当前线段与前一线段的夹角
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="point">折线起点</param>
        public void AngleStart(IPoint point)
        {
            _eMeasureType = EMeasureType.Angle;
            _newLineFeedback = new NewLineFeedbackClass { Display = MapCtrl.ActiveView.ScreenDisplay };
            _newLineFeedback.Start(point);

            _pointCollection = new PolylineClass();
            object ep = System.Reflection.Missing.Value;
            _pointCollection.AddPoint(point, ref ep, ref ep);
            IsSurveying = true;
        }
        /// <summary>
        /// 启动距离测量,point为测量起点，测量结果为
        /// totalLength:线总长度
        /// currentLength:当前线段长度
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="point">测量起点</param>
        public void LengthStart(IPoint point)
        {
            _eMeasureType = EMeasureType.Distance;
            _newLineFeedback = new NewLineFeedbackClass {Display = MapCtrl.ActiveView.ScreenDisplay};
            _newLineFeedback.Start(point);

            _pointCollection = new PolylineClass();
            object ep = System.Reflection.Missing.Value;
            _pointCollection.AddPoint(point, ref ep, ref ep);
            IsSurveying = true;
        }
        /// <summary>
        /// 启动面积测量,point为多边形起点,测量结果为
        /// Area:多边形面积
        /// 在Map的MouseDown事件中调用本方法
        /// </summary>
        /// <param name="point">测量起点</param>
        public void AreaStart(IPoint point)
        {
            _eMeasureType = EMeasureType.Area;
            _newPolygonFeedback = new NewPolygonFeedbackClass {Display = MapCtrl.ActiveView.ScreenDisplay};
            _newPolygonFeedback.Start(point);

            _pointCollection = new PolygonClass();
            object ep = System.Reflection.Missing.Value;
            _pointCollection.AddPoint(point, ref ep, ref ep);
            IsSurveying = true;
        }

        /// <summary>
        /// 向折线或多边形上添加节点
        /// 在Map的MouseDown事件中调用本方法，将当前光标位置作为节点
        /// 添加到折线或多边形上
        /// </summary>
        /// <param name="point">拐点</param>
        public void AddPoint(IPoint point)
        {
            if (_eMeasureType == EMeasureType.Distance || _eMeasureType == EMeasureType.Angle)
                _newLineFeedback.AddPoint(point);
            else if (_eMeasureType == EMeasureType.Area)
                _newPolygonFeedback.AddPoint(point);

            object ep = System.Reflection.Missing.Value;
            _pointCollection.AddPoint(point, ref ep, ref ep);
        }
        /// <summary>
        /// 移动鼠标位置，动态改变折线或多边形最后节点的位置，
        /// 重新计算各个测量值，建议在Map的MouseMove事件中调用，
        /// 并提取当前的测量结果进行显示
        /// </summary>
        /// <param name="point">终点</param>
        public void MoveTo(IPoint point)
        {
            if (_eMeasureType == EMeasureType.Distance || _eMeasureType == EMeasureType.Angle)
            {
                _newLineFeedback.MoveTo(point);
                CalculateLength(point);
                CalculateDirection(point);
                CalculateAngle(point);
            }
            else if (_eMeasureType == EMeasureType.Area)
            {
                _newPolygonFeedback.MoveTo(point);
                CalculateArea(point);
            }
        }
        /// <summary>
        /// 结束当前的测量,返回进行量算时在地图上绘的图形对象
        /// 建议在Map的DoubleClick事件或MouseDown(右键)事件中调用本方法
        /// 将返回对象用map.DrawShape方法绘在地图临时层上
        /// </summary>
        /// <param name="point">终点</param>
        /// <returns>地图上绘的图形对象</returns>
        public IGeometry SurveyEnd(IPoint point)
        {
            IGeometry geometry = null;
            if (_eMeasureType == EMeasureType.Distance || _eMeasureType == EMeasureType.Angle)
            {
                if (_newLineFeedback != null)
                {
                    _newLineFeedback.AddPoint(point);
                    geometry = _newLineFeedback.Stop();
                    _newLineFeedback = null;
                }
            }
            else if (_eMeasureType == EMeasureType.Area)
            {
                if (_newPolygonFeedback != null)
                {
                    _newPolygonFeedback.AddPoint(point);
                    geometry = _newPolygonFeedback.Stop();
                    _newPolygonFeedback = null;
                }
            }

            _pointCollection = null;
            IsSurveying = false;
            return geometry;
        }


        /// <summary>
        /// 计算测量线段长度，结果分别存贮在：
        /// totalLength:线总长度
        /// currentLength:当前线段长度
        /// </summary>
        /// <param name="point">计算时的终点</param>
        private void CalculateLength(IPoint point)
        {
            try
            {
                IPointCollection pointCollection = new PolylineClass();
                double dL = 0;
                pointCollection.AddPointCollection(_pointCollection);
                IPolyline polyline;
                if (pointCollection.PointCount > 1)
                {
                    polyline = (IPolyline)pointCollection;
                    dL = polyline.Length;
                }

                object ep = System.Reflection.Missing.Value;
                pointCollection.AddPoint(point, ref ep, ref ep);

                polyline = (IPolyline)pointCollection;
                TotalLength = polyline.Length;
                CurrentLength = TotalLength - dL;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 计算多边形面积,结果存贮在Area中
        /// </summary>
        /// <param name="point">计算时的终点</param>
        private void CalculateArea(IPoint point)
        {
            try
            {
                IPointCollection pointCollection = new PolygonClass();
                pointCollection.AddPointCollection(_pointCollection);
                object ep = System.Reflection.Missing.Value;
                pointCollection.AddPoint(point, ref ep, ref ep);

                if (pointCollection.PointCount > 2)
                {
                    IPolygon pPolygon = (IPolygon)pointCollection;
                    IArea pArea = (IArea)pPolygon;
                    Area = pArea.Area;
                    Area = Math.Abs(Area);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 计算当前线段的方向角,结果存贮为Direction中
        /// 正北方向为0度，顺时针为正，值域为0--360度
        /// </summary>
        /// <param name="point">计算时的终点</param>
        private void CalculateDirection(IPoint point)
        {
            double da;

            var point1 = _pointCollection.get_Point(_pointCollection.PointCount - 1);
            var point2 = point;

            var dx = point2.X - point1.X;
            var dy = point2.Y - point1.Y;

            if (dx == 0)
            {
                Direction = dy > 0 ? 0 : 180;
            }
            else if (dx > 0)
            {
                if (dy == 0)
                {
                    Direction = 90;
                }
                else if (dy > 0)
                {
                    da = Math.Abs(dx / dy);
                    Direction = Math.Atan(da) * 180 / 3.14159265;
                }
                else if (dy < 0)
                {
                    da = Math.Abs(dx / dy);
                    Direction = Math.Atan(da) * 180 / 3.14159265;
                    Direction = 180 - Direction;
                }
            }
            else
            {
                if (dy == 0)
                {
                    Direction = 270;
                }
                else if (dy >= 0)
                {
                    da = Math.Abs(dx / dy);
                    Direction = Math.Atan(da) * 180 / 3.14159265;
                    Direction = 360 - Direction;
                }
                else
                {
                    da = Math.Abs(dx / dy);
                    Direction = Math.Atan(da) * 180 / 3.14159265;
                    Direction = 180 + Direction;
                }
            }
        }
        /// <summary>
        /// 计算当前线段与前一线段的夹角,结果存贮为Angle中
        /// 算法采用余弦定理，结果值域为0--180度. 
        /// </summary>
        /// <param name="point">计算时的终点</param>
        private void CalculateAngle(IPoint point)
        {
            int iCount = _pointCollection.PointCount;
            if (iCount < 2) return;

            try
            {
                var pnt1 = point; var pnt2 = _pointCollection.get_Point(iCount - 1); var pnt3 = _pointCollection.get_Point(iCount - 2);

                var aa = (pnt1.X - pnt2.X) * (pnt1.X - pnt2.X) + (pnt1.Y - pnt2.Y) * (pnt1.Y - pnt2.Y);
                var a = Math.Sqrt(aa);

                var bb = (pnt3.X - pnt2.X) * (pnt3.X - pnt2.X) + (pnt3.Y - pnt2.Y) * (pnt3.Y - pnt2.Y);
                var b = Math.Sqrt(bb);

                var cc = (pnt1.X - pnt3.X) * (pnt1.X - pnt3.X) + (pnt1.Y - pnt3.Y) * (pnt1.Y - pnt3.Y);

                Angle = Math.Acos((aa + bb - cc) / 2 / a / b) * 180 / 3.14159265;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

