// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    public abstract class SourceViewModel<T> : NotifyPropertyChanged
    {
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

        private T _addItem;
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


        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            if (this.Add(out string error) != null)
            {
                Message.Instance.ShowSnackMessageWithNotice("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + error);
            }
        });

        public RelayCommand EditCommand => new RelayCommand(l =>
        {
            if (this.Edit(out string error))
            {
                Message.Instance.ShowSnackMessageWithNotice("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DeleteCommand => new RelayCommand(l =>
        {
            if (this.Delete(out string error))
            {
                Message.Instance.ShowSnackMessageWithNotice("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DetailCommand => new RelayCommand(l =>
        {
            if (!this.Detail(out string error))
            {
                Message.Instance.ShowSnackMessageWithNotice("查询失败," + error);
            }
        }, l => this.SelectedItem != null);


        public RelayCommand ClearCommand => new RelayCommand(l =>
        {
            if (this.Clear(out string error))
            {
                Message.Instance.ShowSnackMessageWithNotice("清空成功");
                this.Collection.Clear();
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("清空失败," + error);
            }

        }, l => this.Collection != null && this.Collection.Count > 0);



        /// <summary> 添加 </summary>
        protected abstract T Add(out string error);

        /// <summary> 删除 </summary>
        protected abstract bool Delete(out string error);

        /// <summary> 删除选中项 </summary>
        protected abstract bool DeleteAll(SelectViewModel<T> checks, out string error);

        /// <summary> 清空 </summary>
        protected abstract bool Clear(out string error);

        /// <summary> 编辑 </summary>
        protected abstract bool Edit(out string error);

        /// <summary> 查看 </summary>
        protected abstract bool Detail(out string error);

    }
}
