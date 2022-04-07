// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    public class ContextualTabGroupData : INotifyPropertyChanged
    {
        public ContextualTabGroupData()
            : this(null)
        {
        }

        public ContextualTabGroupData(string header)
        {
            Header = header;
        }


        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Header"));
                }
            }
        }
        private string _header;

        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }

            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsVisible"));
                }
            }
        }
        private bool _isVisible;

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                if (_tabDataCollection == null)
                {
                    _tabDataCollection = new ObservableCollection<TabData>();
                }
                return _tabDataCollection;
            }
        }
        private ObservableCollection<TabData> _tabDataCollection;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}
