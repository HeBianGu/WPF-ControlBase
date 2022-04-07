// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class SkewTransition : OpacityTransition
    {
        public double StartAngleX { get; set; } = 90;

        public double EndAngleX { get; set; } = 90;

        public double StartAngleY { get; set; } = 0;

        public double EndAngleY { get; set; } = 0;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationSkewX(this.StartAngleX, 0, this.VisibleDuration * 1.5, l => complate?.Invoke(), l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;
            });
            element.BeginAnimationSkewY(this.StartAngleY, 0, this.VisibleDuration * 1.5, null, l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;
            });
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationSkewX(0, this.EndAngleX, this.HideDuration, l => complate?.Invoke(), l =>
            {
                l.Storyboard.FillBehavior = FillBehavior.Stop;
                l.Easing = this.HideEasing;
            });

            element.BeginAnimationSkewY(0, this.EndAngleY, this.HideDuration, null, l =>
            {
                l.Storyboard.FillBehavior = FillBehavior.Stop;
                l.Easing = this.HideEasing;
            });
        }
    }
}
