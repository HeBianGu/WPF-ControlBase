using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Windows.Markup;

namespace HeBianGu.Base.WpfBase
{
    public abstract class MarkupCommandBase : MarkupExtension, ICommand, INotifyPropertyChanged
    {
        #region - ICommand -
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

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
        #endregion

        #region - MarkupExtension -
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        #endregion

        #region - INotifyPropertyChanged -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
