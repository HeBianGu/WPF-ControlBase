using HeBianGu.Base.WpfBase;
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
            return await ViewAsync();
        }

        [Route("OverView/Button")]
        public async Task<IActionResult> Mdi()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Left()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Right()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Top()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Bottom()
        {
            return await ViewAsync();
        }

        [Route("OverView/Toggle")]
        public async Task<IActionResult> Toggle()
        {
            return await ViewAsync();
        }

        [Route("OverView/Carouse")]
        public async Task<IActionResult> Carouse()
        {
            return await ViewAsync();
        }

        [Route("OverView/Evaluate")]
        public async Task<IActionResult> Evaluate()
        {
            return await ViewAsync();
        }

        [Route("OverView/Expander")]
        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        [Route("OverView/Gif")]
        public async Task<IActionResult> Gif()
        {
            return await ViewAsync();
        }

        [Route("OverView/Message")]
        public async Task<IActionResult> Message()
        {
            return await ViewAsync();
        }

        [Route("OverView/Upgrade")]
        public async Task<IActionResult> Upgrade()
        {
            return await ViewAsync();
        }

        [Route("OverView/Property")]
        public async Task<IActionResult> Property()
        {
            return await ViewAsync();
        }

        [Route("OverView/ProgressBar")]
        public async Task<IActionResult> ProgressBar()
        {
            return await ViewAsync();
        }

        [Route("OverView/Slider")]
        public async Task<IActionResult> Slider()
        {
            return await ViewAsync();
        }

        [Route("OverView/Tab")]
        public async Task<IActionResult> Tab()
        {
            return await ViewAsync();
        }

        [Route("OverView/Tree")]
        public async Task<IActionResult> Tree()
        {
            return await ViewAsync();
        }

        [Route("OverView/Observable")]
        public async Task<IActionResult> Observable()
        {
            return await ViewAsync();
        }

        [Route("OverView/Brush")]
        public async Task<IActionResult> Brush()
        {
            return await ViewAsync();
        }

        [Route("OverView/Shadow")]
        public async Task<IActionResult> Shadow()
        {
            return await ViewAsync();
        }

        [Route("OverView/Button")]
        public async Task<IActionResult> Button()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            this.ViewModel.ButtonContentText = DateTime.Now.ToString();

            return await ViewAsync();

        }



        [Route("OverView/Grid")]
        public async Task<IActionResult> Grid()
        {
            return await ViewAsync();
        }

        [Route("OverView/Combobox")]
        public async Task<IActionResult> Combobox()
        {
            return await ViewAsync();
        }

        [Route("OverView")]
        public async Task<IActionResult> OverView()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(500));

            MessageService.ShowSnackMessageWithNotice("OverView");

            return await ViewAsync();
        }

        [Route("OverView/TextBox")]
        public async Task<IActionResult> TextBox()
        {
            return await ViewAsync();
        }

        [Route("OverView/Book")]
        public async Task<IActionResult> Book()
        {
            return await ViewAsync();
        }

        [Route("OverView/Xaml")]
        public async Task<IActionResult> Xaml()
        {
            return await ViewAsync();
        }

        [Route("OverView/Dimension")]
        public async Task<IActionResult> Dimension()
        {
            return await ViewAsync();
        }

        [Route("OverView/Geometry")]
        public async Task<IActionResult> Geometry()
        {
            return await ViewAsync();
        }

        [Route("OverView/Panel")]
        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }
        [Route("OverView/Transform3D")]
        public async Task<IActionResult> Transform3D()
        {
            return await ViewAsync();
        }

        [Route("OverView/Drawer")]
        public async Task<IActionResult> Drawer()
        {
            return await ViewAsync();
        }


        [Route("OverView/MessageContainer")]
        public async Task<IActionResult> MessageContainer()
        {
            return await ViewAsync();
        }


        [Route("OverView/DatePicker")]
        public async Task<IActionResult> DatePicker()
        {
            return await ViewAsync();
        }

        [Route("OverView/Password")]
        public async Task<IActionResult> Password()
        {
            return await ViewAsync();
        }

        [Route("OverView/ListBox")]
        public async Task<IActionResult> ListBox()
        {
            return await ViewAsync();
        }

        [Route("OverView/Document")]
        public async Task<IActionResult> Document()
        {
            return await ViewAsync();
        }
        

    }
}
