// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 隐藏窗口
    /// </summary>
    public interface IHideWindowViewPresenter : IInvokePresenter
    {

    }


    [Displayer(Name = "隐藏窗口", GroupName = SystemSetting.GroupSystem, Icon = Icons.Eye_slash, Description = "点击隐藏窗口到托盘图标，在托盘图标双击唤醒")]
    public class HideWindowViewPresenter : ServiceMvpSettingBase<HideWindowViewPresenter, IHideWindowViewPresenter>, IHideWindowViewPresenter
    {
        public override bool Invoke(out string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Application.Current.MainWindow is IWindowBase window)
                {
                    window.RefreshHide();
                }
            });

            message = null;
            return true;
        }
    }
}
