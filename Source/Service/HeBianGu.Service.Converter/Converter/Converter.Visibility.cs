// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Service.Converter
{
    public class GetVisibilityValueConverter : MarkupValueConverterBase
    { 
        public object VisibleValue { get; set; }
        public object HiddenValue { get; set; }
        public object CollapsedValue { get; set; }
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(VisibleValue)) return Visibility.Visible;
            if (value.Equals(HiddenValue)) return Visibility.Hidden; 
            if (value.Equals(CollapsedValue)) return Visibility.Collapsed; 
            return DefaultValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
