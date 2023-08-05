// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class PieBase : Layer
    {
        public PieBase()
        {
            this.Loaded += (l, k) =>
              {
                  this.Draw(this);

              };

        }
    }

    /// <summary> 曲线视图 </summary>
    public class Pie : PieBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Pie), "S.Pie.Default");

        static Pie()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Pie), new FrameworkPropertyMetadata(typeof(Pie)));
        }

        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public new ObservableCollection<Color> Foreground
        {
            get { return (ObservableCollection<Color>)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(ObservableCollection<Color>), typeof(Pie), new PropertyMetadata(new ObservableCollection<Color>(), (d, e) =>
            {
                Pie control = d as Pie;

                if (control == null) return;

                ObservableCollection<Color> config = e.NewValue as ObservableCollection<Color>;
                control.TryDraw();
            }));



        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(Pie), new PropertyMetadata(double.NaN, (d, e) =>
             {
                 Pie control = d as Pie;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));


        public double CircleLen
        {
            get { return (double)GetValue(CircleLenProperty); }
            set { SetValue(CircleLenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircleLenProperty =
            DependencyProperty.Register("CircleLen", typeof(double), typeof(Pie), new PropertyMetadata(0.0, (d, e) =>
             {
                 Pie control = d as Pie;

                 if (control == null) return;

                 //double config = e.NewValue as double;

                 control.TryDraw();

             }));



        public bool IsCustomized
        {
            get { return (bool)GetValue(IsCustomizedProperty); }
            set { SetValue(IsCustomizedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCustomizedProperty =
            DependencyProperty.Register("IsCustomized", typeof(bool), typeof(Pie), new PropertyMetadata(default(bool), (d, e) =>
             {
                 Pie control = d as Pie;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.TryDraw();

             }));



        public Style EllipseStyle
        {
            get { return (Style)GetValue(EllipseStyleProperty); }
            set { SetValue(EllipseStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipseStyleProperty =
            DependencyProperty.Register("EllipseStyle", typeof(Style), typeof(Pie), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Pie control = d as Pie;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));



        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double total = 0;

            for (int i = 0; i < this.Data.Count; i++)
            {
                bool use = this.DataBoolean.Count > i ? this.DataBoolean[i] : true;

                if (!use) continue;

                total += this.Data[i];
            }

            //this.Data.Union

            //double total = this.Data.Sum();

            double sum = 0;

            double min = Math.Min(this.ActualHeight, this.ActualWidth);

            //  Do ：中心点
            Point center = new Point(0, 0);

            for (int i = 0; i < this.Data.Count; i++)
            {
                bool use = this.DataBoolean.Count > i ? this.DataBoolean[i] : true;

                //  Do ：半径
                double len = Len == double.NaN ? min / 2 : Len;

                double y = use ? this.Data[i] : 0;

                double startAngle = 360 * sum / total;

                double endAngle = 360 * (sum + y) / total;

                sum = sum + y;

                double max = len;

                //半径跟随值变化
                if (this.IsCustomized)
                {
                    len = len * this.Data[i] / this.Data.Max();
                }

                //Point direction = new Point(0,0);

                //  Do ：增加标记
                {
                    //  Do ：第二个点
                    Point start = new Point(center.X - max - max / 10, center.X);
                    Matrix matrix = new Matrix();
                    matrix.RotateAt((endAngle - startAngle) / 2 + startAngle, center.X, center.Y);
                    Point end = matrix.Transform(start);

                    //  Do ：第一个点
                    Point startFirst = new Point(center.X - len, center.X);
                    Matrix matrixFirst = new Matrix();
                    matrixFirst.RotateAt((endAngle - startAngle) / 2 + startAngle, center.X, center.Y);
                    Point endFirst = matrixFirst.Transform(startFirst);

                    Path path = new Path();

                    path.Visibility = use ? Visibility.Visible : Visibility.Hidden;
                    path.Style = this.LineStyle;
                    if (this.Foreground.Count > i)
                        path.Stroke = new SolidColorBrush(this.Foreground[i]);

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = endFirst;
                    pf.Segments.Add(new LineSegment(end, true));

                    double hlen = len / 8;

                    double endParam = (end.X > center.X ? hlen : -hlen);

                    pf.Segments.Add(new LineSegment(new Point(end.X + endParam, end.Y), true));

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;


                    this.Children.Add(path);

                    //  Do ：显示文本
                    Label t = new Label();
                    t.Content = this.xDisplay.Count > i ? this.xDisplay[i] : this.Data[i].ToString();

                    t.Visibility = use ? Visibility.Visible : Visibility.Hidden;

                    t.Style = this.LabelStyle;

                    t.Loaded += (o, e) =>
                    {
                        endParam = (end.X > center.X ? hlen + hlen / 2 : -hlen - t.ActualWidth - hlen / 2);

                        Canvas.SetLeft(t, end.X + endParam);
                        Canvas.SetTop(t, end.Y - t.ActualHeight / 2);
                    };


                    canvas.Children.Add(t);
                }



                //  Do ：增加扇形
                {
                    ArcSegment arc = new ArcSegment();

                    Matrix matrix = new Matrix();

                    Point p = new Point(center.X - len, center.X);

                    matrix.RotateAt(startAngle, center.X, center.Y);

                    arc.Point = matrix.Transform(p);

                    Point start = arc.Point;

                    matrix = new Matrix();

                    Path path = new Path();
                    path.Style = this.PathStyle;
                    if(this.Foreground.Count>i)
                    path.Fill = path.Stroke = new SolidColorBrush(this.Foreground[i]);

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = center;
                    pf.Segments.Add(new LineSegment(start, true));

                    if (endAngle - startAngle > 180)
                    {
                        matrix.RotateAt((endAngle - startAngle) / 2, center.X, center.Y);
                        Point end = matrix.Transform(start);
                        pf.Segments.Add(new ArcSegment(end, new Size(len, len), 0, false, SweepDirection.Clockwise, true));

                        end = matrix.Transform(end);
                        pf.Segments.Add(new ArcSegment(end, new Size(len, len), 0, false, SweepDirection.Clockwise, true));

                    }
                    else
                    {
                        matrix.RotateAt(endAngle - startAngle, center.X, center.Y);
                        Point end = matrix.Transform(start);
                        pf.Segments.Add(new ArcSegment(end, new Size(len, len), 0, false, SweepDirection.Clockwise, true));

                    }


                    pf.Segments.Add(new LineSegment(center, true));
                    pf.IsClosed = true;

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;

                    //  Do ：用于标识Legend显示信息
                    path.Tag = this.xDisplay.Count > i ? this.xDisplay[i] : this.Data[i].ToString();

                    path.Visibility = use ? Visibility.Visible : Visibility.Hidden;

                    this.Children.Add(path);
                }

                //  Do ：中心空白圆圈
                Ellipse ellipse = new Ellipse();
                ellipse.Width = this.CircleLen * 2;
                ellipse.Height = this.CircleLen * 2;
                ellipse.Style = this.EllipseStyle;
                Canvas.SetLeft(ellipse, -this.CircleLen);
                Canvas.SetTop(ellipse, -this.CircleLen);
                this.Children.Add(ellipse);
            }
        }
    }


}
