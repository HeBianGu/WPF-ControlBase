// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface IObservableSource<T>
    {
        Predicate<T> Fileter { get; set; }

        int MaxValue { get; set; }
        int MinValue { get; set; }
        int PageCount { get; set; }
        int PageIndex { get; set; }

        ObservableCollection<T> Source { get; set; }

        int Total { get; set; }
        int TotalPage { get; set; }

        void Add(params T[] value);
        void Clear();
        void Load(IEnumerable<T> source);
        void RefreshSource();
        void Remove(params T[] value);
        void RemoveAll(Func<T, bool> predicate);
        void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc);
        IEnumerable<T> Where(Func<T, bool> predicate);

        void Foreach(Action<T> predicate);
    }

    /// <summary> 带有页码的 ObservableCollection 集合</summary>
    public class ObservableSource<T> : NotifyPropertyChanged, IObservableSource, IObservableSource<T>
    {
        private ObservableCollection<T> _cache = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
        public ObservableCollection<T> Cache
        {
            get { return _cache; }
            set
            {
                _cache = value;
                RaisePropertyChanged("Cache");
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

                this.RefreshSource();
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

                this.RefreshSource();
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

                this.RefreshSource();
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
        public void RefreshSource()
        {
            List<T> where = this.Cache.Where(l => Fileter(l))?.ToList();

            Task.Run(() =>
            {
                this.Total = where.Count;

                int min = (this.PageIndex - 1) * this.PageCount;

                int max = min + this.PageCount;

                this.MinValue = this.Total == 0 ? 0 : (min + 1);

                this.MaxValue = max < this.Total ? max : this.Total;

                this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : this.Total / this.PageCount + 1;

                IEnumerable<T> collection = where.Skip(this.MinValue).Take(this.PageCount);

                this.Source = collection.ToObservable();
            });
        }

        public void Load(IEnumerable<T> source)
        {
            this.Cache = source?.ToObservable();

            this.RefreshSource();
        }

        public void Add(params T[] value)
        {
            foreach (T item in value)
            {
                this.Cache.Add(item);
            }

            this.RefreshSource();
        }

        public void Remove(params T[] value)
        {
            foreach (T item in value)
            {
                this.Cache.Remove(item);
            }

            this.RefreshSource();
        }

        public void RemoveAll(Func<T, bool> predicate)
        {
            this.Cache.RemoveAll(predicate);

            this.RefreshSource();
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

            this.RefreshSource();
        }

        public void Sort<TKey>(Func<T, TKey> keySelector, bool isdesc)
        {
            if (isdesc)
            {
                this.Cache.Sort(keySelector);
            }
            else
            {
                this.Cache.SortDesc(keySelector);
            }

            this.RefreshSource();
        }
    }


}
