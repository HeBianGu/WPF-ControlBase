using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Loyout
{
    /// <summary>
    /// ZoomControl.xaml 的交互逻辑
    /// </summary>
    public partial class ZoomControl : UserControl
    {
        public ZoomControl()
        {
            InitializeComponent();
        }

        //private void panel_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    var x = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
        //    MyCanvas.Scale *= x;

        //    // Adjust the offset to make the point under the mouse stay still.
        //    var position = (Vector)e.GetPosition(panel);
        //    MyCanvas.Offset = (Point)((Vector)
        //        (MyCanvas.Offset + position) * x - position);

        //    e.Handled = true;
        //}

        //private Point LastMousePosition;

        //private void panel_MouseMove(object sender, MouseEventArgs e)
        //{
        //    var position = e.GetPosition(this.panel);
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        MyCanvas.Offset -= position - LastMousePosition;
        //    }
        //    LastMousePosition = position;
        //}
    }
}
