// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 设置表单的默认模板 </summary>
    public class PropertyControlTemplateSelector : PropertyGridDataTemplateSelectorBase
    {
        protected override void InitTempate()
        {
            //this.StringDefaultTempate = Application.Current.FindResource("S.DataTemplate.TextPropertyItem.Default") as DataTemplate;
            //this.IntDefaultTempate = Application.Current.FindResource("S.DataTemplate.IntPropertyItem.Default") as DataTemplate;
            //this.DoubleDefaultTempate = Application.Current.FindResource("S.DataTemplate.DoublePropertyItem.Default") as DataTemplate;
            //this.DateTimeDefaultTempate = Application.Current.FindResource("S.DataTemplate.DateTimePropertyItem.Default") as DataTemplate;
            //this.BoolDefaultTempate = Application.Current.FindResource("S.DataTemplate.BoolPropertyItem.Default") as DataTemplate;
            //this.ObjectDefaultTempate = Application.Current.FindResource("S.DataTemplate.ObjectPropertyItem.Default") as DataTemplate;
            //this.EnumDefaultTempate = Application.Current.FindResource("S.DataTemplate.EnumPropertyItem.Default") as DataTemplate;
            //this.IEnumerableDefaultTempate = Application.Current.FindResource("S.DataTemplate.IEnumerablePropertyItem.Default") as DataTemplate;
            //this.PrimitivesPropertyItem = Application.Current.FindResource("S.DataTemplate.PrimitivesPropertyItem.Default") as DataTemplate;
            //this.SelectSourcePropertyItem = Application.Current.FindResource("S.DataTemplate.SelectSourcePropertyItem.Default") as DataTemplate;

            //this.CommandTempate = Application.Current.FindResource("S.DataTemplate.CommandPropertyItem.Default") as DataTemplate;
        }


        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ObjectPropertyItem objectItem)
            {
                if (objectItem is TextPropertyItem) return StringDefaultTempate;

                if (objectItem is BoolPropertyItem) return BoolDefaultTempate;

                return StringDefaultTempate;
            }

            return ObjectDefaultTempate;
        }
    }
}
