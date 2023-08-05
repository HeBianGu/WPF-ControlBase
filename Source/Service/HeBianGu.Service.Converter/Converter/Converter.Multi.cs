using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HeBianGu.Service.Converter
{ 
    public class GetMultiValueConverter : MarkupValueConverterBase
    {
        public IValueConverter Value1 { get; set; }
        public IValueConverter Value2 { get; set; }
        public IValueConverter Value3 { get; set; }
        public IValueConverter Value4 { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object result = value;

            if (this.Value1 != null)
                result = Value1.Convert(result, targetType, parameter, culture);
            if (this.Value2 != null)
                result = Value2.Convert(result, targetType, parameter, culture);

            if (this.Value3 != null)
                result = Value3.Convert(result, targetType, parameter, culture);

            if (this.Value4 != null)
                result = Value4.Convert(result, targetType, parameter, culture);

            return result;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
