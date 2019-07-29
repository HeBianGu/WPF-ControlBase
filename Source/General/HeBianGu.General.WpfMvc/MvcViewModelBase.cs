using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            args.Controller = args.Controller ?? this.GetController();

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


        public string GetController()
        {
            var results = this.GetType().GetCustomAttributes(typeof(ViewModelAttribute), true);

            return results?.FirstOrDefault()?.Cast<ViewModelAttribute>().Name;
        }

        protected void GoToLink(string action)
        {
            ILinkActionBase link = new LinkActionBase();
            link.Controller = this.GetController();
            link.Action = action;
            this.SelectLink = link;
        }

        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            this.GoToLink(args?? "List");
        }

        private bool CanLoaded(string args)
        {
            return true;
        }
    }



    public class MvcEntityViewModelBase<M> : MvcViewModelBase where M : new()
    {
        private ObservableCollection<M> _collection = new ObservableCollection<M>();
        /// <summary> 说明  </summary>
        public ObservableCollection<M> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private M _addItem = new M();
        /// <summary> 说明  </summary>
        public M AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }


        private M _seletItem;
        /// <summary> 说明  </summary>
        public M SeletItem
        {
            get { return _seletItem; }
            set
            {
                _seletItem = value;
                RaisePropertyChanged("SeletItem");
            }
        }

    }


    public class MvcViewModelBase<R, M> : MvcEntityViewModelBase<M> where M : new()
    {
        public R Respository { get; set; } = ServiceRegistry.Instance.GetInstance<R>();
    }

}
