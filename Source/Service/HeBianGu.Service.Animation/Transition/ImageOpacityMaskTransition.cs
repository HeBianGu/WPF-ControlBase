// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace HeBianGu.Service.Animation
{
    public class ImageOpacityMaskTransition : TransitionBase
    {
        public ImageOpacityMaskTransition()
        {
            this.ImageSource = new BitmapImage(new Uri("pack://application:,,,/HeBianGu.General.WpfControlLib;component/Resources/logo.png", UriKind.RelativeOrAbsolute));

        }
        public ImageSource ImageSource { get; set; }

        public double StartTime { get; set; }

        public PointOriginType PointOriginType { get; set; }

        public Rect Viewport { get; set; } = new Rect(0.2, 0.2, 0.2, 0.2);

        public BrushMappingMode ViewportUnits { get; set; } = BrushMappingMode.RelativeToBoundingBox;

        public TileMode TileMode { get; set; } = TileMode.Tile;

        public bool IsSingle { get; set; } = true;

        public double ScaleValue { get; set; } = 3;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            Panel.SetZIndex(element, 1);

            FrameworkElement toSlide = element as FrameworkElement;

            Point origin = this.RenderTransformOrigin;

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

            double horizontalProportion = toSlide.ActualWidth * origin.X;

            double verticalProportion = toSlide.ActualHeight * origin.Y;

            ImageBrush image = new ImageBrush(this.ImageSource);

            if (!IsSingle)
            {
                image.Viewport = this.Viewport;
                image.ViewportUnits = this.ViewportUnits;
                image.TileMode = this.TileMode;
            }


            ScaleTransform scaleTransform = new ScaleTransform(0, 0);
            scaleTransform.CenterX = horizontalProportion;
            scaleTransform.CenterY = verticalProportion;
            image.Transform = scaleTransform;
            toSlide.SetCurrentValue(UIElement.OpacityMaskProperty, image);

            //var scaleAnimation = new DoubleAnimationUsingKeyFrames();

            //scaleAnimation.Completed += (sender, args) =>
            //{
            //    element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
            //};
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromMilliseconds(this.StartTime)));
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(10, TimeSpan.FromMilliseconds(this.VisibleDuration)));

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = ScaleValue;
            animation.BeginTime = TimeSpan.FromMilliseconds(this.StartTime);
            animation.Duration = TimeSpan.FromMilliseconds(this.VisibleDuration);
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

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            Panel.SetZIndex(element, 0);

            element.OpacityMask = null;
        }

        public override void BeginHidden(UIElement element, Action complate = null)
        {
            FrameworkElement toSlide = element as FrameworkElement;

            Point origin = this.RenderTransformOrigin;

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

            double horizontalProportion = toSlide.ActualWidth * origin.X;

            double verticalProportion = toSlide.ActualHeight * origin.Y;

            ImageBrush image = new ImageBrush(this.ImageSource);

            if (!IsSingle)
            {
                image.Viewport = this.Viewport;

                image.ViewportUnits = this.ViewportUnits;

                image.TileMode = this.TileMode;
            }

            ScaleTransform scaleTransform = new ScaleTransform(0, 0);

            scaleTransform.CenterX = horizontalProportion;

            scaleTransform.CenterY = verticalProportion;

            image.Transform = scaleTransform;

            toSlide.SetCurrentValue(UIElement.OpacityMaskProperty, image);

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 8;
            animation.To = 0;
            animation.BeginTime = TimeSpan.FromMilliseconds(this.StartTime);
            animation.Duration = TimeSpan.FromMilliseconds(this.HideDuration);
            animation.EasingFunction = this.VisibleEasing;
            animation.Completed += (sender, args) =>
            {
                element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
                element.Visibility = Visibility.Collapsed;
                complate?.Invoke();
            };

            //animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn };
            Timeline.SetDesiredFrameRate(animation, StoryboardSetting.DesiredFrameRate);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }
}
