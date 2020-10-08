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
    public class PieBase : Layer
    {

    }

    /// <summary> 曲线视图 </summary>
    public class Pie : PieBase
    {
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

            double total = this.Data.Sum();

            double sum = 0;

            double min = Math.Min(this.ActualHeight, this.ActualWidth);

            //  Do ：中心点
            Point center = new Point(0, 0);

            for (int i = 0; i < this.Data.Count; i++)
            {
                //  Do ：半径
                double len = Len == double.NaN ? min / 2 : Len;

                double y = this.Data[i];

                double startAngle = 360 * sum / total;

                double endAngle = 360 * (sum + y) / total;

                sum = sum + y;

                //Point direction = new Point(0,0);

                //  Do ：增加标记
                {
                    Point start = new Point(center.X - len - len / 10, center.X);

                    Matrix matrix = new Matrix();

                    matrix.RotateAt((endAngle - startAngle) / 2 + startAngle, center.X, center.Y);

                    Point end = matrix.Transform(start);

                    //direction = end;

                    Path path = new Path();
                    path.Style = this.LineStyle;

                    path.Stroke = new SolidColorBrush(this.Foreground[i]);

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = center;
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
                    t.Style = this.LabelStyle;

                    t.Loaded += (o, e) =>
                    {
                        endParam = (end.X > center.X ? hlen + hlen / 2 : -hlen - t.ActualWidth - hlen / 2);

                        Canvas.SetLeft(t, end.X + endParam);
                        Canvas.SetTop(t, end.Y - t.ActualHeight / 2);
                    };
                    canvas.Children.Add(t);
                }

                //半径跟随值变化
                if (this.IsCustomized)
                {
                    len = len * this.Data[i] / this.Data.Max();
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


                    //Vector vector = new Vector(direction.X,direction.Y);

                    //vector.Normalize();

                    //Point sss = new Point(vector.X, vector.Y);

                    //path.RenderTransformOrigin = sss;

                    //Canvas.SetLeft(path, (this.ActualWidth - min) / 2);
                    //Canvas.SetTop(path, (this.ActualHeight - min) / 2);
                    this.Children.Add(path);
                }

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
