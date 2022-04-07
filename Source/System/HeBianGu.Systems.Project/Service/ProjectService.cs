// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.Systems.Project
{
    public class ProjectService : ProjectService<ProjectItem>
    {

    }

    public class ProjectService<T> : NotifyPropertyChangedBase, IProjectService, IProjectConfig where T : class, IProjectItem
    {
        public static ProjectService<T> Instance { get; } = ServiceRegistry.Instance.GetInstance<IProjectService>() as ProjectService<T>;

        private ObservableCollection<T> _projects = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Projects
        {
            get { return _projects; }
            internal set
            {
                _projects = value;
                RaisePropertyChanged("Projects");
            }
        }

        private T _current;
        /// <summary> 说明  </summary>
        public T Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 工程扩展名
        /// </summary>
        public string Extenstion { get; set; }

        /// <summary>
        /// 工程保存的时机
        /// </summary>
        public ProjectSaveMode SaveMode { get; set; }

        public virtual void Add(T project)
        {
            this.Projects.Add(project);

            this.OnItemChanged();
        }

        public virtual void Delete(Func<T, bool> func)
        {
            this.Projects.RemoveAll(func);

            this.OnItemChanged();
        }

        private void OnItemChanged()
        {
            if (this.SaveMode == ProjectSaveMode.OnProjectChanged)
            {
                this.Save(out string message);
            }
        }

        private ISerializerService XmlSerializer => ServiceRegistry.Instance.GetInstance<ISerializerService>();


        public virtual string GetPath()
        {
            return System.IO.Path.Combine(SystemPathSetting.Instance.Project, "histroy.xml"); ;
        }

        public virtual void Load()
        {
            string path = this.GetPath();

            List<T> ps = XmlSerializer?.Load<List<T>>(path);

            if (ps == null)
            {
                this.Projects.Clear();
            }
            else
            {
                this.Projects = ps?.ToObservable();
            }

            this.Current = this.Projects?.FirstOrDefault();
        }

        public virtual bool Save(out string message)
        {
            message = null;


            try
            {
                XmlSerializer?.Save(this.GetPath(), this.Projects?.ToList());

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
