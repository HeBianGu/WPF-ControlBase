using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class BoolsProperty : CollectionProperty<bool>
    {
        public override bool ConvertItem(string value)
        {
            return Convert.ToBoolean(value);
        }

        public override bool IsTypeOfItem(string value)
        {
            return bool.TryParse(this.Text, out bool v);
        }
    }
}
