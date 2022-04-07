// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Identity
{
    public class IdentityMultiLoginService : IIdentityInitService
    {
        public bool Init(out string error)
        {
            error = null;

            MultiLoginWindow loginWindow = new MultiLoginWindow();

            loginWindow.ShowDialog();

            return loginWindow.Result == true;
        }
    }
}
