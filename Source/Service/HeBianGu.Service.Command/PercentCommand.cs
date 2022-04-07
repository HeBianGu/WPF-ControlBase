// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;

namespace HeBianGu.Service.Command
{
    /// <summary>
    /// 带有绑定百分比的命令
    /// </summary>
    public class PercentCommand : BuzyCommand<PercentCommand>
    {
        private bool AutoMessage { get; set; } = true;

        /// <summary> 执行命令 </summary>
        public PercentCommand(Action<PercentCommand, object> execute, bool autoMesaage = true) : base(execute)
        {
            this.AutoMessage = autoMesaage;
        }

        /// <summary> 执行命令 </summary>
        public PercentCommand(Action<PercentCommand, object> execute, Predicate<object> canExecute, bool autoMesaage = true) : base(execute, canExecute)
        {
            this.AutoMessage = autoMesaage;
        }


        private double _value;
        /// <summary> 当前值  </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();

                if (!this.AutoMessage) return;

                this.Message = value + "%";
            }
        }

        public override async void Execute(object parameter)
        {
            this.Value = 0.0;

            this.IsBuzy = true;

            this.Message = "正在执行";

            this.Exception = null;

            await Task.Run(() =>
            {
                try
                {
                    this.Action?.Invoke(this, parameter);

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
    }


    /// <summary>
    /// 带有绑定百分比的命令
    /// </summary>
    public class MessageCommand : BuzyCommand<MessageCommand>
    {

        /// <summary> 执行命令 </summary>
        public MessageCommand(Action<MessageCommand, object> execute) : base(execute)
        {

        }

        /// <summary> 执行命令 </summary>
        public MessageCommand(Action<MessageCommand, object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }


        public override async void Execute(object parameter)
        {
            this.IsBuzy = true;

            this.Message = "正在执行";

            this.Exception = null;

            await Task.Run(() =>
            {
                try
                {
                    this.Action?.Invoke(this, parameter);

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
    }
}
