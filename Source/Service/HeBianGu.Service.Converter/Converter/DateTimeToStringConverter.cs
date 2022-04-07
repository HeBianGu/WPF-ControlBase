// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;

namespace HeBianGu.Service.Converter
{
    /// <summary>
    /// 根据表达式字符串计算转换
    /// </summary>
    public class DateTimeToStringConverter : Convert<DateTimeToStringConverter>
    {
        public string Format { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString(this.Format, culture);
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is DateTime dateTime)
                {
                    return dateTime;
                }
                if (value is string str)
                {
                    DateTime.TryParse(str, out DateTime resultDateTime);
                    return resultDateTime;
                }
            }
            return null;
        }
    }
}
