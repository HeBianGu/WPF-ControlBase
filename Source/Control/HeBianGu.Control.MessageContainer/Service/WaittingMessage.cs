// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class WaittingMessage : MessageBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(WaittingMessage), "S.WaittingMessage.Default");

        static WaittingMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaittingMessage), new FrameworkPropertyMetadata(typeof(WaittingMessage)));
        }

    }
}
