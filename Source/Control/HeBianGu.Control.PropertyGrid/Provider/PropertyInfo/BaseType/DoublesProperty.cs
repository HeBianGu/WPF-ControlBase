using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class DoublesProperty : CollectionProperty<double>
    {
        public override double ConvertItem(string value)
        {
            return Convert.ToDouble(value);
        }
        public override bool IsTypeOfItem(string value)
        {
            return double.TryParse(this.Text, out double v);
        }
    }
}
