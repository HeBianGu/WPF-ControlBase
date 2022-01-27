using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    static partial class Cattach
    {
        public static readonly DependencyProperty TitleHorizontalAlignmentProperty = DependencyProperty.RegisterAttached(
            "TitleHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(HorizontalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnTitleHorizontalAlignmentChanged));

        public static HorizontalAlignment GetTitleHorizontalAlignment(DependencyObject d)
        {
            return (HorizontalAlignment)d.GetValue(TitleHorizontalAlignmentProperty);
        }

        public static void SetTitleHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(TitleHorizontalAlignmentProperty, value);
        }

        static void OnTitleHorizontalAlignmentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleVerticalAlignmentProperty = DependencyProperty.RegisterAttached(
            "TitleVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(VerticalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnTitleVerticalAlignmentChanged));

        public static VerticalAlignment GetTitleVerticalAlignment(DependencyObject d)
        {
            return (VerticalAlignment)d.GetValue(TitleVerticalAlignmentProperty);
        }

        public static void SetTitleVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(TitleVerticalAlignmentProperty, value);
        }

        static void OnTitleVerticalAlignmentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleMarginProperty = DependencyProperty.RegisterAttached(
            "TitleMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnTitleMarginChanged));

        public static Thickness GetTitleMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(TitleMarginProperty);
        }

        public static void SetTitleMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TitleMarginProperty, value);
        }

        static void OnTitleMarginChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static int GetBackgroundColumn(DependencyObject obj)
        {
            return (int)obj.GetValue(BackgroundColumnProperty);
        }

        public static void SetBackgroundColumn(DependencyObject obj, int value)
        {
            obj.SetValue(BackgroundColumnProperty, value);
        }

        public static readonly DependencyProperty BackgroundColumnProperty =
            DependencyProperty.RegisterAttached("BackgroundColumn", typeof(int), typeof(Cattach), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.Inherits, OnBackgroundColumnChanged));

        static public void OnBackgroundColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            int n = (int)e.NewValue;

            int o = (int)e.OldValue;
        }

        public static readonly DependencyProperty TitleWidthProperty = DependencyProperty.RegisterAttached(
            "TitleWidth", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnTitleWidthChanged));

        [TypeConverter(typeof(LengthConverter))]
        public static double GetTitleWidth(DependencyObject d)
        {
            return (double)d.GetValue(TitleWidthProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetTitleWidth(DependencyObject obj, double value)
        {
            obj.SetValue(TitleWidthProperty, value);
        }

        static void OnTitleWidthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleHeightProperty = DependencyProperty.RegisterAttached(
            "TitleHeight", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnTitleHeightChanged));

        [TypeConverter(typeof(LengthConverter))]
        public static double GetTitleHeight(DependencyObject d)
        {
            return (double)d.GetValue(TitleHeightProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetTitleHeight(DependencyObject obj, double value)
        {
            obj.SetValue(TitleHeightProperty, value);
        }

        static void OnTitleHeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(
            "Title", typeof(object), typeof(Cattach), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleChanged));

        public static object GetTitle(DependencyObject d)
        {
            return (object)d.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, object value)
        {
            obj.SetValue(TitleProperty, value);
        }

        static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.RegisterAttached(
            "TitleTemplate", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(default(ControlTemplate), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTitleTemplateChanged));

        public static ControlTemplate GetTitleTemplate(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(TitleTemplateProperty);
        }

        public static void SetTitleTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(TitleTemplateProperty, value);
        }

        static void OnTitleTemplateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }
        public static readonly DependencyProperty TitleBackgroundProperty = DependencyProperty.RegisterAttached(
            "TitleBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnTitleBackgroundChanged));

        public static Brush GetTitleBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleBackgroundProperty);
        }

        public static void SetTitleBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBackgroundProperty, value);
        }

        static void OnTitleBackgroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.RegisterAttached(
            "TitleForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnTitleForegroundChanged));

        public static Brush GetTitleForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleForegroundProperty);
        }

        public static void SetTitleForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleForegroundProperty, value);
        }

        static void OnTitleForegroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleBorderBrushProperty = DependencyProperty.RegisterAttached(
            "TitleBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnTitleBorderBrushChanged));

        public static Brush GetTitleBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(TitleBorderBrushProperty);
        }

        public static void SetTitleBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(TitleBorderBrushProperty, value);
        }

        static void OnTitleBorderBrushChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty TitleBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "TitleBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnTitleBorderThicknessChanged));

        public static Thickness GetTitleBorderThickness(DependencyObject d)
        {
            return (Thickness)d.GetValue(TitleBorderThicknessProperty);
        }

        public static void SetTitleBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(TitleBorderThicknessProperty, value);
        }

        static void OnTitleBorderThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static CornerRadius GetTitleCornerRaius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(TitleCornerRaiusProperty);
        }

        public static void SetTitleCornerRaius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(TitleCornerRaiusProperty, value);
        }
        public static readonly DependencyProperty TitleCornerRaiusProperty =
            DependencyProperty.RegisterAttached("TitleCornerRaius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits, OnTitleCornerRaiusChanged));

        static public void OnTitleCornerRaiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            CornerRadius n = (CornerRadius)e.NewValue;

            CornerRadius o = (CornerRadius)e.OldValue;
        }
    }

}
