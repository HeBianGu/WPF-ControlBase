using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Above
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Button()
        {
            return await ViewAsync();
        }
        public async Task<IActionResult> TextBox()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> ComboBox()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> CheckBox()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TextBlock()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Slider()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> DatePicker()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> ListBox()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> DataGrid()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> TreeView()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Tab()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Menu()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> ProgressBar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Radio()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Toggle()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> ContextMenu()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> GroupBox()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> ScrollViewer()
        {
            return await ViewAsync();
        }
    }

}
