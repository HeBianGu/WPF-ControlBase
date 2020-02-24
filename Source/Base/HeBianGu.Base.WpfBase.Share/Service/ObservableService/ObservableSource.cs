using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 带有页码的 ObservableCollection 集合</summary>
    public class ObservableSource<T> : NotifyPropertyChanged, IObservableSource
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

        private int _pageIndex=1;
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
            //var where = this.Where(l => Fileter(l));

            if (this.Cache.Count == 0) return;

            this.Total = this.Cache.Count;

            int min = (this.PageIndex - 1) * this.PageCount;

            int max = min + this.PageCount;

            this.MinValue = this.Total == 0 ? 0 : (min + 1);

            this.MaxValue = max < this.Total ? max : this.Total;

            this.TotalPage = this.Total % this.PageCount == 0 ? this.Total / this.PageCount : this.Total / this.PageCount + 1;

            var collection = this.Cache.Skip(this.MinValue).Take(this.PageCount);

            this.Source = collection.ToObservable();
        }

        public void Add(params T[] value)
        {
            foreach (var item in value)
            {
                this.Cache.Add(item);
            }

            this.RefreshSource();
        }

        public void Clear()
        {
            this.Cache.Clear();

            this.RefreshSource();
        }
    }


}
