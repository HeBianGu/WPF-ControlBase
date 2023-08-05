// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 集合类型 </summary>
    public class PrimitiveListPropertyItem : PrimitivesPropertyItem
    {
        public PrimitiveListPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
        }

        protected override void SetValue(ObservableCollection<StringHost> value)
        {
            object to = this.ChangeType(value);

            this.PropertyInfo.SetValue(Obj, to);
        }

        private object ChangeType(ObservableCollection<StringHost> value)
        {
            Type type = this.PropertyInfo.PropertyType.GetGenericTypeDefinition();

            Type[] agrs = this.PropertyInfo.PropertyType.GetGenericArguments();

            Type elementType = agrs.FirstOrDefault();

            Type sfs = type.MakeGenericType(elementType);

            IList list = (IList)Activator.CreateInstance(sfs);

            foreach (StringHost item in value)
            {
                object v = Convert.ChangeType(item.Value, elementType);

                list.Add(v);
            }

            return list;
        }

    }

}
