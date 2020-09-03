using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class StringProperty : PropertyInfo<string>
    {
        public override string ConvertToValue(object value)
        {
            return value?.ToString();
        }

        public override bool IsTypeOf()
        {
            return true;
        }
    }
}
