using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Control
{
    [Route("Loyout")]
    public class LoyoutController : Controller
    {
        public IActionResult Mdi
        {
            get { return View(); }
        }

        public IActionResult Left
        {
            get { return View(); }
        }

        public IActionResult Right
        {
            get { return View(); }
        }

        public IActionResult Top
        {
            get { return View(); }
        }

        public IActionResult Bottom
        {
            get { return View(); }
        }

        public IActionResult Button
        {
            get { return View(); }
        }
    }
}
