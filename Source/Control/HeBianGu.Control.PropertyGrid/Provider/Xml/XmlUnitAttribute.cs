using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
   public class XmlUnitAttribute : Attribute
    {
        public string ID { get; set; }

        public XmlUnitAttribute(string id)
        {
            this.ID = id;
        }
    }
}
