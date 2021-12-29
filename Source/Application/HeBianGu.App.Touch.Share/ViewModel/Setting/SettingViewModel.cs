using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Touch
{
    [ViewModel("Setting")]
    internal class SettingViewModel : MvcViewModelBase
    {
        IAssemblyDomain _domain = null;

        public SettingViewModel(IAssemblyDomain domain)
        {
            _domain = domain;
        }

        protected override void Init()
        {
            this.LinkActions.Clear();
            this.LinkActions.Add(new LinkAction() { DisplayName = "主题设置", Action = "Theme", Controller = "Setting" });
            this.LinkActions.Add(new LinkAction() { DisplayName = "基本设置", Action = "Basic", Controller = "Setting" });
            this.LinkActions.Add(new LinkAction() { DisplayName = "设备设置", Action = "Device", Controller = "Setting" });
            this.LinkActions.Add(new LinkAction() { DisplayName = "高级设置", Action = "Advance", Controller = "Setting" });
            this.LinkActions.Add(new LinkAction() { DisplayName = "数据上传", Action = "UpLoad", Controller = "Setting" });
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

        protected override void Loaded(string args)
        {
            this.SelectLink = this.LinkActions.FirstOrDefault();
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

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
