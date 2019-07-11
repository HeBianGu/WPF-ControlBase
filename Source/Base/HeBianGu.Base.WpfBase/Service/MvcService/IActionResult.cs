using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public interface IActionResult
    {
        ContentControl View { get; set; }

        Uri Uri { get; set; }

        object ViewModel { get; set; }
    }

 
}
