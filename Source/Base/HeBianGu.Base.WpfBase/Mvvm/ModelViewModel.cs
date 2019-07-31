using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public partial class ModelViewModel<T> : NotifyPropertyChanged where T : new()
    {
        public ModelViewModel()
        {
            this.Model = new T();
        }
        public ModelViewModel(T t)
        {
            Model = t;
        }

        private T _model;
        /// <summary> 说明  </summary>
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }
    }
}
