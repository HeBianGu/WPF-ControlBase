using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{

    /// <summary> 应用再Mvc模式中的特性，根据此特性匹配对应的Controller名称 </summary>
    public sealed class RouteAttribute : Attribute
    {
        public string Name { get; set; }

        public RouteAttribute(string path)
        {
            this.Name = path;
        }
    }
}
