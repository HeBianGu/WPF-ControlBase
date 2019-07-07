using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;


namespace  HeBianGu.Base.WpfBase
{
    public class PushMainWindow2TopCommand : ICommand
    {
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {

            //  Message：暂时去掉

            //if (Application.Current.MainWindow != null && Application.Current.MainWindow.Visibility != Visibility.Visible)
            //{
            //    Application.Current.MainWindow.Show();
            //    var hwndSource = (HwndSource)PresentationSource.FromDependencyObject(Application.Current.MainWindow);
            //    if (hwndSource != null)
            //    {
            //        UnsafeNativeMethods.SetForegroundWindow(hwndSource.Handle);
            //    }
            //}
        }

        public event EventHandler CanExecuteChanged;
    }
}