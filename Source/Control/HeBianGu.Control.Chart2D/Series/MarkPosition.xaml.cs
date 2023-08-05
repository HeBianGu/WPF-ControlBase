// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    public class MarkPosition : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MarkPosition), "S.MarkPosition.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(MarkPosition), "S.MarkPosition.Single");

        static MarkPosition()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkPosition), new FrameworkPropertyMetadata(typeof(MarkPosition)));
        }

        public double WidthPercent
        {
            get { return (double)GetValue(WidthPercentProperty); }
            set { SetValue(WidthPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.Register("WidthPercent", typeof(double), typeof(MarkPosition), new PropertyMetadata(0.8, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));

        public double ItemPercent
        {
            get { return (double)GetValue(ItemPercentProperty); }
            set { SetValue(ItemPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPercentProperty =
            DependencyProperty.Register("ItemPercent", typeof(double), typeof(MarkPosition), new PropertyMetadata(1.0, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));

        public int MulCount
        {
            get { return (int)GetValue(MulCountProperty); }
            set { SetValue(MulCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulCountProperty =
            DependencyProperty.Register("MulCount", typeof(int), typeof(MarkPosition), new PropertyMetadata(1, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //int config = e.NewValue as int;

                control.TryDraw();

            }));


        public int MulIndex
        {
            get { return (int)GetValue(MulIndexProperty); }
            set { SetValue(MulIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulIndexProperty =
            DependencyProperty.Register("MulIndex", typeof(int), typeof(MarkPosition), new PropertyMetadata(0, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //int config = e.NewValue as int;
                control.TryDraw();

            }));

        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(MarkPosition), new PropertyMetadata(default(bool), (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.TryDraw();

            }));


        public Brush MarkForeground
        {
            get { return (Brush)GetValue(MarkForegroundProperty); }
            set { SetValue(MarkForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkForegroundProperty =
            DependencyProperty.Register("MarkForeground", typeof(Brush), typeof(MarkPosition), new FrameworkPropertyMetadata(Brushes.White, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

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


        public MarkValueType MarkValueType
        {
            get { return (MarkValueType)GetValue(MarkValueTypeProperty); }
            set { SetValue(MarkValueTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkValueTypeProperty =
            DependencyProperty.Register("MarkValueType", typeof(MarkValueType), typeof(MarkPosition), new PropertyMetadata(default(MarkValueType), (d, e) =>
             {
                 MarkPosition control = d as MarkPosition;

                 if (control == null) return;

                 //MarkValueType config = e.NewValue as MarkValueType;

                 control.TryDraw();

             }));



        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(Point), typeof(MarkPosition), new PropertyMetadata(default(Point), (d, e) =>
             {
                 MarkPosition control = d as MarkPosition;

                 if (control == null) return;

                 //Point config = e.NewValue as Point;

                 control.TryDraw();

             }));


        protected Point GetPoint()
        {
            if (this.MarkValueType == MarkValueType.Default) return this.Point;

            double span = this.AlignmentCenter ? (this.maxX - this.minX) / (this.xAxis.Count) : 0;

            double itemWidth = span * this.WidthPercent;

            double v = double.NaN;

            if (this.Data.Count == 0)
                return new Point(0, 0);

            if (this.MarkValueType == MarkValueType.Max)
                v = this.Data.Max();

            if (this.MarkValueType == MarkValueType.Min)
                v = this.Data.Min();

            if (this.MarkValueType == MarkValueType.First)
                v = this.Data.FirstOrDefault();

            if (this.MarkValueType == MarkValueType.Last)
                v = this.Data.LastOrDefault();

            int index = this.Data.IndexOf(v);

            if (this.xAxis.Count <= index)
                return new Point(0, 0);

            double x = this.xAxis[index];

            return new Point((x - itemWidth / 2) + (this.MulIndex + 0.5) * (itemWidth / this.MulCount), v);
        }

        public virtual void DrawMark()
        {
            Point point = this.GetPoint();

            //  Do ：显示文本
            Label t = new Label();
            t.Content = point.Y.ToString("G3");
            t.Style = this.LabelStyle;
            if (this.MarkForeground != null)
                t.Foreground = this.MarkForeground;
            t.Loaded += (o, e) =>
            {
                if (this.xAxis.Count == 1)
                {
                    Canvas.SetLeft(t, this.ActualWidth / 2 - t.ActualWidth / 2);
                    Canvas.SetTop(t, this.ActualHeight / 2 - t.ActualHeight * 1.2);
                }
                else
                {
                    Canvas.SetLeft(t, this.GetX(point.X) - t.ActualWidth / 2);
                    Canvas.SetTop(t, this.GetY(point.Y) - t.ActualHeight * 1.2);
                }
            };

            this.Children.Add(t);
        }


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            this.DrawMark();
        }
    }


    public enum MarkValueType
    {
        Default = 0, Max, Min, First, Last, Drag, DragStepX
    }

}
