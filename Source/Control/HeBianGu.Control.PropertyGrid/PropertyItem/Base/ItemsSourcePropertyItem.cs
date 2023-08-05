// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class ItemsSourcePropertyItem<T> : ObjectPropertyItem<T>
    {
        public ItemsSourcePropertyItem(PropertyInfo property, object obj)
            : base(property, obj)
        {

        }

        private ObservableCollection<T> _collection = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }
    }

}
