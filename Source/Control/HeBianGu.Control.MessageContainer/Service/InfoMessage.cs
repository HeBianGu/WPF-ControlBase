// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class InfoMessage : AutoCloseMessage
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(InfoMessage), "S.InfoMessage.Default");

        static InfoMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoMessage), new FrameworkPropertyMetadata(typeof(InfoMessage)));
        }
    }
}
