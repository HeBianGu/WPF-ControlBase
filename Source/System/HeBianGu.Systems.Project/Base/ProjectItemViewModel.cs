// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Project
{
    public class ProjectItemViewModel : SelectViewModel<IProjectItem>
    {
        public ProjectItemViewModel(IProjectItem project) : base(project)
        {
        }

        private string _groupName;
        /// <summary> 说明  </summary>
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

    }
}
