using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfMvc
{

    /// <summary> Mvc模式所有ViewModel的基类 </summary>
    public class MvcViewModelBase : NotifyPropertyChanged, IMvcViewModelBase
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

        /// <summary> 获取控制器名称 </summary>
        public string GetController()
        {
            var results = this.GetType().GetCustomAttributes(typeof(ViewModelAttribute), true);

            return (results?.FirstOrDefault() as ViewModelAttribute)?.Name;
        }

        /// <summary> 获取当前LinkAction </summary>
        public ILinkActionBase GetLinkAction(string action)
        {
            ILinkActionBase link = new LinkActionBase();
            link.Controller = this.GetController();
            link.Action = action;
            link.DisplayName = action;
            return link;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void GoToLink(string action)
        {
            this.SelectLink = GetLinkAction(action);
        }

        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        protected virtual void Loaded(string args)
        {
            this.GoToLink(args ?? "List");
        }

        protected virtual bool CanLoaded(string args)
        {
            return true;
        }

        private ObservableCollection<ILinkActionBase> _navigation = new ObservableCollection<ILinkActionBase>();
        /// <summary> 导航索引  </summary>
        public ObservableCollection<ILinkActionBase> Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                RaisePropertyChanged("Navigation");
            }
        }

        /// <summary> 异步运行 </summary>
        public async void RunAsync(Action action)
        {
           await Task.Run(action);
        }

        /// <summary> 使用主线程运行 </summary>
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }

    /// <summary> 带有封装好集合实体的基类 </summary>
    public class MvcEntityViewModelBase<M> : MvcViewModelBase, IMvcEntityViewModelBase<M> where M : new()
    {
        private ObservableCollection<M> _collection = new ObservableCollection<M>();
        /// <summary> 实体集合  </summary>
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
        /// <summary> 要添加的实体  </summary>
        public M AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }

        private M _selectedItem;
        /// <summary> 选中的实体  </summary>
        public M SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcViewModelBase<R, M> : MvcEntityViewModelBase<M> where M : new()
    {
        public R Respository { get; set; } = ServiceRegistry.Instance.GetInstance<R>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcViewModelBase<R1, R2, M> : MvcViewModelBase<R1, M> where M : new()
    {
        public R2 Respository2 { get; set; } = ServiceRegistry.Instance.GetInstance<R2>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcViewModelBase<R1, R2, R3, M> : MvcViewModelBase<R1, R2, M> where M : new()
    {
        public R3 Respository3 { get; set; } = ServiceRegistry.Instance.GetInstance<R3>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcViewModelBase<R1, R2,R3,R4, M> : MvcViewModelBase<R1, R2, R3, M> where M : new()
    {
        public R4 Respository4 { get; set; } = ServiceRegistry.Instance.GetInstance<R4>();
    }

}
