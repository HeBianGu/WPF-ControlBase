// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.Window.Link
{
    public class WindowLinkViewModel : LoginViewModel
    {
        //private ObservableCollection<HeBianGu.Base.WpfBase.Link> _settingLinks = new ObservableCollection<HeBianGu.Base.WpfBase.Link>();
        ///// <summary> 说明  </summary>
        //public ObservableCollection<HeBianGu.Base.WpfBase.Link> SettingLinks
        //{
        //    get { return _settingLinks; }
        //    set
        //    {
        //        _settingLinks = value;
        //        RaisePropertyChanged("SettingLinks");
        //    }
        //}

        private ObservableCollection<ILinkAction> _Links = new ObservableCollection<ILinkAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ILinkAction> Links
        {
            get { return _Links; }
            set
            {
                _Links = value;
                RaisePropertyChanged("Links");
            }
        }


        private ILinkAction _currentLink;
        /// <summary> 说明  </summary>
        public ILinkAction CurrentLink
        {
            get { return _currentLink; }
            set
            {
                _currentLink = value;
                RaisePropertyChanged("CurrentLink");
            }
        }

        public void GoLinkAction(string controller, string action)
        {
            CurrentLink = this.Links.FirstOrDefault(l => l.Controller == controller && l.Action == action);
        }

        public void GoLinkAction(string controller, string action, object[] parameter)
        {
            ILinkAction link = this.Links.FirstOrDefault(l => l.Controller == controller && l.Action == action);

            if (link != null) link.Parameter = parameter;

            //  Do ：如果指向的Link相同则使用刷新
            if (CurrentLink == link)
            {
                CurrentLink = null;
            }

            CurrentLink = link;
        }

    }
}
