using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Grid")]
    class GridController: Controller
    {
        public async Task<IActionResult> GridCenter()
        {
            return await ViewAsync();
        }

    }
}
