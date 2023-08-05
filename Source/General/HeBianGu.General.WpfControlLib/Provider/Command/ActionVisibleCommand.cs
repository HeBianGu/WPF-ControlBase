// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    public class ShowCopyWindowCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
                return;
            string txt = parameter?.ToString();
            Tuple<string, Action<IDialogWindow>> tuple = Tuple.Create("复制", new Action<IDialogWindow>(l =>
             {
                 Clipboard.SetDataObject(txt);
                 l.BeginClose();
             }));
            MessageProxy.Windower.ShowDialogWith(txt, null, true, tuple);
        }

        public event EventHandler CanExecuteChanged;
    }
}