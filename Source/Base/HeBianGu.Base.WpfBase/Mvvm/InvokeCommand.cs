// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    public class InvokeCommand : ICommand
    {
        protected Action<object> _action;
        protected readonly Predicate<object> _canExecute;

        public InvokeCommand(Action<object> action)
        {
            _action = action;
        }
        public InvokeCommand(Action<object> execute, Predicate<object> canExecute) : this(execute)
        {
            _canExecute = canExecute ?? (x => true);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public virtual void Execute(object parameter)
        {
            if (_action != null)
                _action(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }
    }
}
