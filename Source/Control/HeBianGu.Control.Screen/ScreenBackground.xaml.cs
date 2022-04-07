// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenBackground : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenBackground), "S.ScreenBackground.Default");

        static ScreenBackground()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenBackground), new FrameworkPropertyMetadata(typeof(ScreenBackground)));
        }
    }
}
