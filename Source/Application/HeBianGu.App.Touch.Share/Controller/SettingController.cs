
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    [Controller("Setting")]
    internal class SettingController : Controller<SettingViewModel>
    {
        public async Task<IActionResult> Setting()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Theme()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> UpLoad()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Advance()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Device()
        {
            return await ViewAsync();
        }
    }
}
