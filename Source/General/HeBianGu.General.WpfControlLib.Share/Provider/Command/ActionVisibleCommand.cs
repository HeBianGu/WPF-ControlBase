using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    //public class ActionVisibleCommand : ICommand
    //{
    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (parameter is UIElement element)
    //        {
    //            Cattach.SetIsVisible(element, true);
    //        }
    //    }

    //    public event EventHandler CanExecuteChanged;
    //}

    //public class ActionHiddenCommand : ICommand
    //{
    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (parameter is UIElement element)
    //        {
    //            Cattach.SetIsVisible(element, false);
    //        }
    //    }
         
    //    public event EventHandler CanExecuteChanged;
    //}

    //public class ShowMessageWindowCommand : ICommand
    //{
    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public async void Execute(object parameter)
    //    {
    //        if (parameter == null) return;

    //        MessageWindow.ShowSumit(parameter?.ToString(), null, true);

    //        //await MessageService.ShowResultMessge(parameter?.ToString());
    //    }

    //    public event EventHandler CanExecuteChanged;
    //}


    public class ShowCopyWindowCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter == null) return;

            string txt = parameter?.ToString();

            var tuple = Tuple.Create("复制", new Action<IMessageWindow>(l =>
             {
                 Clipboard.SetDataObject(txt);

                 //var ssss=  Clipboard.GetText(TextDataFormat.Html);

                 l.BeginClose();
             }));

            var message = ServiceRegistry.Instance.GetInstance<IMessageWindowService>();

            message.ShowDialogWith(txt, null, true, tuple);

            //MessageWindow.ShowDialogWith(txt, null, true, tuple);
        }

        public event EventHandler CanExecuteChanged;
    }
}