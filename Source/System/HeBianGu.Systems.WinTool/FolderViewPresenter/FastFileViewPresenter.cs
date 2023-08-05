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

namespace HeBianGu.Systems.WinTool
{
    public interface IFastFileViewPresenter : IInvokePresenter
    {

    }

    public interface IFastFileViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "收藏文件", GroupName = SystemSetting.GroupSystem, Icon = "\xe7f1", Description = "应用此功能可以显示自定义收藏的文件")]
    public class FastFileViewPresenter : FolderViewPresenterBase<FastFileViewPresenter, IFastFileViewPresenter>, IFastFileViewPresenter, IFastFileViewPresenterOption
    {
        public FastFileViewPresenter()
        {
            for (int i = 0; i < 40; i++)
            {
                this.Collection.Add(@"D:\Document\screen1.png");
            }
        }

        public override bool Invoke(out string message)
        {
            message = null;
            return true;
        }
    }
}
