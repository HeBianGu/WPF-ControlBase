// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static object GetLeftContent(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(LeftContentProperty);
        }

        public static void SetLeftContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(LeftContentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.RegisterAttached("LeftContent", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(null, OnLeftContentChanged));

        public static void OnLeftContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = (ControlTemplate)e.NewValue;

            object o = (ControlTemplate)e.OldValue;
        }


        public static ControlTemplate GetRightContent(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(RightContentProperty);
        }

        public static void SetRightContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(RightContentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty RightContentProperty =
            DependencyProperty.RegisterAttached("RightContent", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(null, OnRightContentChanged));

        public static void OnRightContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = (ControlTemplate)e.NewValue;

            object o = (ControlTemplate)e.OldValue;
        }


        public static object GetTopContent(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(TopContentProperty);
        }

        public static void SetTopContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(TopContentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty TopContentProperty =
            DependencyProperty.RegisterAttached("TopContent", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(null, OnTopContentChanged));

        public static void OnTopContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = (ControlTemplate)e.NewValue;

            object o = (ControlTemplate)e.OldValue;
        }


        public static ControlTemplate GetBottomContent(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(BottomContentProperty);
        }

        public static void SetBottomContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(BottomContentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty BottomContentProperty =
            DependencyProperty.RegisterAttached("BottomContent", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(null, OnBottomContentChanged));

        public static void OnBottomContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            object n = (ControlTemplate)e.NewValue;

            object o = (ControlTemplate)e.OldValue;
        }

    }
}
