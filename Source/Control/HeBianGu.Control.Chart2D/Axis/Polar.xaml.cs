// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    /// <summary>极坐标 </summary>
    public class Polar : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Polar), "S.Polar.Default");

        static Polar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Polar), new FrameworkPropertyMetadata(typeof(Polar)));
        }
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(Polar), new PropertyMetadata(200.0, (d, e) =>
             {
                 Polar control = d as Polar;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        protected virtual void DrawCircle(Canvas canvas)
        {
            //  Do ：绘制圆环 

            foreach (double item in this.xAxis)
            {
                Path path = new Path();

                path.Style = this.PathStyle;

                EllipseGeometry ellipse = new EllipseGeometry(new Rect(0, 0, this.GetX(item, this.Len * 2), this.GetX(item, this.Len * 2)));

                ellipse.Center = new Point(0, 0);

                path.Data = ellipse;

                this.Children.Add(path);
            }
        }

        private void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);

            double angle = 360 / this.yAxis.Count;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                Point start = new Point(this.Len, center.Y);

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle * i, center.X, center.Y);

                Point end = matrix.Transform(start);

                Path path = new Path();
                path.Style = this.PathStyle;

                PathFigure pf = new PathFigure();
                pf.StartPoint = center;
                pf.Segments.Add(new LineSegment(end, true));

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                path.Data = pg;

                this.Children.Add(path);
            }

        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawCircle(this);

            this.DrawLine(this);
        }
    }

    public class Radar : Polar
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Radar), "S.Radar.Default");

        static Radar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Radar), new FrameworkPropertyMetadata(typeof(Radar)));
        }
        protected override void DrawCircle(Canvas canvas)
        {

            Point center = new Point(0, 0);

            if (this.yAxis.Count == 0)
                return;

            double angle = 360 / this.yAxis.Count;

            //  Do ：绘制圆环 
            foreach (double item in this.xAxis)
            {
                Path path = new Path();
                path.Style = this.PathStyle;

                PathFigure pf = new PathFigure();

                double len = this.GetX(item, this.Len);

                for (int i = 0; i < this.yAxis.Count; i++)
                {
                    if (i == 0)
                    {
                        pf.StartPoint = new Point(len, center.Y);
                        continue;
                    }
                    Point start = new Point(len, center.Y);

                    Matrix matrix = new Matrix();

                    matrix.RotateAt(angle * i, center.X, center.Y);

                    Point end = matrix.Transform(start);

                    pf.Segments.Add(new LineSegment(end, true));
                }
                pf.IsClosed = true;

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                path.Data = pg;

                this.Children.Add(path);
            }
        }
    }
}
