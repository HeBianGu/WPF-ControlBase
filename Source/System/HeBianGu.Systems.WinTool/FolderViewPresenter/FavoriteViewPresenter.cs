using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace HeBianGu.Systems.WinTool
{
    public interface IFavoriteViewPresenter : IInvokePresenter
    {

    }

    public interface IFavoriteViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "收藏夹", GroupName = SystemSetting.GroupSystem, Icon = "\xe602", Description = "应用此功能可以快速查看浏览器收藏夹")]
    public class FavoriteViewPresenter : FolderViewPresenterBase<FavoriteViewPresenter, IFavoriteViewPresenter>, IFavoriteViewPresenter, IFavoriteViewPresenterOption
    {
        protected override IEnumerable<string> CreateFolders()
        {
            yield return Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
        }

        protected override string CreateExtensions()
        {
            return "url";
        }
        public override bool Invoke(out string message)
        {
            message = null;
            return true;
        }
    }
}
