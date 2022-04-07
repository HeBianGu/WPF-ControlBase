// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 认证接口
    /// </summary>
    public interface IIdentityService
    {
        IUser User { get; }

        bool Login(string name, string password, out string message);

        bool Register(string phone, string password, out string message);

        string GetPhoneVerificationCode(string phone, out string message);

        string GetVerificationCode(out string message);

        string GetServiceAgreement();

        string GetPrivacypolicy();

        bool ResetPassword(string phone, string password, out string message);
    }

    public class Identity : ServiceInstance<IIdentityService>
    {

    }

    public interface IUser
    {
        string ID { get; set; }

        string Name { get; set; }
    }

    public class IdentifyConfig
    {
        public static IdentifyConfig Instance = new Lazy<IdentifyConfig>(() => new IdentifyConfig()).Value;

        public string ServiceAgreementUri { get; set; }

        public string PrivacypolicyUri { get; set; }

        public string PasswordRegular { get; set; }
    }
}