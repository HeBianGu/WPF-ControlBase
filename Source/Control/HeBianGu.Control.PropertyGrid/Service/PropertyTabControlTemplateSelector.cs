// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 设置表单的默认模板 </summary>
    public class PropertyTabControlTemplateSelector : PropertyGridDataTemplateSelectorBase
    {
        protected override void InitTempate()
        {
            this.StringDefaultTempate = Application.Current.TryFindResource(PropertyTabTemplateKeys.Text) as DataTemplate;
            this.IntDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Int) as DataTemplate;
            this.DoubleDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Double) as DataTemplate;
            this.DateTimeDefaultTempate = Application.Current.TryFindResource(PropertyTabTemplateKeys.DateTime) as DataTemplate;
            this.BoolDefaultTempate = Application.Current.TryFindResource(PropertyTabTemplateKeys.Bool) as DataTemplate;
            this.ObjectDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.Object) as DataTemplate;
            this.EnumDefaultTempate = Application.Current.TryFindResource(PropertyTabTemplateKeys.Enum) as DataTemplate;
            this.IEnumerableDefaultTempate = Application.Current.TryFindResource(PropertyGridTemplateKeys.IEnumerable) as DataTemplate;
            this.PrimitivesPropertyItem = Application.Current.TryFindResource(PropertyGridTemplateKeys.Primitives) as DataTemplate;
            this.SelectSourcePropertyItem = Application.Current.TryFindResource(PropertyGridTemplateKeys.SelectSource) as DataTemplate;
            this.CommandTempate = Application.Current.TryFindResource(PropertyTabTemplateKeys.Command) as DataTemplate;
        }
    }
}
