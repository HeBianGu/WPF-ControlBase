// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.Panel
{
    /// <summary> 分页布局容器 </summary>
    public class FlashPanel : AnimationPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<FrameworkElement> children = this.GetChildren().OfType<FrameworkElement>()?.ToList();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            foreach (FrameworkElement elment in children)
            {
                if (Double.IsNaN(elment.Width) && elment.HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    elment.Width = finalSize.Width;
                }

                if (Double.IsNaN(elment.Height) && elment.VerticalAlignment == VerticalAlignment.Stretch)
                {
                    elment.Height = finalSize.Height;
                }

                elment.Measure(finalSize);

                Point from = new Point(-2 * elment.DesiredSize.Width, -2 * elment.DesiredSize.Height);

                Point end = new Point(center.X - elment.DesiredSize.Width / 2, center.Y - elment.DesiredSize.Height / 2);

                elment.Arrange(new Rect(end, elment.DesiredSize));
            }

            //if (this.IsAnimation)
            //{
            //    this.RefreshAnimation();
            //}

            return base.ArrangeOverride(finalSize);
        }

        protected override void RefreshAnimation()
        {
            System.Collections.Generic.IEnumerable<UIElement> items = this.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            System.Collections.Generic.List<UIElement> controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = StoryboardFactory.Create();

            for (int i = 0; i < controls.Count; i++)
            {
                foreach (DoubleAnimation item in Timelines.OfType<DoubleAnimation>())
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (SplitMilliSecond + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    if (item.BeginTime == TimeSpan.Zero)
                    {
                        DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

                        EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                        frames.KeyFrames.Add(key1);
                        Storyboard.SetTarget(frames, controls[i]);
                        Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                        storyboard.Children.Add(frames);
                    }

                    DoubleAnimation animation = item.Clone();
                    animation.BeginTime = span + animation.BeginTime;
                    Storyboard.SetTarget(animation, controls[i]);

                    storyboard.Children.Add(animation);
                }

                foreach (ThicknessAnimation item in Timelines.OfType<ThicknessAnimation>())
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (SplitMilliSecond + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    ThicknessAnimationUsingKeyFrames frames = new ThicknessAnimationUsingKeyFrames();

                    EasingThicknessKeyFrame key1 = new EasingThicknessKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                    frames.KeyFrames.Add(key1);
                    Storyboard.SetTarget(frames, controls[i]);
                    Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                    storyboard.Children.Add(frames);

                    ThicknessAnimation animation = item.Clone();
                    animation.BeginTime = span;
                    Storyboard.SetTarget(animation, controls[i]);

                    storyboard.Children.Add(animation);
                }
            }

            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Begin();
        }


        //  Do ：用于设置整个容器的大小
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }
    }
}
