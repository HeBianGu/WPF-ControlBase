using HeBianGu.Base.WpfBase;
using HeBianGu.Control.HwndHost;
using HeBianGu.Control.MessageContainer;
using HeBianGu.Window.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Window.Hwnd
{
    public partial class HwdWindowBase : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(HwdWindowBase), "S.HwdWindowBase.Default");

        static HwdWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HwdWindowBase), new FrameworkPropertyMetadata(typeof(HwdWindowBase)));
        }


        public override void ShowContainer(FrameworkElement element, Predicate<Panel> predicate = null)
        {
            base.ShowContainer(element, predicate);

            if (this.GetChild<ProcessHwndHost>() is ProcessHwndHost host)
            {
                host.Visibility = Visibility.Hidden;
            }
        }

        public override void CloseContainer()
        {
            if (this._layerGroup.Children.Count == 1)
            {
                if (this.GetChild<ProcessHwndHost>() is ProcessHwndHost host)
                {
                    host.Visibility = Visibility.Visible;
                }
            }
            base.CloseContainer();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            //if (SizeToContent == SizeToContent.WidthAndHeight && WindowChrome.GetWindowChrome(this) != null)
            //{
            //    InvalidateMeasure();
            //}

            //IntPtr handle = new WindowInteropHelper(this).Handle;
            //HwndSource.FromHwnd(handle)?.AddHook(WndProc);

            //_mouseHookProc = MouseHookProc;
            //IntPtr hModule = Win32API.GetModuleHandle(null);
            //_handleToHook = Win32API.SetWindowsHookEx((int)WindowsHookCodes.WH_MOUSE_LL, _mouseHookProc, hModule, 0);
        }
    }

}
