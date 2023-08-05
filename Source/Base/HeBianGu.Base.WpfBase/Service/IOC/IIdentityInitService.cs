// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface IIdentityInitService
    {
        bool Init(out string error);
    }

    public class IdentityIniter : ServiceInstance<IIdentityInitService>
    {

    }
}