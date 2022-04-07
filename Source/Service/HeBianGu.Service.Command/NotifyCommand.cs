// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows.Input;

namespace HeBianGu.Service.Command
{
    public abstract class NotifyCommand : NotifyPropertyChangedBase, ICommand
    {
        protected Action<object> _action;

        protected readonly Predicate<object> _canExecute;

        /// <summary> 执行命令 </summary>
        public NotifyCommand(Action<object> action)
        {
            _action = action;
        }

        /// <summary> 执行命令 </summary>
        public NotifyCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _action = execute;

            _canExecute = canExecute ?? (x => true);
        }

        /// <summary> 命令是否可执行 </summary>
        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;

            return _canExecute(parameter);
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

        /// <summary> 刷新命令可执行状态 (会调用CanExecute方法) </summary>
        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary> 执行命令 </summary>
        public virtual void Execute(object parameter)
        {
            _action(parameter);
        }
    }


}
