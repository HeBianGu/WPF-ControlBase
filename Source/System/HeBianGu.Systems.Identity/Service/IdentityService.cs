// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Threading;

namespace HeBianGu.Systems.Identity
{
    internal class IdentityService : IIdentityService
    {
        private Random random = new Random();

        public IUser User { get; private set; }

        private IOperationService Operation => ServiceRegistry.Instance.GetInstance<IOperationService>();

        [Operation("3B11D81A-5201-40DB-95A3-0155C4D4B4BC")]
        public bool Login(string name, string password, out string message)
        {
            if (string.IsNullOrEmpty(name))
            {
                message = "用户名不能为空，请任意输入用户名";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = "密码不能为空，请任意输入密码";
                return false;
            }

            bool result = random.Next(5) != 1;
            Thread.Sleep(2000);
            message = "随机测试失败,再试一下";
            this.User = new User() { Name = name };

            this.Operation?.Log(result.ToString(), message);
            return result;
        }

        public bool Register(string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            Thread.Sleep(2000);
            message = "当前手机号已经注册";
            this.Operation?.Log(result.ToString(), message);
            return result;
        }

        public bool ResetPassword(string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            Thread.Sleep(2000);
            message = "操作超时";
            this.Operation?.Log(result.ToString(), message);
            return result;
        }

        public string GetPhoneVerificationCode(string phone, out string message)
        {
            message = "操作超时";
            return random.Next(100000, 999999).ToString();
        }

        public string GetVerificationCode(out string message)
        {
            message = "操作超时";
            return random.Next(1000, 9999).ToString();
        }

        public string GetServiceAgreement()
        {
            return "https://blog.csdn.net/u010975589";
        }
        public string GetPrivacypolicy()
        {
            return "https://blog.csdn.net/u010975589";
        }

    }

    public class User : IUser
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
    }
}
