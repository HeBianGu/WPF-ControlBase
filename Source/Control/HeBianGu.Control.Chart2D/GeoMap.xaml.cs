// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.Chart2D
{
    public class GeoMap : ViewLayerGroup
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(GeoMap), "S.GeoMap.Default");
        public static ComponentResourceKey WorldKey => new ComponentResourceKey(typeof(GeoMap), "S.GeoMap.World");
        public static ComponentResourceKey ChinaKey => new ComponentResourceKey(typeof(GeoMap), "S.GeoMap.China");

        static GeoMap()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GeoMap), new FrameworkPropertyMetadata(typeof(GeoMap)));
        }
    }

}
