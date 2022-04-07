// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Linq.Expressions;

namespace HeBianGu.Common.Expression
{
    public class Parameter<T> : IParameter
    {
        public Parameter(string name)
        {
            Name = name;

            Expression = System.Linq.Expressions.Expression.Parameter(typeof(T), name);
        }

        public string Name { get; private set; }

        public ParameterExpression Expression { get; private set; }
    }
}
