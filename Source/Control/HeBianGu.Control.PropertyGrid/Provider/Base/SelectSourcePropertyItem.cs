// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class SelectSourcePropertyItem : ObjectPropertyItem<object>
    {
        public SelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            CustomValidationAttribute attr = property.GetCustomAttribute<CustomValidationAttribute>();

            MethodInfo p = this.Obj.GetType().GetMethod(attr.Method);

            IEnumerable<object> source = p.Invoke(obj, null) as IEnumerable<object>;

            this.Collection = source.ToObservable();

            this.LoadValue();

        }


        private ObservableCollection<object> _collection = new ObservableCollection<object>();
        /// <summary> 说明  </summary>
        public ObservableCollection<object> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        protected override bool CheckType(object value, out string error)
        {
            error = null;

            return true;
        }


        protected override void SetValue(object value)
        {
            this.PropertyInfo.SetValue(Obj, value);
        }
    }

}
