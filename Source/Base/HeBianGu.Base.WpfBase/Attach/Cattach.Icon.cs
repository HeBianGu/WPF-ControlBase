// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static double GetOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(OpacityProperty);
        }

        public static void SetOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(OpacityProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.RegisterAttached("Opacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnOpacityChanged));

        public static void OnOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
            "Icon", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetIcon(DependencyObject d)
        {
            return (string)d.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, string value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty FIconChangedProperty = DependencyProperty.RegisterAttached(
            "FIconChanged", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(""));

        public static string GetFIconChanged(DependencyObject d)
        {
            return (string)d.GetValue(FIconChangedProperty);
        }

        public static void SetFIconChanged(DependencyObject obj, string value)
        {
            obj.SetValue(FIconChangedProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.RegisterAttached(
            "IconSize", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(12D));

        public static double GetIconSize(DependencyObject d)
        {
            return (double)d.GetValue(IconSizeProperty);
        }

        public static void SetIconSize(DependencyObject obj, double value)
        {
            obj.SetValue(IconSizeProperty, value);
        }

        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.RegisterAttached(
            "IconMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static Thickness GetIconMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(IconMarginProperty);
        }

        public static void SetIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(IconMarginProperty, value);
        }


        public static Brush GetIconForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconForegroundProperty);
        }

        public static void SetIconForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconForegroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.RegisterAttached("IconForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnIconForegroundChanged));

        public static void OnIconForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

    }
}
