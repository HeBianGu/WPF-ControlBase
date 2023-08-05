// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Shapes;
using System.Xml.Linq;
using Path = System.IO.Path;

namespace HeBianGu.Control.Explorer
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [Displayer(Name = "资源管理器配置", GroupName = SystemSetting.GroupControl)]
    public class ExplorerSetting : LazySettingInstance<ExplorerSetting>
    {
        private int _histCapacity = 10;
        [Display(Name = "历史容量")]
        [DefaultValue(10)]
        [Range(1, 30)]
        public int HistCapacity
        {
            get { return _histCapacity; }
            set
            {
                _histCapacity = value;
                RaisePropertyChanged();
            }
        }

    }

    public class WindowExplorerService : IExplorerService
    {
        public IEnumerable<IDirectoryInfo> GetDirectories(IDirectoryInfo dir)
        {
            DirectoryInfo directory = new DirectoryInfo(dir.FullName);
            var dirs = directory.GetDirectories().Where(l => !l.Attributes.HasFlag(FileAttributes.System));
            foreach (var item in dirs)
            {
                yield return new WindowDirectoryInfo(item.FullName);
            }
        }

        public IEnumerable<IFileInfo> GetFiles(IDirectoryInfo dir)
        {
            var dirs = Directory.GetFiles(dir.FullName);
            foreach (var item in dirs)
            {
                yield return new WindowFileInfo(item);
            }
        }

        public IEnumerable<IDirectoryInfo> GetDrives()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                yield return new WindowDirectoryInfo(drive.RootDirectory.FullName);
            }
        }

        public IDirectoryInfo CreateDirectoryInfo(string fullName)
        {
            return new WindowDirectoryInfo(fullName);
        }

        public bool ExistsDirectoryInfo(IDirectoryInfo dir)
        {
            return Directory.Exists(dir.FullName);
        }

        public IDirectoryInfo GetParentDirectory(IDirectoryInfo dir)
        {
            var parent = Path.GetDirectoryName(dir.FullName);
            if (string.IsNullOrEmpty(parent))
                return null;
            return this.CreateDirectoryInfo(Path.GetDirectoryName(dir.FullName));
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class WindowDirectoryInfo : IDirectoryInfo
    {
        private readonly string _fullName;
        public WindowDirectoryInfo(string fullName)
        {
            _fullName = fullName;
        }

        public string FullName => _fullName;
        public string Name => Path.GetPathRoot(_fullName) == _fullName ? _fullName : Path.GetFileName(_fullName);
    }

    public class WindowFileInfo : IFileInfo
    {
        private readonly string _fullName;
        public WindowFileInfo(string fullName)
        {
            _fullName = fullName;
        }

        public string FullName => _fullName;
        public string Name => Path.GetFileName(_fullName);
    }

    public class ExplorerProxy : ServiceInstance<IExplorerService>
    {

    }


}



