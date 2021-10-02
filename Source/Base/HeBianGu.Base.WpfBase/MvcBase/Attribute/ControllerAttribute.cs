using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 应用再Mvc模式中的特性，根据此特性匹配对应的Controller名称 </summary>
    public sealed class ControllerAttribute : Attribute
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Logo { get; set; } = "\xe7fd";

        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }


    /// <summary> 设置只加载一次 </summary>
    public sealed class InitializeOperationAttribute : Attribute
    {
      
    }
}
