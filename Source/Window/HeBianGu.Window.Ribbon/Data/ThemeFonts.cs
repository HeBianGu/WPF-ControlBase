// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    /// <summary>
    /// DataModel for ThemeFonts Gallery
    /// </summary>
    public class ThemeFonts : INotifyPropertyChanged
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


        public string Field1
        {
            get
            {
                return _field1;
            }

            set
            {
                if (_field1 != value)
                {
                    _field1 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Field1"));
                }
            }
        }
        private string _field1;

        public string Field2
        {
            get
            {
                return _field2;
            }

            set
            {
                if (_field2 != value)
                {
                    _field2 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Field2"));
                }
            }
        }
        private string _field2;

        public string Field3
        {
            get
            {
                return _field3;
            }

            set
            {
                if (_field3 != value)
                {
                    _field3 = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Field3"));
                }
            }
        }
        private string _field3;

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
