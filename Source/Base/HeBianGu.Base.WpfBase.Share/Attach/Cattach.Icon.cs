using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    static partial class Cattach
    {
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
    }
}
