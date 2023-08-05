// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Service.Converter.Converter
{
    public class GetKeyValueConverter<T> : MarkupValueConverterBase where T : IComparable
    {
        public T Key1 { get; set; }
        public object Value1 { get; set; }

        public T Key2 { get; set; }
        public object Value2 { get; set; }

        public T Key3 { get; set; }
        public object Value3 { get; set; }

        public T Key4 { get; set; }
        public object Value4 { get; set; }

        public T Key5 { get; set; }
        public object Value5 { get; set; }

        public T Key6 { get; set; }
        public object Value6 { get; set; }

        public T Key7 { get; set; }
        public object Value7 { get; set; }

        public T Key8 { get; set; }
        public object Value8 { get; set; }

        public T Key9 { get; set; }
        public object Value9 { get; set; }

        public T Key10 { get; set; }
        public object Value10 { get; set; }

        public object DefaultValue { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IComparable comparable)
            {
                if (comparable.CompareTo(this.Key1) == 0)
                    return Value1;
                if (comparable.CompareTo(this.Key2) == 0)
                    return Value2;
                if (comparable.CompareTo(this.Key3) == 0)
                    return Value3;
                if (comparable.CompareTo(this.Key4) == 0)
                    return Value4;
                if (comparable.CompareTo(this.Key5) == 0)
                    return Value5;
                if (comparable.CompareTo(this.Key6) == 0)
                    return Value6;
                if (comparable.CompareTo(this.Key7) == 0)
                    return Value7;
                if (comparable.CompareTo(this.Key8) == 0)
                    return Value8;
                if (comparable.CompareTo(this.Key9) == 0)
                    return Value9;
                if (comparable.CompareTo(this.Key10) == 0)
                    return Value10;
            }
            return this.DefaultValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GetConvertibleKeyValueConverter<T> : GetKeyValueConverter<T> where T : IComparable, IConvertible
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IConvertible))
            {
                return this.DefaultValue;
            }

            if (!(value is IComparable))
            {
                return this.DefaultValue;
            }

            T current = default(T);

            try
            {
                current = (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return this.DefaultValue;
            }

            if (current.CompareTo(this.Key1) == 0)
                return Value1;
            if (current.CompareTo(this.Key2) == 0)
                return Value2;
            if (current.CompareTo(this.Key3) == 0)
                return Value3;
            if (current.CompareTo(this.Key4) == 0)
                return Value4;
            if (current.CompareTo(this.Key5) == 0)
                return Value5;
            if (current.CompareTo(this.Key6) == 0)
                return Value6;
            if (current.CompareTo(this.Key7) == 0)
                return Value7;
            if (current.CompareTo(this.Key8) == 0)
                return Value8;
            if (current.CompareTo(this.Key9) == 0)
                return Value9;
            if (current.CompareTo(this.Key10) == 0)
                return Value10;

            return this.DefaultValue;
        }

    }


    public class GetStringKeyValueConverter : GetConvertibleKeyValueConverter<string>
    {

    }


    public class GetKeyRangeConverter<T> : GetKeyValueConverter<T> where T : IComparable
    {
        public object MaxValue { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IComparable comparable)
            {
                if (comparable.CompareTo(this.Key1) == -1)
                    return Value1;
                if (comparable.CompareTo(this.Key2) == -1)
                    return Value2;
                if (comparable.CompareTo(this.Key3) == -1)
                    return Value3;
                if (comparable.CompareTo(this.Key4) == -1)
                    return Value4;
                if (comparable.CompareTo(this.Key5) == -1)
                    return Value5;
                if (comparable.CompareTo(this.Key6) == -1)
                    return Value6;
                if (comparable.CompareTo(this.Key7) == -1)
                    return Value7;
                if (comparable.CompareTo(this.Key8) == -1)
                    return Value8;
                if (comparable.CompareTo(this.Key9) == -1)
                    return Value9;
                if (comparable.CompareTo(this.Key10) == -1)
                    return Value10;

                return this.MaxValue;
            }

            return this.DefaultValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class GetConvertibleKeyRangeConverter<T> : GetKeyRangeConverter<T> where T : IComparable, IConvertible
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IConvertible))
            {
                return this.DefaultValue;
            }

            if (!(value is IComparable))
            {
                return this.DefaultValue;
            }

            T current = default(T);

            try
            {
                current = (T)System.Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return this.DefaultValue;
            }

            if (current.CompareTo(this.Key1) == -1)
                return Value1;
            if (current.CompareTo(this.Key2) == -1)
                return Value2;
            if (current.CompareTo(this.Key3) == -1)
                return Value3;
            if (current.CompareTo(this.Key4) == -1)
                return Value4;
            if (current.CompareTo(this.Key5) == -1)
                return Value5;
            if (current.CompareTo(this.Key6) == -1)
                return Value6;
            if (current.CompareTo(this.Key7) == -1)
                return Value7;
            if (current.CompareTo(this.Key8) == -1)
                return Value8;
            if (current.CompareTo(this.Key9) == -1)
                return Value9;
            if (current.CompareTo(this.Key10) == -1)
                return Value10;
            return this.MaxValue;
        }

    }

    public class GetIntKeyRangeConverter : GetConvertibleKeyRangeConverter<int>
    {

    }
}
