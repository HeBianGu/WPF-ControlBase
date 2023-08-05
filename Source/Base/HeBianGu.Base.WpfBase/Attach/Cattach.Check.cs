// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {

        public static bool? GetIsChecked(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(Cattach), new PropertyMetadata(default(bool?), OnIsCheckedChanged));

        static public void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool? n = (bool?)e.NewValue;

            bool? o = (bool?)e.OldValue;
        }


        public static string GetCheckedIcon(DependencyObject obj)
        {
            return (string)obj.GetValue(CheckedIconProperty);
        }

        public static void SetCheckedIcon(DependencyObject obj, string value)
        {
            obj.SetValue(CheckedIconProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedIconProperty =
            DependencyProperty.RegisterAttached("CheckedIcon", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnCheckedIconChanged));

        static public void OnCheckedIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        public static string GetUncheckedIcon(DependencyObject obj)
        {
            return (string)obj.GetValue(UncheckedIconProperty);
        }

        public static void SetUncheckedIcon(DependencyObject obj, string value)
        {
            obj.SetValue(UncheckedIconProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UncheckedIconProperty =
            DependencyProperty.RegisterAttached("UncheckedIcon", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnUncheckedIconChanged));

        static public void OnUncheckedIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        //public static string GetChecknullIcon(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(ChecknullIconProperty);
        //}

        //public static void SetChecknullIcon(DependencyObject obj, string value)
        //{
        //    obj.SetValue(ChecknullIconProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullIconProperty =
        //    DependencyProperty.RegisterAttached("ChecknullIcon", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnChecknullIconChanged));

        //static public void OnChecknullIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    string n = (string)e.NewValue;

        //    string o = (string)e.OldValue;
        //}


        public static string GetCheckedText(DependencyObject obj)
        {
            return (string)obj.GetValue(CheckedTextProperty);
        }

        public static void SetCheckedText(DependencyObject obj, string value)
        {
            obj.SetValue(CheckedTextProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedTextProperty =
            DependencyProperty.RegisterAttached("CheckedText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnCheckedTextChanged));

        static public void OnCheckedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        public static string GetUncheckedText(DependencyObject obj)
        {
            return (string)obj.GetValue(UncheckedTextProperty);
        }

        public static void SetUncheckedText(DependencyObject obj, string value)
        {
            obj.SetValue(UncheckedTextProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UncheckedTextProperty =
            DependencyProperty.RegisterAttached("UncheckedText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnUncheckedTextChanged));

        static public void OnUncheckedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            string n = (string)e.NewValue;

            string o = (string)e.OldValue;
        }


        //public static string GetChecknullText(DependencyObject obj)
        //{
        //    return (string)obj.GetValue(ChecknullTextProperty);
        //}

        //public static void SetChecknullText(DependencyObject obj, string value)
        //{
        //    obj.SetValue(ChecknullTextProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullTextProperty =
        //    DependencyProperty.RegisterAttached("ChecknullText", typeof(string), typeof(Cattach), new PropertyMetadata(default(string), OnChecknullTextChanged));

        //static public void OnChecknullTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    string n = (string)e.NewValue;

        //    string o = (string)e.OldValue;
        //}




        public static Brush GetCheckedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedForegroundProperty);
        }

        public static void SetCheckedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedForegroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedForegroundProperty =
            DependencyProperty.RegisterAttached("CheckedForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedForegroundChanged));

        static public void OnCheckedForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }

        public static Brush GetUncheckForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(UncheckForegroundProperty);
        }

        public static void SetUncheckForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(UncheckForegroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UncheckForegroundProperty =
            DependencyProperty.RegisterAttached("UncheckForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUncheckForegroundChanged));

        static public void OnUncheckForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        //public static Brush GetChecknullForeground(DependencyObject obj)
        //{
        //    return (Brush)obj.GetValue(ChecknullForegroundProperty);
        //}

        //public static void SetChecknullForeground(DependencyObject obj, Brush value)
        //{
        //    obj.SetValue(ChecknullForegroundProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullForegroundProperty =
        //    DependencyProperty.RegisterAttached("ChecknullForeground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnChecknullForegroundChanged));

        //static public void OnChecknullForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    Brush n = (Brush)e.NewValue;

        //    Brush o = (Brush)e.OldValue;
        //}


        public static Brush GetCheckedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBackgroundProperty);
        }

        public static void SetCheckedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBackgroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.RegisterAttached("CheckedBackground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedBackgroundChanged));

        static public void OnCheckedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetUncheckedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(UncheckedBackgroundProperty);
        }

        public static void SetUncheckedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(UncheckedBackgroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UncheckedBackgroundProperty =
            DependencyProperty.RegisterAttached("UncheckedBackground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUncheckedBackgroundChanged));

        static public void OnUncheckedBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        //public static Brush GetChecknullBackground(DependencyObject obj)
        //{
        //    return (Brush)obj.GetValue(ChecknullBackgroundProperty);
        //}

        //public static void SetChecknullBackground(DependencyObject obj, Brush value)
        //{
        //    obj.SetValue(ChecknullBackgroundProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullBackgroundProperty =
        //    DependencyProperty.RegisterAttached("ChecknullBackground", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnChecknullBackgroundChanged));

        //static public void OnChecknullBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    Brush n = (Brush)e.NewValue;

        //    Brush o = (Brush)e.OldValue;
        //}


        public static Brush GetCheckedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(CheckedBorderBrushProperty);
        }

        public static void SetCheckedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(CheckedBorderBrushProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedBorderBrushProperty =
            DependencyProperty.RegisterAttached("CheckedBorderBrush", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnCheckedBorderBrushChanged));

        static public void OnCheckedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        public static Brush GetUnCheckedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(UnCheckedBorderBrushProperty);
        }

        public static void SetUnCheckedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(UnCheckedBorderBrushProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UnCheckedBorderBrushProperty =
            DependencyProperty.RegisterAttached("UnCheckedBorderBrush", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnUnCheckedBorderBrushChanged));

        static public void OnUnCheckedBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }


        //public static Brush GetChecknullBorderBrush(DependencyObject obj)
        //{
        //    return (Brush)obj.GetValue(ChecknullBorderBrushProperty);
        //}

        //public static void SetChecknullBorderBrush(DependencyObject obj, Brush value)
        //{
        //    obj.SetValue(ChecknullBorderBrushProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullBorderBrushProperty =
        //    DependencyProperty.RegisterAttached("ChecknullBorderBrush", typeof(Brush), typeof(Cattach), new PropertyMetadata(default(Brush), OnChecknullBorderBrushChanged));

        //static public void OnChecknullBorderBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    Brush n = (Brush)e.NewValue;

        //    Brush o = (Brush)e.OldValue;
        //}


        public static Thickness GetCheckedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(CheckedBorderThicknessProperty);
        }

        public static void SetCheckedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(CheckedBorderThicknessProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("CheckedBorderThickness", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnCheckedBorderThicknessChanged));

        static public void OnCheckedBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        public static Thickness GetUnCheckedBorderThickness(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(UnCheckedBorderThicknessProperty);
        }

        public static void SetUnCheckedBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(UnCheckedBorderThicknessProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UnCheckedBorderThicknessProperty =
            DependencyProperty.RegisterAttached("UnCheckedBorderThickness", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnUnCheckedBorderThicknessChanged));

        static public void OnUnCheckedBorderThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Thickness n = (Thickness)e.NewValue;

            Thickness o = (Thickness)e.OldValue;
        }


        //public static Thickness GetChecknullThickness(DependencyObject obj)
        //{
        //    return (Thickness)obj.GetValue(ChecknullThicknessProperty);
        //}

        //public static void SetChecknullThickness(DependencyObject obj, Thickness value)
        //{
        //    obj.SetValue(ChecknullThicknessProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullThicknessProperty =
        //    DependencyProperty.RegisterAttached("ChecknullThickness", typeof(Thickness), typeof(Cattach), new PropertyMetadata(default(Thickness), OnChecknullThicknessChanged));

        //static public void OnChecknullThicknessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    Thickness n = (Thickness)e.NewValue;

        //    Thickness o = (Thickness)e.OldValue;
        //}


        public static double GetCheckedOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(CheckedOpacityProperty);
        }

        public static void SetCheckedOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(CheckedOpacityProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty CheckedOpacityProperty =
            DependencyProperty.RegisterAttached("CheckedOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnCheckedOpacityChanged));

        static public void OnCheckedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static double GetUncheckedOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(UncheckedOpacityProperty);
        }

        public static void SetUncheckedOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(UncheckedOpacityProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UncheckedOpacityProperty =
            DependencyProperty.RegisterAttached("UncheckedOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnUncheckedOpacityChanged));

        static public void OnUncheckedOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        //public static double GetChecknullOpacity(DependencyObject obj)
        //{
        //    return (double)obj.GetValue(ChecknullOpacityProperty);
        //}

        //public static void SetChecknullOpacity(DependencyObject obj, double value)
        //{
        //    obj.SetValue(ChecknullOpacityProperty, value);
        //}

        ///// <summary> 应用窗体关闭和显示 </summary>
        //public static readonly DependencyProperty ChecknullOpacityProperty =
        //    DependencyProperty.RegisterAttached("ChecknullOpacity", typeof(double), typeof(Cattach), new PropertyMetadata(1.0, OnChecknullOpacityChanged));

        //static public void OnChecknullOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    DependencyObject control = d as DependencyObject;

        //    double n = (double)e.NewValue;

        //    double o = (double)e.OldValue;
        //}

    }
}
