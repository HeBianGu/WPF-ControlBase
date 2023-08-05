using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Phone
{
    [Controller("Personal")]
    internal class PersonalController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}
