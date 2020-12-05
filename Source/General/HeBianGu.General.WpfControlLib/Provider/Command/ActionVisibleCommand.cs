using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class ActionVisibleCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is UIElement element)
            {
                Cattach.SetIsVisible(element, true);
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    public class ActionHiddenCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                Cattach.SetIsVisible(element, false);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}