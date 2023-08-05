// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Upgrade
{
    /// <summary>
    /// 检查更新
    /// </summary>
    public interface IUpgradeViewPresenter : IInvokePresenter
    {

    }

    public interface IUpgradeViewPresenterOption : IMvpSettingOption
    {

    }
    [Displayer(Name = "软件更新", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Cloud_Download, Description = "应用此功能检查软件更新")]
    public class UpgradeViewPresenter : ServiceMvpSettingBase<UpgradeViewPresenter, IUpgradeViewPresenter>, IUpgradeViewPresenter, IUpgradeViewPresenterOption
    {
        public UpgradeViewPresenter()
        {
            this.Flag = new UpgradeFlagViewPresenter();
        }
        public override bool Invoke(out string message)
        {
            if(UpgradeIniter.Instance==null)
            {
                message = "没有注册服务";
                return false;
            }
           return UpgradeIniter.Instance.CheckUpgrade(out message);
        }
    }

    public class UpgradeFlagViewPresenter
    {

    }
}
