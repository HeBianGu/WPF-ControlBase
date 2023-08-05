// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Service.Animation
{
    public class OpacityTransition : TransitionBase
    {
        public double StartOpacity { get; set; }

        public double EndOpacity { get; set; }


        public OpacityTransition()
        {
            this.StartOpacity = 0;
            this.EndOpacity = 0;
        }
        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            //element.RenderTransformOrigin = this.RenderTransformOrigin;
            if (!this.CanAnimation(element))
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
                return;
            }
            element.BeginAnimationOpacity(1, this.StartOpacity, this.HideDuration, l =>
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
            });

        }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) 
                return;
            element.Visibility = Visibility.Visible;
            if (!this.CanAnimation(element))
            {
                complate?.Invoke();
                return;
            }
            element.BeginAnimationOpacity(this.StartOpacity, 1, this.VisibleDuration, l =>
            {
                element.Visibility = Visibility.Visible;
                complate?.Invoke();
            });
        }
    }
}
