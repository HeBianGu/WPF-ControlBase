using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace HeBianGu.Systems.WinTool
{
    public interface IExtensionViewPresenter : IInvokePresenter
    {

    }

    public interface IExtensionViewPresenterOption : IMvpSettingOption
    {
        ObservableCollection<string> Folders { get; }

        string Extension { get; set; }
    }


    [Displayer(Name = "扩展工具", GroupName = SystemSetting.GroupSystem, Icon = "\xe734", Description = "应用此功能可以管理扩展的第三方共嗯")]
    public class ExtensionViewPresenter : FolderViewPresenterBase<ExtensionViewPresenter, IExtensionViewPresenter>, IExtensionViewPresenter, IExtensionViewPresenterOption
    {
        public ExtensionViewPresenter()
        {
            this.FileFilter = x => Path.GetFileNameWithoutExtension(x) != Assembly.GetEntryAssembly().GetName().Name;
        }
        protected override IEnumerable<string> CreateFolders()
        {
            yield return AppDomain.CurrentDomain.BaseDirectory;
            yield return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extension");

        }

        public override bool Invoke(out string message)
        {
            message = null;
            return true;
        }

    }
}
