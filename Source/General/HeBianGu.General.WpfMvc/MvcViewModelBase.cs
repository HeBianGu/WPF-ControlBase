using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        /// <summary> 跳转到Link页面 </summary>
        public RelayCommand<ILinkActionBase> GoToLinkCommand => new Lazy<RelayCommand<ILinkActionBase>>(() =>
    new RelayCommand<ILinkActionBase>(GoToLink, CanGoToLink)).Value;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GoToLink(ILinkActionBase args)
        {
            args.Controller = args.Controller ?? this.GetController();

            this.SelectLink = args;
        }

        private bool CanGoToLink(ILinkActionBase args)
        {
            return true;
        }

        /// <summary> 跳转到Action页面 </summary>
        public RelayCommand<string> GoToActionCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(GoToAction, CanGoToAction)).Value;

        private void GoToAction(string args)
        {
            this.GoToLink(args);
        }

        private bool CanGoToAction(string args)
        {
            return true;
        }

        /// <summary> 执行异步操作操作 </summary>
        public RelayCommand<string> DoActionCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(DoAction, CanDoAction)).Value;

        private async void DoAction(string args)
        {
            string controller = this.GetController();

            string action = args;

            await Task.Run(() =>
              {
                  return ControllerService.DoActionResult(controller, action);
              });
        }

        private bool CanDoAction(string args)
        {
            return true;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
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
            this.GoToLink(args ?? "List");
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
