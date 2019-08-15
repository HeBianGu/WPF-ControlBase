using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public abstract class ValidationAttribute : Attribute
    {
        public new Predicate<object> Match { get; set; }

        public ValidationAttribute(Predicate<object> match)
        {
            Match = match;
        }

        public ValidationAttribute()
        {

        }

        public string ErrorMessage { get; set; }

        public virtual bool IsValid(object value)
        {
            return Match(value);
        }

    }


}
