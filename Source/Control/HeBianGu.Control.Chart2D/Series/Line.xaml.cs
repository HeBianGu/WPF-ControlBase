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
                xAxis control = d as xAxis;

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

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：添加曲线
                pls.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));

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
    public class PolayLine : LineBase
    {

        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(PolayLine), new PropertyMetadata(200.0, (d, e) =>
             {
                 PolayLine control = d as PolayLine;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

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

        }

        //void DrawLine(Canvas canvas)
        //{
        //    Point center = new Point(0, 0);

        //    for (int i = 0; i < this.yAxis.Count; i++)
        //    {
        //        double y = this.yAxis[i];

        //        double angle = y / 360;

        //        Path path = new Path();
        //        path.Style = this.LineStyle;

        //        PathFigure pf = new PathFigure();
        //        {
        //            Point start = new Point(-this.Len, center.Y);

        //            Matrix matrix = new Matrix();

        //            matrix.RotateAt(angle, center.X, center.Y);

        //            Point end = matrix.Transform(start);

        //            pf.StartPoint = end;

        //        }

        //        {
        //            Point start = new Point(-this.Len - this.AlignLenght, center.Y);

        //            Matrix matrix = new Matrix();

        //            matrix.RotateAt(angle * i, center.X, center.Y);

        //            Point end = matrix.Transform(start);

        //            pf.Segments.Add(new LineSegment(end, true));

        //        }

        //        PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

        //        path.Data = pg;

        //        this.Children.Add(path);


        //    }
        //}
    }

}
