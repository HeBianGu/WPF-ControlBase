// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Systems.Setting
{
    public class SettingViewPresenter : ISettingViewPresenter
    {
        public bool Invoke()
        {
            SettingDialog setting = new SettingDialog();

            Message.Instance.ShowLayer(setting);

            return true;
        }
    }
}
