// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Project
{


    public interface IProjectViewPresenter : IViewPresenter
    {

    }
    public interface IProjectViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "工程管理", GroupName = SystemSetting.GroupData, Icon = IconAll.QuetionCircle, Description = "应用此功能可以管理工程信息")]
    public class ProjectViewPresenter : ServiceMvpSettingBase<ProjectViewPresenter, IProjectViewPresenter>, IProjectViewPresenter, IProjectViewPresenterOption
    {
        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand NewCommand => new RelayCommand(async (s, e) =>
        {
            var project = ProjectProxy.Instance.Create();
            bool r = await MessageProxy.PropertyGrid.ShowEdit(project, null, "新建工程");
            if (!r) return;
            ProjectProxy.Instance.Add(project);
            ProjectProxy.Instance.Current = project;
        }, (s, e) => ProjectProxy.Instance != null);

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand NewOrListCommand => new RelayCommand(async (s, e) =>
        {
            if (ProjectProxy.Instance.Where().Count() == 0)
            {
                this.NewCommand.Execute(null);
                return;
            }
            this.ListCommand.Execute(null);

        }, (s, e) => ProjectProxy.Instance != null);

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand ListCommand => new RelayCommand(async (s, e) =>
        {
            var project = new ProjectListPresenter();
            project.SelectedItem = ProjectProxy.Instance.Current;
            bool r = await MessageProxy.Presenter.Show(project, null, "选择工程", x =>
            {
                x.MinWidth = 600;
                x.MinHeight = 400;
            });
            if (project.SelectedItem == null)
                return;
            if (r)
            {
                ProjectProxy.Instance.Current = project.SelectedItem;
            }
        }, (s, e) => ProjectProxy.Instance != null && ProjectProxy.Instance.Where().Count() > 0);
    }

    public class ProjectListPresenter : NotifyPropertyChangedBase
    {
        private IProjectItem _selectedItem;
        /// <summary> 说明  </summary>
        public IProjectItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
    }
}
