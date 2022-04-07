// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    public class PropertyGridTemplateKeys
    {
        public static ComponentResourceKey Text => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.TextPropertyItem.Default");
        public static ComponentResourceKey DateTime => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.DateTimePropertyItem.Default");
        public static ComponentResourceKey Bool => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.BoolPropertyItem.Default");
        public static ComponentResourceKey Enum => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.EnumPropertyItem.Default");

        public static ComponentResourceKey Int => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.IntPropertyItem.Default");
        public static ComponentResourceKey Double => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.DoublePropertyItem.Default");
        public static ComponentResourceKey Object => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.ObjectPropertyItem.Default");
        public static ComponentResourceKey IEnumerable => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.IEnumerablePropertyItem.Default");
        public static ComponentResourceKey Primitives => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.PrimitivesPropertyItem.Default");
        public static ComponentResourceKey SelectSource => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.SelectSourcePropertyItem.Default");
        public static ComponentResourceKey Command => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.CommandPropertyItem.Default");
    }

    public class PropertyViewTemplateKeys
    {
        public static ComponentResourceKey Text => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.TextPropertyItem.View");
        public static ComponentResourceKey DateTime => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.DateTimePropertyItem.View");
        public static ComponentResourceKey Bool => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.BoolPropertyItem.View");
        public static ComponentResourceKey Enum => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.EnumPropertyItem.View");

        public static ComponentResourceKey Int => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.IntPropertyItem.View");
        public static ComponentResourceKey Double => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.DoublePropertyItem.View");
        public static ComponentResourceKey Object => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.ObjectPropertyItem.View");
        public static ComponentResourceKey IEnumerable => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.IEnumerablePropertyItem.View");
        public static ComponentResourceKey Primitives => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.PrimitivesPropertyItem.View");
        public static ComponentResourceKey SelectSource => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.SelectSourcePropertyItem.View");
        public static ComponentResourceKey Command => new ComponentResourceKey(typeof(PropertyGridTemplateKeys), "S.DataTemplate.CommandPropertyItem.View");
    }

    public class PropertyTabTemplateKeys
    {
        public static ComponentResourceKey Text => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.TextPropertyItem.PropertyTabControl");
        public static ComponentResourceKey DateTime => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.DateTimePropertyItem.PropertyTabControl");
        public static ComponentResourceKey Bool => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.BoolPropertyItem.PropertyTabControl");
        public static ComponentResourceKey Enum => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.EnumPropertyItem.PropertyTabControl");
        public static ComponentResourceKey Command => new ComponentResourceKey(typeof(PropertyTabTemplateKeys), "S.DataTemplate.CommandPropertyItem.Office");
    }
}
