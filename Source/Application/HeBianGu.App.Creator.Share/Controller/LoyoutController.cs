using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Creator
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller
    {
        [Action(DisplayName = "信息")]
        public async Task<IActionResult> Info()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "新建")]
        public async Task<IActionResult> New()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "打开")]
        public async Task<IActionResult> Open()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "账户")]
        public async Task<IActionResult> Account()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "连接")]
        public async Task<IActionResult> Connect()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "共享")]
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        [Action(DisplayName = "保存")]
        public async Task<IActionResult> Save()
        {
            return await ViewAsync();
        }
    }
}
