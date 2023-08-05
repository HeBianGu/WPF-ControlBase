// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace HeBianGu.Base.WpfBase
{
    public class SecondsToTimeSpanDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            long l = (long)System.Convert.ChangeType(value, typeof(long));
            TimeSpan t = TimeSpan.FromSeconds(l);

            StringBuilder sb = new StringBuilder();

            if (t.TotalDays > 0)
            {
                sb.Append($"{t.Days}天 ");
            }

            if (t.TotalHours > 0)
            {
                sb.Append($"{t.Hours}小时 ");
            }

            if (t.TotalMinutes > 0)
            {
                sb.Append($"{t.Minutes}分 ");
            }

            if (t.TotalSeconds > 0)
            {
                sb.Append($"{t.Seconds}秒");
            }

            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TicksToTimeSpanDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null; 
            double d = double.Parse(value.ToString()); 
            TimeSpan sp = TimeSpan.FromTicks((long)d); 
            return sp.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MillisecondsTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            double d = double.Parse(value.ToString());

            TimeSpan sp = TimeSpan.FromMilliseconds((long)d);

            return sp.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanSplitPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            return value.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            TimeSpan span = DateTime.Now - ((DateTime)value);

            return (int)(span.TotalDays / 365.0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            return ((DateTime)value).ToString(parameter?.ToString() ?? "yyyy-MM-dd HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
