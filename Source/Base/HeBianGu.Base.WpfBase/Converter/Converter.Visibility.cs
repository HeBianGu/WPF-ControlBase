// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 匹配文本本不可用 </summary>
    public class VisibilityContainWithStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            IList collection = parameter as IList;

            if (collection.Contains(value.ToString().Trim()))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }



    /// <summary> 匹配文本本不可用 </summary>
    public class VisibilityContainWithOutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            IList collection = parameter as IList;

            if (collection.Contains(value.ToString().Trim()))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 布尔为true转不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class FalseToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (Visibility)value == Visibility.Collapsed;
        }
    }

    /// <summary> 布尔转不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (Visibility)value == Visibility.Visible;
        }
    }

    /// <summary> 布尔为true转不可用 </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityToBoolenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //将bool值转换为什么呢？自己在这里定义
            return (Visibility)value == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //反转换方法，就是对照上面的把男女再转换回去
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    /// <summary> 空文本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;

            if (string.IsNullOrEmpty(value.ToString().Trim())) return Visibility.Hidden;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 匹配文本本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            if (value.ToString().Trim() != parameter.ToString().Trim()) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary> 匹配文本本不可用 </summary>
    [ValueConversion(typeof(Visibility), typeof(string))]
    public class VisibilityWithOutStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            if (parameter == null) return Visibility.Visible;

            if (value.ToString().Trim() == parameter.ToString().Trim()) return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }

    [ValueConversion(typeof(Visibility), typeof(string))]
    public class NullToCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;

            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new Exception();
        }
    }
}
