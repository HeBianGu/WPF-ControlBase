// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static readonly DependencyProperty SelectForegroundProperty = DependencyProperty.RegisterAttached(
            "SelectForeground", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetSelectForeground(DependencyObject element, Brush value)
        {
            element.SetValue(SelectForegroundProperty, value);
        }

        public static Brush SelectForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(SelectForegroundProperty);
        }

        public static Brush GetSelectBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(SelectBorderBrushProperty);
        }

        public static void SetSelectBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(SelectBorderBrushProperty, value);
        }

        public static readonly DependencyProperty SelectBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectBorderBrush", typeof(Brush), typeof(Cattach), new FrameworkPropertyMetadata(default(Brush), OnSelectBorderBrushChanged));

        public static void OnSelectBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

        public static Thickness GetSelectBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(SelectBorderThicknessProperty);
        }

        public static void SetSelectBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(SelectBorderThicknessProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SelectBorderThicknessProperty =
            DependencyProperty.RegisterAttached("SelectBorderThickness", typeof(Thickness), typeof(Cattach), new FrameworkPropertyMetadata(default(Thickness), OnSelectBorderThicknessChanged));

        public static void OnSelectBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }

        public static Effect GetSelectEffect(DependencyObject obj)
        {
            return (Effect)obj.GetValue(SelectEffectProperty);
        }

        public static void SetSelectEffect(DependencyObject obj, Effect value)
        {
            obj.SetValue(SelectEffectProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty SelectEffectProperty =
            DependencyProperty.RegisterAttached("SelectEffect", typeof(Effect), typeof(Cattach), new FrameworkPropertyMetadata(default(Effect), FrameworkPropertyMetadataOptions.Inherits, OnSelectEffectChanged));

        public static void OnSelectEffectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            Effect n = (Effect)e.NewValue;

            Effect o = (Effect)e.OldValue;
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsSelectedChanged));

        public static bool GetIsSelected(DependencyObject d)
        {
            return (bool)d.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        private static void OnIsSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        public static readonly DependencyProperty SelectedTextProperty = DependencyProperty.RegisterAttached("SelectedText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedTextChanged));

        public static string GetSelectedText(DependencyObject d)
        {
            return (string)d.GetValue(SelectedTextProperty);
        }

        public static void SetSelectedText(DependencyObject obj, string value)
        {
            obj.SetValue(SelectedTextProperty, value);
        }

        private static void OnSelectedTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        //public static readonly DependencyProperty UnSelectedTextProperty = DependencyProperty.RegisterAttached(
        //    "UnSelectedText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnUnSelectedTextChanged));

        //public static string GetUnSelectedText(DependencyObject d)
        //{
        //    return (string)d.GetValue(UnSelectedTextProperty);
        //}

        //public static void SetUnSelectedText(DependencyObject obj, string value)
        //{
        //    obj.SetValue(UnSelectedTextProperty, value);
        //}

        //private static void OnUnSelectedTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        //{

        //}


        //public static string GetNoneSelectedText(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(NoneSelectedTextProperty);
        //}

        //public static void SetNoneSelectedText(DependencyObject obj, string value)
        //{
        //    obj.SetValue(NoneSelectedTextProperty, value);
        //}

        //public static readonly DependencyProperty NoneSelectedTextProperty =
        //    DependencyProperty.RegisterAttached("NoneSelectedText", typeof(string), typeof(Cattach), new FrameworkPropertyMetadata(default(string), OnNoneSelectedTextChanged));

        //public static void OnNoneSelectedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d;

        //    string n = (string)e.NewValue;

        //    string o = (string)e.OldValue;
        //}

        public static double GetSelectedOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(SelectedOpacityProperty);
        }

        public static void SetSelectedOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(SelectedOpacityProperty, value);
        }

        public static readonly DependencyProperty SelectedOpacityProperty =
            DependencyProperty.RegisterAttached("SelectedOpacity", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(1.0, OnSelectedOpacityChanged));

        public static void OnSelectedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }

        //public static double GetUnSelectedOpacity(DependencyObject obj)
        //{
        //    return (double)obj.GetValue(UnSelectedOpacityProperty);
        //}

        //public static void SetUnSelectedOpacity(DependencyObject obj, double value)
        //{
        //    obj.SetValue(UnSelectedOpacityProperty, value);
        //}

        //public static readonly DependencyProperty UnSelectedOpacityProperty =
        //    DependencyProperty.RegisterAttached("UnSelectedOpacity", typeof(double), typeof(Cattach), new FrameworkPropertyMetadata(0.8, OnUnSelectedOpacityChanged));

        //public static void OnUnSelectedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d;

        //    double n = (double)e.NewValue;

        //    double o = (double)e.OldValue;
        //}
    }
}
