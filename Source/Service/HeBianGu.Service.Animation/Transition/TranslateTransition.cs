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
            if (!element.CheckDefaultTransformGroup()) 
                return;

            base.BeginCurrent(element);

            if (!this.CanAnimation(element))
            {
                complate?.Invoke();
                return;
            }

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

            if (!this.CanAnimation(element))
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
                return;
            }

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


        public static TranslateTransition GetDockRight()
        {
            var trans = new TranslateTransition()
            {
                StartPoint = new System.Windows.Point(400, 0),
                EndPoint = new System.Windows.Point(300, 0)
            };
            return trans;
        }

        public static TranslateTransition GetDockLeft()
        {
            var trans = new TranslateTransition()
            {
                StartPoint = new System.Windows.Point(-400, 0),
                EndPoint = new System.Windows.Point(-300, 0)
            };
            return trans;
        }

        public static TranslateTransition GetDockTop()
        {
            var trans = new TranslateTransition()
            {
                StartPoint = new System.Windows.Point(0, -200),
                EndPoint = new System.Windows.Point(0, -200)
            };
            return trans;
        }

        public static TranslateTransition GetDockBottm()
        {
            var trans = new TranslateTransition()
            {
                StartPoint = new System.Windows.Point(0, 200),
                EndPoint = new System.Windows.Point(0, 200)
            };
            return trans;
        }
    }
}
