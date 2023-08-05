// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
    public class SlideOutWipe : ITransitionWipe
    {
        private readonly SineEase _sineEase = new SineEase();

        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {
            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));

            KeyTime zeroKeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
            KeyTime midishKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200));
            KeyTime endKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400));

            //back out old slide setup
            ScaleTransform scaleTransform = new ScaleTransform(1, 1);
            fromSlide.RenderTransform = scaleTransform;
            DoubleAnimationUsingKeyFrames scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, zeroKeyTime));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(.8, endKeyTime));
            scaleAnimation.Completed += (sender, args) =>
            {
                fromSlide.RenderTransform = null;
            };
            DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, zeroKeyTime));
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime));
            opacityAnimation.Completed += (sender, args) =>
            {
                fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
                fromSlide.Opacity = 0;
            };

            //slide in new slide setup
            TranslateTransform translateTransform = new TranslateTransform(0, toSlide.ActualHeight);
            toSlide.RenderTransform = translateTransform;
            DoubleAnimationUsingKeyFrames slideAnimation = new DoubleAnimationUsingKeyFrames();
            slideAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(toSlide.ActualHeight, zeroKeyTime));
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(toSlide.ActualHeight, midishKeyTime) { EasingFunction = _sineEase });
            slideAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, endKeyTime) { EasingFunction = _sineEase });

            //kick off!
            translateTransform.BeginAnimation(TranslateTransform.YProperty, slideAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            fromSlide.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            zIndexController.Stack(toSlide, fromSlide);
        }
    }
}