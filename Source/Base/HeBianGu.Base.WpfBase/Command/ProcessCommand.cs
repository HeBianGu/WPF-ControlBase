using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class ProcessCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            var result= File.Exists(parameter?.ToString()) || Directory.Exists(parameter?.ToString());

            return result;
        }

        public void Execute(object parameter)
        {
            System.Diagnostics.Process.Start(parameter?.ToString());
        }

        public event EventHandler CanExecuteChanged;
    }
}