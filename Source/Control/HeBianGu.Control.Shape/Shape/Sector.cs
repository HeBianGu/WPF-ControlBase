// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Shape
{
    /// <summary> 扇形 </summary>
    public class Sector : System.Windows.Shapes.Shape
    {
        /// <summary> 起始角度 </summary>
        public double Start
        {
            get { return (double)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(double), typeof(Sector), new PropertyMetadata(0.0, (d, e) =>
             {
                 Sector control = d as Sector;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateVisual();

             }));

        /// <summary> 结束角度 </summary>
        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(double), typeof(Sector), new PropertyMetadata(90.0, (d, e) =>
             {
                 Sector control = d as Sector;

                 if (control == null) return;

                 //double config = e.NewValue as double; 

                 control.InvalidateVisual();

             }));

        /// <summary> 角度偏移 </summary>
        public double OffSet
        {
            get { return (double)GetValue(OffSetProperty); }
            set { SetValue(OffSetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffSetProperty =
            DependencyProperty.Register("OffSet", typeof(double), typeof(Sector), new PropertyMetadata(default(double), (d, e) =>
             {
                 Sector control = d as Sector;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateVisual();

             }));


        /// <summary> 半径 </summary>
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(Sector), new PropertyMetadata(100.0, (d, e) =>
             {
                 Sector control = d as Sector;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.InvalidateVisual();

             }));

        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                if (this.End < this.Start) return null;

                //Point center = new Point(this.DesiredSize.Width / 2, this.DesiredSize.Height / 2);

                Point center = new Point(0, 0);


                Debug.WriteLine(center);


                double startAngle = this.Start + this.OffSet;

                double endAngle = this.End + this.OffSet;

                //  Do ：增加扇形

                ArcSegment arc = new ArcSegment();

                Matrix matrix = new Matrix();

                Point p = new Point(center.X - Len, center.X);

                matrix.RotateAt(startAngle, center.X, center.Y);

                arc.Point = matrix.Transform(p);

                Point start = arc.Point;

                matrix = new Matrix();

                //Path path = new Path();
                //path.Style = this.PathStyle;

                //path.Fill = path.Stroke = new SolidColorBrush(this.Foreground[i]);

                PathFigure pf = new PathFigure();
                pf.StartPoint = center;
                pf.Segments.Add(new LineSegment(start, true));

                //if (endAngle - startAngle > 180)
                //{
                //matrix.RotateAt((endAngle - startAngle) / 2, center.X, center.Y);
                //Point end = matrix.Transform(start);
                //pf.Segments.Add(new ArcSegment(end, new Size(Len, Len), 0, false, SweepDirection.Clockwise, true));

                //end = matrix.Transform(end);
                //pf.Segments.Add(new ArcSegment(end, new Size(Len, Len), 0, false, SweepDirection.Clockwise, true)); 

                matrix.RotateAt(endAngle - startAngle, center.X, center.Y);
                Point end = matrix.Transform(start);
                pf.Segments.Add(new ArcSegment(end, new Size(Len, Len), 0, endAngle - startAngle > 180, SweepDirection.Clockwise, true));
                //}
                //else
                //{
                //    matrix.RotateAt(endAngle - startAngle, center.X, center.Y);
                //    Point end = matrix.Transform(start);
                //    pf.Segments.Add(new ArcSegment(end, new Size(Len, Len), 0, false, SweepDirection.Clockwise, true));

                //}


                pf.Segments.Add(new LineSegment(center, true));
                //pf.IsClosed = true;

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });



                return pg;
            }
        }

        ///// <summary> 绘制形状 </summary>
        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);

        //    Pen p = new Pen(this.Fill, 1);

        //    drawingContext.DrawGeometry(Brushes.White, p, DefiningGeometry);
        //}
    }

}
