// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class TextPropertyViewItem : TextPropertyItem, IPropertyViewItem
    {
        public TextPropertyViewItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }

    public class DictionaryPropertyViewItem : ObjectPropertyItem<IDictionary>, IPropertyViewItem
    {
        public DictionaryPropertyViewItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

    }
}
