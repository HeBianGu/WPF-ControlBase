using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{

 
    public class BoolProperty: PropertyInfo<bool>
    {
        public override bool ConvertToValue(object value)
        {
            return Convert.ToBoolean(value?.ToString());
        }

        public override bool IsTypeOf()
        {
            return bool.TryParse(this.Text,out bool v);
        }
    }



}
