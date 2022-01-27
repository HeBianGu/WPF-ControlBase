using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Tool
{
    [Controller("Common")]
    internal class CommonController : Controller<CommonViewModel>
    {
        public async Task<IActionResult> Common()
        {
            return await ViewAsync();
        }
    }
}
