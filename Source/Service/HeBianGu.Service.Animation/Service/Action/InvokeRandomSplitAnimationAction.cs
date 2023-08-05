// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    /// <summary> 根据输入动画执行渐进效果 </summary>
    public sealed class InvokeRandomSplitAnimationAction : InvokeTimeSplitAnimationAction
    {
        /// <summary> 参考点随机 </summary>
        public bool UseOrigin
        {
            get { return (bool)GetValue(UseOriginProperty); }
            set { SetValue(UseOriginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseOriginProperty =
            DependencyProperty.Register("UseOrigin", typeof(bool), typeof(InvokeRandomSplitAnimationAction), new PropertyMetadata(default(bool), (d, e) =>
             {
                 InvokeRandomSplitAnimationAction control = d as InvokeRandomSplitAnimationAction;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public Point OriginFrom
        {
            get { return (Point)GetValue(OriginFromProperty); }
            set { SetValue(OriginFromProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginFromProperty =
            DependencyProperty.Register("OriginFrom", typeof(Point), typeof(InvokeRandomSplitAnimationAction), new PropertyMetadata(default(Point), (d, e) =>
             {
                 InvokeRandomSplitAnimationAction control = d as InvokeRandomSplitAnimationAction;

                 if (control == null) return;

                 //Point config = e.NewValue as Point;

             }));


        public Point OriginTo
        {
            get { return (Point)GetValue(OriginToProperty); }
            set { SetValue(OriginToProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginToProperty =
            DependencyProperty.Register("OriginTo", typeof(Point), typeof(InvokeRandomSplitAnimationAction), new PropertyMetadata(default(Point), (d, e) =>
             {
                 InvokeRandomSplitAnimationAction control = d as InvokeRandomSplitAnimationAction;

                 if (control == null) return;

                 //Point config = e.NewValue as Point;

             }));

        public static Random random = new Random();

        private Point GetRandomOrigin()
        {
            Point point = new Point(random.NextDouble() * (this.OriginTo.X - this.OriginFrom.X) + this.OriginFrom.X,
                random.NextDouble() * (this.OriginTo.Y - this.OriginFrom.Y) + this.OriginFrom.Y);
            return point;
        }


        public bool UseIndex
        {
            get { return (bool)GetValue(UseIndexProperty); }
            set { SetValue(UseIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseIndexProperty =
            DependencyProperty.Register("UseIndex", typeof(bool), typeof(InvokeRandomSplitAnimationAction), new PropertyMetadata(default(bool), (d, e) =>
             {
                 InvokeRandomSplitAnimationAction control = d as InvokeRandomSplitAnimationAction;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        /// <summary> 执行方法 </summary>
        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject == null) return;

            if (this.AutoFindParent)
            {
                this.Target = this.AssociatedObject.GetParent<Panel>();
            }

            if (this.Target == null)
            {
                this.Target = this.AssociatedObject;
            }

            IEnumerable<UIElement> children = null;

            if (IsUseAll)
            {

                children = this.Target.GetChildren<UIElement>(l => Cattach.GetIsExcepChildren(l) == false, true).Where(l => l.RenderTransform is TransformGroup).ToList();

                children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                children = children.Where(l => Cattach.GetIsExceptSelf(l) == false);
                children = children.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except");
                children = children.Where(l => !InvokeRandomSplitAnimationAction.GetIsExcept(l));
            }
            else
            {
                if (this.Target is Panel panel)
                {
                    children = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);

                    children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4 && !InvokeRandomSplitAnimationAction.GetIsExcept(l));
                }
            }

            if (children == null) return;

            List<UIElement> controls = children?.ToList();

            if (controls == null || controls.Count == 0) return;

            if (this.UseIndex)
            {
                controls = this.RandomList(controls);
            }

            Storyboard storyboard = StoryboardFactory.Create();

            for (int i = 0; i < controls.Count; i++)
            {
                if (this.UseOrigin)
                {
                    controls[i].RenderTransformOrigin = this.GetRandomOrigin();
                }

                foreach (RandomDoubleAnimation item in Timelines.OfType<RandomDoubleAnimation>())
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (SplitMilliSecond + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    DoubleAnimation animation = item.ConvertTo();

                    if (item.BeginTime == TimeSpan.Zero)
                    {
                        DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

                        EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(animation.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                        frames.KeyFrames.Add(key1);
                        Storyboard.SetTarget(frames, controls[i]);
                        Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                        storyboard.Children.Add(frames);
                    }


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
            storyboard.RepeatBehavior = this.RepeatBehavior;
            storyboard.AutoReverse = this.AutoReverse;
            storyboard.Begin();
        }

        public List<T> RandomList<T>(List<T> from)
        {
            T temp;

            for (int i = 0; i < from.Count; i++)
            {
                int index = random.Next(i, from.Count - 1);

                temp = from[i];

                from[i] = from[index];

                from[index] = temp;
            }

            return from;
        }
    }
}
