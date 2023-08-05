// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 设置当前控件是否处于拖拽状态 </summary>
    public class ElementDragStateBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 是否正在拖拽
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.RegisterAttached(
            "IsDragging", typeof(bool), typeof(ElementDragStateBehavior), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDraggingChanged));

        public static bool GetIsDragging(DependencyObject d)
        {
            if (d == null) return false;

            return (bool)d.GetValue(IsDraggingProperty);
        }

        public static void SetIsDragging(DependencyObject obj, bool value)
        {
            if (obj == null) return;

            obj.SetValue(IsDraggingProperty, value);
        }

        private static void OnIsDraggingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary> 是否检测鼠标离开后设置未拖动状态 </summary>
        public bool IsUseMouseLeave { get; set; }

        protected override void OnAttached()
        {
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
            this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsUseMouseLeave) return;

            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, false);
        }

        private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, false);
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
            this.AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;

            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, true);

            System.Diagnostics.Debug.WriteLine("拖拽中");

        }
    }


    /// <summary> 动画帧 闪烁效果</summary>
    public class FlashBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUseAll)
            {
                IEnumerable<UIElement> items = AssociatedObject.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

                items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                this.RefreshAnimation(items);

            }
            else
            {
                if (AssociatedObject is Panel panel)
                {
                    IEnumerable<UIElement> items = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);

                    items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                    this.RefreshAnimation(items);
                }

            }
        }

        private void RefreshAnimation(IEnumerable<UIElement> items)
        {
            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            List<UIElement> controls = items?.ToList();

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
            storyboard.Begin();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }


        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepeatBehaviorProperty =
            DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(FlashBehavior), new PropertyMetadata(default(RepeatBehavior), (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

                if (control == null) return;

                //RepeatBehavior config = e.NewValue as RepeatBehavior;

            }));


        public bool IsUseAll
        {
            get { return (bool)GetValue(IsUseAllProperty); }
            set { SetValue(IsUseAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseAllProperty =
            DependencyProperty.Register("IsUseAll", typeof(bool), typeof(FlashBehavior), new PropertyMetadata(false, (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

                if (control == null) return;
            }));

        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(FlashBehavior), new PropertyMetadata(200.0, (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

                if (control == null) return;

                //double config = e.NewValue as double;

            }));


        public bool IsAnimation
        {
            get { return (bool)GetValue(IsAnimationProperty); }
            set { SetValue(IsAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAnimationProperty =
            DependencyProperty.Register("IsAnimation", typeof(bool), typeof(FlashBehavior), new PropertyMetadata(true, (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

                if (control == null) return;

                //bool config = e.NewValue as bool;

            }));

        public ArrayList Timelines
        {
            get { return (ArrayList)GetValue(TimelinesProperty); }
            set { SetValue(TimelinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimelinesProperty =
            DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(FlashBehavior), new PropertyMetadata(new ArrayList(), (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

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
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(FlashBehavior), new PropertyMetadata(5.0, (d, e) =>
            {
                FlashBehavior control = d as FlashBehavior;

                if (control == null) return;
            }));

    }


    public class MouseDownCancelBehavior : Behavior<FrameworkElement>
    {
        public bool UseCancel
        {
            get { return (bool)GetValue(UseCancelProperty); }
            set { SetValue(UseCancelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCancelProperty =
            DependencyProperty.Register("UseCancel", typeof(bool), typeof(MouseDownCancelBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                MouseDownCancelBehavior control = d as MouseDownCancelBehavior;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        protected override void OnAttached()
        {
            AssociatedObject.MouseDown += AssociatedObject_MouseDown; ;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.UseCancel) return;
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        }
    }
}