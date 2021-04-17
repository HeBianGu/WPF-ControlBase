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

        public RelayCommand AddCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand DetailCommand { get; set; }

        public RelayCommand SelectedAllCommand { get; set; }

        public RelayCommand DeleteItemCommand { get; set; }
        

        public ObservableSourceViewModel() : base()
        {
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
            DetailCommand = new RelayCommand(Detail);
            SelectedAllCommand = new RelayCommand(SelectedAll);
            DeleteItemCommand = new RelayCommand(DeleteItem);
        }

        /// <summary> 添加 </summary>
        protected virtual void Add(object obj)
        {

        }

        /// <summary> 删除 </summary>
        protected virtual void Delete(object obj)
        {

        }

        /// <summary> 编辑 </summary>
        protected virtual void Edit(object obj)
        {

        }

        /// <summary> 查看 </summary>
        protected virtual void Detail(object obj)
        {

        }

        /// <summary> 全选 </summary>
        protected virtual void SelectedAll(object obj)
        {
            bool value = (bool)obj;

            this.Collection.Foreach(l => l.Selected = value);
        }

        /// <summary> 删除选中项 </summary>
        protected virtual void DeleteItem(object obj)
        {
            if (obj == null) return;

            if(obj is SelectViewModel<T> s)
            {
                this.Collection.Remove(s);
            } 
        } 
        
        private ObservableCollection<T> _models = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Models

        {
            get { return _models; }
            set
            {
                _models = value;
                RaisePropertyChanged("Models");
            }
        }

        private ObservableCollection<SelectViewModel<T>> _collection = new ObservableCollection<SelectViewModel<T>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<SelectViewModel<T>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
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

        private T _addItem = new T();
        /// <summary> 要添加的实体  </summary>
        public T AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }
    }
}
