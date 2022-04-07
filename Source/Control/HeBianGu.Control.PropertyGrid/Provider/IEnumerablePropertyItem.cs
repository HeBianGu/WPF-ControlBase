// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 集合类型 </summary>
    public class ListPropertyItem : IEnumerablePropertyItem
    {
        public ListPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override object ChangeType(ObservableCollection<object> value)
        {
            Type type = this.PropertyInfo.PropertyType.GetGenericTypeDefinition();

            Type[] agrs = this.PropertyInfo.PropertyType.GetGenericArguments();

            Type elementType = agrs.FirstOrDefault();

            Type sfs = type.MakeGenericType(elementType);

            IList list = (IList)Activator.CreateInstance(sfs);

            foreach (object item in value)
            {
                list.Add(item);
            }

            return list;
        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();

        }
    }

    /// <summary> 集合类型 </summary>
    public class ArrayPropertyItem : IEnumerablePropertyItem
    {
        public ArrayPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override object ChangeType(ObservableCollection<object> value)
        {
            Type elementType = this.PropertyInfo.PropertyType.GetElementType();

            Array array = Array.CreateInstance(elementType, value.Count);

            for (int i = 0; i < value.Count; i++)
            {
                array.SetValue(value[i], i);
            }

            return array;
        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetElementType();

        }
    }
}
