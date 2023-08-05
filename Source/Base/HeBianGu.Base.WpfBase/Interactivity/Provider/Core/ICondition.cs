// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// An interface that a given object must implement in order to be 
    /// set on a ConditionBehavior.Condition property. 
    /// </summary>
    public interface ICondition
    {
        bool Evaluate();
    }
}
