using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.Generic;

namespace HeBianGu.Systems.WinTool
{
    public interface IRecenterViewPresenter : IInvokePresenter
    {

    }

    public interface IRecenterViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "最近访问的文件", GroupName = SystemSetting.GroupSystem, Icon = "\xe84d", Description = "应用此功能可以快速访问最近访问的文件")]
    public class RecenterViewPresenter : FolderViewPresenterBase<RecenterViewPresenter, IRecenterViewPresenter>, IRecenterViewPresenter, IRecenterViewPresenterOption
    {
        protected override IEnumerable<string> CreateFolders()
        {
            yield return Environment.GetFolderPath(Environment.SpecialFolder.Recent);
        }

        protected override string CreateExtensions()
        {
            return null;
        }

        public override bool Invoke(out string message)
        {
            message = null;
            return true;
        }
    }
}
