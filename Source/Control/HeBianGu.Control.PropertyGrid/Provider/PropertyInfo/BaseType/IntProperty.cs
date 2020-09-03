using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class IntProperty : PropertyInfo<int>
    {
        public override int ConvertToValue(object value)
        {
            return Convert.ToInt32(value?.ToString());
        }

        public override bool IsTypeOf()
        {
            return int.TryParse(this.Text, out int v);
        }
    }
}
