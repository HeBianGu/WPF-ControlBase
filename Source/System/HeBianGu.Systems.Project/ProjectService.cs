// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Project
{
    public class ProjectService : ProjectService<ProjectService, IProjectService>, IProjectService
    {
        protected override void Load(string path)
        {
            base.Load(path);

            string b = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Project");
            if (Directory.Exists(b) == false)
                return;

            var files = Directory.GetFiles(path).Where(x => x.EndsWith(this.Extenstion));
            foreach (var file in files)
            {
                if (this.Projects.Any(x => x.Path == file)) continue;
                this.Projects.Add(new ProjectItem()
                {
                    Title = Path.GetFileNameWithoutExtension(file),
                    Path = file,
                    CreateTime = File.GetCreationTime(file),
                    UpdateTime = File.GetLastWriteTime(file)
                });
            }
        }

        public IProjectItem Create()
        {
            return new ProjectItem() { Path = SystemPathSetting.Instance.Project };
        }
    }

    [Displayer(Name = "工程数据", GroupName = SystemSetting.GroupData, Icon = Icons.Clock)]
    public abstract class ProjectService<Setting, Interface> : ServiceSettingInstance<Setting, Interface>, IProjectOption where Setting : class, Interface, new()
    {
        protected override string GetDefaultFolder()
        {
            return SystemPathSetting.Instance.Project;
        }

        public bool Load(out string message)
        {
            base.Load();
            message = null;
            return true;
        }
        //public string Name => "工程数据";
        //public static ProjectService<T> Instance { get; } = ServiceRegistry.Instance.GetInstance<IProjectService>() as ProjectService<T>;

        private ObservableCollection<IProjectItem> _projects = new ObservableCollection<IProjectItem>();
        [Browsable(false)]
        public ObservableCollection<IProjectItem> Projects
        {
            get { return _projects; }
            internal set
            {
                _projects = value;
                RaisePropertyChanged();
            }
        }

        private IProjectItem _current;
        [XmlIgnore]
        [Browsable(false)]
        public IProjectItem Current
        {
            get { return _current; }
            set
            {
                if (_current == value)
                    return;
                IProjectItem old = _current;
                _current = value;
                RaisePropertyChanged();
                this.OnCurrentChanged(old, _current);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }

        protected virtual void OnCurrentChanged(IProjectItem o, IProjectItem n)
        {
            this.CurrentChanged?.Invoke(o, n);
        }

        private string _extenstion;
        [DefaultValue(".wf")]
        [Display(Name = "扩展名")]
        public string Extenstion
        {
            get { return _extenstion; }
            set
            {
                _extenstion = value;
                RaisePropertyChanged();
            }
        }


        private ProjectSaveMode _saveMode;
        [DefaultValue(ProjectSaveMode.OnProjectChanged)]
        [Display(Name = "保存的时机")]
        public ProjectSaveMode SaveMode
        {
            get { return _saveMode; }
            set
            {
                _saveMode = value;
                RaisePropertyChanged();
            }
        }

        public virtual void Add(IProjectItem project)
        {
            if (this.Projects.Contains(project)) return;
            this.Projects.Add(project);
            this.OnItemChanged();
        }

        public virtual void Delete(Func<IProjectItem, bool> func)
        {
            var finds = this.Projects.Where(func).ToList();
            foreach (var item in finds)
            {
                if (File.Exists(item.Path))
                    File.Delete(item.Path);
                if (!string.IsNullOrEmpty(item.Path))
                {
                    var find = Path.Combine(item.Path, item.Title + "" + this.Extenstion);
                    if (File.Exists(find))
                        File.Delete(find);
                }

                this.Projects.Remove(item);
            }
            this.OnItemChanged();
        }

        public virtual IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null)
        {
            return func == null ? this.Projects : this.Projects.Where(func);
        }

        protected virtual void OnItemChanged()
        {
            if (this.SaveMode == ProjectSaveMode.OnProjectChanged)
            {
                this.Save(out string message);
            }
        }

        public virtual string GetPath()
        {
            return System.IO.Path.Combine(SystemPathSetting.Instance.Project, "histroy.xml"); ;
        }
    }
}
