// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class SuccessMessage : AutoCloseMessage
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SuccessMessage), "S.SuccessMessage.Default");

        static SuccessMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SuccessMessage), new FrameworkPropertyMetadata(typeof(SuccessMessage)));
        }
    }
}
