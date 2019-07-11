using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{
   public class MvcViewModelBase : NotifyPropertyChanged
    {
        private ILinkActionBase _selectLink;
        /// <summary> 说明  </summary>
        public ILinkActionBase SelectLink
        {
            get { return _selectLink; }
            set
            {
                _selectLink = value;
                RaisePropertyChanged("SelectLink");
            }
        }

        public RelayCommand<ILinkActionBase> MvcCommand => new Lazy<RelayCommand<ILinkActionBase>>(() =>
    new RelayCommand<ILinkActionBase>(Mvc, CanMvc)).Value;

        private void Mvc(ILinkActionBase args)
        {

            this.SelectLink = args;
        }  

        private bool CanMvc(ILinkActionBase args)
        {
            return true;
        }
         
    }
}
