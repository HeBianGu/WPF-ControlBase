// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Service.Mvc
{
    public abstract class MvcViewModelBase : DisplayerViewModelBase, IMvcViewModelBase, IAuthority
    {
        public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.ID) != false;

        private ILinkAction _selectLink;
        /// <summary> 说明  </summary>
        public ILinkAction SelectLink
        {
            get { return _selectLink; }
            set
            {
                _selectLink = value;
                RaisePropertyChanged("SelectLink");
                MvcProxy.Instance.History.Push(_selectLink);
            }
        }

        private ObservableCollection<ILinkAction> _linkActions = new ObservableCollection<ILinkAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ILinkAction> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 跳转到Link页面 </summary>
        public RelayCommand<ILinkAction> GoToLinkCommand => new Lazy<RelayCommand<ILinkAction>>(() => new RelayCommand<ILinkAction>(GoToLink, CanGoToLink)).Value;

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GoToLink(ILinkAction args)
        {
            args.Controller = args.Controller ?? this.GetController();

            this.SelectLink = args;
        }

        private bool CanGoToLink(ILinkAction args)
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
                  LinkAction linkAction = new LinkAction() { Controller = controller, Action = action };

                  return Mvc.Instance.DoActionResult(linkAction);
              });
        }

        private bool CanDoAction(string args)
        {
            return true;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        protected void GoToLink(string controller, string action)
        {
            ILinkAction link = new LinkAction();
            link.Controller = controller;
            link.Action = action;
            this.SelectLink = link;
        }

        /// <summary> 获取控制器名称 </summary>
        public string GetController()
        {
            object[] results = this.GetType().GetCustomAttributes(typeof(ViewModelAttribute), true);

            return (results?.FirstOrDefault() as ViewModelAttribute)?.Name;
        }

        /// <summary> 获取当前LinkAction </summary>
        public ILinkAction GetLinkAction(string action)
        {
            ILinkAction link = new LinkAction();
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

        protected virtual void Loaded(string args)
        {
            if (!string.IsNullOrEmpty(args))
            {
                this.GoToLink(args);
            }
        }

        protected virtual bool CanLoaded(string args)
        {
            return true;
        }

        private ObservableCollection<ILinkAction> _navigation = new ObservableCollection<ILinkAction>();
        /// <summary> 导航索引  </summary>
        public ObservableCollection<ILinkAction> Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                RaisePropertyChanged("Navigation");
            }
        }

        /// <summary> 异步运行 </summary>
        public void RunAsync(Action action)
        {
            Task.Run(action);
        }

        /// <summary> 使用主线程运行 </summary>
        public void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        ///// <summary> 验证实体模型是否可用 </summary>
        //protected bool ModelState(object obj, out string message)
        //{
        //    var result = ObjectPropertyFactory.ModelState(obj, out List<string> errors);

        //    message = errors.FirstOrDefault();

        //    return result;
        //}
    }


}
