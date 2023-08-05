using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.Converter
{
    public class GetDateTimeFormatStringConverter : MarkupValueConverterBase
    {
        public string Format { get; set; } = "yyyy-MM-dd HH:mm:ss";

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToLocalTime().ToString(this.Format, culture);
            }

            return this.DefaultValue;
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

    public class GetTimeSpanFromMethodConverter : MarkupValueConverterBase
    {
        public string MethodName { get; set; } = "FromSeconds";
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return this.DefaultValue;
            var method = typeof(TimeSpan).GetMethod(this.MethodName);
            var param = method.GetParameters().FirstOrDefault();
            var p = System.Convert.ChangeType(value, param.ParameterType);
            TimeSpan result = (TimeSpan)method.Invoke(null, new object[] { p }); 
            return result;
        }
    }
}
