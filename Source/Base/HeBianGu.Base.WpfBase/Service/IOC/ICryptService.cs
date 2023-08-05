// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface ICryptService
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }

    public class CryptProxy : ServiceInstance<ICryptService>
    {

    }
}