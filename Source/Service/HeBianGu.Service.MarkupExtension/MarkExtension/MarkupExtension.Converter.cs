// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary>
    /// TypeConverter需继承TypeConverter
    /// </summary>
    public class GetTypeConverterExtension : GetValueToTypeExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            TypeConverter converter = Activator.CreateInstance(this.Type) as TypeConverter; 
            return converter.ConvertFromString(Value);
        }
    }

    /// <summary>
    /// ToType需要实现IConvertible
    /// </summary>
    public class GetIConvertibleExtension : GetValueToTypeExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Convert.ChangeType(Value, Type);
        }
    } 
}
