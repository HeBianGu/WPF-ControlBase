// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HeBianGu.General.WpfControlLib
{
    public class PresenterMessageCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            string title = null;
            if (parameter is DisplayerViewModelBase displayer)
            {
                title = displayer.Name;
            }
            else
            {
                title = parameter.GetType().GetCustomAttribute<DisplayerAttribute>()?.Name ?? parameter.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? parameter.GetType().Name;
            }

            await MessageProxy.Presenter.Show(parameter, null, title);
        }

        public override bool CanExecute(object parameter)
        {
            return parameter != null;
        }
    }
}