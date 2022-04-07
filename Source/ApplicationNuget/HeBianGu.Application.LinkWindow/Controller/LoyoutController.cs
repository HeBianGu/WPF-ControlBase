using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Loyout")]
    internal class LoyoutController : MvcNavigationControllerBase<LoyoutViewModel>
    {

        public LoyoutController(ShareViewModel shareViewModel) : base(shareViewModel)
        {

        }

        public async Task<IActionResult> Center()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Button")]
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

        [Controller("OverView/Toggle")]
        public async Task<IActionResult> Toggle()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Carouse")]
        public async Task<IActionResult> Carouse()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Evaluate")]
        public async Task<IActionResult> Evaluate()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Expander")]
        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Gif")]
        public async Task<IActionResult> Gif()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Message")]
        public async Task<IActionResult> Message()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Upgrade")]
        public async Task<IActionResult> Upgrade()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Property")]
        public async Task<IActionResult> Property()
        {
            return await ViewAsync();
        }

        [Controller("OverView/ProgressBar")]
        public async Task<IActionResult> ProgressBar()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Slider")]
        public async Task<IActionResult> Slider()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Tab")]
        public async Task<IActionResult> Tab()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Tree")]
        public async Task<IActionResult> Tree()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Observable")]
        public async Task<IActionResult> Observable()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Brush")]
        public async Task<IActionResult> Brush()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Shadow")]
        public async Task<IActionResult> Shadow()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Button")]
        public async Task<IActionResult> Button()
        {
            await Message.Instance.ShowWaittingMessge(() => Thread.Sleep(500));

            ViewModel.ButtonContentText = DateTime.Now.ToString();

            return await ViewAsync();

        }



        [Controller("OverView/Grid")]
        public async Task<IActionResult> Grid()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Combobox")]
        public async Task<IActionResult> Combobox()
        {
            return await ViewAsync();
        }

        [Controller("OverView")]
        public async Task<IActionResult> OverView()
        {
            await Message.Instance.ShowWaittingMessge(() => Thread.Sleep(500));

            Message.Instance.ShowSnackMessageWithNotice("OverView");

            return await ViewAsync();
        }

        [Controller("OverView/TextBox")]
        public async Task<IActionResult> TextBox()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Book")]
        public async Task<IActionResult> Book()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Xaml")]
        public async Task<IActionResult> Xaml()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Dimension")]
        public async Task<IActionResult> Dimension()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Geometry")]
        public async Task<IActionResult> Geometry()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Panel")]
        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }
        [Controller("OverView/Transform3D")]
        public async Task<IActionResult> Transform3D()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Drawer")]
        public async Task<IActionResult> Drawer()
        {
            return await ViewAsync();
        }


        [Controller("OverView/MessageContainer")]
        public async Task<IActionResult> MessageContainer()
        {
            return await ViewAsync();
        }


        [Controller("OverView/DatePicker")]
        public async Task<IActionResult> DatePicker()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Password")]
        public async Task<IActionResult> Password()
        {
            return await ViewAsync();
        }

        [Controller("OverView/ListBox")]
        public async Task<IActionResult> ListBox()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Document")]
        public async Task<IActionResult> Document()
        {
            return await ViewAsync();
        }


        [Controller("OverView/StoryBoardPlayer")]
        public async Task<IActionResult> StoryBoardPlayer()
        {
            return await ViewAsync();
        }


        [Controller("OverView/System")]
        public async Task<IActionResult> System()
        {
            return await ViewAsync();
        }

        [Controller("OverView/Zoom")]
        public async Task<IActionResult> Zoom()
        {
            return await ViewAsync();
        }

    }
}
