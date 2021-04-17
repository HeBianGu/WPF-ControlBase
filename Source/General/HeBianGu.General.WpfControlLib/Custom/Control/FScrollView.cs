using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    public class FScrollView : ScrollViewer
    {
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (ViewportHeight + VerticalOffset >= ExtentHeight && e.Delta <= 0) return;

            if (VerticalOffset == 0 && e.Delta > 0) return;

            base.OnMouseWheel(e);
        }
    }
}
