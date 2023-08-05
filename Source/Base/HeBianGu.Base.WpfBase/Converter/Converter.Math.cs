// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public enum MathOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class OpacityProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            parameter = parameter ?? "0.1-1";
            double param1 = double.Parse(parameter.ToString().Split('-')[0]);
            double param2 = double.Parse(parameter.ToString().Split('-')[1]);

            double v = 100 - double.Parse(value.ToString());

            return param2 - v * (param2 - param1) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DoubleRoundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            parameter = parameter ?? 0;

            int param = int.Parse(parameter.ToString());

            double v = double.Parse(value.ToString());

            return Math.Round(v, param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CircleProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double v = double.Parse(value.ToString());

            return 182 - v * (182 - 172.5) / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Number2PercentageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2) return .0;

            object obj1 = values[0];
            object obj2 = values[1];

            if (obj1 == null || obj2 == null) return .0;

            string str1 = values[0].ToString();
            string str2 = values[1].ToString();

            double v1 = double.Parse(str1);
            double v2 = double.Parse(str2);

            if (Math.Abs(v2) < 1E-06) return 100.0;

            return Math.Round(v1 / v2 * 100, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class MathMultipleConverter : IMultiValueConverter
    {
        public MathOperation Operation { get; set; }

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.Length < 2 || value[0] == null || value[1] == null) return Binding.DoNothing;

            if (!double.TryParse(value[0].ToString(), out double value1) || !double.TryParse(value[1].ToString(), out double value2))
                return 0;

            switch (Operation)
            {
                default:
                    // (case MathOperation.Add:)
                    return value1 + value2;
                case MathOperation.Divide:
                    return value1 / value2;
                case MathOperation.Multiply:
                    return value1 * value2;
                case MathOperation.Subtract:
                    return value1 - value2;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary> 乘除法转换计算 </summary>
    public class MathMultiplicationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double from = System.Convert.ToDouble(value?.ToString());
            double param = System.Convert.ToDouble(parameter?.ToString());

            return from * param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary> 加减法转换计算 </summary>
    public class MathAdditionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double from = (double)value;

            if (double.TryParse(parameter.ToString(), out double v))
            {
                return from + v;
            }
           
            return from;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class LessThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IComparable comparable && parameter is IComparable comparable1)
            {
                return comparable.CompareTo(comparable1) == -1;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IComparable comparable && parameter is IComparable comparable1)
            {
                return comparable.CompareTo(comparable1) == 1;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ComparaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IComparable comparable)
            {
                object v = System.Convert.ChangeType(parameter, value.GetType());
                return comparable.CompareTo(v);
            }

            return -9;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
