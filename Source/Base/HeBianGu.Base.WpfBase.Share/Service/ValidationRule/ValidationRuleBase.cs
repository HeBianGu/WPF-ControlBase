using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public abstract class ValidationRuleBase : ValidationRule
    {
        public string ErrorMessage { get; set; } = "数据不匹配";
    }


}
