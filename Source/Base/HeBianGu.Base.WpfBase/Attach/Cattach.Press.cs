// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static readonly DependencyProperty PressBackgroundProperty = DependencyProperty.RegisterAttached(
            "PressBackground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetPressBackground(DependencyObject element, Brush value)
        {
            element.SetValue(PressBackgroundProperty, value);
        }

        public static Brush PressBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressBackgroundProperty);
        }
        public static readonly DependencyProperty PressForegroundProperty = DependencyProperty.RegisterAttached(
            "PressForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null));

        public static void SetPressForeground(DependencyObject element, Brush value)
        {
            element.SetValue(PressForegroundProperty, value);
        }

        public static Brush PressForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressForegroundProperty);
        }

        public static readonly DependencyProperty PressBorderBrushProperty = DependencyProperty.RegisterAttached(
            "PressBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPressBorderBrushChanged));

        public static Brush GetPressBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(PressBorderBrushProperty);
        }

        public static void SetPressBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(PressBorderBrushProperty, value);
        }

        private static void OnPressBorderBrushChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }


        public static Thickness GetPressBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(PressBorderThicknessProperty);
        }

        public static void SetPressBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(PressBorderThicknessProperty, value);
        }

        public static readonly DependencyProperty PressBorderThicknessProperty =
            DependencyProperty.RegisterAttached("PressBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), OnPressBorderThicknessChanged));

        public static void OnPressBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


    }
}
