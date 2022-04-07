// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class VerticalTransition : TransitionBase
    {
        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationY(-(element as FrameworkElement).ActualHeight, 0, this.VisibleDuration, l => complate?.Invoke(), l =>
             {
                 l.Easing = this.VisibleEasing;
             });
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationY(0, (element as FrameworkElement).ActualHeight, this.HideDuration, l =>
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
