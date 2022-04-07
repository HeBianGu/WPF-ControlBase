// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows.Input;

namespace HeBianGu.Service.Mvp
{
    public class InvokeCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
            //if (parameter == null) return false;

            //var find = ServiceRegistry.Instance.GetService(parameter.GetType());

            //if (find == null) return false;

            //return typeof(IOperate).IsAssignableFrom(parameter.GetType());
        }

        public void Execute(object parameter)
        {
            if (parameter is Type type)
            {
                object service = ServiceRegistry.Instance.GetService(type);

                if (service == null) return;

                if (service is IInvokePresenter dialog)
                {
                    dialog.Invoke();
                }
            }

        }

        public event EventHandler CanExecuteChanged;
    }
}
