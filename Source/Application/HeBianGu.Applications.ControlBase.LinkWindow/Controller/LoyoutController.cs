using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Control
{
    [Route("Loyout")]
    class LoyoutController : MvcNavigationControllerBase<LoyoutViewModel>
    {

        public LoyoutController(ShareViewModel shareViewModel) : base(shareViewModel)
        {

        }

        public async Task<IActionResult> Center()
        {
            return View();
        }

        [Route("OverView/Button")]
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

        [Route("OverView/Toggle")]
        public async Task<IActionResult> Toggle()
        {
            return View();
        }

        [Route("OverView/Carouse")]
        public async Task<IActionResult> Carouse()
        {
            return View();
        }

        [Route("OverView/Evaluate")]
        public async Task<IActionResult> Evaluate()
        {
            return View();
        }

        [Route("OverView/Expander")]
        public async Task<IActionResult> Expander()
        {
            return View();
        }

        [Route("OverView/Gif")]
        public async Task<IActionResult> Gif()
        {
            return View();
        }

        [Route("OverView/Message")]
        public async Task<IActionResult> Message()
        {
            return View();
        }

        [Route("OverView/Upgrade")]
        public async Task<IActionResult> Upgrade()
        {
            return View();
        }

        [Route("OverView/Property")]
        public async Task<IActionResult> Property()
        {
            return View();
        }

        [Route("OverView/ProgressBar")]
        public async Task<IActionResult> ProgressBar()
        {
            return View();
        }

        [Route("OverView/Slider")]
        public async Task<IActionResult> Slider()
        {
            return View();
        }

        [Route("OverView/Tab")]
        public async Task<IActionResult> Tab()
        {
            return View();
        }

        [Route("OverView/Tree")]
        public async Task<IActionResult> Tree()
        {
            return View();
        }

        [Route("OverView/Observable")]
        public async Task<IActionResult> Observable()
        {
            return View();
        }

        [Route("OverView/Brush")]
        public async Task<IActionResult> Brush()
        {
            return View();
        }

        [Route("OverView/Shadow")]
        public async Task<IActionResult> Shadow()
        {
            return View();
        }

        [Route("OverView/Button")]
        public async Task<IActionResult> Button()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            this.ViewModel.ButtonContentText = DateTime.Now.ToString();

            return View();

        }



        [Route("OverView/Grid")]
        public async Task<IActionResult> Grid()
        {
            return View();
        }

        [Route("OverView/Combobox")]
        public async Task<IActionResult> Combobox()
        {
            return View();
        }

        [Route("OverView")]
        public async Task<IActionResult> OverView()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            MessageService.ShowSnackMessageWithNotice("OverView");

            return View();
        }

        [Route("OverView/TextBox")]
        public async Task<IActionResult> TextBox()
        {
            return View();
        }

        [Route("OverView/Book")]
        public async Task<IActionResult> Book()
        {
            return View();
        }

        [Route("OverView/Xaml")]
        public async Task<IActionResult> Xaml()
        {
            return View();
        }

        [Route("OverView/Dimension")]
        public async Task<IActionResult> Dimension()
        {
            return View();
        }

        [Route("OverView/Geometry")]
        public async Task<IActionResult> Geometry()
        {
            return View();
        }

        [Route("OverView/Panel")]
        public async Task<IActionResult> Panel()
        {
            return View();
        }
        [Route("OverView/Transform3D")]
        public async Task<IActionResult> Transform3D()
        {
            return View();
        }

        [Route("OverView/Drawer")]
        public async Task<IActionResult> Drawer()
        {
            return View();
        }
    }
}
