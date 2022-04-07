// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
                    object instance = Activator.CreateInstance(elementType);

                    if (instance != null)
                    {
                        list.Add(instance);
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

            if (enumerable == null) return result;

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

}
