using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.App.Touch
{
    [ViewModel("Setting")]
    internal class SettingViewModel : MvcViewModelBase
    {
        private IAssemblyDomain _domain = null;

        public SettingViewModel(IAssemblyDomain domain)
        {
            _domain = domain;
        }

        protected override void Init()
        {
            LinkActions.Clear();
            LinkActions.Add(new LinkAction() { DisplayName = "主题设置", Action = "Theme", Controller = "Setting" });
            LinkActions.Add(new LinkAction() { DisplayName = "基本设置", Action = "Basic", Controller = "Setting" });
            LinkActions.Add(new LinkAction() { DisplayName = "设备设置", Action = "Device", Controller = "Setting" });
            LinkActions.Add(new LinkAction() { DisplayName = "高级设置", Action = "Advance", Controller = "Setting" });
            LinkActions.Add(new LinkAction() { DisplayName = "数据上传", Action = "UpLoad", Controller = "Setting" });
        }


        private ObservableCollection<LinkAction> _linkActions = new ObservableCollection<LinkAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LinkAction> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged("LinkActions");
            }
        }

        protected override void Loaded(object args)
        {
            SelectLink = LinkActions.FirstOrDefault();
        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);
        protected void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.Save")
            {
                _domain.GoToLinkAction("Login", "Login", new object[] { false });
            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }
}
