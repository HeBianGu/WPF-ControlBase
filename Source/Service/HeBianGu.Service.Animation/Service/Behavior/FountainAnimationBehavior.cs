// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace HeBianGu.Service.Animation
{
    /// <summary> 容器内子控件加载时触发喷泉效果</summary>
    //[Obsolete("使用 InvokeTimeSplitAnimationAction 更灵活,后面不继续使用")]
    public class FountainAnimationBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }


        public static bool GetIsExcept(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExceptProperty);
        }

        public static void SetIsExcept(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExceptProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsExceptProperty =
            DependencyProperty.RegisterAttached("IsExcept", typeof(bool), typeof(FountainAnimationBehavior), new PropertyMetadata(default(bool), OnIsExceptChanged));

        static public void OnIsExceptChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUseAll)
            {
                System.Collections.Generic.IEnumerable<UIElement> items = AssociatedObject.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);
                items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                items = items.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except" && FountainAnimationBehavior.GetIsExcept(l) == false);
                StoryBoardService.FountainAnimation(items, PointLeft, PointTop, Mul, MiddleValue, EndValue, Split);
            }
            else
            {
                if (AssociatedObject is Panel panel)
                {
                    System.Collections.Generic.IEnumerable<UIElement> items = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);
                    items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);
                    items = items.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except" && FountainAnimationBehavior.GetIsExcept(l) == false);
                    StoryBoardService.FountainAnimation(items, PointLeft, PointTop, Mul, MiddleValue, EndValue, Split);
                }

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
            DependencyProperty.Register("IsUseAll", typeof(bool), typeof(FountainAnimationBehavior), new PropertyMetadata(false, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));


        /// <summary> 左右范围 </summary>
        public int PointLeft
        {
            get { return (int)GetValue(PointLeftProperty); }
            set { SetValue(PointLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointLeftProperty =
            DependencyProperty.Register("PointLeft", typeof(int), typeof(FountainAnimationBehavior), new PropertyMetadata(-500, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

             }));

        /// <summary> 上下范围 </summary>
        public int PointTop
        {
            get { return (int)GetValue(PointTopProperty); }
            set { SetValue(PointTopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointTopProperty =
            DependencyProperty.Register("PointTop", typeof(int), typeof(FountainAnimationBehavior), new PropertyMetadata(500, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));

        /// <summary> 放大倍数 </summary>
        public double Mul
        {
            get { return (double)GetValue(MulProperty); }
            set { SetValue(MulProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulProperty =
            DependencyProperty.Register("Mul", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(10.0, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));

        /// <summary> 放大时间点 </summary>
        public double MiddleValue
        {
            get { return (double)GetValue(MiddleValueProperty); }
            set { SetValue(MiddleValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiddleValueProperty =
            DependencyProperty.Register("MiddleValue", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(0.1, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));

        /// <summary> 还原时间点 </summary>
        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(1.0, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));


        public double Split
        {
            get { return (double)GetValue(SplitProperty); }
            set { SetValue(SplitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitProperty =
            DependencyProperty.Register("Split", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(0.05, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;
             }));

    }
}