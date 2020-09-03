using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Control.PropertyGrid
{
   public class UnitGroups: List<UnitGroup>
    {

    }


    public class UnitGroup
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        public List<Unit> Units { get; set; } = new List<Unit>();

    }

    public class Unit
    {
        [XmlAttribute("Param")]
        public double Param { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
