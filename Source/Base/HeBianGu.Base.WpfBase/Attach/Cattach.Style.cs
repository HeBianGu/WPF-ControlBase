// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static LayoutStyle GetLayoutStyle(DependencyObject obj)
        {
            return (LayoutStyle)obj.GetValue(LayoutStyleProperty);
        }

        public static void SetLayoutStyle(DependencyObject obj, LayoutStyle value)
        {
            obj.SetValue(LayoutStyleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty LayoutStyleProperty =
            DependencyProperty.RegisterAttached("LayoutStyle", typeof(LayoutStyle), typeof(Cattach), new PropertyMetadata(default(LayoutStyle), OnLayoutStyleChanged));

        public static void OnLayoutStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            LayoutStyle n = (LayoutStyle)e.NewValue;

            LayoutStyle o = (LayoutStyle)e.OldValue;
        }


        public static StyleType GetStyleType(DependencyObject obj)
        {
            return (StyleType)obj.GetValue(StyleTypeProperty);
        }

        public static void SetStyleType(DependencyObject obj, StyleType value)
        {
            obj.SetValue(StyleTypeProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty StyleTypeProperty =
            DependencyProperty.RegisterAttached("StyleType", typeof(StyleType), typeof(Cattach), new PropertyMetadata(default(StyleType), OnStyleTypeChanged));

        public static void OnStyleTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            StyleType n = (StyleType)e.NewValue;

            StyleType o = (StyleType)e.OldValue;
        }

        public static EffectType GetEffectType(DependencyObject obj)
        {
            return (EffectType)obj.GetValue(EffectTypeProperty);
        }

        public static void SetEffectType(DependencyObject obj, EffectType value)
        {
            obj.SetValue(EffectTypeProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty EffectTypeProperty =
            DependencyProperty.RegisterAttached("EffectType", typeof(EffectType), typeof(Cattach), new PropertyMetadata(default(EffectType), OnEffectTypeChanged));

        public static void OnEffectTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            EffectType n = (EffectType)e.NewValue;

            EffectType o = (EffectType)e.OldValue;
        }


        public static bool GetUseClear(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseClearProperty);
        }

        public static void SetUseClear(DependencyObject obj, bool value)
        {
            obj.SetValue(UseClearProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseClearProperty =
            DependencyProperty.RegisterAttached("UseClear", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseClearChanged));

        public static void OnUseClearChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseTitle(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseTitleProperty);
        }

        public static void SetUseTitle(DependencyObject obj, bool value)
        {
            obj.SetValue(UseTitleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseTitleProperty =
            DependencyProperty.RegisterAttached("UseTitle", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseTitleChanged));

        public static void OnUseTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseBackground(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseBackgroundProperty);
        }

        public static void SetUseBackground(DependencyObject obj, bool value)
        {
            obj.SetValue(UseBackgroundProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseBackgroundProperty =
            DependencyProperty.RegisterAttached("UseBackground", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseBackgroundChanged));

        public static void OnUseBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseBorderProperty);
        }

        public static void SetUseBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseBorderProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseBorderProperty =
            DependencyProperty.RegisterAttached("UseBorder", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseBorderChanged));

        public static void OnUseBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUseMouseOverScale(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseMouseOverScaleProperty);
        }

        public static void SetUseMouseOverScale(DependencyObject obj, bool value)
        {
            obj.SetValue(UseMouseOverScaleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseMouseOverScaleProperty =
            DependencyProperty.RegisterAttached("UseMouseOverScale", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnUseMouseOverScaleChanged));

        static public void OnUseMouseOverScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static double GetUseMouseOverScaleToValue(DependencyObject obj)
        {
            return (double)obj.GetValue(UseMouseOverScaleToValueProperty);
        }

        public static void SetUseMouseOverScaleToValue(DependencyObject obj, double value)
        {
            obj.SetValue(UseMouseOverScaleToValueProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseMouseOverScaleToValueProperty =
            DependencyProperty.RegisterAttached("UseMouseOverScaleToValue", typeof(double), typeof(Cattach), new PropertyMetadata(1.5, OnUseMouseOverScaleToValueChanged));

        static public void OnUseMouseOverScaleToValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }



        public static bool GetUseMouseOverScale_1_5(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseMouseOverScale_1_5Property);
        }

        public static void SetUseMouseOverScale_1_5(DependencyObject obj, bool value)
        {
            obj.SetValue(UseMouseOverScale_1_5Property, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseMouseOverScale_1_5Property =
            DependencyProperty.RegisterAttached("UseMouseOverScale_1_5", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseMouseOverScale_1_5Changed));

        static public void OnUseMouseOverScale_1_5Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }



        public static bool GetUseMouseOverOpacity(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseMouseOverOpacityProperty);
        }

        public static void SetUseMouseOverOpacity(DependencyObject obj, bool value)
        {
            obj.SetValue(UseMouseOverOpacityProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UseMouseOverOpacityProperty =
            DependencyProperty.RegisterAttached("UseMouseOverOpacity", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnUseMouseOverOpacityChanged));

        static public void OnUseMouseOverOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetUsePressScale(DependencyObject obj)
        {
            return (bool)obj.GetValue(UsePressScaleProperty);
        }

        public static void SetUsePressScale(DependencyObject obj, bool value)
        {
            obj.SetValue(UsePressScaleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty UsePressScaleProperty =
            DependencyProperty.RegisterAttached("UsePressScale", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnUsePressScaleChanged));

        static public void OnUsePressScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            "IsFocused", typeof(bool), typeof(Cattach), new FrameworkPropertyMetadata(false, (l, k) =>
            {
                //if (k.NewValue == k.NewValue) return;

                if (l is UIElement element)
                {
                    element?.Focus();
                }
            }
            ));

        public static bool GetIsFocused(DependencyObject d)
        {
            return (bool)d.GetValue(DoubleAttachProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(DoubleAttachProperty, value);
        }

        public static bool GetSelectAllText(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllTextProperty);
        }
        public static void SetSelectAllText(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllTextProperty, value);
        }
        public static readonly DependencyProperty SelectAllTextProperty =
            DependencyProperty.RegisterAttached("SelectAllText", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnSelectAllTextChanged));

        static public void OnSelectAllTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;

            if (n)
            {
                if (control is TextBoxBase element)
                {
                    element?.SelectAll();
                    Cattach.SetSelectAllText(element, false);
                }
            }
        }

        public static bool GetIsKeyboardFocusWithin(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsKeyboardFocusWithinProperty);
        }

        public static void SetIsKeyboardFocusWithin(DependencyObject obj, bool value)
        {
            obj.SetValue(IsKeyboardFocusWithinProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsKeyboardFocusWithinProperty =
            DependencyProperty.RegisterAttached("IsKeyboardFocusWithin", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnIsKeyboardFocusWithinChanged));

        static public void OnIsKeyboardFocusWithinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetIsMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseOverProperty);
        }

        public static void SetIsMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseOverProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached("IsMouseOver", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnIsMouseOverChanged));

        static public void OnIsMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetIsReadOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsReadOnlyProperty);
        }

        public static void SetIsReadOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsReadOnlyProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnIsReadOnlyChanged));

        static public void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetHasError(DependencyObject obj)
        {
            return (bool)obj.GetValue(HasErrorProperty);
        }

        public static void SetHasError(DependencyObject obj, bool value)
        {
            obj.SetValue(HasErrorProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty HasErrorProperty =
            DependencyProperty.RegisterAttached("HasError", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnHasErrorChanged));

        static public void OnHasErrorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetIsPressed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPressedProperty);
        }

        public static void SetIsPressed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPressedProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.RegisterAttached("IsPressed", typeof(bool), typeof(Cattach), new PropertyMetadata(default(bool), OnIsPressedChanged));

        static public void OnIsPressedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


    }

    public enum LayoutStyle
    {
        Normal = 0,
        Circle,
        UnderLine
    }

    public enum EffectType
    {
        None, Default1, Default2, Default3, Default4, Default5
    }

    public enum StyleType
    {
        Default = 0, Light, Single, Accent, Clear, Reverse
    }

    public enum MouseOverStyleType
    {
        None = 0, Default, Accent, Reverse, Single
    }
}


