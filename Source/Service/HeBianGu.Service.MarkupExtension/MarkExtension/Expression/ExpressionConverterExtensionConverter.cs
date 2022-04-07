// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Globalization;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary>
    /// 应用TypeConverter转换ExpressionConverterExtension 使写法更简单 {h:ExpressionConverter arg+200}
    /// </summary>
    public class ExpressionConverterExtensionConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return new ExpressionConverterExtension(value?.ToString());
        }
    }
}
