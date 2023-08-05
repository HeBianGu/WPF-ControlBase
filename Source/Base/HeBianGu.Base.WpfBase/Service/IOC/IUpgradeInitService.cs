// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface IUpgradeInitService : ILoad
    {
        //bool Init(VersionData data, out string error);

        bool CheckUpgrade(out string message);
    }

    public class UpgradeIniter : ServiceInstance<IUpgradeInitService>
    {

    }
}