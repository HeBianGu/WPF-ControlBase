using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace HeBianGu.Base.WpfBase
{
    public interface IActionResult
    {
        object View { get; set; }

        Uri Uri { get; set; }

        object ViewModel { get; set; }
    }

 
}
