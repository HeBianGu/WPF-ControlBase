using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Image")]
    class ImageController : Controller
    {
        public async Task<IActionResult> ImageCenter()
        {
            return await ViewAsync();
        }
    }
}
