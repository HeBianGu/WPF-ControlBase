using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Application.TouchWindow
{
    public class TextBoxGotFocusShowKeyBoardBehavior : Behavior<UIElement>
    {

        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += AssociatedObject_GotFocus;
        }

        private void AssociatedObject_GotFocus(object sender, RoutedEventArgs e)
        {

            string file = Path.Combine(Environment.CurrentDirectory, "Resources", "SystemKeyBoard.exe");

            var exe = Process.GetProcessesByName("SystemKeyBoard.exe");

            if (exe.Length > 0) return;

            if (File.Exists(file))
            {
                Process.Start(file);
            }

            //AssociatedObject.Focus();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.GotFocus -= AssociatedObject_GotFocus;
        }




    }
}