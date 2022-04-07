// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Globalization;
using System.Windows.Controls;

namespace HeBianGu.Service.Validation
{
    public class RequiredValidationRule : ValidationRuleBase
    {

        public RequiredValidationRule()
        {
            this.ErrorMessage = "数据不能为空";
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && value.ToString() != string.Empty)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, ErrorMessage);
            }
        }
    }
}
