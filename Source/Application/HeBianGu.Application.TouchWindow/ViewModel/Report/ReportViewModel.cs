using HeBianGu.Base.WpfBase;
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


namespace HeBianGu.Application.TouchWindow
{
    [ViewModel("Report")]
    class ReportViewModel : MvcViewModelBase
    {
        IAssemblyDomain _domain = null;

        public ReportViewModel(IAssemblyDomain domain)
        {
            _domain = domain;
        }


        private bool _runNextEngine;
        /// <summary> 说明  </summary>
        public bool RunNextEngine
        {
            get { return _runNextEngine; }
            set
            {
                _runNextEngine = value;
                RaisePropertyChanged("RunNextEngine");
            }
        }

        private ObservableCollection<LinkActionEntity> _linkActions = new ObservableCollection<LinkActionEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<LinkActionEntity> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged("LinkActions");
            }
        }

        protected override void Init()
        {

        }


        protected override void Loaded(string args)
        {

        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.LogOut")
            {
                _domain.GoToLinkAction("Login", "Login", new object[] { true });
            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }
}
