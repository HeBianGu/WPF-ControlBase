using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace HeBianGu.App.Touch
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

            Process[] exe = Process.GetProcessesByName("SystemKeyBoard.exe");

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