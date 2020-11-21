// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE file in the project root for full license information. 

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 根据输入动画执行渐进效果 </summary>
    public class InvokeTimeSplitAnimationAction : TriggerAction<DependencyObject>
    {
        /// <summary> 绑定的容器对象 </summary>
        public DependencyObject Target
        {
            get { return (DependencyObject)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(DependencyObject), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(default(DependencyObject), (d, e) =>
             {
                 InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                 if (control == null) return;

                 DependencyObject config = e.NewValue as DependencyObject;

             }));


        /// <summary> 自动寻找上级容器执行 </summary>
        public bool AutoFindParent
        {
            get { return (bool)GetValue(AutoFindParentProperty); }
            set { SetValue(AutoFindParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoFindParentProperty =
            DependencyProperty.Register("AutoFindParent", typeof(bool), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(default(bool), (d, e) =>
             {
                 InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        /// <summary> 是否遍历子集执行动画 </summary>
        public bool IsUseAll
        {
            get { return (bool)GetValue(IsUseAllProperty); }
            set { SetValue(IsUseAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseAllProperty =
            DependencyProperty.Register("IsUseAll", typeof(bool), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(false, (d, e) =>
            {
                InvokeFountainAnimationAction control = d as InvokeFountainAnimationAction;

                if (control == null) return;
            }));



        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(200.0, (d, e) =>
            {
                InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                if (control == null) return;

                //double config = e.NewValue as double;

            }));

        public ArrayList Timelines
        {
            get { return (ArrayList)GetValue(TimelinesProperty); }
            set { SetValue(TimelinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimelinesProperty =
            DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(new ArrayList(), (d, e) =>
            {
                InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                if (control == null) return;

                ArrayList config = e.NewValue as ArrayList;

            }));

        public double SplitMilliSecond
        {
            get { return (double)GetValue(SplitMilliSecondProperty); }
            set { SetValue(SplitMilliSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitMilliSecondProperty =
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(5.0, (d, e) =>
            {
                InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                if (control == null) return;
            }));


        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepeatBehaviorProperty =
            DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(new RepeatBehavior(1), (d, e) =>
            {
                InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                if (control == null) return;

                //RepeatBehavior config = e.NewValue as RepeatBehavior;

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
                children = this.Target.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

                children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                children = children.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except");
            }
            else
            {
                if (this.Target is Panel panel)
                {
                    children = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);

                    children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                }
            }

            if (children == null) return;

            var controls = children?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = new Storyboard();

            for (int i = 0; i < controls.Count; i++)
            {
                foreach (var item in Timelines.OfType<DoubleAnimation>())
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

                foreach (var item in Timelines.OfType<ThicknessAnimation>())
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

            storyboard.Begin();
        }
    }

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

        Point GetRandomOrigin()
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
                children = this.Target.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

                children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                children = children.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except");
            }
            else
            {
                if (this.Target is Panel panel)
                {
                    children = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);

                    children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                }
            }

            if (children == null) return;

            var controls = children?.ToList();

            if (controls == null || controls.Count == 0) return;

            if (this.UseIndex)
            {
                controls = this.RandomList(controls);
            }

            Storyboard storyboard = new Storyboard();

            for (int i = 0; i < controls.Count; i++)
            {
                if (this.UseOrigin)
                {
                    controls[i].RenderTransformOrigin = this.GetRandomOrigin();
                }

                foreach (var item in Timelines.OfType<RandomDoubleAnimation>())
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

                foreach (var item in Timelines.OfType<ThicknessAnimation>())
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

    /// <summary> 带有随机标识的动画 </summary>
    public class RandomDoubleAnimation : DoubleAnimation
    {
        public bool UseFrom { get; set; } = false;

        public bool UseTo { get; set; } = false;

        public static Random _random = new Random();

        public double Max { get; set; }

        public double Min { get; set; }

        double GetRandom()
        {
            return _random.NextDouble() * (this.Max - this.Min) + this.Min;
        }

        public DoubleAnimation ConvertTo()
        {
            DoubleAnimation animation = this.Clone();

            if (this.UseFrom)
            {
                //  Do ：From随机
                animation.From = this.GetRandom();
            }

            if (this.UseTo)
            {
                //  Do ：To随机
                animation.To = this.GetRandom();
            }

            return animation;

        }
    }
}
