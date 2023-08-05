// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class LineBase : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LineBase), "S.LineBase.Default");

        static LineBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineBase), new FrameworkPropertyMetadata(typeof(LineBase)));
        }
        public ThinningType ThinningType
        {
            get { return (ThinningType)GetValue(ThinningTypeProperty); }
            set { SetValue(ThinningTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThinningTypeProperty =
            DependencyProperty.Register("ThinningType", typeof(ThinningType), typeof(LineBase), new PropertyMetadata(default(ThinningType), (d, e) =>
             {
                 LineBase control = d as LineBase;

                 if (control == null) return;

                 //ThinningType config = e.NewValue as ThinningType;

             }));


        public int Threshold
        {
            get { return (int)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(int), typeof(LineBase), new PropertyMetadata(10, (d, e) =>
             {
                 LineBase control = d as LineBase;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


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

        public override void Draw(Canvas canvas)
        {
            //base.Draw(canvas);

            this.InitX();

            this.InitY();

        }


        /// <summary>
        /// 获得贝塞尔曲线
        /// </summary>
        /// <param name="currentPt">当前点</param>
        /// <param name="lastPt">上一个点</param>
        /// <param name="nextPt1">下一个点1</param>
        /// <param name="nextPt2">下一个点2</param>
        /// <returns></returns>
        protected BezierSegment GetBezierSegment(Point currentPt, Point lastPt, Point nextPt1, Point nextPt2)
        {
            //计算中点
            Point lastC = GetCenterPoint(lastPt, currentPt);
            Point nextC1 = GetCenterPoint(currentPt, nextPt1); //贝塞尔控制点
            Point nextC2 = GetCenterPoint(nextPt1, nextPt2);

            //计算相邻中点连线跟目的点的垂足
            //效果并不算太好，因为可能点在两个线上或者线的延长线上，计算会有误差
            //所以就直接使用中点平移方法。
            //var C1 = GetFootPoint(lastC, nextC1, currentPt);
            //var C2 = GetFootPoint(nextC1, nextC2, nextPt1);


            //计算“相邻中点”的中点
            Point c1 = GetCenterPoint(lastC, nextC1);
            Point c2 = GetCenterPoint(nextC1, nextC2);


            //计算【"中点"的中点】需要的点位移
            Vector controlPtOffset1 = currentPt - c1;
            Vector controlPtOffset2 = nextPt1 - c2;

            //移动控制点
            Point controlPt1 = nextC1 + controlPtOffset1;
            Point controlPt2 = nextC1 + controlPtOffset2;

            //如果觉得曲线幅度太大，可以将控制点向当前点靠近一定的系数。
            controlPt1 = controlPt1 + 0 * (currentPt - controlPt1);
            controlPt2 = controlPt2 + 0 * (nextPt1 - controlPt2);

            BezierSegment bzs = new BezierSegment(controlPt1, controlPt2, nextPt1, true);
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
            double offsetADist = (Math.Pow(cPoint.X - aPoint.X, 2) + Math.Pow(cPoint.Y - aPoint.Y, 2) - Math.Pow(bPoint.X - cPoint.X, 2) - Math.Pow(bPoint.Y - cPoint.Y, 2) + Math.Pow(aPoint.X - bPoint.X, 2) + Math.Pow(aPoint.Y - bPoint.Y, 2)) / (2 * GetDistance(aPoint, bPoint));

            Vector v = bPoint - aPoint;
            double distab = GetDistance(aPoint, bPoint);
            Vector offsetVector = v * offsetADist / distab;
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

    public class Line : LineBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Line), "S.Line.Default");
        public static ComponentResourceKey AlignmentCenterKey => new ComponentResourceKey(typeof(Line), "S.Line.AlignmentCenter");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(Line), "S.Line.Single");

        static Line()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Line), new FrameworkPropertyMetadata(typeof(Line)));
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            this.DrawLine(canvas);
        }

        protected virtual void DrawLine(Canvas canvas)
        {
            if (this.xAxis.Count == 1) 
                return;
            if (this.Data == null)
                return;
            if (this.UseVisual)
            {
                PointCollection points = new PointCollection();

                for (int i = 0; i < this.xAxis.Count; i++)
                {
                    double x = this.xAxis[i];

                    double y = this.Data[i];

                    y = this.GetMapY(i, y);

                    points.Add(new Point(this.GetX(x), this.GetY(y)));

                }

                IEnumerable<Setter> setters = PathStyle?.Setters.OfType<Setter>();

                object st = setters?.FirstOrDefault(l => l.Property.Name == "StrokeThickness")?.Value;

                double thickness = st == null ? 1 : Convert.ToDouble(st);

                Brush brush = this.Foreground.CloneCurrentValue();

                Visual v = this.Polyline(points, brush, thickness);

                this.Clear();

                this.AddVisual(v);
            }
            else
            {
                Path path = new Path();
                path.Style = this.PathStyle;
                path.StrokeLineJoin=PenLineJoin.Round;
                PolyLineSegment pls = new PolyLineSegment();
                for (int i = 0; i < this.xAxis.Count; i++)
                {
                    double x = this.xAxis[i];
                    double y = this.Data[i];
                    y = this.GetMapY(i, y);
                    //  Do ：添加曲线
                    pls.Points.Add(new Point(this.GetX(x), this.GetY(y)));
                }

                //DouglasProvider.Instance.Do(pls.Points.ToList(), 10);

                //var outPoints = DouglasProvider.Instance.GetPoints(); 

                if (this.ThinningType == ThinningType.Douglas)
                {
                    Debug.WriteLine("抽稀前点数:" + pls.Points.Count);

                    List<Point> outPoints = DouglasProvider.DouglasThinningMachine(pls.Points.ToList(), this.Threshold);

                    pls.Points = new PointCollection(outPoints);

                    Debug.WriteLine("抽稀后点数:" + pls.Points.Count);

                }

                PathFigure pf = new PathFigure();
                pf.StartPoint = pls.Points.FirstOrDefault();
                pf.Segments.Add(pls);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                path.Data = pg;

                this.Clear();

                this.Children.Add(path);
            }
        }

    }

    public class Area : LineBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Area), "S.Area.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(Area), "S.Area.Single");

        static Area()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Area), new FrameworkPropertyMetadata(typeof(Area)));
        }


        public bool UseSmooth
        {
            get { return (bool)GetValue(UseSmoothProperty); }
            set { SetValue(UseSmoothProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSmoothProperty =
            DependencyProperty.Register("UseSmooth", typeof(bool), typeof(Area), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 Area control = d as Area;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

                 control.TryDraw();

             }));


        public Style PathAreaStyle
        {
            get { return (Style)GetValue(PathAreaStyleProperty); }
            set { SetValue(PathAreaStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathAreaStyleProperty =
            DependencyProperty.Register("PathAreaStyle", typeof(Style), typeof(Area), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 Area control = d as Area;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));


        public Brush AreaBackground
        {
            get { return (Brush)GetValue(AreaBackgroundProperty); }
            set { SetValue(AreaBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AreaBackgroundProperty =
            DependencyProperty.Register("AreaBackground", typeof(Brush), typeof(Area), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 Area control = d as Area;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Path path = new Path();
            path.Style = this.PathStyle;

            Path pathArea = new Path();
            pathArea.Style = this.PathAreaStyle;
            pathArea.Fill = this.AreaBackground;

            if (this.UseSmooth)
            {
                PathFigure pf = new PathFigure();
                PathFigure pfArea = new PathFigure();

                List<Point> points = new List<Point>();
                for (int i = 0; i < this.xAxis.Count; i++)
                {
                    double x = this.xAxis[i];
                    double y = this.Data[i];
                    points.Add(new Point(this.GetX(x), this.GetY(y)));
                }

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
                    BezierSegment bzs = GetBezierSegment(points[current], points[last], points[next], points[next2]);

                    pf.Segments.Add(bzs);
                    pfArea.Segments.Add(bzs);
                }

                pf.StartPoint = points.FirstOrDefault();
                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });
                path.Data = pg;

                PolyLineSegment area = new PolyLineSegment();
                area.Points.Add(new Point(this.ActualWidth, this.ActualHeight));
                area.Points.Add(new Point(0, this.ActualHeight));
                area.Points.Add(points.FirstOrDefault());
                pfArea.Segments.Add(area);
                pfArea.StartPoint = points.FirstOrDefault();
                PathGeometry pgArea = new PathGeometry(new List<PathFigure>() { pfArea });
                pathArea.Data = pgArea;
            }
            else
            {
                PolyLineSegment point = new PolyLineSegment();
                PolyLineSegment area = new PolyLineSegment();

                for (int i = 0; i < this.xAxis.Count; i++)
                {
                    double x = this.xAxis[i];
                    double y = this.Data[i];
                    area.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));
                    point.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));
                }

                PathFigure pf = new PathFigure();
                pf.Segments.Add(point);
                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });
                path.Data = pg;

                PathFigure pfArea = new PathFigure();
                area.Points.Add(new Point(this.ActualWidth, this.ActualHeight));
                area.Points.Add(new Point(0, this.ActualHeight));
                //area.Points.Add(area.Points.FirstOrDefault());
                pfArea.Segments.Add(area);
                pfArea.StartPoint = point.Points.FirstOrDefault();

                PathGeometry pgArea = new PathGeometry(new List<PathFigure>() { pfArea });
                pathArea.Data = pgArea;
            }

            this.Clear();
            this.Children.Add(pathArea);
            this.Children.Add(path);
        }
    }

    public class PolarLine : Line
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PolarLine), "S.PolarLine.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PolarLine), "S.PolarLine.Single");

        static PolarLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PolarLine), new FrameworkPropertyMetadata(typeof(PolarLine)));
        }

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

            this.Clear();

            this.Children.Add(path);

            path.Loaded += (l, k) =>
            {
                this.RunPath(path);
            };
        }

    }

    public class RadarLine : PolarLine
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(RadarLine), "S.RadarLine.Default");

        static RadarLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadarLine), new FrameworkPropertyMetadata(typeof(RadarLine)));
        }

        protected override void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);
            Path path = new Path();
            path.Style = this.PathStyle;
            PolyLineSegment pls = new PolyLineSegment();
            for (int i = 0; i < this.yAxis.Count; i++)
            {
                double x = this.yAxis[i];
                if (this.Data.Count <= i)
                    continue;
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

            this.Clear();

            this.Children.Add(path);

            path.Loaded += (l, k) =>
            {
                this.RunPath(path);
            };

        }
    }

    public class SmoothLine : LineBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SmoothLine), "S.SmoothLine.Default");
        public static ComponentResourceKey AlignmentCenterKey => new ComponentResourceKey(typeof(SmoothLine), "S.SmoothLine.AlignmentCenter");

        static SmoothLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SmoothLine), new FrameworkPropertyMetadata(typeof(SmoothLine)));
        }

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
                BezierSegment bzs = GetBezierSegment(points[current], points[last], points[next], points[next2]);

                pf.Segments.Add(bzs);
            }

            pf.StartPoint = points.FirstOrDefault();

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Clear();

            this.Children.Add(path);

        }
    }

    public class StepLine : LineBase
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(StepLine), "S.StepLine.Default");

        static StepLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepLine), new FrameworkPropertyMetadata(typeof(StepLine)));
        }

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

            this.Clear();

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
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Candlestick), "S.Candlestick.Default");

        static Candlestick()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Candlestick), new FrameworkPropertyMetadata(typeof(Candlestick)));
        }

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
        public static new readonly DependencyProperty DataProperty =
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

            this.Clear();

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
