// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Service.Animation
{
    /// <summary> 容器内子控件加载时触发动画效果</summary>
    //[Obsolete("使用 InvokeTimeSplitAnimationAction 更灵活,后面不继续使用")]
    public class LoadAnimationBehavior : Behavior<FrameworkElement>
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
                this.BeginAnimation(items);
            }
            else
            {
                if (AssociatedObject is Panel panel)
                {
                    IEnumerable<UIElement> items = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);
                    items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                    this.BeginAnimation(items);
                }
            }
        }

        private void BeginAnimation(IEnumerable<DependencyObject> controls)
        {
            if (LoadAnimationType == LoadAnimationType.HorizontalAlignment)
            {
                StoryBoardService.BeginAnimationX(controls, this.StartValue, this.EndValue, this.End, this.Split);
            }
            else if (LoadAnimationType == LoadAnimationType.VerticalAlignment)
            {
                StoryBoardService.BeginAnimationY(controls, this.StartValue, this.EndValue, this.End, this.Split);
            }
            else if (LoadAnimationType == LoadAnimationType.Opactiy)
            {
                StoryBoardService.BeginAnimationOpacity(controls, this.StartValue, this.EndValue, this.End, this.Split);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

        public bool IsUseAll
        {
            get { return (bool)GetValue(IsUseAllProperty); }
            set { SetValue(IsUseAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseAllProperty =
            DependencyProperty.Register("IsUseAll", typeof(bool), typeof(LoadAnimationBehavior), new PropertyMetadata(false, (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;
             }));


        public LoadAnimationType LoadAnimationType
        {
            get { return (LoadAnimationType)GetValue(LoadAnimationTypeProperty); }
            set { SetValue(LoadAnimationTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadAnimationTypeProperty =
            DependencyProperty.Register("LoadAnimationType", typeof(LoadAnimationType), typeof(LoadAnimationBehavior), new PropertyMetadata(default(LoadAnimationType), (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;

                 //LoadAnimationType config = e.NewValue as LoadAnimationType;

             }));

        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(double), typeof(LoadAnimationBehavior), new PropertyMetadata(default(double), (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(LoadAnimationBehavior), new PropertyMetadata(default(double), (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));




        public double End
        {
            get { return (double)GetValue(EndProperty); }
            set { SetValue(EndProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndProperty =
            DependencyProperty.Register("End", typeof(double), typeof(LoadAnimationBehavior), new PropertyMetadata(default(double), (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public double Split
        {
            get { return (double)GetValue(SplitProperty); }
            set { SetValue(SplitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitProperty =
            DependencyProperty.Register("Split", typeof(double), typeof(LoadAnimationBehavior), new PropertyMetadata(0.05, (d, e) =>
             {
                 LoadAnimationBehavior control = d as LoadAnimationBehavior;

                 if (control == null) return;
             }));

    }

    /// <summary> 加载动画效果类型 </summary> 
    public enum LoadAnimationType
    {
        HorizontalAlignment = 0, VerticalAlignment, Opactiy
    }
}