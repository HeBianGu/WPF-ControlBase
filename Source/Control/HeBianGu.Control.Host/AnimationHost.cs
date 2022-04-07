// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Control.Host
{
    public abstract class AnimationHost : HostBase
    {
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(AnimationHost), new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(100)), (d, e) =>
            {
                AnimationHost control = d as AnimationHost;

                if (control == null) return;

                if (e.OldValue is Duration o)
                {

                }

                if (e.NewValue is Duration n)
                {

                }

            }));

        public virtual void AnimationLocation(double x, double y)
        {
            if (this.Container.CheckDefaultTransformGroup())
            {
                this.Container.BeginAnimationXY(x, y, this.Duration.TimeSpan.TotalMilliseconds);
            }
        }
    }
}
