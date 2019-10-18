using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfControlLib
{
    public class WindowLinkViewModel : LoginViewModel
    {
        private ObservableCollection<Link> _settingLinks = new ObservableCollection<Link>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Link> SettingLinks
        {
            get { return _settingLinks; }
            set
            {
                _settingLinks = value;
                RaisePropertyChanged("SettingLinks");
            }
        }

        private ObservableCollection<Link> _tabLinks = new ObservableCollection<Link>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Link> TabLinks
        {
            get { return _tabLinks; }
            set
            {
                _tabLinks = value;
                RaisePropertyChanged("TabLinks");
            }
        }


    }
}
