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
    /// <summary>极坐标 </summary>
    public class Polar : Layer
    {

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

        void DrawCircle(Canvas canvas)
        {
            //  Do ：绘制圆环 

            foreach (var item in this.xAxis)
            {
                Path path = new Path();

                path.Style = this.PathStyle;

                EllipseGeometry ellipse = new EllipseGeometry(new Rect(0, 0, this.GetX(item, this.Len*2), this.GetX(item, this.Len * 2)));

                ellipse.Center = new Point(0, 0);

                path.Data = ellipse;

                this.Children.Add(path);
            }

        }

        void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);

            double angle = 360 / this.yAxis.Count;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                Point start = new Point(-this.Len, center.Y);

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

            //foreach (var item in this.yAxis)
            //{
            //    System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
            //    l.X1 = 0;
            //    l.Y1 = 0;
            //    l.Y2 = 0;
            //    l.Height = 1;
            //    l.Style = this.VerticalLineStyle;
            //    l.X2 = this.ActualWidth;

            //    //   l.MouseEnter +=(m, k) =>
            //    //{
            //    //    l.Stroke = Brushes.Red;

            //    //};


            //    Canvas.SetTop(l, this.GetY(item, this.ActualHeight));

            //    this.Children.Add(l);
            //}

            //foreach (var item in this.xAxis)
            //{
            //    System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
            //    l.X1 = 0;
            //    l.Y1 = 0;
            //    l.Y2 = this.ActualHeight;
            //    l.Style = this.HorizontalLineStyle;
            //    l.X2 = 0;
            //    l.Width = 1;

            //    Canvas.SetLeft(l, this.GetX(item, this.ActualWidth));
            //    this.Children.Add(l);
            //}
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawCircle(this);

            this.DrawLine(this);
        }

        //protected override void InitX()
        //{
        //    if (this.xAxis.Count > 0)
        //    {
        //        this.minX = this.xAxis.Min();
        //        this.maxX = this.xAxis.Max();
        //    }
        //}

        //protected override void InitY()
        //{
        //    if (this.yAxis.Count > 0)
        //    {
        //        this.minY = this.yAxis.Min();
        //        this.maxY = this.yAxis.Max();
        //    }
        //}
    }
}
