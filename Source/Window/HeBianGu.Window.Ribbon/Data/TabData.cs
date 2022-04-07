// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    public class TabData : ControlData, INotifyPropertyChanged
    {
        public TabData()
            : this(null)
        {
        }

        public TabData(string header)
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
                    RaisePropertyChanged();
                }
            }
        }
        private string _header;

        public string ContextualTabGroupHeader
        {
            get
            {
                return _contextualTabGroupHeader;
            }

            set
            {
                if (_contextualTabGroupHeader != value)
                {
                    _contextualTabGroupHeader = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _contextualTabGroupHeader;

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
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isSelected;

        public ObservableCollection<GroupData> GroupDataCollection
        {
            get
            {
                if (_groupDataCollection == null)
                {
                    _groupDataCollection = new ObservableCollection<GroupData>();
                }
                return _groupDataCollection;
            }
        }
        private ObservableCollection<GroupData> _groupDataCollection;
    }
}
