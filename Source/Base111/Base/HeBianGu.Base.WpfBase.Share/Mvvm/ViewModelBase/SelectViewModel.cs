using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface ISelectViewModel<T> : IModelViewModel<T>
    {
        bool Selected { get; set; }
    }

    public partial class SelectViewModel<T> : ModelViewModel<T>, ISelectViewModel<T>
    {
        public SelectViewModel(T t) : base(t)
        {

        }

        private bool _selected;
        /// <summary> 是否选中  </summary>
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
