// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{


    /// <summary> 设置表单的默认模板 </summary>
    public class PropertyGridTemplateSelector : PropertyGridDataTemplateSelectorBase
    {
        protected override void InitTempate()
        {
            this.StringDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Text) as DataTemplate;
            this.DateTimeDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.DateTime) as DataTemplate;
            this.BoolDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Bool) as DataTemplate;
            this.EnumDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Enum) as DataTemplate;

            this.IntDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Int) as DataTemplate;
            this.DoubleDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Double) as DataTemplate;

            this.ObjectDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Object) as DataTemplate;
            this.IEnumerableDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.IEnumerable) as DataTemplate;
            this.PrimitivesPropertyItem = Application.Current.TryFindResource(PropertyGridTemplateKeys.Primitives) as DataTemplate;
            this.SelectSourcePropertyItem = Application.Current.TryFindResource(PropertyGridTemplateKeys.SelectSource) as DataTemplate;
            this.CommandTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Command) as DataTemplate;
        }
    }
}
