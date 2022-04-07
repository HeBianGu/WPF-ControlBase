using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Custom")]
    internal class CustomController : Controller
    {
        public async Task<IActionResult> Custom()
        {
            return await ViewAsync();
        }
    }
}
