// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        [TypeConverter(typeof(LengthConverter))]
        public static double GetItemHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemHeightProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
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


        public static VerticalAlignment GetItemVerticalAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(ItemVerticalAlignmentProperty);
        }

        public static void SetItemVerticalAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(ItemVerticalAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("ItemVerticalAlignment", typeof(VerticalAlignment), typeof(Cattach), new PropertyMetadata(default(VerticalAlignment), OnItemVerticalAlignmentChanged));

        static public void OnItemVerticalAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            VerticalAlignment n = (VerticalAlignment)e.NewValue;

            VerticalAlignment o = (VerticalAlignment)e.OldValue;
        }



        [TypeConverter(typeof(LengthConverter))]
        public static double GetItemMinHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemMinHeightProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetItemMinHeight(DependencyObject obj, double value)
        {
            obj.SetValue(ItemMinHeightProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemMinHeightProperty =
            DependencyProperty.RegisterAttached("ItemMinHeight", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnItemMinHeightChanged));

        static public void OnItemMinHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        [TypeConverter(typeof(LengthConverter))]
        public static double GetItemMinWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemMinWidthProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetItemMinWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ItemMinWidthProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemMinWidthProperty =
            DependencyProperty.RegisterAttached("ItemMinWidth", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnItemMinWidthChanged));

        static public void OnItemMinWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static Thickness GetItemMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ItemMarginProperty);
        }

        public static void SetItemMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemMarginProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemMarginProperty =
            DependencyProperty.RegisterAttached("ItemMargin", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnItemMarginChanged));

        static public void OnItemMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static Thickness GetItemPadding(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(ItemPaddingProperty);
        }

        public static void SetItemPadding(DependencyObject obj, Thickness value)
        {
            obj.SetValue(ItemPaddingProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemPaddingProperty =
            DependencyProperty.RegisterAttached("ItemPadding", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnItemPaddingChanged));

        static public void OnItemPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static HorizontalAlignment GetItemHorizontalContentAlignment(DependencyObject obj)
        {
            return (HorizontalAlignment)obj.GetValue(ItemHorizontalContentAlignmentProperty);
        }

        public static void SetItemHorizontalContentAlignment(DependencyObject obj, HorizontalAlignment value)
        {
            obj.SetValue(ItemHorizontalContentAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemHorizontalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("ItemHorizontalContentAlignment", typeof(HorizontalAlignment), typeof(Cattach), new PropertyMetadata(default(HorizontalAlignment), OnItemHorizontalContentAlignmentChanged));

        static public void OnItemHorizontalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            HorizontalAlignment n = (HorizontalAlignment)e.NewValue;

            HorizontalAlignment o = (HorizontalAlignment)e.OldValue;
        }


        public static VerticalAlignment GetItemVerticalContentAlignment(DependencyObject obj)
        {
            return (VerticalAlignment)obj.GetValue(ItemVerticalContentAlignmentProperty);
        }

        public static void SetItemVerticalContentAlignment(DependencyObject obj, VerticalAlignment value)
        {
            obj.SetValue(ItemVerticalContentAlignmentProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemVerticalContentAlignmentProperty =
            DependencyProperty.RegisterAttached("ItemVerticalContentAlignment", typeof(VerticalAlignment), typeof(Cattach), new PropertyMetadata(default(VerticalAlignment), OnItemVerticalContentAlignmentChanged));

        static public void OnItemVerticalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            VerticalAlignment n = (VerticalAlignment)e.NewValue;

            VerticalAlignment o = (VerticalAlignment)e.OldValue;
        }

        public static ControlTemplate GetItemOverTemplate(DependencyObject obj)
        {
            return (ControlTemplate)obj.GetValue(ItemOverTemplateProperty);
        }

        public static void SetItemOverTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(ItemOverTemplateProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemOverTemplateProperty =
            DependencyProperty.RegisterAttached("ItemOverTemplate", typeof(ControlTemplate), typeof(Cattach), new PropertyMetadata(default(ControlTemplate), OnItemOverTemplateChanged));

        static public void OnItemOverTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            ControlTemplate n = (ControlTemplate)e.NewValue;

            ControlTemplate o = (ControlTemplate)e.OldValue;
        }

        [TypeConverter(typeof(LengthConverter))]
        public static double GetItemsContianerWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(ItemsContianerWidthProperty);
        }
        [TypeConverter(typeof(LengthConverter))]
        public static void SetItemsContianerWidth(DependencyObject obj, double value)
        {
            obj.SetValue(ItemsContianerWidthProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ItemsContianerWidthProperty =
            DependencyProperty.RegisterAttached("ItemsContianerWidth", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnItemsContianerWidthChanged));

        static public void OnItemsContianerWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static DataTemplate GetSelectedItemTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(SelectedItemTemplateProperty);
        }

        public static void SetSelectedItemTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(SelectedItemTemplateProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.RegisterAttached("SelectedItemTemplate", typeof(DataTemplate), typeof(Cattach), new PropertyMetadata(default(DataTemplate), OnSelectedItemTemplateChanged));

        static public void OnSelectedItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            DataTemplate n = (DataTemplate)e.NewValue;

            DataTemplate o = (DataTemplate)e.OldValue;
        }

    }
}
