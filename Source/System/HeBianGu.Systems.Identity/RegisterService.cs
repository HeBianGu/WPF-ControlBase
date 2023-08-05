// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Threading;

namespace HeBianGu.Systems.Identity
{
    internal class RegisterService : IRegisterService
    {
        private Random random = new Random();
        public bool Register(string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            Thread.Sleep(2000);
            message = "当前手机号已经注册";
            //Operationner.Instance?.Log(result.ToString(), message);
            OparationViewPresenterProxy.Instance?.Log(message, null, null, false);
            return result;
        }

        public bool ResetPassword(string phone, string password, out string message)
        {
            bool result = random.Next(5) == 1;

            Thread.Sleep(2000);
            message = "操作超时";
            //Operationner.Instance?.Log(result.ToString(), message);
            OparationViewPresenterProxy.Instance?.Log(message, message, null, result);
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
    }
}
