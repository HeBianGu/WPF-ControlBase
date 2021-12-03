using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public partial class ModelViewModel<T> : NotifyPropertyChanged
    {
        //public ModelViewModel()
        //{
        //    this.Model = new T();
        //}
        public ModelViewModel(T t)
        {
            Model = t;

        }

        private T _model;
        /// <summary> Model层  </summary>
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        private bool _visible = true;
        /// <summary> 是否可见  </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged("Visible");
            }
        }

        private bool _isEnbled;
        /// <summary> 是否可用  </summary>
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


        private bool _isBuzy;
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private int _value;
        /// <summary> 说明  </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }


        /// <summary> 将Model数据加载到ViewModel属性上面 通过名称匹配 </summary>
        protected virtual bool LoadValue(out string message)
        {
            message = string.Empty;

            var ps = this.GetType().GetProperties();

            if (ps == null) return true;

            foreach (var property in ps)
            {
                var find = typeof(T).GetProperty(property.Name);

                if (find == null) continue;

                property.SetValue(this, find.GetValue(this.Model));
            }

            return true;
        }

        /// <summary> 将ViewModel数据加载到Model属性上面 通过名称匹配 </summary>
        protected virtual bool SaveValue(out string message)
        {
            message = string.Empty;

            var ps = this.GetType().GetProperties();

            if (ps == null) return true;

            foreach (var property in ps)
            {
                var find = typeof(T).GetProperty(property.Name);

                if (find == null) continue;

                find.SetValue(this, property.GetValue(this.Model));
            }

            return true;
        }
    }

}
