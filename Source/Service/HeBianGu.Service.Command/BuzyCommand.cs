// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;

namespace HeBianGu.Service.Command
{
    /// <summary>
    /// 带有Busy的命令
    /// </summary>
    public class BuzyCommand : NotifyCommand
    {
        /// <summary> 执行命令 </summary>
        public BuzyCommand(Action<object> action) : base(action)
        {

        }

        /// <summary> 执行命令 </summary>
        public BuzyCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }

        private bool _isBuzy;
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private string _text;
        /// <summary> 说明  </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        private Exception _exception;
        /// <summary> 说明  </summary>
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                RaisePropertyChanged();
            }
        }


        /// <summary> 执行命令 </summary>
        public override async void Execute(object parameter)
        {
            this.IsBuzy = true;

            this.Message = "正在执行";

            this.Exception = null;

            await Task.Run(() =>
            {
                try
                {
                    _action(parameter);

                    this.Message = "执行成功";
                }
                catch (Exception ex)
                {
                    this.Message = ex.Message;
                    this.Exception = ex;
                }
                finally
                {
                    this.IsBuzy = false;
                }
            });
        }

        /// <summary> 隐式转换 </summary>
        public static implicit operator BuzyCommand(Action<object> action)
        {
            BuzyCommand s = new BuzyCommand(action);
            return s;
        }
    }

    public abstract class BuzyCommand<T> : BuzyCommand
    {
        protected Action<T, object> Action { get; }

        /// <summary> 执行命令 </summary>
        public BuzyCommand(Action<T, object> execute) : base(null)
        {
            this.Action = execute;
        }

        /// <summary> 执行命令 </summary>
        public BuzyCommand(Action<T, object> execute, Predicate<object> canExecute) : base(null, canExecute)
        {
            this.Action = execute;
        }
    }
}
