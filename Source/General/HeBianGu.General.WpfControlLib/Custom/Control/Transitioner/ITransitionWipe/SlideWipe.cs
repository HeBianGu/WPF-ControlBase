// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
    public class SlideWipe : ITransitionWipe
    {
        private readonly SineEase _sineEase = new SineEase();

        /// <summary>
        /// Direction of the slide wipe
        /// </summary>
        public SlideDirection Direction { get; set; }

        /// <summary>
        /// Duration of the animation
        /// </summary>
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(0.5);

        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            // Set up time points
            KeyTime zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            KeyTime endKeyTime = KeyTime.FromTimeSpan(Duration);

            // Set up coordinates
            double fromStartX = 0, fromEndX = 0, toStartX = 0, toEndX = 0;
            double fromStartY = 0, fromEndY = 0, toStartY = 0, toEndY = 0;

            if (Direction == SlideDirection.Left)
            {
                fromEndX = -fromSlide.ActualWidth;
                toStartX = toSlide.ActualWidth;
            }
            else if (Direction == SlideDirection.Right)
            {
                fromEndX = fromSlide.ActualWidth;
                toStartX = -toSlide.ActualWidth;
            }
            else if (Direction == SlideDirection.Up)
            {
                fromEndY = -fromSlide.ActualHeight;
                toStartY = toSlide.ActualHeight;
            }
            else if (Direction == SlideDirection.Down)
            {
                fromEndY = fromSlide.ActualHeight;
                toStartY = -toSlide.ActualHeight;
            }

            // From
            TranslateTransform fromTransform = new TranslateTransform(fromStartX, fromStartY);
            fromSlide.RenderTransform = fromTransform;
            DoubleAnimationUsingKeyFrames fromXAnimation = new DoubleAnimationUsingKeyFrames();
            fromXAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(fromStartX, zeroKeyTime));
            fromXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(fromEndX, endKeyTime, _sineEase));
            DoubleAnimationUsingKeyFrames fromYAnimation = new DoubleAnimationUsingKeyFrames();
            fromYAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(fromStartY, zeroKeyTime));
            fromYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(fromEndY, endKeyTime, _sineEase));

            // To
            TranslateTransform toTransform = new TranslateTransform(toStartX, toStartY);
            toSlide.RenderTransform = toTransform;
            DoubleAnimationUsingKeyFrames toXAnimation = new DoubleAnimationUsingKeyFrames();
            toXAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(toStartX, zeroKeyTime));
            toXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toEndX, endKeyTime, _sineEase));
            DoubleAnimationUsingKeyFrames toYAnimation = new DoubleAnimationUsingKeyFrames();
            toYAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(toStartY, zeroKeyTime));
            toYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toEndY, endKeyTime, _sineEase));

            // Set up events
            fromXAnimation.Completed += (sender, args) =>
            {
                fromTransform.BeginAnimation(TranslateTransform.XProperty, null);
                fromTransform.X = fromEndX;
                fromSlide.RenderTransform = null;
            };
            fromYAnimation.Completed += (sender, args) =>
            {
                fromTransform.BeginAnimation(TranslateTransform.YProperty, null);
                fromTransform.Y = fromEndY;
                fromSlide.RenderTransform = null;
            };
            toXAnimation.Completed += (sender, args) =>
            {
                toTransform.BeginAnimation(TranslateTransform.XProperty, null);
                toTransform.X = toEndX;
                toSlide.RenderTransform = null;
            };
            toYAnimation.Completed += (sender, args) =>
            {
                toTransform.BeginAnimation(TranslateTransform.YProperty, null);
                toTransform.Y = toEndY;
                toSlide.RenderTransform = null;
            };

            // Animate
            fromTransform.BeginAnimation(TranslateTransform.XProperty, fromXAnimation);
            fromTransform.BeginAnimation(TranslateTransform.YProperty, fromYAnimation);
            toTransform.BeginAnimation(TranslateTransform.XProperty, toXAnimation);
            toTransform.BeginAnimation(TranslateTransform.YProperty, toYAnimation);
            zIndexController.Stack(toSlide, fromSlide);
        }
    }
}