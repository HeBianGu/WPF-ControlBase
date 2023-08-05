// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class SliderKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Default");
        public static ComponentResourceKey Circle => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Circle");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Accent");
        public static ComponentResourceKey Value => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.WithValue");
        public static ComponentResourceKey Label => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Label");
        public static ComponentResourceKey ThumbDefault => new ComponentResourceKey(typeof(SliderKeys), "S.Slider.Thumb.Default");

    }
}
