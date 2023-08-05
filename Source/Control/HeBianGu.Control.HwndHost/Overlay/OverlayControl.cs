using System;
using System.Windows.Interop;
using System.Windows.Controls;

namespace HeBianGu.Control.HwndHost
{
    public class OverlayControl : ContentControl, IDisposable
    {
        OverlayWindow w;
        IntPtrHwndHost host;
        public OverlayControl()
        {
            Loaded += OverlayControl_Loaded;
        }

        private void OverlayControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var content = Content;
            w = new OverlayWindow();
            w.Content = content;
            //w.Topmost = true;
            w.Show();
            IntPtr windowHandle = new WindowInteropHelper(w).Handle;
            host = new IntPtrHwndHost(windowHandle);
            Content = host;
        }

        public void Dispose()
        {
            host?.Dispose();
            w?.Close();
        }
    }
}
