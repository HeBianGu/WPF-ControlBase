// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class RadialOpacityMaskTransition : TransitionBase
    {
        public RadialGradientBrush Brush { get; set; }

        public RadialOpacityMaskTransition()
        {
            GradientStopCollection stops = new GradientStopCollection();

            stops.Add(new GradientStop() { Offset = 0, Color = Colors.Transparent });
            stops.Add(new GradientStop() { Offset = 0.2, Color = Colors.Transparent });
            stops.Add(new GradientStop() { Offset = 1, Color = Colors.Black });

            this.Brush = new RadialGradientBrush(stops);
            this.Start = 0;
            this.End = 5;
        }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            if (!Reverse) return;

            this.Begin(element as FrameworkElement, this.End, this.Start, this.VisibleDuration, complate);

            //this.Begin(element as FrameworkElement, this.End, this.Start, this.VisibleDuration, complate);
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            if (Reverse)
            {
                Action<UIElement> action = l =>
                {
                    element.Visibility = HiddenOrCollapsed;
                    complate?.Invoke();
                };

                element.Wait(this.HideDuration, action);
            }
            else
            {
                Action action = () =>
                {
                    element.Visibility = HiddenOrCollapsed;
                    complate?.Invoke();
                };

                this.Begin(element as FrameworkElement, this.Start, this.End, this.HideDuration, action);
            }
        }

        public override void BeginVisible(UIElement element, Action complate = null)
        {
            base.BeginVisible(element);

            this.Begin(element as FrameworkElement, this.End, this.Start, this.VisibleDuration, complate);
        }

        public override void BeginHidden(UIElement element, Action complate = null)
        {
            //base.BeginHidden(element, complate);

            Action action = () =>
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
            };

            this.Begin(element as FrameworkElement, this.Start, this.End, this.HideDuration, action);
        }

        private void Begin(FrameworkElement element, double from, double to, double duration, Action complate = null)
        {
            if (element == null) return;

            element.OpacityMask = this.Brush;

            ScaleTransform scaleTransform = new ScaleTransform(0, 0);

            scaleTransform.CenterX = element.ActualWidth / 2;

            scaleTransform.CenterY = element.ActualHeight / 2;

            this.Brush.Transform = scaleTransform;

            element.SetCurrentValue(UIElement.OpacityMaskProperty, this.Brush);

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = from;
            animation.To = to;
            animation.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime);
            animation.Duration = TimeSpan.FromMilliseconds(this.HideDuration);
            animation.EasingFunction = this.VisibleEasing;
            animation.Completed += (sender, args) =>
            {
                element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
                complate?.Invoke();
            };

            //animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn };
            Timeline.SetDesiredFrameRate(animation, StoryboardSetting.DesiredFrameRate); 
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
