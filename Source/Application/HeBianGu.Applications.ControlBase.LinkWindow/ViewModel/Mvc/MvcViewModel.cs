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
    [ViewModel("Mvc")]
    class MvcViewModel : MvcViewModelBase
    {
        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() =>
new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            string sss=string.Empty;

        }

        private bool CanLoaded(string args)
        {
            return true;
        }
    }
}
