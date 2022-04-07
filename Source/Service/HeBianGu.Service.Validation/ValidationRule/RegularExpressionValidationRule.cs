// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace HeBianGu.Service.Validation
{
    public class RegularExpressionValidationRule : ValidationRuleBase
    {
        public string Pattern { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null) return new ValidationResult(false, "数据不能为空");

            if (Regex.IsMatch(value.ToString(), Pattern))
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
