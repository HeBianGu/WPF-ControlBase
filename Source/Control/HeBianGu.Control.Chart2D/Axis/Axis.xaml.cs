// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class Axis : XyLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Axis), "S.Axis.Default");

        static Axis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Axis), new FrameworkPropertyMetadata(typeof(Axis)));
        }
        public int AlignLenght
        {
            get { return (int)GetValue(AlignLenghtProperty); }
            set { SetValue(AlignLenghtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignLenghtProperty =
            DependencyProperty.Register("AlignLenght", typeof(int), typeof(Axis), new PropertyMetadata(5, (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 //int config = e.NewValue as int;


             }));

        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.Register("LabelStyle", typeof(Style), typeof(Axis), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

        public bool TextAlignmentCenter
        {
            get { return (bool)GetValue(TextAlignmentCenterProperty); }
            set { SetValue(TextAlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextAlignmentCenterProperty =
            DependencyProperty.Register("TextAlignmentCenter", typeof(bool), typeof(Axis), new PropertyMetadata(default(bool), (d, e) =>
            {
                Axis control = d as Axis;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.TryDraw();

            }));


        public bool AlignAlignmentCenter
        {
            get { return (bool)GetValue(AlignAlignmentCenterProperty); }
            set { SetValue(AlignAlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignAlignmentCenterProperty =
            DependencyProperty.Register("AlignAlignmentCenter", typeof(bool), typeof(Axis), new PropertyMetadata(default(bool), (d, e) =>
            {
                Axis control = d as Axis;

                if (control == null) return;

                control.TryDraw();

            }));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Axis), new PropertyMetadata(0.0, (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 //double config = e.NewValue as double;


                 control.TryDraw();

             }));


        public Dock DockAlignment
        {
            get { return (Dock)GetValue(DockAlignmentProperty); }
            set { SetValue(DockAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DockAlignmentProperty =
            DependencyProperty.Register("DockAlignment", typeof(Dock), typeof(Axis), new PropertyMetadata(Dock.Left, (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 //Dock config = e.NewValue as Dock;


                 control.TryDraw();
             }));
    }

    public class xAxis : Axis
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(xAxis), "S.xAxis.Default");

        static xAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(xAxis), new FrameworkPropertyMetadata(typeof(xAxis)));
        }
        protected override void InitX()
        {
            base.InitX();

            if (this.TextAlignmentCenter)
            {
                double span = (this.maxX - this.minX) / this.xAxis.Count;

                this.maxX = this.maxX + span / 2;

                this.minX = this.minX - span / 2;
            }

            if (this.TextAlignmentCenter)
            {
                double span = (this.maxY - this.minY) / this.yAxis.Count;

                this.maxY = this.maxY + span / 2;

                this.minY = this.minY - span / 2;
            }
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = this.TextAlignmentCenter ? this.AlignAlignmentCenter ? 0 : (this.maxX - this.minX) / (this.xAxis.Count) : 0;

            //this.Y = this.DockTop ? this.yAxis.Max() : this.Y;

            //double y = this.DockTop ? this.yAxis.Max() : this.Y;

            double y = this.Value;

            //  Do ：绘制坐标
            foreach (double item in this.xAxis)
            {
                //  Do ：底线
                System.Windows.Shapes.Line yright = new System.Windows.Shapes.Line();
                yright.X1 = 0;
                yright.X2 = this.ActualWidth;
                yright.Y1 = 0;
                yright.Y2 = 0;
                yright.Style = this.LineStyle;

                if (this.xAxis.Count == 1)
                {
                    Canvas.SetBottom(yright, this.ActualHeight / 2);
                }
                else
                {
                    Canvas.SetBottom(yright, this.ActualHeight - this.GetY(y));
                }

                canvas.Children.Add(yright);


                //  Do ：刻度线
                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.X2 = 0;
                l.Y1 = 0;
                l.Y2 = this.AlignLenght;
                l.Style = this.LineStyle;
                l.Width = 1;

                if (this.xAxis.Count == 1)
                {
                    Canvas.SetLeft(l, this.ActualWidth / 2);
                }
                else
                {
                    Canvas.SetLeft(l, this.GetX(item + span / 2));
                }

                double bottom = this.ActualHeight - this.GetY(y) + (this.DockAlignment == Dock.Top || this.DockAlignment == Dock.Left ? 0 : -this.AlignLenght);
                Canvas.SetBottom(l, bottom);
                canvas.Children.Add(l);
                //  Do ：显示文本
                Label t = new Label();

                if (this.xConvert == null)
                {
                    t.Content = this.xDisplay.Count > this.xAxis.IndexOf(item) ? this.xDisplay[this.xAxis.IndexOf(item)] : item.ToString("G4");
                }
                else
                {
                    t.Content = this.xConvert(item);
                }

                t.Style = this.LabelStyle;
                t.Loaded += (o, e) =>
                {
                    if (this.xAxis.Count == 1)
                    {
                        Canvas.SetLeft(t, this.ActualWidth / 2 - t.ActualWidth / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(t, this.GetX(item, this.ActualWidth) - t.ActualWidth / 2);
                    }

                    double top = (this.DockAlignment == Dock.Top || this.DockAlignment == Dock.Left ? -(this.AlignLenght + t.ActualHeight) : this.AlignLenght) + this.GetY(y);
                    Canvas.SetTop(t, top);
                };
                canvas.Children.Add(t);
            }
        }

    }

    public class yAxis : Axis
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(yAxis), "S.yAxis.Default");

        static yAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(yAxis), new FrameworkPropertyMetadata(typeof(yAxis)));
        }

        protected override void InitY()
        {
            base.InitY();

            if (this.TextAlignmentCenter)
            {
                double span = (this.maxY - this.minY) / this.yAxis.Count;

                this.maxY = this.maxY + span / 2;

                this.minY = this.minY - span / 2;
            }
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = this.TextAlignmentCenter ? this.AlignAlignmentCenter ? 0 : (this.maxY - this.minY) / (this.yAxis.Count) : 0;

            //Y坐标
            foreach (double item in this.yAxis)
            {
                double postion = this.GetX(this.Value);

                //  Do ：底线
                System.Windows.Shapes.Line xleft = new System.Windows.Shapes.Line();
                xleft.X1 = 0;
                xleft.X2 = 0;
                xleft.Y1 = 0;
                xleft.Y2 = this.ActualHeight;
                xleft.Style = this.LineStyle;
                Canvas.SetLeft(xleft, postion);

                canvas.Children.Add(xleft);

                //  Do ：刻度
                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.X2 = this.AlignLenght;
                l.Y1 = 0;
                l.Y2 = 0;
                l.Style = this.LineStyle;

                if (this.yAxis.Count == 1)
                {
                    Canvas.SetTop(l, this.ActualHeight / 2);
                }
                else
                {
                    Canvas.SetTop(l, this.GetY(item + span / 2, this.ActualHeight));
                }

                if (this.DockAlignment == Dock.Right || this.DockAlignment == Dock.Bottom)
                {

                    Canvas.SetLeft(l, postion);
                }
                else
                {
                    Canvas.SetLeft(l, postion - this.AlignLenght);
                }

                canvas.Children.Add(l);

                // Todo ：绘制文本 
                Label t = new Label();

                if (this.yConvert == null)
                {
                    t.Content = this.yDisplay.Count > this.yAxis.IndexOf(item) ? this.yDisplay[this.yAxis.IndexOf(item)] : item.ToString("G4");
                }
                else
                {
                    t.Content = this.yConvert(item);
                }
                t.Style = this.LabelStyle;

                t.Loaded += (o, e) =>
                {
                    if (this.yAxis.Count == 1)
                    {
                        Canvas.SetTop(t, this.ActualHeight / 2 - t.ActualHeight / 2);
                    }
                    else
                    {
                        Canvas.SetTop(t, this.GetY(item, this.ActualHeight) - t.ActualHeight / 2);
                    }
                    Canvas.SetLeft(t, (this.DockAlignment == Dock.Right || this.DockAlignment == Dock.Bottom ? (this.AlignLenght) : -this.AlignLenght - t.ActualWidth) + postion);
                };

                canvas.Children.Add(t);
            }
        }
    }

    public class RadiusAxis : Axis
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(RadiusAxis), "S.RadiusAxis.Default");

        static RadiusAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadiusAxis), new FrameworkPropertyMetadata(typeof(RadiusAxis)));
        }
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(RadiusAxis), new PropertyMetadata(default(double), (d, e) =>
             {
                 RadiusAxis control = d as RadiusAxis;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = this.TextAlignmentCenter ? this.AlignAlignmentCenter ? 0 : (this.maxX - this.minX) / (this.xAxis.Count) : 0;

            //  Do ：绘制坐标
            foreach (double item in this.xAxis)
            {
                //  Do ：底线
                System.Windows.Shapes.Line yright = new System.Windows.Shapes.Line();
                yright.X1 = this.ActualWidth / 2;
                yright.X2 = this.ActualWidth / 2 + Len;
                yright.Y1 = 0;
                yright.Y2 = 0;
                yright.Style = this.LineStyle;

                Canvas.SetBottom(yright, this.GetY(0, this.ActualHeight));

                canvas.Children.Add(yright);

                //  Do ：刻度线
                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.X2 = 0;
                l.Y1 = 0;
                l.Y2 = this.VerticalAlignment == VerticalAlignment.Top ? -this.AlignLenght : this.AlignLenght;
                l.Style = this.LineStyle;
                Canvas.SetLeft(l, this.GetX(item + span / 2, this.Len));

                canvas.Children.Add(l);

                //  Do ：显示文本
                Label t = new Label();

                t.Content = this.xDisplay.Count > this.xAxis.IndexOf(item) ? this.xDisplay[this.xAxis.IndexOf(item)] : item.ToString("G4");
                t.Style = this.LabelStyle;

                t.Loaded += (o, e) =>
                {
                    Canvas.SetLeft(t, this.GetX(item, this.Len) - t.ActualWidth / 2);
                    Canvas.SetTop(t, this.VerticalAlignment == VerticalAlignment.Top ? -(this.AlignLenght + t.ActualHeight) : this.AlignLenght);
                };
                canvas.Children.Add(t);
            }
        }
    }

    public class AngleAxis : Axis
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AngleAxis), "S.AngleAxis.Default");

        static AngleAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngleAxis), new FrameworkPropertyMetadata(typeof(AngleAxis)));
        }

        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(AngleAxis), new PropertyMetadata(default(double), (d, e) =>
            {
                AngleAxis control = d as AngleAxis;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));


        protected virtual void DrawCircle(Canvas canvas)
        {
            //  Do ：绘制圆环 
            Path path = new Path();

            double item = this.xAxis.Max();

            path.Style = this.LineStyle;

            EllipseGeometry ellipse = new EllipseGeometry(new Rect(0, 0, this.GetX(item, this.Len * 2), this.GetX(item, this.Len * 2)));

            ellipse.Center = new Point(0, 0);

            path.Data = ellipse;

            this.Children.Add(path);
        }

        private void DrawLine(Canvas canvas)
        {
            Point center = new Point(0, 0);

            double angle = 360 / this.yAxis.Count;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                Path path = new Path();
                path.Style = this.LineStyle;

                PathFigure pf = new PathFigure();


                {
                    Point start = new Point(-this.Len, center.Y);

                    Matrix matrix = new Matrix();

                    matrix.RotateAt(angle * i, center.X, center.Y);

                    Point end = matrix.Transform(start);

                    pf.StartPoint = end;

                }

                {
                    Point start = new Point(-this.Len - this.AlignLenght, center.Y);

                    Matrix matrix = new Matrix();

                    matrix.RotateAt(angle * i, center.X, center.Y);

                    Point end = matrix.Transform(start);

                    pf.Segments.Add(new LineSegment(end, true));

                }

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                path.Data = pg;

                this.Children.Add(path);


            }
        }

        private void DrawText()
        {
            double angle = 360 / this.yAxis.Count;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                Point center = new Point(0, 0);

                Point start = new Point(this.Len + this.AlignLenght * 4, center.Y);

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle * i, center.X, center.Y);

                Point end = matrix.Transform(start);

                //  Do ：显示文本
                Label t = new Label();

                t.Content = this.yDisplay.Count > i ? this.yDisplay[i] : this.yAxis[i].ToString("G4");
                t.Style = this.LabelStyle;

                double hlen = this.AlignLenght / 10;

                t.Loaded += (o, e) =>
                {
                    double endParam = (end.X > center.X ? hlen + hlen / 2 : -hlen - t.ActualWidth - hlen / 2);

                    Canvas.SetLeft(t, end.X + endParam);
                    Canvas.SetTop(t, end.Y - t.ActualHeight / 2);
                };
                this.Children.Add(t);
            }
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawCircle(this);

            this.DrawLine(this);

            this.DrawText();
        }
    }

    public class RadarAxis : AngleAxis
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(RadarAxis), "S.RadarAxis.Default");

        static RadarAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadarAxis), new FrameworkPropertyMetadata(typeof(RadarAxis)));
        }
        protected override void DrawCircle(Canvas canvas)
        {
            Point center = new Point(0, 0);

            double angle = 360 / this.yAxis.Count;

            Path path = new Path();
            path.Style = this.LineStyle;

            PathFigure pf = new PathFigure();

            pf.IsClosed = true;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                if (i == 0)
                {
                    pf.StartPoint = new Point(this.Len, center.Y);
                    continue;
                }

                {
                    Point start = new Point(this.Len, center.Y);

                    Matrix matrix = new Matrix();

                    matrix.RotateAt(angle * i, center.X, center.Y);

                    Point end = matrix.Transform(start);

                    pf.Segments.Add(new LineSegment(end, true));
                }
            }

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);


            path.Loaded += (l, k) =>
            {
                this.RunPath(path, 500);
            };
        }
    }

}
