// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    /// <summary>
    /// DataModel for Styles Gallery.
    /// </summary>
    public class StylesSet : INotifyPropertyChanged
    {
        public string Label
        {
            get
            {
                return _label;
            }

            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Label"));
                }
            }
        }
        private string _label;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        private bool _isSelected;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
