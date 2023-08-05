// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.Globalization;

namespace HeBianGu.Service.TypeConverter
{
    public class FileSizeDisplayTypeConverter : System.ComponentModel.TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return XConverter.ByteSizeDisplayConverter.Convert(value, null, null, null)?.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return XConverter.ByteSizeDisplayConverter.ConvertBack(value, null,null, culture);
            //return base.ConvertFrom(context, culture, value);
        }
    }
}
