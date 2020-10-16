using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeBianGu.Control.PropertyGrid
{
    public class StringSelectProperty : PropertyInfo<string>
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
