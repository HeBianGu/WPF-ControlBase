#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/12/1 9:54:54 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
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
        public RelayCommand(Action<object> action)
        {
            _action = action;
        }
        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
            else
            {
                _action("Hello");
            }
        }
        #endregion



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
