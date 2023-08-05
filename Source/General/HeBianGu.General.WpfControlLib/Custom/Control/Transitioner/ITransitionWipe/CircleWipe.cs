// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 原型过度效果 </summary>
    public class CircleWipe : ITransitionWipe
    {
        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;

        public TimeSpan MidTime { get; set; } = TimeSpan.FromMilliseconds(400);

        public TimeSpan Duration { get; set; } = TimeSpan.FromMilliseconds(1000);


        public PointOriginType PointOriginType { get; set; }


        public void Wipe(TransitionerSlide fromSlide, TransitionerSlide toSlide, Point origin, IZIndexController zIndexController)
        {

            if (fromSlide == null) throw new ArgumentNullException(nameof(fromSlide));
            if (toSlide == null) throw new ArgumentNullException(nameof(toSlide));
            if (zIndexController == null) throw new ArgumentNullException(nameof(zIndexController));


            if (this.PointOriginType == PointOriginType.MousePosition)
            {
                //  Do ：按鼠标位置计算
                Point postion = Mouse.GetPosition(toSlide);
                double x = postion.X / toSlide.ActualWidth;
                double y = postion.Y / toSlide.ActualHeight;
                origin = new Point(x, y);
            }
            else if (this.PointOriginType == PointOriginType.RandomInner)
            {
                //  Do ：随机计算
                Random random = new Random();
                origin = new Point(random.NextDouble(), random.NextDouble());
            }
            else if (this.PointOriginType == PointOriginType.Center)
            {
                //  Do ：中心点计算
                origin = new Point(0.5, 0.5);
            }

            double horizontalProportion = Math.Max(1.0 - origin.X, 1.0 * origin.X);
            double verticalProportion = Math.Max(1.0 - origin.Y, 1.0 * origin.Y);

            double radius = Math.Sqrt(Math.Pow(toSlide.ActualWidth * horizontalProportion, 2) + Math.Pow(toSlide.ActualHeight * verticalProportion, 2));

            ScaleTransform scaleTransform = new ScaleTransform(0, 0);
            TranslateTransform translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            EllipseGeometry ellipseGeomotry = new EllipseGeometry()
            {
                RadiusX = radius,
                RadiusY = radius,
                Transform = transformGroup
            };

            toSlide.SetCurrentValue(UIElement.ClipProperty, ellipseGeomotry);
            zIndexController.Stack(toSlide, fromSlide);

            //var zeroKeyTime = KeyTime.FromTimeSpan(StartTime);
            //var midKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(400));
            //var endKeyTime = KeyTime.FromTimeSpan(Duration);

            DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, StartTime));
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, MidTime));
            opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, Duration));
            opacityAnimation.Completed += (sender, args) =>
            {
                fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
                fromSlide.Opacity = 0;
            };
            fromSlide.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);

            DoubleAnimationUsingKeyFrames scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.Completed += (sender, args) =>
           {
               toSlide.SetCurrentValue(UIElement.ClipProperty, null);
               fromSlide.BeginAnimation(UIElement.OpacityProperty, null);
               fromSlide.Opacity = 0;
           };
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, MidTime));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, Duration));
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
    }


}