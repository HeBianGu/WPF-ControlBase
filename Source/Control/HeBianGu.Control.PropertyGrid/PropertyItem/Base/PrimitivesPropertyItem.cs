// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class PrimitivesPropertyItem : ObjectPropertyItem<ObservableCollection<StringHost>>
    {
        public PrimitivesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            Type elementType = this.GetElementType();

            this.AddCommand = new RelayCommand(l =>
            {
                if (this.Value is IList list)
                {
                    if (elementType == typeof(string))
                    {
                        list.Add(new StringHost("", elementType));
                    }
                    else
                    {
                        object n = Activator.CreateInstance(elementType);

                        list.Add(new StringHost(n.ToString(), elementType));
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

        ~PrimitivesPropertyItem()
        {
            this.Value.CollectionChanged -= Value_CollectionChanged;
        }

        protected override ObservableCollection<StringHost> GetValue()
        {
            ObservableCollection<StringHost> result = new ObservableCollection<StringHost>();

            IEnumerable enumerable = this.GetValue<IEnumerable>();

            if (enumerable == null) return result;

            foreach (object item in enumerable)
            {
                result.Add(new StringHost(item.ToString(), this.GetElementType()));
            }

            return result;
        }


        protected override bool CheckType(ObservableCollection<StringHost> value, out string error)
        {
            error = null;

            Type elementType = this.GetElementType();

            foreach (StringHost item in value)
            {
                try
                {
                    Convert.ChangeType(item.Value, elementType);
                }
                catch (Exception ex)
                {
                    error = ex.Message + $"[{elementType.Name}]";
                }
            }

            return value.Count(l => !string.IsNullOrEmpty(l.Message)) == 0;
        }


    }

}
