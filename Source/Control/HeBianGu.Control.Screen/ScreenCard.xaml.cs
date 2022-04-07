// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenCard : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenCard), "S.ScreenCard.Default");

        static ScreenCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenCard), new FrameworkPropertyMetadata(typeof(ScreenCard)));
        }
    }
}
