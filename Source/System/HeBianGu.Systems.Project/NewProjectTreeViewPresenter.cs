// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Systems.Project
{
    public interface INewProjectTreeViewPresenter : ITreeViewItemPresenter
    {

    }

    public interface INewProjectTreeViewPresenterOption : IMvpSettingOption
    {
        string PredefinePath { get; set; }
    }

    [Displayer(Name = "新建工程", GroupName = SystemSetting.GroupData, Icon = IconAll.QuetionCircle, Description = "应用此功能可以新建工程")]
    public class NewProjectTreeViewPresenter : ServiceMvpSettingBase<NewProjectTreeViewPresenter, INewProjectTreeViewPresenter>, INewProjectTreeViewPresenter, INewProjectTreeViewPresenterOption
    {
        public string PredefinePath { get; set; } = "文件";

    }
}
