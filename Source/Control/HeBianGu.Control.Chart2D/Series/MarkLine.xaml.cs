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
    /// <summary> 曲线视图 </summary>
    public class MarkLine : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MarkLine), "S.MarkLine.Default");
        public static ComponentResourceKey HorizontalKey => new ComponentResourceKey(typeof(MarkLine), "S.MarkLine.Horizontal");
        public static ComponentResourceKey HorizontalStaticKey => new ComponentResourceKey(typeof(MarkLine), "S.MarkLine.Horizontal.Static");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(MarkLine), "S.MarkLine.Single");
        public static ComponentResourceKey VerticalKey => new ComponentResourceKey(typeof(MarkLine), "S.MarkLine.Vertical");

        static MarkLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkLine), new FrameworkPropertyMetadata(typeof(MarkLine)));
        }

        public Style TrangleStyle
        {
            get { return (Style)GetValue(TrangleStyleProperty); }
            set { SetValue(TrangleStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrangleStyleProperty =
            DependencyProperty.Register("TrangleStyle", typeof(Style), typeof(MarkLine), new PropertyMetadata(default(Style), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

                 control.TryDraw();

             }));


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MarkLine), new PropertyMetadata(default(Orientation), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 //Orientation config = e.NewValue as Orientation;
                 control.TryDraw();
             }));


        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public ObservableCollection<Color> MarkBrushes
        {
            get { return (ObservableCollection<Color>)GetValue(MarkBrushesProperty); }
            set { SetValue(MarkBrushesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushesProperty =
            DependencyProperty.Register("MarkBrushes", typeof(ObservableCollection<Color>), typeof(MarkLine), new PropertyMetadata(new ObservableCollection<Color>(), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 ObservableCollection<Color> config = e.NewValue as ObservableCollection<Color>;

                 control.TryDraw();

             }));


        public Point Start
        {
            get { return (Point)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(Point), typeof(MarkLine), new PropertyMetadata(default(Point), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 //Point config = e.NewValue as Point;

             }));



        public Point End
        {
            get { return (Point)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(Point), typeof(MarkLine), new PropertyMetadata(default(Point), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 //Point config = e.NewValue as Point;

             }));



        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            List<double> data = this.GetData().ToList();
            List<string> names = this.GetDisplayName();
            for (int i = 0; i < data.Count; i++)
            {
                double value = data[i];

                if (double.IsNaN(value)) continue;

                Point start;

                Point end;

                if (this.MarkLineType == MarkLineType.Custom)
                {
                    start = this.Start;
                    end = this.End;
                }

                else
                {
                    start = this.Orientation == Orientation.Horizontal ? new Point(this.minX, value) : new Point(value, this.minY);
                    end = this.Orientation == Orientation.Horizontal ? new Point(this.maxX, value) : new Point(value, this.maxY);
                }

                {
                    //  Do ：添加标定线
                    Path path = new Path();

                    path.Style = this.PathStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        path.Stroke = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        path.Stroke = this.Foreground;
                    }

                    PolyLineSegment pls = new PolyLineSegment();

                    pls.Points.Add(new Point(this.GetX(start.X), this.GetY(start.Y)));
                    pls.Points.Add(new Point(this.GetX(end.X), this.GetY(end.Y)));

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = pls.Points.FirstOrDefault();
                    pf.Segments.Add(pls);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    pg.Figures.Add(pf);

                    path.Data = pg;

                    this.Children.Add(path);
                }

                //  Do ：增加箭头 
                {
                    //  Do ：添加标定线
                    Path path = new Path();
                    path.Style = this.TrangleStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        path.Stroke = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        path.Stroke = this.Foreground;
                    }

                    path.Fill = path.Stroke;

                    PolyLineSegment pls = new PolyLineSegment();

                    pls.Points.Add(new Point(25, 0));
                    pls.Points.Add(new Point(50, 5));
                    pls.Points.Add(new Point(25, 10));
                    pls.Points.Add(new Point(20, 5));
                    pls.Points.Add(new Point(25, 0));

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = pls.Points.FirstOrDefault();
                    pf.Segments.Add(pls);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;

                    if (path.RenderTransform is TransformGroup group)
                    {
                        TransformGroup ng = group.Clone();

                        if (ng.Children[2] is RotateTransform rotate)
                        {
                            double angle = Math.Atan2((end.Y - start.Y), (end.X - start.X)) * 180 / Math.PI;
                            rotate.Angle = -angle;
                        }

                        path.RenderTransform = ng;
                    }

                    path.Loaded += (l, k) =>
                    {
                        Canvas.SetBottom(path, this.ActualHeight - this.GetY(end.Y) - path.ActualHeight / 2);
                        Canvas.SetLeft(path, this.GetX(end.X) - path.ActualWidth / 2);
                    };

                    this.Children.Add(path);
                }


                {
                    //  Do ：绘制文本
                    Label text = new Label();
                    text.Content = names.Count > i ? names[i] : names.FirstOrDefault();
                    text.Style = this.LabelStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        text.Foreground = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        text.Foreground = this.Foreground;
                    }

                    text.Loaded += (l, k) =>
                    {
                        Canvas.SetTop(text, this.GetY(end.Y) - text.ActualHeight / 2);
                        Canvas.SetLeft(text, this.GetX(end.X) + text.ActualWidth / 2);
                    };



                    this.Children.Add(text);
                }

                {
                    //  Do ：绘制文本
                    Label text = new Label();

                    if (this.MarkBrushes.Count > i)
                    {
                        text.Foreground = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        text.Foreground = this.Foreground;
                    }
                    text.Content = value.ToString("G4");

                    text.Style = this.LabelStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        text.Foreground = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        text.Foreground = this.Foreground;
                    }
                    text.Loaded += (l, k) =>
                    {
                        Canvas.SetTop(text, this.GetY(start.Y) - text.ActualHeight / 2);
                        Canvas.SetLeft(text, this.GetX(start.X) - text.ActualWidth * 1.5);
                    };

                    this.Children.Add(text);
                }

                //  Do ：显示标记
                if (this.MarkStyle == null) return;

                EllipseMarker m = Activator.CreateInstance(this.MarkStyle.TargetType) as EllipseMarker;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        m.Stroke = new SolidColorBrush(this.MarkBrushes[i]);
                    }
                    else
                    {
                        m.Stroke = this.Foreground;
                    }

                    Canvas.SetLeft(m, this.GetX(start.X));
                    Canvas.SetTop(m, this.GetY(start.Y));
                    this.Children.Add(m);
                }
            }
        }


        public MarkLineType MarkLineType
        {
            get { return (MarkLineType)GetValue(MarkLineTypeProperty); }
            set { SetValue(MarkLineTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkLineTypeProperty =
            DependencyProperty.Register("MarkLineType", typeof(MarkLineType), typeof(MarkLine), new PropertyMetadata(default(MarkLineType), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 //MarkLineType config = e.NewValue as MarkLineType;

                 control.TryDraw();

             }));

        private IEnumerable<double> GetData()
        {
            List<double> result = new List<double>();

            if (this.MarkLineType == MarkLineType.Max)
            {
                yield return this.maxY;
            }

            else if (this.MarkLineType == MarkLineType.Min)
            {
                yield return this.minX;
            }

            else if (this.MarkLineType == MarkLineType.Average)
            {
                if (this.Data.Count > 0)
                {
                    yield return this.Data.Average();
                }
            }
            else
            {
                if (this.Data.Count == 0 && this.Value != double.NaN)
                    yield return this.Value;
                else
                    foreach (var item in this.Data)
                    {
                        yield return item;
                    }
            }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(MarkLine), new FrameworkPropertyMetadata(double.NaN, (d, e) =>
            {
                MarkLine control = d as MarkLine;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));



        private List<string> GetDisplayName()
        {
            List<string> result = new List<string>();

            if (this.MarkLineType == MarkLineType.Max)
            {
                result.Add("最大值");
            }

            else if (this.MarkLineType == MarkLineType.Min)
            {
                result.Add("最小值");
            }

            else if (this.MarkLineType == MarkLineType.Average)
            {
                result.Add("平均值");
            }
            else
            {
                result = this.yDisplay?.ToList();
            }

            return result;
        }
    }

    public enum MarkLineType
    {
        Default = 0, Max, Min, Average, Custom
    }

}
