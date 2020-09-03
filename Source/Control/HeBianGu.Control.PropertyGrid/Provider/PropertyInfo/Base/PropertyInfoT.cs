using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{

    public abstract class PropertyInfo<T> : PropertyInfo
    {

        private T _current;
        /// <summary> 说明  </summary>
        public T Current
        {
            get { return _current; }
            set
            {
                _current = value;

                this.CurrentChanged();

                RaisePropertyChanged("Current");
            }
        }


        private List<T> _source = new List<T>();
        /// <summary> 数据源  </summary>
        public List<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged("Source");
            }
        }

        public override void LoadProperty(Property config, string defaultName = null)
        {
            base.LoadProperty(config);

            if (string.IsNullOrEmpty(config?.ItemSource)) return;

            var source= config.ItemSource?.Split(',');

           this.Source=  source.Select(l=>ConvertToValue(l)).ToList();

        }

        public abstract T ConvertToValue(object value);

        public override object ConvertToObject(string value)
        {
            return this.ConvertToValue(value);
        }

        public override bool IsValidation(Property source)
        {
            if (IsTypeOf())
            {
                return base.IsValidation(source);
            }

            this.Message = $"不是{typeof(T).Name}的有效值";

            return false;
        }

        public abstract bool IsTypeOf();

        protected virtual void CurrentChanged()
        {

        }

    }
}
