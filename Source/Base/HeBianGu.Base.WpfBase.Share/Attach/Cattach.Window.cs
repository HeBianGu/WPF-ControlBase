// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static object GetBottom(DependencyObject obj)
        {
            return obj.GetValue(BottomProperty);
        }

        public static void SetBottom(DependencyObject obj, object value)
        {
            obj.SetValue(BottomProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty BottomProperty =
            DependencyProperty.RegisterAttached("Bottom", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnBottomChanged));

        public static void OnBottomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static object GetCaptionLeft(DependencyObject obj)
        {
            return obj.GetValue(CaptionLeftProperty);
        }

        public static void SetCaptionLeft(DependencyObject obj, object value)
        {
            obj.SetValue(CaptionLeftProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionLeftProperty =
            DependencyProperty.RegisterAttached("CaptionLeft", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnCaptionLeftChanged));

        public static void OnCaptionLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static object GetCaptionRight(DependencyObject obj)
        {
            return obj.GetValue(CaptionRightProperty);
        }

        public static void SetCaptionRight(DependencyObject obj, object value)
        {
            obj.SetValue(CaptionRightProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionRightProperty =
            DependencyProperty.RegisterAttached("CaptionRight", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnCaptionRightChanged));

        public static void OnCaptionRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static object GetTop(DependencyObject obj)
        {
            return obj.GetValue(TopProperty);
        }

        public static void SetTop(DependencyObject obj, object value)
        {
            obj.SetValue(TopProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty TopProperty =
            DependencyProperty.RegisterAttached("Top", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnTopChanged));

        public static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static object GetLeft(DependencyObject obj)
        {
            return obj.GetValue(LeftProperty);
        }

        public static void SetLeft(DependencyObject obj, object value)
        {
            obj.SetValue(LeftProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.RegisterAttached("Left", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnLeftChanged));

        public static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }


        public static object GetRight(DependencyObject obj)
        {
            return obj.GetValue(RightProperty);
        }

        public static void SetRight(DependencyObject obj, object value)
        {
            obj.SetValue(RightProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.RegisterAttached("Right", typeof(object), typeof(Cattach), new PropertyMetadata(false, OnRightChanged));

        public static void OnRightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = e.NewValue;

            object o = e.OldValue;
        }

    }
}
