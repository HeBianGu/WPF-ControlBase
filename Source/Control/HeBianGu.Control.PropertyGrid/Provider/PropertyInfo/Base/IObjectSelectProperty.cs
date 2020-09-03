using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 用于带下拉选项和子项的配置属性 </summary>
    public interface IObjectSelectProperty
    {
        object GetModel();

        string DisplayName { get; set; }
    }

}
