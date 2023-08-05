// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public class IntToSexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return (int)value == 0 ? "女" : "男";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 设置文本框PropertyChanged 可以输入小数点 </summary>
    public class DoubleTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();

            if (str.EndsWith(".")) return ".";

            if (str.Contains(".") && str.EndsWith("0")) return ".";

            return value;
        }
    }

    /// <summary> 替换字符串 </summary>
    public class IsEqualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            bool result = value.Equals(parameter);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter;
            }

            return null;
        }
    }

    /// <summary> 分割字符串取第一个值 </summary>
    public class StringSplitFirstConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null) return value.ToString().Split(' ')[0];

            return value.ToString().Split(parameter.ToString().ToCharArray())[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary> 替换字符串 </summary>
    public class StringReplaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            if (parameter == null) return value;

            return value.ToString().Replace(value.ToString().Split(' ')[0], value.ToString().Split(' ')[1]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
