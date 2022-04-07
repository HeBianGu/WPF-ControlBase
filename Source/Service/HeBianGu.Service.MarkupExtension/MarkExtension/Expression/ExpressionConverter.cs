// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Common.Expression;
using System;
using System.Windows.Data;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary>
    /// 根据表达式字符串计算转换
    /// </summary>
    public class ExpressionConverter : IValueConverter
    {
        public string Expression { get; set; }

        public Type ResultType { get; set; }

        public string ArgName { get; set; } = "arg";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Type valueType = value == null ? targetType : value.GetType();

            Type returnType = this.ResultType ?? valueType;

            System.Linq.Expressions.LambdaExpression func = ExpressionService.ParseLambda(this.Expression, returnType, System.Linq.Expressions.Expression.Parameter(valueType, this.ArgName));

            return func?.Compile().DynamicInvoke(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
