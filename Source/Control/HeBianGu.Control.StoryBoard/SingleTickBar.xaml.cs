// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.Control.StoryBoard
{
    public class SingleTickBar : TickBar
    {
        static SingleTickBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SingleTickBar), new FrameworkPropertyMetadata(typeof(SingleTickBar)));
        }

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(SingleTickBar), new PropertyMetadata(Brushes.Black, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

            }));


        public int SplitValue
        {
            get { return (int)GetValue(SplitValueProperty); }
            set { SetValue(SplitValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register("SplitValue", typeof(int), typeof(SingleTickBar), new PropertyMetadata(6, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.InvalidateVisual();

            }));


        public int SmallSplitValue
        {
            get { return (int)GetValue(SmallSplitValueProperty); }
            set { SetValue(SmallSplitValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallSplitValueProperty =
            DependencyProperty.Register("SmallSplitValue", typeof(int), typeof(SingleTickBar), new FrameworkPropertyMetadata(1, (d, e) =>
             {
                 SingleTickBar control = d as SingleTickBar;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public VerticalAlignment TickVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TickVerticalAlignmentProperty); }
            set { SetValue(TickVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickVerticalAlignmentProperty =
            DependencyProperty.Register("TickVerticalAlignment", typeof(VerticalAlignment), typeof(SingleTickBar), new FrameworkPropertyMetadata(VerticalAlignment.Bottom, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                if (e.OldValue is VerticalAlignment o)
                {

                }

                if (e.NewValue is VerticalAlignment n)
                {

                }

            }));


        protected override void OnRender(DrawingContext dc)
        {
            this.Width = (this.Maximum - this.Minimum) * this.TickFrequency;

            //Size size = new Size(base.ActualWidth, base.ActualHeight);

            //int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;

            //if ((this.Maximum - this.Minimum) % this.TickFrequency == 0)
            //    tickCount -= 1;

            //Double tickFrequencySize;

            //tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));

            double num = this.Maximum - this.Minimum;

            int i = 0;

            for (i = 0; i <= num; i++)
            {

                //text = Convert.ToString(Convert.ToInt32(this.Minimum + this.TickFrequency * i), 10);

                TimeSpan ts = TimeSpan.FromSeconds(Convert.ToInt32(this.Minimum + i));
                string text = ts.ToString().Substring(3);
                FormattedText formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 10, this.Foreground);
                double x = i * this.TickFrequency;
                if (i == this.Maximum)
                {
                    x = x - 5;
                }

                if (i % this.SplitValue == 0)
                {
                    double y = this.LargeLen;
                    Pen pen = new Pen(this.Foreground, 1);
                    if (this.TickVerticalAlignment == VerticalAlignment.Bottom)
                    {
                        dc.DrawLine(pen, new Point(x + 2, this.RenderSize.Height), new Point(x + 2, this.RenderSize.Height - y));
                        dc.DrawText(formattedText, new Point(x, y - 15));
                    }
                    else
                    {
                        dc.DrawLine(pen, new Point(x + 2, 0), new Point(x + 2, y));
                        dc.DrawText(formattedText, new Point(x, y));
                    }
                }

                if (i % this.SmallSplitValue == 0)
                {
                    double y = this.SmallLen;
                    Pen pen = new Pen(this.Foreground, 0.5);
                    if (this.TickVerticalAlignment == VerticalAlignment.Bottom)
                    {
                        dc.DrawLine(pen, new Point(x + 2, this.RenderSize.Height), new Point(x + 2, this.RenderSize.Height - y));
                    }
                    else
                    {
                        dc.DrawLine(pen, new Point(x + 2, 0), new Point(x + 2, y));
                    }
                }
            }
        }


        public double SmallLen
        {
            get { return (double)GetValue(SmallLenProperty); }
            set { SetValue(SmallLenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallLenProperty =
            DependencyProperty.Register("SmallLen", typeof(double), typeof(SingleTickBar), new FrameworkPropertyMetadata(10.0, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public double LargeLen
        {
            get { return (double)GetValue(LargeLenProperty); }
            set { SetValue(LargeLenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeLenProperty =
            DependencyProperty.Register("LargeLen", typeof(double), typeof(SingleTickBar), new FrameworkPropertyMetadata(15.0, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));
    }
}
