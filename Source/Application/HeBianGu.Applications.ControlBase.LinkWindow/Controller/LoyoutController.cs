using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
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

        public async Task<IActionResult> Toggle()
        {
            return View();
        }

        public async Task<IActionResult> Carouse()
        {
            return View();
        }
        public async Task<IActionResult> Evaluate()
        {
            return View();
        }

        public async Task<IActionResult> Expander()
        {
            return View();
        }

        public async Task<IActionResult> Gif()
        {
            return View();
        }

        public async Task<IActionResult> Message()
        {
            return View();
        }

        public async Task<IActionResult> Upgrade()
        {
            return View();
        }

        public async Task<IActionResult> Property()
        {
            return View();
        }

        public async Task<IActionResult> ProgressBar()
        {
            return View();
        }

        public async Task<IActionResult> Slider()
        {
            return View();
        }

        public async Task<IActionResult> Tab()
        {
            return View();
        }

        public async Task<IActionResult> Tree()
        {
            return View();
        }

        public async Task<IActionResult> Observable()
        {
            return View();
        }


        public async Task<IActionResult> Brush()
        {
            return View();
        }

        public async Task<IActionResult> Shadow()
        {
            return View();
        }

        
        public async Task<IActionResult> Button()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            this.ViewModel.ButtonContentText = DateTime.Now.ToString();

            return View();

        }

        public async Task<IActionResult> Center()
        {
            return View();
        }

        public async Task<IActionResult> Grid()
        {
            return View();
        }


        public async Task<IActionResult> Combobox()
        {
            return View();
        }

        public async Task<IActionResult> OverView()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            MessageService.ShowSnackMessageWithNotice("OverView");

            return View();
        }

        public async Task<IActionResult> TextBox()
        {
            return View();
        }
    }
}
