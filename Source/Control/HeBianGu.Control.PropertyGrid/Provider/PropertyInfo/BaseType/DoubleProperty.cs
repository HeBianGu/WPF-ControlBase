using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class DoubleProperty : PropertyInfo<double>
    {
        public override double ConvertToValue(object value)
        {
            return Convert.ToDouble(value?.ToString());
        }

        public override bool IsTypeOf()
        {
            return double.TryParse(this.Text, out double v);
        }
    }
}
