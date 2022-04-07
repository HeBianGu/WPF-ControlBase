// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class WarnMessage : MessageBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(WarnMessage), "S.WarnMessage.Default");

        static WarnMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WarnMessage), new FrameworkPropertyMetadata(typeof(WarnMessage)));
        }
    }
}
