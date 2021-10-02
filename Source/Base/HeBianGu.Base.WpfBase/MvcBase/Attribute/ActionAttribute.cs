using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public sealed class ActionAttribute : Attribute
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Logo { get; set; } = "\xe7fd";
    }
}
