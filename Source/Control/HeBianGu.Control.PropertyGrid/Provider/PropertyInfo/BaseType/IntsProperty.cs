using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class IntsProperty : CollectionProperty<int>
    {
        public override int ConvertItem(string value)
        {
            return Convert.ToInt32(value);
        }

        public override bool IsTypeOfItem(string value)
        {
            return int.TryParse(value, out int v);
        }
    }
}
