using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
   public class ObjectPropertyFactory
    {
        public static ObjectPropertyItem Create(PropertyInfo info, object obj)
        {
            if (info.PropertyType == typeof(int))
            {
                return new IntPropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(string))
            {
                return new StringPropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(DateTime))
            {
                return new DateTimePropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(double))
            {
                return new DoublePropertyItem(info, obj);
            }
            else if (info.PropertyType == typeof(bool))
            {
                return new BoolPropertyItem(info, obj);
            }

            return null;
        }
    }
}
