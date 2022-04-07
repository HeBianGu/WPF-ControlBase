// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.Host
{
    public static class HostContianerKeys
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(HostContianerKeys), "S.Container.Default");
        public static ComponentResourceKey LeftKey => new ComponentResourceKey(typeof(HostContianerKeys), "S.Container.Left");
        public static ComponentResourceKey RightKey => new ComponentResourceKey(typeof(HostContianerKeys), "S.Container.Right");
        public static ComponentResourceKey TopKey => new ComponentResourceKey(typeof(HostContianerKeys), "S.Container.Top");
        public static ComponentResourceKey BottomKey => new ComponentResourceKey(typeof(HostContianerKeys), "S.Container.Bottom");
    }
}
