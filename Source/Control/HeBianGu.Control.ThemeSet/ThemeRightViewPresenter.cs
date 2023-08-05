// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using HeBianGu.Service.Animation;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Control.ThemeSet
{
    public interface IThemeRightToolViewPresenterOption : IMvpSettingOption
    {

    }

    public interface IThemeRightToolViewPresenter : IInvokePresenter
    {

    }

    [Displayer(Name = "主题设置", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Theme, Description = "点击设置软件主题")]
    public class ThemeRightToolViewPresenter : ServiceMvpSettingBase<ThemeRightToolViewPresenter, IThemeRightToolViewPresenter>, IThemeRightToolViewPresenter, IThemeRightToolViewPresenterOption
    {
        public override bool Invoke(out string message)
        {
            message = null;
            MessageProxy.Presenter.ShowClose(ThemeRightViewPresenter.Instance, null, ThemeRightViewPresenter.Instance.Name, x =>
            {
                var trans = TranslateTransition.GetDockRight();
                ContainPanel.SetTransition(x, trans);
                x.Padding = new System.Windows.Thickness(0);
                x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
                x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                Cattach.SetCornerRadius(x, new System.Windows.CornerRadius(0));
            });
            return true;
        }
    }

    public interface IThemeRightViewPresenterOption : IMvpSettingOption
    {

    }

    public interface IThemeRightViewPresenter : IInvokePresenter
    {

    }
    [Displayer(Name = "主题设置", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Theme, Description = "点击设置软件主题")]
    public class ThemeRightViewPresenter : ServiceMvpSettingBase<ThemeRightViewPresenter, IThemeRightViewPresenter>, IThemeRightViewPresenter, IThemeRightViewPresenterOption
    {

    }
}
