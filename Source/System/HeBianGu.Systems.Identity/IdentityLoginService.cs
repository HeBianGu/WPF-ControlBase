// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;

namespace HeBianGu.Systems.Identity
{
    public class IdentityLoginService : IIdentityInitService
    {
        public bool Init(out string error)
        {
            error = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            if (loginWindow.Result == false && IdentifySetting.Instance.UseVisitor)
            {
                return true;
            }
            return loginWindow.Result == true;
        }
    }
}
