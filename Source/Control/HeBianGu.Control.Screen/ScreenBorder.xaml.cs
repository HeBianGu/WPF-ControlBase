// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenBorder : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenBorder), "S.ScreenBorder.Default");
        public static ComponentResourceKey Border1Key => new ComponentResourceKey(typeof(ScreenBorder), "S.ScreenBorder.1");

        static ScreenBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenBorder), new FrameworkPropertyMetadata(typeof(ScreenBorder)));
        }
    }
}
