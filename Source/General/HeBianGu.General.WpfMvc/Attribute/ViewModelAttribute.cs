using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{
    public sealed class ViewModelAttribute : Attribute
    {
        public string Name { get; set; }

        public ViewModelAttribute(string path)
        {
            this.Name = path;
        }
    }
}
