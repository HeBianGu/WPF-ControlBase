using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 带有集合的数据源 </summary>
    public partial class ObservableSourceViewModel<T> : NotifyPropertyChanged where T : new()
    {

        private ObservableCollection<T> _source = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged("Collection");
            }
        }

        private T _selectedItem;
        /// <summary> 说明  </summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
    }
}
