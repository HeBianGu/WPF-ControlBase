using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Grid")]
    class GridController : Controller
    {
        public async Task<IActionResult> GridCenter()
        {
            return await ViewAsync();
        }

    }
}
