// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HeBianGu.Service.Animation
{
    public class ScaleGeomotryTransition : TransitionBase
    {
        public ScaleGeomotryTransition()
        {
            this.Geometry = new EllipseGeometry();
        }

        public double StartTime { get; set; }

        public PointOriginType PointOriginType { get; set; }

        public Geometry Geometry { get; set; }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            this.Begin(element, 0, 1, this.VisibleDuration, complate);
        }


        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            Panel.SetZIndex(element, 0);

            element.BeginAnimationOpacity(1, 0.9, this.HideDuration, l => element.Visibility = HiddenOrCollapsed);
        }

        public override void BeginHidden(UIElement element, Action complate = null)
        {
            if(this.CanAnimation(element) ==false)
            {
                element.Visibility = HiddenOrCollapsed;
                complate?.Invoke();
                return;
            }
            this.Begin(element, 1, 0, this.HideDuration, () =>
             {
                 //  Message：Visibility.Hidden 当设置成折叠时，显示时窗口布局没有更新会产生异常
                 element.Visibility = HiddenOrCollapsed;
                 complate?.Invoke();
             });
        }

        //public override void BeginVisible(UIElement element, Action complate = null)
        //{
        //    this.Begin(element, 0, 1, this.VisibleDuration);
        //}


        private void Begin(UIElement element, double from, double to, double duration, Action complate = null)
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

            if (this.PointOriginType == PointOriginType.MouseInnerOrCenter)
            {
                //  Do ：按鼠标位置计算
                Point postion = Mouse.GetPosition(toSlide);

                if (postion.X < 0 || postion.Y < 0)
                {
                    origin = new Point(0.5, 0.5);
                }
                else
                {
                    double x = postion.X / toSlide.ActualWidth;
                    double y = postion.Y / toSlide.ActualHeight;
                    origin = new Point(x, y);
                }
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

            double horizontalProportion = toSlide.ActualWidth * Math.Max(1.0 - origin.X, 1.0 * origin.X);

            double verticalProportion = toSlide.ActualHeight * Math.Max(1.0 - origin.Y, 1.0 * origin.Y);

            double radius = Math.Sqrt(Math.Pow(horizontalProportion, 2) + Math.Pow(verticalProportion, 2));

            TransformGroup transformGroup = new TransformGroup();

            ScaleTransform scaleTransform = new ScaleTransform(0, 0);
            transformGroup.Children.Add(scaleTransform);

            TranslateTransform translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
            transformGroup.Children.Add(translateTransform);

            if (this.Geometry is EllipseGeometry ellipse)
            {
                ellipse.RadiusX = radius;
                ellipse.RadiusY = radius;

            }
            else if (this.Geometry is RectangleGeometry rectangle)
            {
                rectangle.Rect = new Rect(-horizontalProportion, -verticalProportion, 2 * horizontalProportion, 2 * verticalProportion);
            }

            this.Geometry.Transform = transformGroup;
            toSlide.SetCurrentValue(UIElement.ClipProperty, this.Geometry);

            DoubleAnimationUsingKeyFrames scaleAnimation = new DoubleAnimationUsingKeyFrames();
            scaleAnimation.Completed += (sender, args) =>
            {
                //System.Diagnostics.Debug.WriteLine(this.Geometry.Bounds.Width); 
                toSlide.SetCurrentValue(UIElement.ClipProperty, null);
                complate?.Invoke();
            };
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(from, TimeSpan.FromMilliseconds(this.StartTime)));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(to, TimeSpan.FromMilliseconds(duration)) { EasingFunction = this.VisibleEasing });
            Timeline.SetDesiredFrameRate(scaleAnimation, StoryboardSetting.DesiredFrameRate);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
    }

    public enum PointOriginType
    {
        Default = 0, Custom, Center, MousePosition, RandomInner, MouseInnerOrCenter
    }
}
