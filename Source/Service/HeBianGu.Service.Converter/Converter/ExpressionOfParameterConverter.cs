// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Common.Expression;
using System;
using System.Windows.Data;

namespace HeBianGu.Service.Converter
{
    /// <summary>
    /// 根据表达式字符串计算转换
    /// </summary>
    public class ExpressionOfParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Type returnType = value == null ? targetType : value.GetType();

            System.Linq.Expressions.LambdaExpression func = ExpressionService.ParseLambda(parameter.ToString(), returnType, System.Linq.Expressions.Expression.Parameter(returnType, "arg"));

            return func?.Compile().DynamicInvoke(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
