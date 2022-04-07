// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Panel
{
    public class EffectBox : ContentControl
    {
        public static ComponentResourceKey CircleWaveKey => new ComponentResourceKey(typeof(EffectBox), "S.EffectBox.CircleWave");
        public static ComponentResourceKey HeartBeatKey => new ComponentResourceKey(typeof(EffectBox), "S.EffectBox.HeartBeat");


        static EffectBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EffectBox), new FrameworkPropertyMetadata(typeof(EffectBox)));
        }

    }
}
