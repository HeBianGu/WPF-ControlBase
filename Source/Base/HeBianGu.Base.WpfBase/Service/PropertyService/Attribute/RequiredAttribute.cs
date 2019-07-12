using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>Specifies that a data field value is required.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class RequiredAttribute : ValidationAttribute
    {

        public RequiredAttribute()
        {
            this.ErrorMessage = "数据不能为空";
        }
        public override bool IsValid(object value)
        {
            return value != null && value.ToString() != string.Empty;
        }
    }
}
