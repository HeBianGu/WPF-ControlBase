// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class ScaleTransition : OpacityTransition
    {
        public double StartX { get; set; } = 0.2;

        public double EndX { get; set; } = 0.5;

        public double StartY { get; set; } = 0.2;

        public double EndY { get; set; } = 0.5;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.RenderTransformOrigin = this.RenderTransformOrigin;
            element.BeginAnimationScaleX(this.StartX, 1, this.VisibleDuration, null, l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;

            });

            element.BeginAnimationScaleY(this.StartY, 1, this.VisibleDuration, l => complate?.Invoke(), l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;
            });
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.RenderTransformOrigin = this.RenderTransformOrigin;

            element.BeginAnimationScaleX(1, this.EndX, this.HideDuration, l => element.Visibility = HiddenOrCollapsed, l =>
            {
                l.Storyboard.FillBehavior = FillBehavior.Stop;
                l.Easing = this.HideEasing;
            });

            element.BeginAnimationScaleY(1, this.EndY, this.HideDuration, l =>
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();

            }, l =>
            {
                l.Storyboard.FillBehavior = FillBehavior.Stop;
                l.Easing = this.HideEasing;
            });
        }
    }
}
