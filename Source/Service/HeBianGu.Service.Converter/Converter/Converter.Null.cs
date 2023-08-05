// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HeBianGu.Service.Converter
{
    public class GetNullValueConverter : MarkupValueConverterBase
    {
        public object NullValue { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return this.NullValue;

            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
