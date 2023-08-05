// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.Converter
{
    public class StringValueConverter : MarkupValueConverterBase
    {
        public string NullValue { get; set; } = null;
        public string EmptyValue { get; set; } = string.Empty;
        public string SpaceValue { get; set; } = " ";

        public string Key { get; set; }
        public string Value { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return this.NullValue;
            if (value.ToString() == string.Empty) return this.NullValue;
            if (value.ToString() == " ") return this.SpaceValue;
            if (value.ToString() == Key) return this.Value;
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GetPasswordValueConverter : MarkupValueConverterBase
    {
        public int StartIndex { get; set; }
        public int Count { get; set; }
        public string Key { get; set; } = "*";
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString())) return null;
            return Enumerable.Repeat(this.Key, value.ToString().Count()).Aggregate((x, c) => x + c);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

}
