// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;
using System.Windows.Data;

namespace HeBianGu.Control.PropertyGrid
{
    internal class DoubleArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            if (value is double[] ds)
            {
                return string.Join(",", ds);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            System.Collections.Generic.IEnumerable<double> result = value?.ToString().Split(',')?.Select(l =>
            {
                if (double.TryParse(l, out double d))
                {
                    return d;
                }
                else
                {
                    return double.NaN;
                }
            });

            return result.ToArray();

        }


    }
}
