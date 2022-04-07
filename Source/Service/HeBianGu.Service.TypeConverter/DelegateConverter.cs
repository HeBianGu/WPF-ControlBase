// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Common.Expression;
using System;
using System.ComponentModel;
using System.Globalization;

namespace HeBianGu.Service.TypeConverter
{
    public class DelegateConverter<TDelegate> : System.ComponentModel.TypeConverter where TDelegate : Delegate
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value == null) return null;

                return ExpressionService.ParseDelegate<TDelegate>(value.ToString());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return ExpressionService.ParseDelegate<TDelegate>(value.ToString());
        }
    }
}
