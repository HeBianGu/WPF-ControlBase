using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfControlLib
{

   public abstract class WindowBaseNotifyPropertyChanged : NotifyPropertyChanged
    {
        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        protected virtual void Loaded(string args)
        {

        }

        protected virtual bool CanLoaded(string args)
        {
            return true;
        }
    }
}
