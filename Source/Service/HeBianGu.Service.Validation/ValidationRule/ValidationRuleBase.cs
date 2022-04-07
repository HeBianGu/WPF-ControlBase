// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Controls;

namespace HeBianGu.Service.Validation
{
    public abstract class ValidationRuleBase : ValidationRule
    {
        public string ErrorMessage { get; set; } = "数据不匹配";
    }


}
