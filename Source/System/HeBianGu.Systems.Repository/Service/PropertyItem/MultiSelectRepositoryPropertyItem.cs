// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Systems.Repository
{
    public class MultiSelectRepositoryPropertyItem : ObjectPropertyItem<IEnumerable>
    {
        public MultiSelectRepositoryPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            Type modelType = property.PropertyType.GenericTypeArguments?.FirstOrDefault();

            Type rType = typeof(IRepository<>).MakeGenericType(modelType);
            object r = ServiceRegistry.Instance.GetInstance(rType);

            IEnumerable source = rType.GetMethod("GetAll").Invoke(r, new object[] { null }) as IEnumerable;

            this.ItemsSource = source;

            IEnumerable v = property.GetValue(obj) as IEnumerable;

            this.Value = v ?? Activator.CreateInstance(typeof(List<>).MakeGenericType(modelType)) as IEnumerable;
        }

        private IEnumerable _itemsSource;
        /// <summary> 说明  </summary>
        public IEnumerable ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                RaisePropertyChanged("ItemsSource");
            }
        }
    }
}
