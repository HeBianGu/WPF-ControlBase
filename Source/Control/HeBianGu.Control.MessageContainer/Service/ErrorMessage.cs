// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class ErrorMessage : MessageBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ErrorMessage), "S.ErrorMessage.Default");

        static ErrorMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ErrorMessage), new FrameworkPropertyMetadata(typeof(ErrorMessage)));
        }
    }
}
