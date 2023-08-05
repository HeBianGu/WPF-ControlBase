// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static partial class Cattach
    {

        public static double GetFromDouble(DependencyObject obj)
        {
            return (double)obj.GetValue(FromDoubleProperty);
        }

        public static void SetFromDouble(DependencyObject obj, double value)
        {
            obj.SetValue(FromDoubleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty FromDoubleProperty =
            DependencyProperty.RegisterAttached("FromDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnFromDoubleChanged));

        static public void OnFromDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static double GetToDouble(DependencyObject obj)
        {
            return (double)obj.GetValue(ToDoubleProperty);
        }

        public static void SetToDouble(DependencyObject obj, double value)
        {
            obj.SetValue(ToDoubleProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty ToDoubleProperty =
            DependencyProperty.RegisterAttached("ToDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnToDoubleChanged));

        static public void OnToDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


    }
}
