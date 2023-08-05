// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Service.Mvp
{
    public class MvpKeys
    {
        public static ComponentResourceKey Button => new ComponentResourceKey(typeof(MvpKeys), "S.Button.Mvp.Icon");
        public static ComponentResourceKey ButtonDefault => new ComponentResourceKey(typeof(MvpKeys), "S.Button.Mvp.Default");
        public static ComponentResourceKey ToggleButton => new ComponentResourceKey(typeof(MvpKeys), "S.ToggleButton.Mvp.Default");
        public static ComponentResourceKey MenuItem => new ComponentResourceKey(typeof(MvpKeys), "S.MenuItem.Mvp.Default");
        public static ComponentResourceKey WindowCaption => new ComponentResourceKey(typeof(MvpKeys), "S.ItemsControl.Mvp.WindowCaption");
        public static ComponentResourceKey Status => new ComponentResourceKey(typeof(MvpKeys), "S.ItemsControl.Mvp.Status");
    }
}
