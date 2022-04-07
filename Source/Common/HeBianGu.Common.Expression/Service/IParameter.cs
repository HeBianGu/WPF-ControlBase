// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Linq.Expressions;

namespace HeBianGu.Common.Expression
{
    public interface IParameter
    {
        ParameterExpression Expression { get; }
        string Name { get; }
    }
}