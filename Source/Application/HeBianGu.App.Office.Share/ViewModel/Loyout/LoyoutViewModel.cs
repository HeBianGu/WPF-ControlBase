using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Data;

namespace HeBianGu.App.Office
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


        private ObservableCollection<ProjectNotify> _project = new ObservableCollection<ProjectNotify>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ProjectNotify> Projects
        {
            get { return _project; }
            set
            {
                _project = value;
                RaisePropertyChanged("Projects");
            }
        }


        private ProjectNotify _selectedProject;
        /// <summary> 说明  </summary>
        public ProjectNotify SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                RaisePropertyChanged("SelectedProject");
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
            //  Do ：加载打开

            for (int i = 0; i < 20; i++)
            {
                Project p = new Project()
                {
                    Name = "工程" + i,
                    Type = "类型",
                    Path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments)
                };
                ProjectNotify pn = new ProjectNotify(p)
                {
                    Group = "更早"
                };

                if (i < 5)
                {
                    pn.Group = "今天";
                }

                if (i < 2)
                {
                    pn.Group = "已固定";
                }

                Projects.Add(pn);
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
             {
                 //Do ：分组
                 ICollectionView vw = CollectionViewSource.GetDefaultView(Projects);
                 vw.GroupDescriptions.Clear();
                 vw.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
             });

            SelectedProject = Projects?.FirstOrDefault();
        }

        protected override void Loaded(string args)
        {

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

    /// <summary> 说明</summary>
    internal class ProjectNotify : ModelViewModel<Project>
    {
        public ProjectNotify() : base(new Project())
        {

        }

        public ProjectNotify(Project model) : base(model)
        {

        }
        #region - 属性 -


        private string _group;
        /// <summary> 说明  </summary>
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                RaisePropertyChanged("Group");
            }
        }


        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }

    internal class Project
    {
        [Required]
        [Display(Name = "工程名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "类型")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "地址")]
        public string Path { get; set; }

        [Display(Name = "编辑时间")]
        public DateTime EDate { get; set; } = DateTime.Now;

        [Display(Name = "编辑时间")]
        public DateTime CDate { get; set; } = DateTime.Now;
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
