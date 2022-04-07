// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {

        public static CornerRadius GetItemCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(ItemCornerRadiusProperty);
        }

        public static void SetItemCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(ItemCornerRadiusProperty, value);
        }

        public static readonly DependencyProperty ItemCornerRadiusProperty =
            DependencyProperty.RegisterAttached("ItemCornerRadius", typeof(CornerRadius), typeof(Cattach), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.Inherits, OnItemCornerRadiusChanged));

        public static void OnItemCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            CornerRadius n = (CornerRadius)e.NewValue;

            CornerRadius o = (CornerRadius)e.OldValue;
        }


        public static Brush GetItemBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ItemBackgroundProperty);
        }

        public static void SetItemBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ItemBackgroundProperty, value);
        }

        public static readonly DependencyProperty ItemBackgroundProperty =
            DependencyProperty.RegisterAttached("ItemBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnItemBackgroundChanged));

        public static void OnItemBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetItemForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ItemForegroundProperty);
        }

        public static void SetItemForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ItemForegroundProperty, value);
        }

        public static readonly DependencyProperty ItemForegroundProperty =
            DependencyProperty.RegisterAttached("ItemForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnItemForegroundChanged));

        public static void OnItemForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetItemBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ItemBorderBrushProperty);
        }

        public static void SetItemBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(ItemBorderBrushProperty, value);
        }

        public static readonly DependencyProperty ItemBorderBrushProperty =
            DependencyProperty.RegisterAttached("ItemBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.Inherits, OnItemBorderBrushChanged));

        public static void OnItemBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

        public static double GetItemHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemHeightProperty);
        }

        public static void SetItemHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ItemHeightProperty, value);
        }

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.RegisterAttached("ItemHeight", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits, OnItemHeightChanged));

        public static void OnItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static Thickness GetItemBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ItemBorderThicknessProperty);
        }

        public static void SetItemBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemBorderThicknessProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemBorderThicknessProperty =
            DependencyProperty.RegisterAttached("ItemBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), FrameworkPropertyMetadataOptions.Inherits, OnItemBorderThicknessChanged));

        public static void OnItemBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static HorizontalAlignment GetItemHorizontalAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(ItemHorizontalAlignmentProperty);
        }

        public static void SetItemHorizontalAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(ItemHorizontalAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemHorizontalAlignmentProperty =
            DependencyProperty.RegisterAttached("ItemHorizontalAlignment", typeof(HorizontalAlignment), typeof(Cattach), new PropertyMetadata(default(HorizontalAlignment), OnItemHorizontalAlignmentChanged));

        public static void OnItemHorizontalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

            HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
        }

    }
}
