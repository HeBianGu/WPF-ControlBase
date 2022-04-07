// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Timers;
using System.Windows;

namespace HeBianGu.Control.Panel
{
    public class RandomPanel : AnimationPanel
    {
        private static Random _random = new Random();

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<UIElement> children = this.GetChildren();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            for (int i = 0; i < children.Count; i++)
            {
                UIElement elment = children[i];
                elment.Measure(finalSize);
                elment.RenderTransformOrigin = new Point(0.5, 0.5);

                Point end = new Point();
                end.X = finalSize.Width * _random.NextDouble() - elment.DesiredSize.Width / 2;
                end.Y = finalSize.Height * _random.NextDouble() - elment.DesiredSize.Height / 2;

                if (this.IsAnimation)
                {
                    elment.Arrange(new Rect(new Point(0, 0), elment.DesiredSize));

                    if (elment.CheckDefaultTransformGroup())
                    {
                        elment.BeginAnimationXY(end.X, end.Y, this.Duration);
                    }
                }
                else
                {
                    elment.Arrange(new Rect(end, elment.DesiredSize));
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        private Timer _timer = new Timer();

        public RandomPanel()
        {
            _timer.Interval = this.Interval;
            _timer.Elapsed += (l, k) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.InvalidateArrange();
                });

            };
        }

        private void Start()
        {
            _timer.Start();
        }

        private void Stop()
        {
            _timer.Stop();
        }

        public bool Forever
        {
            get { return (bool)GetValue(ForeverProperty); }
            set { SetValue(ForeverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForeverProperty =
            DependencyProperty.Register("Forever", typeof(bool), typeof(RandomPanel), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 RandomPanel control = d as RandomPanel;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     if (n)
                     {
                         control.Start();
                     }
                     else
                     {
                         control.Stop();
                     }
                 }

             }));

        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(RandomPanel), new FrameworkPropertyMetadata(1000.0, (d, e) =>
            {
                RandomPanel control = d as RandomPanel;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {
                    control._timer.Interval = n;
                }
            }));
    }
}
