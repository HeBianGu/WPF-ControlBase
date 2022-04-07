// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Systems.Project
{
    public interface IProjectItem
    {
        DateTime UpdateTime { get; set; }

        bool IsFixed { get; set; }

        string Title { get; set; }

        string Path { get; set; }
    }
}
