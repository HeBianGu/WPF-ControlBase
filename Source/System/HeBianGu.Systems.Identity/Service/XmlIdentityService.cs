// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HeBianGu.Systems.Identity
{
    internal class XmlIdentityService : IIdentityService
    {
        public string Path { get; } = System.IO.Path.Combine(SystemPathSetting.Instance.DefaultPath, nameof(IdentityService) + ".xml");

        private List<IdentityData> _datas = new List<IdentityData>();

        public XmlIdentityService()
        {
            _datas = XmlSerialize.Instance.Load<List<IdentityData>>(this.Path);
        }

        private Random random = new Random();

        public IUser User { get; private set; }

        private IOperationService Operation => ServiceRegistry.Instance.GetInstance<IOperationService>();

        [Operation("3B11D81A-5201-40DB-95A3-0155C4D4B4BC")]
        public bool Login(string name, string password, out string message)
        {
            if (string.IsNullOrEmpty(name))
            {
                message = "用户名不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = "密码不能为空";
                return false;
            }

            IdentityData user = this._datas?.FirstOrDefault(l => l.UserName == name);

            if (user == null)
            {
                message = "用户名不存在，请先注册";
                return false;
            }

            user = this._datas.FirstOrDefault(l => l.UserName == name && l.Password == password);

            if (user == null)
            {
                message = "密码不正确";
                return false;
            }

            bool result = random.Next(5) != 1;
            Thread.Sleep(1000);
            message = "登录成功";
            this.User = new User() { Name = name };
            this.Operation?.Log("用户登录成功", message);
            return result;
        }

        public bool Register(string phone, string password, out string message)
        {
            if (string.IsNullOrEmpty(phone))
            {
                message = "用户名不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = "密码不能为空";
                return false;
            }

            IdentityData user = this._datas?.FirstOrDefault(l => l.UserName == phone);

            if (user != null)
            {
                message = "用户名已经注册";
                return false;
            }

            if (this._datas == null)
                this._datas = new List<IdentityData>();

            IdentityData data = new IdentityData() { UserName = phone, Password = password };

            this._datas.Add(data);
            XmlSerialize.Instance.Save(this.Path, this._datas);

            Thread.Sleep(1000);
            message = "注册成功，请重新登录";
            this.Operation?.Log("注册用户成功", message);
            return true;
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

    public class IdentityData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
