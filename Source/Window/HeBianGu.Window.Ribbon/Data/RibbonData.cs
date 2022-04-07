// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HeBianGu.Window.Ribbon
{
    public class RibbonData : INotifyPropertyChanged
    {
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

        public ObservableCollection<ContextualTabGroupData> ContextualTabGroupDataCollection
        {
            get
            {
                if (_contextualTabGroupDataCollection == null)
                {
                    _contextualTabGroupDataCollection = new ObservableCollection<ContextualTabGroupData>();
                    _contextualTabGroupDataCollection.Add(new ContextualTabGroupData("Grp ") { IsVisible = true });
                }
                return _contextualTabGroupDataCollection;
            }
        }
        private ObservableCollection<ContextualTabGroupData> _contextualTabGroupDataCollection;

        public MenuButtonData ApplicationMenuData
        {
            get
            {
                if (_applicationMenuData == null)
                {
                    _applicationMenuData = new MenuButtonData(true);
                }
                return _applicationMenuData;
            }
        }
        private MenuButtonData _applicationMenuData;

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
