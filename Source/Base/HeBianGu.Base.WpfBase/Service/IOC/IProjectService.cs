// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace HeBianGu.Base.WpfBase
{
    public interface IProjectItem
    {
        DateTime UpdateTime { get; set; }
        bool IsFixed { get; set; }
        string Title { get; set; }
        string Path { get; set; }
    }

    public interface IProjectService : ISave, ILoad, IProjectOption
    {
        IProjectItem Current { get; set; }
        IProjectItem Create();
        void Add(IProjectItem project);
        void Delete(Func<IProjectItem, bool> func);
        IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null);
        Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }
    }

    public interface IProjectOption
    {
        string Extenstion { get; set; }

        ProjectSaveMode SaveMode { get; set; }
    }

    public enum ProjectSaveMode
    {
        OnAppExit = 0, OnProjectChanged
    }

    public class ProjectProxy : ServiceInstance<IProjectService>
    {

    }

    public interface IDataBaseInitService : ILoad
    {
        /// <summary>
        /// 初始化启动尝试连接
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool TryConnect(out string message);
    }


    public interface ILicenseInitService : ILoad
    {

    }
}