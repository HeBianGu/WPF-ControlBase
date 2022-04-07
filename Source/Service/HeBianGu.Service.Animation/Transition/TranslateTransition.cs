// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class TranslateTransition : OpacityTransition
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationX(this.StartPoint.X, 0, this.VisibleDuration, l => complate?.Invoke(), l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;
            });
            element.BeginAnimationY(this.StartPoint.Y, 0, this.VisibleDuration, l => complate?.Invoke(), l =>
            {
                l.Storyboard.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
                l.Easing = this.VisibleEasing;
            });

        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationX(0, this.EndPoint.X, this.HideDuration, l =>
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
            }, l =>
            {
                l.Storyboard.FillBehavior = this.FillBehavior;
                l.Easing = this.HideEasing;
            });
            element.BeginAnimationY(0, this.EndPoint.Y, this.HideDuration, l => element.Visibility = HiddenOrCollapsed, l =>
            {
                l.Storyboard.FillBehavior = FillBehavior.Stop;
                l.Easing = this.HideEasing;
            });

        }
    }
}
