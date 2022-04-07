// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary>
    /// 应用MarkupMarkupExtension转换ExpressionConverter 使写法更简单{h:ExpressionConverter Expression=arg+200}
    /// </summary>
    [TypeConverter(typeof(ExpressionConverterExtensionConverter))]
    public class ExpressionConverterExtension : System.Windows.Markup.MarkupExtension
    {
        public ExpressionConverterExtension()
        {

        }

        public ExpressionConverterExtension(string expression)
        {
            this.Expression = expression;
        }
        public string Expression { get; set; }

        public Type ResultType { get; set; }

        public string ArgName { get; set; } = "arg";


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            ExpressionConverter converter = new ExpressionConverter();

            converter.Expression = this.Expression;

            converter.ResultType = this.ResultType;

            converter.ArgName = this.ArgName;

            return converter;
        }
    }
}
