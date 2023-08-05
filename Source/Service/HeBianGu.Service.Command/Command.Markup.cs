// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace HeBianGu.Service.Command
{
    //public abstract class MarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged
    //{
    //    #region - ICommand -
    //    public virtual bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public abstract void Execute(object parameter);

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add
    //        {
    //            CommandManager.RequerySuggested += value;
    //        }
    //        remove
    //        {
    //            CommandManager.RequerySuggested -= value;
    //        }
    //    }
    //    #endregion


    //    #region - MarkupExtension -
    //    public override object ProvideValue(IServiceProvider serviceProvider)
    //    {
    //        return this;
    //    }
    //    #endregion


    //    #region - INotifyPropertyChanged -

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
    //    {
    //        if (PropertyChanged != null)
    //            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    #endregion
    //}
}
