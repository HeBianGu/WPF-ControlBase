// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Systems.Setting
{
    public partial class SettingBox : TabControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SettingBox), "S.SettingBox.Default");

        static SettingBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingBox), new FrameworkPropertyMetadata(typeof(SettingBox)));
        }

        public SettingBox()
        {
            this.ItemsSource = SystemDisplay.Instance.Settings?.Where(x => x.IsVisibleInSetting)?.GroupBy(l => l.GroupName);
        }
    }
}
