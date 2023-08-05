// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading.Tasks;

namespace HeBianGu.Control.MessageContainer
{
    public abstract class AutoCloseMessage : MessageBase
    {
        public AutoCloseMessage()
        {
            this.Loaded += (l, k) =>
            {
                Task.Delay(MessageContainerSetting.Instance.HideDuration).ContinueWith(m => this.Close());
            };
        }
    }
}
