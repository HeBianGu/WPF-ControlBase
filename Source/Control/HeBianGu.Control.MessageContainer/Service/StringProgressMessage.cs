// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    public class StringProgressMessage : MessageBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(StringProgressMessage), "S.StringProgressMessage.Default");

        static StringProgressMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StringProgressMessage), new FrameworkPropertyMetadata(typeof(StringProgressMessage)));
        }

    }
}
