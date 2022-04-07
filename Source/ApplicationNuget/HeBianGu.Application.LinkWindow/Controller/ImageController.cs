using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Image")]
    internal class ImageController : Controller
    {
        public async Task<IActionResult> ImageCenter()
        {
            return await ViewAsync();
        }
    }
}
