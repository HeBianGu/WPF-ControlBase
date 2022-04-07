using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Vedio")]
    internal class VedioController : Controller
    {
        public async Task<IActionResult> VedioCenter()
        {
            return await ViewAsync();
        }
    }
}
