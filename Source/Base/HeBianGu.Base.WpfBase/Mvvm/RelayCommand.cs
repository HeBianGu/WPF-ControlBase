using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 带参数的命令 </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> _action;

        private readonly Predicate<object> _canExecute;

        /// <summary> 执行命令 </summary>
        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        /// <summary> 执行命令 </summary>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _action = execute;

            _canExecute = canExecute ?? (x => true);
        }

        /// <summary> 命令是否可执行 </summary>
        public bool CanExecute(object parameter)
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
        public void Execute(object parameter)
        {
            _action(parameter);
        }

        /// <summary> 隐式转换 </summary>
        static public implicit operator RelayCommand(Action<object> action)
        {
            RelayCommand s = new RelayCommand(action);
            return s;
        }
    }



    /// <summary>
    /// 广播命令：基本ICommand实现接口，带参数
    /// </summary>
    public class RelayCommand<T> : ICommand
    {
        public Action<T> ExecuteCommand { get; private set; }


        public Predicate<T> CanExecuteCommand { get; private set; }


        public RelayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand)
        {
            this.ExecuteCommand = executeCommand;
            this.CanExecuteCommand = canExecuteCommand;
        }

        public RelayCommand(Action<T> executeCommand)
            : this(executeCommand, null) { }

        /// <summary>
        /// 定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand((T)parameter);
        }

        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand == null || CanExecuteCommand((T)parameter);
        }


        public event EventHandler CanExecuteChanged
        {
            add { if (this.CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
            remove { if (this.CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
        }
    }

}
