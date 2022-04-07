// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {

        public static CornerRadius GetCaptionCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CaptionCornerRadiusProperty);
        }

        public static void SetCaptionCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(CaptionCornerRadiusProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionCornerRadiusProperty =
            DependencyProperty.RegisterAttached("CaptionCornerRadius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits, OnCaptionCornerRadiusChanged));

        public static void OnCaptionCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            CornerRadius n = (CornerRadius)e.NewValue;

            CornerRadius o = (CornerRadius)e.OldValue;
        }


        public static Brush GetCaptionBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CaptionBackgroundProperty);
        }

        public static void SetCaptionBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CaptionBackgroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionBackgroundProperty =
            DependencyProperty.RegisterAttached("CaptionBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnCaptionBackgroundChanged));

        public static void OnCaptionBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetCaptionForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CaptionForegroundProperty);
        }

        public static void SetCaptionForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CaptionForegroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionForegroundProperty =
            DependencyProperty.RegisterAttached("CaptionForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnCaptionForegroundChanged));

        public static void OnCaptionForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetCaptionBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CaptionBorderBrushProperty);
        }

        public static void SetCaptionBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(CaptionBorderBrushProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionBorderBrushProperty =
            DependencyProperty.RegisterAttached("CaptionBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnCaptionBorderBrushChanged));

        public static void OnCaptionBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

        public static double GetCaptionHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(CaptionHeightProperty);
        }

        public static void SetCaptionHeight(DependencyObject obj, double value)
        {
            obj.SetValue(CaptionHeightProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.RegisterAttached("CaptionHeight", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnCaptionHeightChanged));

        public static void OnCaptionHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static Thickness GetCaptionBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CaptionBorderThicknessProperty);
        }

        public static void SetCaptionBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CaptionBorderThicknessProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionBorderThicknessProperty =
            DependencyProperty.RegisterAttached("CaptionBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnCaptionBorderThicknessChanged));

        public static void OnCaptionBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static HorizontalAlignment GetCaptionHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(CaptionHorizontalAlignmentProperty);
        }

        public static void SetCaptionHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(CaptionHorizontalAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("CaptionHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new PropertyMetadata(default(HorizontalAlignment), OnCaptionHorizontalAlignmentChanged));

        public static void OnCaptionHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

            HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
        }


        public static VerticalAlignment GetCaptionVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(CaptionVerticalAlignmentProperty);
        }

        public static void SetCaptionVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(CaptionVerticalAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("CaptionVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new PropertyMetadata(default(VerticalAlignment), OnCaptionVerticalAlignmentChanged));

        public static void OnCaptionVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            VerticalAlignment n = (VerticalAlignment)e.NewValue;

            VerticalAlignment o = (VerticalAlignment)e.OldValue;
        }


        public static Thickness GetCaptionMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CaptionMarginProperty);
        }

        public static void SetCaptionMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CaptionMarginProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CaptionMarginProperty =
            DependencyProperty.RegisterAttached("CaptionMargin", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnCaptionMarginChanged));

        public static void OnCaptionMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }

    }
}
