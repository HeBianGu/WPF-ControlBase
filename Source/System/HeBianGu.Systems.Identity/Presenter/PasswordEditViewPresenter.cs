// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.Windows;

namespace HeBianGu.Systems.Identity
{
    public interface IPasswordEditViewPresenter : IInvokePresenter
    {

    }
    public interface IPasswordEditViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "密码修改", GroupName = SystemSetting.GroupSecurity, Icon = "\xe7dd", Description = "应用此功能进行密码修改")]
    public class PasswordEditViewPresenter : ServiceMvpSettingBase<PasswordEditViewPresenter, IPasswordEditViewPresenter>, IPasswordEditViewPresenter, IPasswordEditViewPresenterOption
    {
        public override bool Invoke(out string message)
        {
            message = null;
            MessageProxy.Presenter.ShowClose(this, null, this.Name, x =>
            {
                x.Margin = new Thickness(10);
            });
            return true;
        }
    }
}
