// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Systems.Project
{
    /// <summary>
    /// 用于配置工程信息
    /// </summary>
    public interface IProjectConfig
    {
        string Extenstion { get; set; }

        ProjectSaveMode SaveMode { get; set; }
    }
}