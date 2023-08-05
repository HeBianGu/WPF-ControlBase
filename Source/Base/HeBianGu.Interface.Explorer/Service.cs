// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace HeBianGu.Interface.Explorer
{
    public interface IExplorerService : IDisposable
    {
        void Init();
        IEnumerable<IDirectoryInfo> GetDirectories(IDirectoryInfo dir);
        IEnumerable<IFileInfo> GetFiles(IDirectoryInfo dir);
        IEnumerable<IDirectoryInfo> GetDrives();
        IDirectoryInfo CreateDirectoryInfo(string fullName);
        bool ExistsDirectoryInfo(IDirectoryInfo dir);
        IDirectoryInfo GetParentDirectory(IDirectoryInfo dir);
    }

    public interface IDirectoryInfo : ISystemFileInfo
    {

    }
    public interface IFileInfo : ISystemFileInfo
    {

    }

    public interface ISystemFileInfo
    {
        string Name { get; }
        string FullName { get; }
    }
}
