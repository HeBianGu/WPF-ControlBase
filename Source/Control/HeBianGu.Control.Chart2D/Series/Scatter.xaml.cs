// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class ScatterBase : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScatterBase), "S.ScatterBase.Default");

        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(ScatterBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                xAxis control = d as xAxis;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.TryDraw();

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

        /// <summary> 散点显示的最大数量 </summary>
        public int ShowCount
        {
            get { return (int)GetValue(ShowCountProperty); }
            set { SetValue(ShowCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowCountProperty =
            DependencyProperty.Register("ShowCount", typeof(int), typeof(ScatterBase), new PropertyMetadata(500, (d, e) =>
             {
                 ScatterBase control = d as ScatterBase;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));

    }

    public class Scatter : ScatterBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Scatter), "S.Scatter.Default");
        public static ComponentResourceKey AlignmentCenterKey => new ComponentResourceKey(typeof(Scatter), "S.Scatter.AlignmentCenter");

        static Scatter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Scatter), new FrameworkPropertyMetadata(typeof(Scatter)));
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (this.xAxis.Count > this.ShowCount) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：显示标记
                Shape m = Activator.CreateInstance(this.MarkStyle.TargetType) as Shape;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    if (this.xAxis.Count == 1)
                    {
                        Canvas.SetLeft(m, this.ActualWidth / 2 - m.ActualWidth / 2);
                        Canvas.SetTop(m, this.ActualHeight / 2 - m.ActualHeight / 2);
                        this.Children.Add(m);
                    }

                    else
                    {
                        Canvas.SetLeft(m, this.GetX(x, this.ActualWidth) - m.ActualWidth / 2);
                        Canvas.SetTop(m, this.GetY(y, this.ActualHeight) - m.ActualHeight / 2);
                        this.Children.Add(m);
                    }


                    m.Tag = $"（{x.ToString("G4")}，{y.ToString("G4")}）";
                }
            }
        }
    }


    public class AreaScatter : ScatterBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AreaScatter), "S.AreaScatter.Default");

        static AreaScatter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AreaScatter), new FrameworkPropertyMetadata(typeof(AreaScatter)));
        }

        [TypeConverter(typeof(DataXyTypeConverter))]
        public new ObservableCollection<double[]> Data
        {
            get { return (ObservableCollection<double[]>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double[]>), typeof(AreaScatter), new PropertyMetadata(new ObservableCollection<double[]>(), (d, e) =>
            {
                AreaScatter control = d as AreaScatter;

                if (control == null) return;

                ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

                control.TryDraw();

            }));

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (this.MarkStyle == null) return;

            for (int i = 0; i < this.Data.Count; i++)
            {
                double x = this.Data[i][0];

                double y = this.Data[i][1];

                //  Do ：显示标记
                Shape m = Activator.CreateInstance(this.MarkStyle.TargetType) as Shape;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    Canvas.SetLeft(m, this.GetX(x, this.ActualWidth) - m.ActualWidth / 2);
                    Canvas.SetTop(m, this.GetY(y, this.ActualHeight) - m.ActualHeight / 2);
                    this.Children.Add(m);

                    m.Tag = $"（{x.ToString("G4")}，{y.ToString("G4")}）";
                }
            }
        }
    }

    public class Bubble : ScatterBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Bubble), "S.Bubble.Default");

        static Bubble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Bubble), new FrameworkPropertyMetadata(typeof(Bubble)));
        }

        //[TypeConverter(typeof(DataTypeConverter))]
        public DoubleCollection BubbleData
        {
            get { return (DoubleCollection)GetValue(BubbleDataProperty); }
            set { SetValue(BubbleDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BubbleDataProperty =
            DependencyProperty.Register("BubbleData", typeof(DoubleCollection), typeof(Bubble), new PropertyMetadata(default(DoubleCollection), (d, e) =>
             {
                 Bubble control = d as Bubble;

                 if (control == null) return;

                 DoubleCollection config = e.NewValue as DoubleCollection;

                 control.TryDraw();

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (this.BubbleData == null) return;

            if (this.Data == null) return;

            if (this.xAxis == null) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                double v = this.BubbleData[i];

                //  Do ：显示标记
                EllipseMarker m = Activator.CreateInstance(this.MarkStyle?.TargetType) as EllipseMarker;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    m.Rect = new Rect(0, 0, v, v);

                    Canvas.SetLeft(m, this.GetX(x, this.ActualWidth));
                    Canvas.SetTop(m, this.GetY(y, this.ActualHeight));
                    this.Children.Add(m);

                    m.Tag = $"（{x.ToString("G4")}，{y.ToString("G4")}）";
                }
            }
        }
    }

    public class PolarScatter : ScatterBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PolarScatter), "S.PolarScatter.Default");

        static PolarScatter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PolarScatter), new FrameworkPropertyMetadata(typeof(PolarScatter)));
        }
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(PolarScatter), new PropertyMetadata(200.0, (d, e) =>
            {
                PolarScatter control = d as PolarScatter;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Point center = new Point(0, 0);
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
                //  Do ：显示标记
                Shape m = Activator.CreateInstance(this.MarkStyle.TargetType) as Shape;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    Canvas.SetLeft(m, end.X - m.ActualWidth / 2);
                    Canvas.SetTop(m, end.Y - m.ActualHeight / 2);

                    this.Children.Add(m);

                    m.Tag = $"（{x.ToString("G4")}，{d.ToString("G4")}）";
                }
            }
        }
    }

    public class PolarBubble : ScatterBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PolarBubble), "S.PolarBubble.Default");

        static PolarBubble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PolarBubble), new FrameworkPropertyMetadata(typeof(PolarBubble)));
        }

        //[TypeConverter(typeof(DataTypeConverter))]
        public DoubleCollection BubbleData
        {
            get { return (DoubleCollection)GetValue(BubbleDataProperty); }
            set { SetValue(BubbleDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BubbleDataProperty =
            DependencyProperty.Register("BubbleData", typeof(DoubleCollection), typeof(PolarBubble), new PropertyMetadata(default(DoubleCollection), (d, e) =>
            {
                PolarBubble control = d as PolarBubble;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

            }));


        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(PolarBubble), new PropertyMetadata(200.0, (d, e) =>
            {
                PolarBubble control = d as PolarBubble;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Point center = new Point(0, 0);

            for (int i = 0; i < this.Data.Count; i++)
            {
                double x = this.yAxis[i];

                double d = this.Data[i];

                double v = this.BubbleData[i];

                double angle = x;

                Point start = new Point(this.GetX(d, this.Len), center.Y);

                Matrix matrix = new Matrix();

                matrix.RotateAt(angle, center.X, center.Y);

                Point end = matrix.Transform(start);

                //  Do ：显示标记
                EllipseMarker m = Activator.CreateInstance(this.MarkStyle.TargetType) as EllipseMarker;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    m.Rect = new Rect(0, 0, v, v);

                    Canvas.SetLeft(m, end.X);
                    Canvas.SetTop(m, end.Y);

                    this.Children.Add(m);

                    m.Tag = $"( {x.ToString("G4")} , {d.ToString("G4")} )";
                }
            }
        }
    }
}
