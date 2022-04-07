// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenEffect : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Default");
        public static ComponentResourceKey ScaleSunKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.ScaleSun");
        public static ComponentResourceKey ScaleKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Scale");
        public static ComponentResourceKey ScaleRotateKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.ScaleRotate");

        public static ComponentResourceKey UpKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Up");
        public static ComponentResourceKey DownKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Down");
        public static ComponentResourceKey RotateKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Rotate");
        public static ComponentResourceKey OpacityKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.Opacity");
        public static ComponentResourceKey ToRightKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.ToRight");
        public static ComponentResourceKey ToLeftKey => new ComponentResourceKey(typeof(ScreenEffect), "S.ScreenEffect.ToLeft");

        static ScreenEffect()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenEffect), new FrameworkPropertyMetadata(typeof(ScreenEffect)));
        }
    }
}
