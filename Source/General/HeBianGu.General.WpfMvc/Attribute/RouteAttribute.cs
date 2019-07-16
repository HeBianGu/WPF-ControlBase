using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{
    public sealed class RouteAttribute : Attribute
    {
        public string Name { get; set; }

        public RouteAttribute(string path)
        {
            this.Name = path;
        }
    }
}
