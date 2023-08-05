// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Collections.ObjectModel;

namespace HeBianGu.General.WpfControlLib
{
    public abstract class SourceViewModel<T> : NotifyPropertyChanged
    {
        private ObservableCollection<T> _collection = new ObservableCollection<T>();
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
                MessageProxy.Snacker.ShowTime("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + error);
            }
        });

        public RelayCommand EditCommand => new RelayCommand(l =>
        {
            if (this.Edit(out string error))
            {
                MessageProxy.Snacker.ShowTime("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DeleteCommand => new RelayCommand(l =>
        {
            if (this.Delete(out string error))
            {
                MessageProxy.Snacker.ShowTime("删除成功");
                this.Collection.Remove(this.SelectedItem);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DetailCommand => new RelayCommand(l =>
        {
            if (!this.Detail(out string error))
            {
                MessageProxy.Snacker.ShowTime("查询失败," + error);
            }
        }, l => this.SelectedItem != null);

        public RelayCommand ClearCommand => new RelayCommand(l =>
        {
            if (this.Clear(out string error))
            {
                MessageProxy.Snacker.ShowTime("清空成功");
                this.Collection.Clear();
            }
            else
            {
                MessageProxy.Snacker.ShowTime("清空失败," + error);
            }

        }, l => this.Collection != null && this.Collection.Count > 0);
        protected abstract T Add(out string error);
        protected abstract bool Delete(out string error);
        protected abstract bool DeleteAll(SelectViewModel<T> checks, out string error);
        protected abstract bool Clear(out string error);
        protected abstract bool Edit(out string error);
        protected abstract bool Detail(out string error);
    }
}
