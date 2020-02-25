using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class MessageLayerCloseCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public  void Execute(object parameter)
        {
            MessageService.CloseWithLayer();
        }

        public event EventHandler CanExecuteChanged;
    }
}