// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

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

    }

    public enum LayoutStyle
    {
        Normal = 0, Circle, UnderLine
    }

    public enum EffectType
    {
        None, Default1, Default2, Default3, Default4, Default5
    }

    public enum StyleType
    {
        Default = 0, Single, Accent, Clear, Reverse
    }

    public enum MouseOverStyleType
    {
        None = 0, Default, Accent, Reverse, Single
    }
}
