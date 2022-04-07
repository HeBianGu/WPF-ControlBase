// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class PropertyGridDataTemplateSelectorBase : DataTemplateSelector
    {
        public PropertyGridDataTemplateSelectorBase()
        {
            this.InitTempate();
        }

        public DataTemplate CommandTempate { get; set; }

        public DataTemplate StringDefaultTempate { get; set; }
        public DataTemplate IntDefaultTempate { get; set; }
        public DataTemplate DoubleDefaultTempate { get; set; }
        public DataTemplate DateTimeDefaultTempate { get; set; }
        public DataTemplate BoolDefaultTempate { get; set; }
        public DataTemplate ObjectDefaultTempate { get; set; }
        public DataTemplate EnumDefaultTempate { get; set; }
        public DataTemplate IEnumerableDefaultTempate { get; set; }
        public DataTemplate PrimitivesPropertyItem { get; set; }
        public DataTemplate SelectSourcePropertyItem { get; set; }

        protected abstract void InitTempate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item.GetType() == typeof(TextPropertyItem)) return StringDefaultTempate;

            if (item.GetType() == typeof(IntPropertyItem)) return IntDefaultTempate;

            if (item.GetType() == typeof(DoublePropertyItem)) return DoubleDefaultTempate;

            if (item.GetType() == typeof(BoolPropertyItem)) return BoolDefaultTempate;

            if (item.GetType() == typeof(DateTimePropertyItem)) return DateTimeDefaultTempate;

            if (item.GetType() == typeof(EnumPropertyItem)) return EnumDefaultTempate;

            if (item.GetType() == typeof(ListPropertyItem)) return IEnumerableDefaultTempate;

            if (item.GetType() == typeof(ArrayPropertyItem)) return IEnumerableDefaultTempate;

            if (item.GetType() == typeof(PrimitiveArrayPropertyItem)) return PrimitivesPropertyItem;

            if (item.GetType() == typeof(PrimitiveListPropertyItem)) return PrimitivesPropertyItem;

            if (item.GetType() == typeof(SelectSourcePropertyItem)) return SelectSourcePropertyItem;

            if (item.GetType() == typeof(CommandPropertyItem)) return CommandTempate;

            if (item.GetType() == typeof(ObjectPropertyItem)) return ObjectDefaultTempate;

            return null;
        }
    }
}
