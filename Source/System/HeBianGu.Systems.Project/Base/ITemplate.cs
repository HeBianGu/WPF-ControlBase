// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Systems.Project
{
    public interface IProjectTemplate
    {
        string Name { get; set; }
    }

    public class ProjectTemplate : IProjectTemplate
    {
        public string Name { get; set; }
    }

}
