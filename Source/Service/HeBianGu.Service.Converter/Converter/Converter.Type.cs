// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Linq;

namespace HeBianGu.Service.Converter
{
    public class GetTypeConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            return value.GetType();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetAttributeConverter : MarkupValueConverterBase
    {
        public Type AttributeType { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var result = value.GetType().GetCustomAttributes(AttributeType, false)?.FirstOrDefault();
            return result == null ? this.DefaultValue : result;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GetPropertyValueConverter : MarkupValueConverterBase
    {
        public string PropertyName { get; set; }
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return value.GetType().GetProperty(this.PropertyName)?.GetValue(value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GetPropertyInfoValueConverter : MarkupValueConverterBase
    {
        public string PropertyName { get; set; }
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return value.GetType().GetProperty(this.PropertyName);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GetMethodInfoValueConverter : MarkupValueConverterBase
    {
        public string MethodName { get; set; }
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return value.GetType().GetMethod(this.MethodName);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class CallMethodValueConverter : MarkupValueConverterBase
    {
        public string MethodName { get; set; }
        public object Paremeter1 { get; set; }
        public object Paremeter2 { get; set; }
        public object Paremeter3 { get; set; }
        public object Paremeter4 { get; set; }
        public object Paremeter5 { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            var method = value.GetType().GetMethod(this.MethodName);

            var ps = method.GetParameters();

            if (ps.Length == 0)
                return method.Invoke(value, null);
            if (ps.Length == 1)
                return method.Invoke(value, new object[] { this.Paremeter1 });
            if (ps.Length == 2)
                return method.Invoke(value, new object[] { this.Paremeter1, this.Paremeter2 });
            if (ps.Length == 3)
                return method.Invoke(value, new object[] { this.Paremeter1, this.Paremeter2, this.Paremeter3 });
            if (ps.Length == 4)
                return method.Invoke(value, new object[] { this.Paremeter1, this.Paremeter2, this.Paremeter3, this.Paremeter4 });
            if (ps.Length == 5)
                return method.Invoke(value, new object[] { this.Paremeter1, this.Paremeter2, this.Paremeter3, this.Paremeter4, this.Paremeter5 });

            throw new NotImplementedException();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
