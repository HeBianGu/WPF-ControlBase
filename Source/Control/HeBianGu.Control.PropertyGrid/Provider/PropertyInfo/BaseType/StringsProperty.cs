using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class StringsProperty : CollectionProperty<string>
    {
        public override string ConvertItem(string value)
        {
            return value;
        }

        public override bool IsTypeOfItem(string value)
        {
            return true;
        }
    }
}
