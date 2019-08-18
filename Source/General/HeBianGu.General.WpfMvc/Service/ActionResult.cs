using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.General.WpfMvc
{
    public class ActionResult : IActionResult
    {
        public object View { get; set; }

        public Uri Uri { get; set; }

        public object ViewModel { get; set; }
    }
}
