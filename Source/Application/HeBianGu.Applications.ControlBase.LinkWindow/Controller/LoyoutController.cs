using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Control
{
    [Route("Loyout")]
    class LoyoutController : Controller<LoyoutViewModel>
    {
        public IActionResult Mdi()
        {
            return View();
        }

        public IActionResult Left()
        {
            return View();
        }

        public IActionResult Right()
        {
            return View();
        }

        public IActionResult Top()
        {
            return View();
        }

        public IActionResult Bottom()
        {
           

            return View();
        }

        public IActionResult Button()
        {
            this.ViewModel.ButtonContentText = DateTime.Now.ToString();
            return View();
        }

        public IActionResult Center()
        {
            return View();
        }

        public IActionResult Combobox()
        {
            return View();
        }

        public IActionResult OverView()
        {
            return View();
        }

        public IActionResult ProgressBar()
        {
            return View();
        }

        public IActionResult TextBox()
        {
            return View();
        }
    }
}
