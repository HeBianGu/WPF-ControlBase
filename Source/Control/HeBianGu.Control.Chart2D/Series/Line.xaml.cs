using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    public class LineBase : Layer
    {
        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(LineBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                LineBase control = d as LineBase;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.Draw(control);

            }));


        protected override void InitX()
        {
            base.InitX();

            if (this.AlignmentCenter)
            {
                double span = (this.maxX - this.minX) / this.xAxis.Count;

                this.maxX = this.maxX + span / 2;

                this.minX = this.minX - span / 2;
            }
        }
    }

    /// <summary> 曲线视图 </summary>
    public class Line : LineBase
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawLine(canvas);
        }

        protected virtual void DrawLine(Canvas canvas)
        {

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                y = this.GetMapY(i, y);

                //  Do ：添加曲线
                pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));

            }

            PathFigure pf = new PathFigure();
            pf.StartPoint = pls.Points.FirstOrDefault();
            pf.Segments.Add(pls);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

        }



    }

    public class Area : LineBase
    {

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment area = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));

            }

            area.Points.Add(new Point(this.ActualWidth, this.ActualHeight));

            area.Points.Add(new Point(0, this.ActualHeight));

            area.Points.Add(area.Points.FirstOrDefault());

            PathFigure pf = new PathFigure();
            pf.StartPoint = area.Points.FirstOrDefault();
            pf.Segments.Add(area);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);



        }
    }

    /// <summary> 极坐标曲线图 </summary>
    public class PolarLine : Line
    {

        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(PolarLine), new PropertyMetadata(200.0, (d, e) =>
             {
                 PolarLine control = d as PolarLine;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        protected override void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.Data.Count; i++)
            {
                double x = this.yAxis[i];

                double d = this.Data[i];

                double angle = x;

                Point start = new Point(this.GetX(d, this.Len), center.Y);

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle, center.X, center.Y);

                Point end = matrix.Transform(start);

                //  Do ：添加曲线
                //pls.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                pls.Points.Add(end);
            }

            PathFigure pf = new PathFigure();
            pf.StartPoint = pls.Points.FirstOrDefault();
            pf.Segments.Add(pls);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

            path.Loaded += (l, k) =>
            {
                this.RunPath(path);
            };
        }

    }


    /// <summary> 极坐标曲线图 </summary>
    public class RadarLine : PolarLine
    {
        protected override void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                double x = this.yAxis[i];

                double d = this.Data[i];

                double angle = x;

                Point start = new Point(this.GetX(d, this.Len), center.Y);

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle, center.X, center.Y);

                Point end = matrix.Transform(start);

                //  Do ：添加曲线
                //pls.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                pls.Points.Add(end);
            }

            PathFigure pf = new PathFigure();
            pf.StartPoint = pls.Points.FirstOrDefault();

            pf.IsClosed = true;
            pf.Segments.Add(pls);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

            path.Loaded += (l, k) =>
            {
                this.RunPath(path);
            };

        }
    }


    /// <summary> 曲线视图 </summary>
    public class SmoothLine : LineBase
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawLine(canvas);
        }

        protected virtual void DrawLine(Canvas canvas)
        {

            Path path = new Path();

            path.Style = this.PathStyle;

            List<Point> points = new List<Point>();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                points.Add(new Point(this.GetX(x), this.GetY(y)));
            }


            PathFigure pf = new PathFigure();

            for (int i = 0; i < points.Count - 1; i++)
            {
                int current = i, last = i - 1, next = i + 1, next2 = i + 2;

                if (last == -1)
                {
                    last = 0;
                }
                if (next == points.Count)
                {
                    next = points.Count - 1;
                }
                if (next2 == points.Count)
                {
                    next2 = points.Count - 1;
                }
                var bzs = GetBezierSegment(points[current], points[last], points[next], points[next2]);

                pf.Segments.Add(bzs);

            }

            pf.StartPoint = points.FirstOrDefault();

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

        }

        /// <summary>
        /// 获得贝塞尔曲线
        /// </summary>
        /// <param name="currentPt">当前点</param>
        /// <param name="lastPt">上一个点</param>
        /// <param name="nextPt1">下一个点1</param>
        /// <param name="nextPt2">下一个点2</param>
        /// <returns></returns>
        private BezierSegment GetBezierSegment(Point currentPt, Point lastPt, Point nextPt1, Point nextPt2)
        {
            //计算中点
            var lastC = GetCenterPoint(lastPt, currentPt);
            var nextC1 = GetCenterPoint(currentPt, nextPt1); //贝塞尔控制点
            var nextC2 = GetCenterPoint(nextPt1, nextPt2);

            //计算相邻中点连线跟目的点的垂足
            //效果并不算太好，因为可能点在两个线上或者线的延长线上，计算会有误差
            //所以就直接使用中点平移方法。
            //var C1 = GetFootPoint(lastC, nextC1, currentPt);
            //var C2 = GetFootPoint(nextC1, nextC2, nextPt1);


            //计算“相邻中点”的中点
            var c1 = GetCenterPoint(lastC, nextC1);
            var c2 = GetCenterPoint(nextC1, nextC2);


            //计算【"中点"的中点】需要的点位移
            var controlPtOffset1 = currentPt - c1;
            var controlPtOffset2 = nextPt1 - c2;

            //移动控制点
            var controlPt1 = nextC1 + controlPtOffset1;
            var controlPt2 = nextC1 + controlPtOffset2;

            //如果觉得曲线幅度太大，可以将控制点向当前点靠近一定的系数。
            controlPt1 = controlPt1 + 0 * (currentPt - controlPt1);
            controlPt2 = controlPt2 + 0 * (nextPt1 - controlPt2);

            var bzs = new BezierSegment(controlPt1, controlPt2, nextPt1, true);
            return bzs;
        }

        /// <summary>
        ///     过c点做A和B连线的垂足
        /// </summary>
        /// <param name="aPoint"></param>
        /// <param name="bPoint"></param>
        /// <param name="cPoint"></param>
        /// <returns></returns>
        private Point GetFootPoint(Point aPoint, Point bPoint, Point cPoint)
        {
            //设三点坐标是A，B，C，AB构成直线，C是线外的点
            //三点对边距离是a,b,c,垂足为D，
            //根据距离推导公式得：AD距离是（b平方-a平方+c平方）/2c
            //本人数学不好，可能没考虑点c在线ab上的情况
            var offsetADist = (Math.Pow(cPoint.X - aPoint.X, 2) + Math.Pow(cPoint.Y - aPoint.Y, 2) - Math.Pow(bPoint.X - cPoint.X, 2) - Math.Pow(bPoint.Y - cPoint.Y, 2) + Math.Pow(aPoint.X - bPoint.X, 2) + Math.Pow(aPoint.Y - bPoint.Y, 2)) / (2 * GetDistance(aPoint, bPoint));

            var v = bPoint - aPoint;
            var distab = GetDistance(aPoint, bPoint);
            var offsetVector = v * offsetADist / distab;
            return aPoint + offsetVector;
        }

        private Point GetCenterPoint(Point pt1, Point pt2)
        {
            return new Point((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
        }

        private double GetDistance(Point pt1, Point pt2)
        {
            return Math.Sqrt(Math.Pow(pt1.X - pt2.X, 2) + Math.Pow(pt1.Y - pt2.Y, 2));
        }
    }


    /// <summary> 曲线视图 </summary>
    public class StepLine : LineBase
    {

        public StepType StepType
        {
            get { return (StepType)GetValue(StepTypeProperty); }
            set { SetValue(StepTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepTypeProperty =
            DependencyProperty.Register("StepType", typeof(StepType), typeof(StepLine), new PropertyMetadata(default(StepType), (d, e) =>
             {
                 StepLine control = d as StepLine;

                 if (control == null) return;

                 //StepType config = e.NewValue as StepType;

                 control.TryDraw();

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawLine(canvas);
        }

        protected virtual void DrawLine(Canvas canvas)
        {

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                y = this.GetMapY(i, y);

                if (this.StepType == StepType.Start)
                {
                    if (i == this.xAxis.Count - 1)
                    {
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                    }
                    else
                    {
                        double yn = this.Data[i + 1];

                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(yn)));
                    }
                }
                else if (this.StepType == StepType.End)
                {
                    if (i == 0)
                    {
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                    }
                    else
                    {
                        double yl = this.Data[i - 1];

                        pls.Points.Add(new Point(this.GetX(x), this.GetY(yl)));
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                    }
                }
                else if (this.StepType == StepType.Center)
                {
                    if (i == 0)
                    {
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                    }

                    if (i == this.xAxis.Count - 1)
                    {
                        pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                    }
                    else
                    {
                        double yn = this.Data[i + 1];
                        double xc = (this.xAxis[i + 1] - this.xAxis[i]) / 2 + this.xAxis[i];

                        pls.Points.Add(new Point(this.GetX(xc), this.GetY(y)));
                        pls.Points.Add(new Point(this.GetX(xc), this.GetY(yn)));
                    }
                }
                else
                {
                    pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                }
            }

            PathFigure pf = new PathFigure();
            pf.StartPoint = pls.Points.FirstOrDefault();
            pf.Segments.Add(pls);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

        }



    }


    public enum StepType
    {
        Default, Start, End, Center
    }

    /// <summary> K线图 </summary>
    public class Candlestick : LineBase
    {

        public Brush ForegroundContrarty
        {
            get { return (Brush)GetValue(ForegroundContrartyProperty); }
            set { SetValue(ForegroundContrartyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundContrartyProperty =
            DependencyProperty.Register("ForegroundContrarty", typeof(Brush), typeof(Candlestick), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 Candlestick control = d as Candlestick;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

                 control.TryDraw();

             }));



        /// <summary> ohlc </summary>
        [TypeConverter(typeof(DataArrayTypeConverter))]
        public new ObservableCollection<double[]> Data
        {
            get { return (ObservableCollection<double[]>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double[]>), typeof(Candlestick), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
             {
                 Candlestick control = d as Candlestick;

                 if (control == null) return;

                 ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

             }));

        public double WidthPercent
        {
            get { return (double)GetValue(WidthPercentProperty); }
            set { SetValue(WidthPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.Register("WidthPercent", typeof(double), typeof(Candlestick), new PropertyMetadata(0.5, (d, e) =>
             {
                 Candlestick control = d as Candlestick;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        protected override void InitX()
        {
            base.InitX();

            double span = (this.maxX - this.minX) / this.xAxis.Count;

            this.maxX = this.maxX + span / 2;

            this.minX = this.minX - span / 2;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawLine(canvas);
        }

        protected virtual void DrawLine(Canvas canvas)
        {
            double span = (this.maxX - this.minX) / this.xAxis.Count;

            double itemWidth = span * this.WidthPercent;

            if (this.Data == null || this.xAxis == null) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double open = this.Data[i][0];

                double close = this.Data[i][1];

                double low = this.Data[i][2];

                double hight = this.Data[i][3];

                Path path = new Path();

                path.Style = this.PathStyle;

                path.Stroke = path.Fill = open >= close ? this.ForegroundContrarty : this.Foreground;

                PolyLineSegment area = new PolyLineSegment();

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX((x - itemWidth / 2)), this.GetY(open)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2)), this.GetY(close)));

                area.Points.Add(new Point(this.GetX((x + itemWidth / 2)), this.GetY(close)));

                area.Points.Add(new Point(this.GetX((x + itemWidth / 2)), this.GetY(open)));

                area.Points.Add(area.Points.FirstOrDefault());

                PathFigure pf = new PathFigure();
                pf.StartPoint = area.Points.FirstOrDefault();
                pf.Segments.Add(area);

                PolyLineSegment line = new PolyLineSegment();

                //  Do ：添加曲线
                line.Points.Add(new Point(this.GetX(x), this.GetY(low)));

                line.Points.Add(new Point(this.GetX(x), this.GetY(hight)));

                PathFigure pfline = new PathFigure();
                pfline.StartPoint = line.Points.FirstOrDefault();
                pfline.Segments.Add(line);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf, pfline });

                path.Data = pg;

                //  Do ：用于标识Legend显示信息
                path.Tag = this.xDisplay.Count > i ? this.xDisplay[i] : this.Data[i].ToString();

                this.Children.Add(path);
            }

        }



    }
}
