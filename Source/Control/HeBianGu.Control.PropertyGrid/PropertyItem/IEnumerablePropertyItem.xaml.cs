// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class IEnumerablePropertyItem : ObjectPropertyItem<ObservableCollection<object>>
    {
        public IEnumerablePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            Type elementType = this.GetElementType();
            this.AddCommand = new RelayCommand(l =>
            {
                if (this.Value is IList list)
                {
                    if (elementType == typeof(string))
                    {
                        list.Add(string.Empty);
                    }
                    else
                    {
                        object instance = Activator.CreateInstance(elementType);

                        if (instance != null)
                        {
                            list.Add(instance);
                        }
                    }
                }
            });

            this.Value.CollectionChanged += Value_CollectionChanged;
        }

        protected abstract Type GetElementType();

        private void Value_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RefreshValue();
        }

        public void RefreshValue()
        {
            this.Value = this.Value;
        }

        public RelayCommand AddCommand { get; set; }

        ~IEnumerablePropertyItem()
        {
            this.Value.CollectionChanged -= Value_CollectionChanged;
        }

        protected override bool CheckType(ObservableCollection<object> value, out string error)
        {
            error = null;

            return true;
        }


        protected override ObservableCollection<object> GetValue()
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            IEnumerable enumerable = this.GetValue<IEnumerable>();
            if (enumerable == null)
                return result;
            foreach (object item in enumerable)
            {
                result.Add(item);
            }
            return result;
        }

        protected override void SetValue(ObservableCollection<object> value)
        {
            object to = this.ChangeType(value);
            this.PropertyInfo.SetValue(Obj, to);
        }

        protected abstract object ChangeType(ObservableCollection<object> value);
    }

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
                list.Add(item);
            return list;
        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();

        }
    }

    public class ListPresenterPropertyItem : ListPropertyItem
    {
        public ListPresenterPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

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
                array.SetValue(value[i], i);
            return array;
        }

        protected override Type GetElementType()
        {
            return this.PropertyInfo.PropertyType.GetElementType();
        }
    }
}
