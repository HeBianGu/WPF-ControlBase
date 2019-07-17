using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [ViewModel("Tab")]
    class TabViewModel : MvcViewModelBase
    { 
        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() =>
    new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            LinkAction link = new LinkAction();
            link.DisplayName = "AnimatedTabControl";
            link.Logo = "&#xe69f;";
            link.Controller = "Tab";
            link.Action = "AnimatedTab"; 
            this.SelectLink = link;
        }


        private bool CanLoaded(string args)
        {
            return true;
        } 
    }
}
