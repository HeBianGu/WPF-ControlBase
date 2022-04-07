using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Share")]
    internal class ShareController : Controller
    {
        public async Task<IActionResult> Bottom()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Top()
        {
            return await ViewAsync();
        }
    }
}
