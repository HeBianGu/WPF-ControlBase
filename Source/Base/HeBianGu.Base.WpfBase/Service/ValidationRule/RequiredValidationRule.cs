using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
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
