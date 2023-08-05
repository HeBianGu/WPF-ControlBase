// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Base.WpfBase
{
    public interface IObservableSource<T>
    {
        Predicate<T> Fileter { get; set; }
        int Count { get; }
        int MaxValue { get; set; }
        int MinValue { get; set; }
        int PageCount { get; set; }
        int PageIndex { get; set; }
        int SelectedIndex { get; set; }
        T SelectedItem { get; set; }

        ObservableCollection<T> Source { get; set; }

        ObservableCollection<T> FilterSource { get; set; }

        int Total { get; set; }
        int TotalPage { get; set; }

        void Add(params T[] value);
        void Clear();
        void Load(IEnumerable<T> source);
        void RefreshPage(Action after = null);
        void Remove(params T[] value);
        void RemoveAll(Func<T, bool> predicate);
        void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc=false);
        IEnumerable<T> Where(Func<T, bool> predicate);
        T FirstOrDefault(Func<T, bool> predicate);
        void Foreach(Action<T> predicate);
        bool Any(Func<T, bool> predicate);
        IEnumerable<TResult> Select<TResult>(Func<T, TResult> predicate);

        void Next();
        void Previous();
    }

    /// <summary> 带有页码的 ObservableCollection 集合</summary>
    public class ObservableSource<T> : NotifyPropertyChanged, IObservableSource, IObservableSource<T>
    {
        public int Count => this.Cache.Count;

        private ObservableCollection<T> _cache = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Cache
        {
            get { return _cache; }
            set
            {
                _cache = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<T> _filterSource = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> FilterSource
        {
            get { return _filterSource; }
            set
            {
                _filterSource = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<T> _source = new ObservableCollection<T>();
        /// <summary> 要显示的资源  </summary>
        public ObservableCollection<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged("Source");
            }
        }

        private T _selectedItem;
        /// <summary> 说明  </summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem?.Equals(value) == true)
                    return;
                _selectedItem = value;
                RaisePropertyChanged();
                this.SelectedIndex = this.FilterSource.IndexOf(value);
            }
        }

        private int _selectedIndex;
        /// <summary> 说明  </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value)
                    return;
                _selectedIndex = value;
                RaisePropertyChanged();
                this.SelectedItem = this.FilterSource.ElementAtOrDefault(value);
            }
        }

        private int _total;
        /// <summary> 总数量  </summary>
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged("Total");
            }
        }

        private int _totalPage;
        /// <summary> 总页数  </summary>
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                RaisePropertyChanged("TotalPage");
            }
        }

        private int _pageIndex = 1;
        /// <summary> 当前页索引  </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                _pageIndex = value;
                RaisePropertyChanged("PageIndex");
                this.RefreshPage();
            }
        }

        private int _pageCount = 20;
        /// <summary> 每页数量  </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged("PageCount");
                this.RefreshPage();
            }
        }

        private Predicate<T> _fileter = l => true;
        /// <summary> 过滤条件  </summary>
        public Predicate<T> Fileter
        {
            get { return _fileter; }
            set
            {
                _fileter = value;
                RaisePropertyChanged("Fileter");
                this.RefreshPage();
            }
        }

        private int _minValue;
        /// <summary> 当前页最小值  </summary>
        public int MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }

        private int _maxValue;
        /// <summary> 当前页最大值  </summary>
        public int MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }

        /// <summary> 刷新当前显示Source </summary>
        public void RefreshPage(Action after = null)
        {
            var where = this.Cache.Where(l => this.Fileter?.Invoke(l) != false);
            this.FilterSource = where.ToObservable();
            this.Total = this.FilterSource.Count;
            int min = (this.PageIndex - 1) * this.PageCount;
            int max = min + this.PageCount;
            this.MinValue = this.Total == 0 ? 0 : (min + 1);
            this.MaxValue = max < this.Total ? max : this.Total;
            this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : this.Total / this.PageCount + 1;
            List<T> collection = where.Skip(this.MinValue-1).Take(this.PageCount).ToList();

            //this.Source = collection.ToObservable();
            this.DelayInvoke(this.Source, collection, () =>
            {
                if (after == null)
                    this.SelectedItem = collection.FirstOrDefault();
                else
                    after?.Invoke();
            });
        }

        public void Load(IEnumerable<T> source)
        {
            this.Cache = source?.ToObservable();
            this.RefreshPage();
        }

        public void Add(params T[] value)
        {
            this.Cache.AddRange(value);
            this.RefreshPage();
        }

        public void Remove(params T[] value)
        {
            foreach (T item in value)
            {
                this.Cache.Remove(item);
            }
            this.RefreshPage();
        }

        public void RemoveAll(Func<T, bool> predicate)
        {
            this.Cache.RemoveAll(predicate);
            this.RefreshPage();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return this.Cache.Where(predicate);
        }

        public void Foreach(Action<T> predicate)
        {
            this.Cache.Foreach(predicate);
        }

        public void Clear()
        {
            this.Cache.Clear();
            this.RefreshPage();
        }

        public void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc)
        {
            if (isdesc)
            {
                this.Cache.OrderBy(keySelector);
            }
            else
            {
                this.Cache.OrderByDesc(keySelector);
            }
            this.RefreshPage();
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return this.Cache.FirstOrDefault(predicate);
        }

        public bool Any(Func<T, bool> predicate)
        {
            return this.Cache.Any(predicate);
        }

        public IEnumerable<TResult> Select<TResult>(Func<T, TResult> predicate)
        {
            return this.Cache.Select(predicate);
        }

        public void Next()
        {
            if (this.SelectedItem == null)
                return;
            int index = this.FilterSource.IndexOf(this.SelectedItem);
            if (index >= this.Total-1 || index == -1)
                index = 0;
            else
                index++;
            int cPage = (index / this.PageCount) + 1;
            if (cPage != this._pageIndex)
            {
                this._pageIndex = cPage;
                this.RefreshPage(() => this.SelectedItem = this.FilterSource[index]);
            }
            else
            {
                this._pageIndex = cPage;
                this.SelectedItem = this.FilterSource[index];
            }
        }

        public void Previous()
        {
            if (this.SelectedItem == null)
                return;
            int index = this.FilterSource.IndexOf(this.SelectedItem);
            if (index == 0 || index == -1)
                index = this.FilterSource.Count - 1;
            else
                index--;
            int cPage = (index / this.PageCount) + 1;
            if (cPage != this._pageIndex)
            {
                this._pageIndex = cPage;
                this.RefreshPage(() => this.SelectedItem = this.FilterSource[index]);
            }
            else
            {
                this._pageIndex = cPage;
                this.SelectedItem = this.FilterSource[index];
            }
        }
    }


}
