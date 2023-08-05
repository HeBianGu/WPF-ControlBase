// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {
        public static bool GetIsExceptSelf(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExceptSelfProperty);
        }

        public static void SetIsExceptSelf(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExceptSelfProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsExceptSelfProperty =
            DependencyProperty.RegisterAttached("IsExceptSelf", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnIsExceptSelfChanged));

        public static void OnIsExceptSelfChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        public static bool GetIsExcepChildren(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExcepChildrenProperty);
        }

        public static void SetIsExcepChildren(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExcepChildrenProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsExcepChildrenProperty =
            DependencyProperty.RegisterAttached("IsExcepChildren", typeof(bool), typeof(Cattach), new PropertyMetadata(false, OnIsExcepChildrenChanged));

        public static void OnIsExcepChildrenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


    }
}
