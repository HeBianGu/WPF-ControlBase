using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Control
{
    [Route("Loyout")]
    class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Mdi()
        {
            return View();
        }

        public async Task<IActionResult> Left()
        {
            return View();
        }

        public async Task<IActionResult> Right()
        {
            return View();
        }

        public async Task<IActionResult> Top()
        {
            return View();
        }

        public async Task<IActionResult> Bottom()
        {
            return View();
        }

        public async Task<IActionResult> Button()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(1000));

            this.ViewModel.ButtonContentText = DateTime.Now.ToString();

            return View();

        }

        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> Combobox()
        {
            return View();
        }

        public async Task<IActionResult> OverView()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(1000));

            MessageService.ShowSnackMessageWithNotice("OverView");

            return View();
        }

        public async Task<IActionResult> ProgressBar()
        {
            return View();
        }

        public async Task<IActionResult> TextBox()
        {
            return View();
        }
    }
}
