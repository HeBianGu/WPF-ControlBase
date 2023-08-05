using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.Converter
{
    public abstract class MarkupValueConverterBase : MarkupExtension, IValueConverter, INotifyPropertyChanged
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public virtual void Refresh()
        {
            this.ConvertBack(null, null, null, null);
        }

        public object DefaultValue { get; set; } = DependencyProperty.UnsetValue;

        public object DefaultBackValue { get; set; } = DependencyProperty.UnsetValue; 

        //  Do ：目前作用不大
        #region - INotifyPropertyChanged -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            this.Refresh();
        }

        #endregion
    }

    public class AddTestConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Result = this.First + this.Second;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.First + this.Second;
        }

        private int _first;
        /// <summary> 说明  </summary>
        public int First
        {
            get { return _first; }
            set
            {
                _first = value;
                RaisePropertyChanged();
            }
        }


        private int _second;
        /// <summary> 说明  </summary>
        public int Second
        {
            get { return _second; }
            set
            {
                _second = value;
                RaisePropertyChanged();
            }
        }


        private int _result;
        /// <summary> 说明  </summary>
        public int Result
        {
            get { return _result; }
            set
            {
                if (_result == value)
                    return;
                _result = value;
                RaisePropertyChanged();
            }
        }
    }
}
