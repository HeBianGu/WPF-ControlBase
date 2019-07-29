
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public partial class SelectViewModel<T> : NotifyPropertyChanged
    {

        public SelectViewModel(T t)
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

        private bool _selected;
        /// <summary> 说明  </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged("Selected");
            }
        }

    }
}
