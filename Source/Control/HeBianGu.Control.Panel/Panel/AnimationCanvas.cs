// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Panel
{
    public class AnimationCanvas : Canvas
    {

        public bool UseAnimation
        {
            get { return (bool)GetValue(UseAnimationProperty); }
            set { SetValue(UseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAnimationProperty =
            DependencyProperty.Register("UseAnimation", typeof(bool), typeof(AnimationCanvas), new PropertyMetadata(true, (d, e) =>
            {
                AnimationCanvas control = d as AnimationCanvas;

                if (control == null) return;

                //bool config = e.NewValue as bool;

            }));


        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(AnimationCanvas), new PropertyMetadata(TimeSpan.FromSeconds(1000), (d, e) =>
            {
                AnimationCanvas control = d as AnimationCanvas;

                if (control == null) return;

                //TimeSpan config = e.NewValue as TimeSpan;

            }));


        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }

                double x = 0;
                double y = 0;

                double left = GetLeft(child);

                if (!Double.IsNaN(left))
                {
                    x = left;
                }
                else
                {
                    double right = GetRight(child);

                    if (!Double.IsNaN(right))
                    {
                        x = arrangeSize.Width - child.DesiredSize.Width - right;
                    }
                }

                double top = GetTop(child);

                if (!Double.IsNaN(top))
                {
                    y = top;
                }
                else
                {
                    double bottom = GetBottom(child);

                    if (!Double.IsNaN(bottom))
                    {
                        y = arrangeSize.Height - child.DesiredSize.Height - bottom;
                    }
                }

                if (UseAnimation)
                {
                    child.Arrange(new Rect(new Point(0, 0), child.DesiredSize));

                    if (child.CheckDefaultTransformGroup())
                    {
                        child.BeginAnimationXY(x, y, this.Duration.TotalSeconds);
                    }
                }
                else
                {
                    child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
                }



            }
            return arrangeSize;
        }

    }
}
