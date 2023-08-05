// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Systems.Identity
{
    /// <summary>
    /// 登录
    /// </summary>
    public interface ILoginViewPresenter : IInvokePresenter
    {

    }
    public interface ILoginViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "登录", GroupName = SystemSetting.GroupBase, Icon = "\xe739", Description = "这是一个登录页面的信息")]
    public class LoginViewPresenter : ServiceMvpSettingBase<LoginViewPresenter, ILoginViewPresenter>, ILoginViewPresenter, ILoginViewPresenterOption
    {
        public override bool Invoke(out string message)
        {
            message = null;

            if(LoginManagerViewPresenter.Instance==null)
            {
                message = "没有找到账户管理接口,请先注册";
                return false;
            }
            MessageProxy.Presenter.ShowClose(LoginManagerViewPresenter.Instance, null, LoginManagerViewPresenter.Instance.Name, x =>
            {
                x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
                x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                var trans = TranslateTransition.GetDockRight();
                ContainPanel.SetTransition(x, trans);
                x.Padding = new System.Windows.Thickness(0);
                x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                Cattach.SetCornerRadius(x, new System.Windows.CornerRadius(0));
            });
            return true;
        }
    }
}
