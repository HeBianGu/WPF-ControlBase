// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 带有集合的数据源 </summary>
    [Obsolete]
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
            //SelectedAllCommand = new RelayCommand(SelectedAll);
            //DeleteItemCommand = new RelayCommand(DeleteItem);
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

        ///// <summary> 全选 </summary>
        //protected virtual void SelectedAll(object obj)
        //{
        //    bool value = (bool)obj;

        //    this.Collection.Foreach(l => l.Selected = value);
        //}

        ///// <summary> 删除选中项 </summary>
        //protected virtual void DeleteItem(object obj)
        //{
        //    if (obj == null) return;

        //    if (obj is SelectViewModel<T> s)
        //    {
        //        this.Collection.Remove(s);
        //    }
        //}

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
    }



}
