// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 设置表单的默认模板 </summary>
    public class PropertyViewTemplateSelector : PropertyGridDataTemplateSelectorBase
    {
        protected override void InitTempate()
        {
            //this.StringDefaultTempate = Application.Current.FindResource("S.DataTemplate.TextPropertyItem.View") as DataTemplate;
            //this.IntDefaultTempate = Application.Current.FindResource("S.DataTemplate.IntPropertyItem.View") as DataTemplate;

            //this.DoubleDefaultTempate = Application.Current.FindResource("S.DataTemplate.DoublePropertyItem.View") as DataTemplate;
            //this.DateTimeDefaultTempate = Application.Current.FindResource("S.DataTemplate.DateTimePropertyItem.View") as DataTemplate;
            //this.BoolDefaultTempate = Application.Current.FindResource("S.DataTemplate.BoolPropertyItem.View") as DataTemplate;
            //this.ObjectDefaultTempate = Application.Current.FindResource("S.DataTemplate.ObjectPropertyItem.View") as DataTemplate;
            //this.EnumDefaultTempate = Application.Current.FindResource("S.DataTemplate.EnumPropertyItem.View") as DataTemplate;
            //this.IEnumerableDefaultTempate = Application.Current.FindResource("S.DataTemplate.IEnumerablePropertyItem.View") as DataTemplate;
            //this.PrimitivesPropertyItem = Application.Current.FindResource("S.DataTemplate.PrimitivesPropertyItem.View") as DataTemplate;
            //this.SelectSourcePropertyItem = Application.Current.FindResource("S.DataTemplate.SelectSourcePropertyItem.View") as DataTemplate;
            //this.CommandTempate = Application.Current.FindResource("S.DataTemplate.CommandPropertyItem.Default") as DataTemplate;

            this.StringDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Text) as DataTemplate;
            this.DateTimeDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.DateTime) as DataTemplate;
            this.BoolDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Bool) as DataTemplate;
            this.EnumDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Enum) as DataTemplate;

            this.IntDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Int) as DataTemplate;
            this.DoubleDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Double) as DataTemplate;

            this.ObjectDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Object) as DataTemplate;
            this.IEnumerableDefaultTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.IEnumerable) as DataTemplate;
            this.PrimitivesPropertyItem = Application.Current.TryFindResource(PropertyViewTemplateKeys.Primitives) as DataTemplate;
            this.SelectSourcePropertyItem = Application.Current.TryFindResource(PropertyViewTemplateKeys.SelectSource) as DataTemplate;
            this.CommandTempate = Application.Current.TryFindResource(PropertyViewTemplateKeys.Command) as DataTemplate;
        }
    }
}
