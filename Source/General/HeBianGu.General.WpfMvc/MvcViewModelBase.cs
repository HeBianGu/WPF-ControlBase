using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void Mvc(ILinkActionBase args)
        {

            this.SelectLink = args;
        }

        private bool CanMvc(ILinkActionBase args)
        {
            return true;
        }


        protected void GoToLink(string controller, string action)
        {  
            ILinkActionBase link = new LinkActionBase();
            link.Controller = controller;
            link.Action = action;
            this.SelectLink = link;
        }

        protected void GoToLink(string action)
        {

            var results = this.GetType().GetCustomAttributes(typeof(ViewModelAttribute), true);

            if (results == null) return;

            ILinkActionBase link = new LinkActionBase();
            link.Controller = results.FirstOrDefault().Cast<ViewModelAttribute>().Name;
            link.Action = action;
            this.SelectLink = link;
        }
    }

    public class MvcViewModelBase<T> : MvcViewModelBase
    {
        public T Respository { get; set; } = ServiceRegistry.Instance.GetInstance<T>();

    }

}
