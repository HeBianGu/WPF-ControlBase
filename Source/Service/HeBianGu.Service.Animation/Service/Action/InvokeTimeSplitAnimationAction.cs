// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    //[DefaultMember("Timelines")]
    [DefaultProperty("Timelines")]
    [ContentProperty("Timelines")]
    public class InvokeTimeSplitAnimationAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// 是否过滤不应用动画
        /// </summary>
        public static readonly DependencyProperty IsExceptProperty = DependencyProperty.RegisterAttached(
            "IsExcept", typeof(bool), typeof(InvokeTimeSplitAnimationAction), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExceptChanged));

        public static bool GetIsExcept(DependencyObject d)
        {
            return (bool)d.GetValue(IsExceptProperty);
        }

        public static void SetIsExcept(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExceptProperty, value);
        }

        private static void OnIsExceptChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


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

        public ObservableCollection<Timeline> Timelines { get; } = new ObservableCollection<Timeline>();


        //public ArrayList Timelines
        //{
        //    get { return (ArrayList)GetValue(TimelinesProperty); }
        //    set { SetValue(TimelinesProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TimelinesProperty =
        //    DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(InvokeTimeSplitAnimationAction), new PropertyMetadata(new ArrayList(), (d, e) =>
        //    {
        //        InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

        //        if (control == null) return;

        //        ArrayList config = e.NewValue as ArrayList;

        //    }));

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


        public bool AutoReverse
        {
            get { return (bool)GetValue(AutoReverseProperty); }
            set { SetValue(AutoReverseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoReverseProperty =
            DependencyProperty.Register("AutoReverse", typeof(bool), typeof(InvokeTimeSplitAnimationAction), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 InvokeTimeSplitAnimationAction control = d as InvokeTimeSplitAnimationAction;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

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
                //children = this.Target.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

                //children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                //children = children.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except");

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

                    children = children.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                }
            }

            if (children == null) return;

            List<UIElement> controls = children?.ToList();

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
            storyboard.RepeatBehavior = this.RepeatBehavior;
            storyboard.AutoReverse = this.AutoReverse;

            storyboard.Begin();
        }
    }
}
