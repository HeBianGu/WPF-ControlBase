// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Windows;

namespace HeBianGu.Systems.Identity
{
    /// <summary>
    /// 退出登录
    /// </summary>
    public interface ILogoutViewPresenter : IInvokePresenter
    {

    }

    public interface ILogoutViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "退出登录", GroupName = SystemSetting.GroupAuthority, Icon = "\xe8a8", Description = "点击此按钮退出登录")]
    public class LogoutViewPresenter : ServiceMvpSettingBase<LogoutViewPresenter, ILogoutViewPresenter>, ILogoutViewPresenter, ILogoutViewPresenterOption
    {
        public override bool Invoke(out string message)
        {
            message = null;

            if (Loginner.Instance == null)
            {
                message = $"没有注册登录接口<{nameof(ILoginService)}>";
                return false;
            }

            if (IdentityIniter.Instance == null)
            {
                message = $"没有注册登录接口<{nameof(IIdentityInitService)}>";
                return false;
            }

            var r = Loginner.Instance.Logout(out message);
            if (!r) return false;


            var tuple= Application.Current.Dispatcher.Invoke(() =>
             {
                 var rustl= IdentityIniter.Instance.Init(out string error);
                 return Tuple.Create(rustl,error);
             });

            message = tuple.Item2;
            return tuple.Item1;
        }
    }
}
