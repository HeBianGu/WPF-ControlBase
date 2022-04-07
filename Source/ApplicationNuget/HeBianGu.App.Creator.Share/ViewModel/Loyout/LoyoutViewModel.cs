using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using HeBianGu.Systems.Component;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HeBianGu.App.Creator
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {
        private ObservableCollection<License> _Licenses = new ObservableCollection<License>();
        /// <summary> 说明  </summary>
        public ObservableCollection<License> Licenses
        {
            get { return _Licenses; }
            set
            {
                _Licenses = value;
                RaisePropertyChanged("Licenses");
            }
        }

        private License _selectedLicense;
        /// <summary> 说明  </summary>
        public License SelectedLicense
        {
            get { return _selectedLicense; }
            set
            {
                _selectedLicense = value;
                RaisePropertyChanged("SelectedLicense");
            }
        }

        private ObservableCollection<ComponentAttribute> _componentGroup = new ObservableCollection<ComponentAttribute>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ComponentAttribute> ComponentGroup
        {
            get { return _componentGroup; }
            set
            {
                _componentGroup = value;
                RaisePropertyChanged("Collection");
            }
        }

        private WorkflowProject _selectedProject;
        /// <summary> 说明  </summary>
        public WorkflowProject SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged();
            }
        }

        protected override void Init()
        {
            //  Do ：许可
            for (int i = 0; i < 10; i++)
            {
                License license = new License
                {
                    ModuleName = "模块" + i,
                    State = "未激活",
                    Date = string.Empty
                };
                Licenses.Add(license);
            }

            ComponentGroup = ComponentService.GetAssemblys()?.Select(l => l.GetCustomAttribute<ComponentAttribute>())?.ToObservable();

        }

        protected override void Loaded(string args)
        {

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.Click.SaveFileDialog")
            {

            }
        }

    }

    internal class Connect
    {
        [Required]
        [Display(Name = "连接地址")]
        public string IP { get; set; }

        [Required]
        [Display(Name = "连接地址")]
        public string Port { get; set; }
    }

    internal class License : NotifyPropertyChanged
    {

        private string _moduleName;
        /// <summary> 说明  </summary>
        public string ModuleName
        {
            get { return _moduleName; }
            set
            {
                _moduleName = value;
                RaisePropertyChanged("ModuleName");
            }
        }

        private string _state;
        /// <summary> 说明  </summary>
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("Value");
            }
        }

        private string _date;
        /// <summary> 说明  </summary>
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }
    }
}
