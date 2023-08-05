using HeBianGu.Base.WpfBase;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.HwndHost
{
    public class ProcessHwndHost : System.Windows.Interop.HwndHost
    {
        string _fileName;
        public ProcessHwndHost(string fileName = "notepad")
        {
            _fileName = fileName;
        }
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        public const int GWL_STYLE = (-16);
        public const int WS_CHILD = 0x40000000;
        Process proc = null;
        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            ProcessStartInfo psi1 = new ProcessStartInfo(this._fileName);
            proc = new Process() { StartInfo = psi1 };
            proc.Start();
            proc.WaitForInputIdle();
            Thread.Sleep(5000);
            IntPtr handle = proc.MainWindowHandle;
            uint exStyle1 = WinAPI.GetWindowLongPtr(handle, -20);
            SetWindowLong(handle, GWL_STYLE, WS_CHILD);
            //SetWindowLong(handle, GWL_STYLE, 0x04000000);
            WinAPI.SetParent(handle, hwndParent.Handle);
            return new HandleRef(this, handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            proc?.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            proc?.Dispose();
        }
    }

    public class ProcesstBox : ContentControl, IDisposable
    {
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(ProcesstBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                ProcesstBox control = d as ProcesstBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshPath();
            }));

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        ProcessHwndHost _host;
        public void RefreshPath()
        {
            _host?.Dispose();
            this.IsBuzy = true;
            string path = this.FileName;
            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                      {
                          _host = new ProcessHwndHost(path);
                          this.Content = _host;
                          this.IsBuzy = false;
                      }));
        }

        public bool IsBuzy
        {
            get { return (bool)GetValue(IsBuzyProperty); }
            set { SetValue(IsBuzyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBuzyProperty =
            DependencyProperty.Register("IsBuzy", typeof(bool), typeof(ProcesstBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                ProcesstBox control = d as ProcesstBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public void Dispose()
        {
            _host?.Dispose();
        }
    }

}
