// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public NotifyPropertyChangedBase()
        {
            this.Init();
        }

        protected virtual void Init()
        {

        }

        #region - MVVM -

        bool _isRefreshing;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        public virtual void DispatcherRaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (_isRefreshing)
                return;
            _isRefreshing = true;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                _isRefreshing = false;
                this.OnDispatcherPropertyChanged();
            }));
        }

        protected virtual void OnDispatcherPropertyChanged()
        {

        }

        #endregion
    }
}
