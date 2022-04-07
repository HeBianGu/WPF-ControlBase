// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Service.Animation
{
    public class AngleTransition : TransitionBase
    {
        public AngleTransition()
        {
            this.RenderTransformOrigin = new Point(0, 0);

            //this.VisibleDuration = 1000;
        }
        public double StartAngle { get; set; } = 90;

        public double EndAngle { get; set; } = 90;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationAngle(this.StartAngle, 0, this.VisibleDuration, l =>
            {
                element.Visibility = Visibility.Visible;
                complate?.Invoke();
            }, l =>
            {
                l.Easing = this.VisibleEasing;
            });
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationAngle(0, this.EndAngle, this.HideDuration,
                l =>
                {
                    l.Visibility = Visibility.Hidden;
                    complate?.Invoke();
                }, l =>
                {
                    l.Easing = this.HideEasing;
                });
        }

        //public override void BeginVisible(UIElement element, Action complate = null)
        //{
        //    if (!element.CheckDefaultTransformGroup()) return;

        //    base.BeginPrevious(element);

        //    element.BeginAnimationAngle(this.StartAngle, this.EndAngle, this.VisibleDuration,
        //        l =>
        //        {
        //            l.Visibility = Visibility.Visible;
        //            complate?.Invoke();
        //        });
        //}

    }
}
