using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand RelayCommand { get; set; }

        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
