// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Systems.Identity
{
    public class IMultidentityInitService : IIdentityInitService
    {
        public bool Init(out string error)
        {
            error = null;
            MultiLoginWindow loginWindow = new MultiLoginWindow();
            loginWindow.ShowDialog();
            if (loginWindow.Result == false && IdentifySetting.Instance.UseVisitor)
            {
                return true;
            }
            return loginWindow.Result == true;
        }
    }
}
