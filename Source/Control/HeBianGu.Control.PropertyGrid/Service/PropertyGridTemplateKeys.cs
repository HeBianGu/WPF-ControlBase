// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    public class PropertyTabTemplateKeys
    {
        public static ComponentResourceKey Text => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.TextPropertyItem.PropertyTabControl");
        public static ComponentResourceKey DateTime => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.DateTimePropertyItem.PropertyTabControl");
        public static ComponentResourceKey Bool => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.BoolPropertyItem.PropertyTabControl");
        public static ComponentResourceKey Enum => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.EnumPropertyItem.PropertyTabControl");
        public static ComponentResourceKey Command => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.CommandPropertyItem.Office");
    }
}
