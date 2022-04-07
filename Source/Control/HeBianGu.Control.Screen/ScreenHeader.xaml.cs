// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenHeader : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenHeader), "S.ScreenHeader.Default");

        static ScreenHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenHeader), new FrameworkPropertyMetadata(typeof(ScreenHeader)));
        }
    }
}
