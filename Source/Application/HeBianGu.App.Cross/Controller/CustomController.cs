using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Cross
{
    [Controller("Custom")]
    internal class CustomController : Controller<CustomViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}
