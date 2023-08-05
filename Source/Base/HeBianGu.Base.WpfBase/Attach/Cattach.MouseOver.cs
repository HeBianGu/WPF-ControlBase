// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBackgroundProperty, value);
        }

        public static Brush MouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBackgroundProperty);
        }

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush MouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverForegroundProperty);
        }

        public static readonly DependencyProperty SelectBackgroundProperty = DependencyProperty.RegisterAttached(
            "SelectBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetSelectBackground(DependencyObject element, Brush value)
        {
            element.SetValue(SelectBackgroundProperty, value);
        }

        public static Brush SelectBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectBackgroundProperty);
        }


        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderBrush", typeof(Brush), typeof(Cattach),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBorderBrushProperty, value);
        }

        //[AttachedPropertyBrowsableForType(typeof(TextBox))]
        //[AttachedPropertyBrowsableForType(typeof(CheckBox))]
        //[AttachedPropertyBrowsableForType(typeof(RadioButton))]
        //[AttachedPropertyBrowsableForType(typeof(DatePicker))]
        //[AttachedPropertyBrowsableForType(typeof(ComboBox))]
        //[AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        public static Brush GetMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBorderBrushProperty);
        }


        public static Thickness GetMouseOverBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MouseOverBorderThicknessProperty);
        }

        public static void SetMouseOverBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MouseOverBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty MouseOverBorderThicknessProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(new Thickness(1), OnMouseOverBorderThicknessChanged));

        public static void OnMouseOverBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static Effect GetMouseOverEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(MouseOverEffectProperty);
        }

        public static void SetMouseOverEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(MouseOverEffectProperty, value);
        }

        public static readonly DependencyProperty MouseOverEffectProperty =
            DependencyProperty.RegisterAttached("MouseOverEffect", typeof(Effect), typeof(Cattach), new PropertyMetadata(default(Effect), OnMouseOverEffectChanged));

        public static void OnMouseOverEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Effect n = (Effect)e.NewValue;

            Effect o = (Effect)e.OldValue;
        }



    }
}
