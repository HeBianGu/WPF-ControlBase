// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static readonly DependencyProperty AttachContentProperty = DependencyProperty.RegisterAttached(
            "AttachContent", typeof(ControlTemplate), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static ControlTemplate GetAttachContent(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(AttachContentProperty);
        }

        public static void SetAttachContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(AttachContentProperty, value);
        }


        public static Brush GetAttachForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(AttachForegroundProperty);
        }

        public static void SetAttachForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(AttachForegroundProperty, value);
        }

        public static readonly DependencyProperty AttachForegroundProperty =
            DependencyProperty.RegisterAttached("AttachForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachForegroundChanged));

        public static void OnAttachForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetAttachBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(AttachBackgroundProperty);
        }

        public static void SetAttachBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(AttachBackgroundProperty, value);
        }

        public static readonly DependencyProperty AttachBackgroundProperty =
            DependencyProperty.RegisterAttached("AttachBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachBackgroundChanged));

        public static void OnAttachBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetAttachBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(AttachBorderBrushProperty);
        }

        public static void SetAttachBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(AttachBorderBrushProperty, value);
        }

        public static readonly DependencyProperty AttachBorderBrushProperty =
            DependencyProperty.RegisterAttached("AttachBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnAttachBorderBrushChanged));

        public static void OnAttachBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

        public static double GetAttachWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(AttachWidthProperty);
        }

        public static void SetAttachWidth(DependencyObject obj, double value)
        {
            obj.SetValue(AttachWidthProperty, value);
        }

        public static readonly DependencyProperty AttachWidthProperty =
            DependencyProperty.RegisterAttached("AttachWidth", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnAttachWidthChanged));

        public static void OnAttachWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static Thickness GetAttachBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(AttachBorderThicknessProperty);
        }

        public static void SetAttachBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(AttachBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty AttachBorderThicknessProperty =
            DependencyProperty.RegisterAttached("AttachBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnAttachBorderThicknessChanged));

        public static void OnAttachBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static HorizontalAlignment GetAttachHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(AttachHorizontalAlignmentProperty);
        }

        public static void SetAttachHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(AttachHorizontalAlignmentProperty, value);
        }


        public static readonly DependencyProperty AttachHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("AttachHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(HorizontalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnAttachHorizontalAlignmentChanged));

        public static void OnAttachHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

            HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
        }


        public static VerticalAlignment GetAttachVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(AttachVerticalAlignmentProperty);
        }

        public static void SetAttachVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(AttachVerticalAlignmentProperty, value);
        }

        public static readonly DependencyProperty AttachVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("AttachVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new FrameworkPropertyMetadata(default(VerticalAlignment), FrameworkPropertyMetadataOptions.Inherits, OnAttachVerticalAlignmentChanged));

        public static void OnAttachVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            VerticalAlignment n = (VerticalAlignment)e.NewValue;

            VerticalAlignment o = (VerticalAlignment)e.OldValue;
        }


        public static Thickness GetAttachMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(AttachMarginProperty);
        }

        public static void SetAttachMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(AttachMarginProperty, value);
        }

        public static readonly DependencyProperty AttachMarginProperty =
            DependencyProperty.RegisterAttached("AttachMargin", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnAttachMarginChanged));

        public static void OnAttachMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }

        [TypeConverter(typeof(LengthConverter))]
        public static double GetAttachHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(AttachHeightProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetAttachHeight(DependencyObject obj, double value)
        {
            obj.SetValue(AttachHeightProperty, value);
        }

        public static readonly DependencyProperty AttachHeightProperty =
            DependencyProperty.RegisterAttached("AttachHeight", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnAttachHeightChanged));

        public static void OnAttachHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static Dock GetAttachDock(DependencyObject obj)
        {
            return (Dock)obj.GetValue(AttachDockProperty);
        }

        public static void SetAttachDock(DependencyObject obj, Dock value)
        {
            obj.SetValue(AttachDockProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty AttachDockProperty =
            DependencyProperty.RegisterAttached("AttachDock", typeof(Dock), typeof(Cattach), new PropertyMetadata(default(Dock), OnAttachDockChanged));

        static public void OnAttachDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Dock n = (Dock)e.NewValue;

            Dock o = (Dock)e.OldValue;
        }

    }

}
