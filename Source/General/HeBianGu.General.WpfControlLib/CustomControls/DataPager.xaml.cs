using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// DataPager.xaml 的交互逻辑
    /// </summary>
    public partial class DataPager : UserControl, INotifyPropertyChanged
    {
        public DataPager()
        {
            InitializeComponent();
        }

        #region 依赖属性和事件
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeProperty =
            DependencyProperty.Register("PageSize", typeof(int), typeof(DataPager), new UIPropertyMetadata(10));

        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(DataPager), new UIPropertyMetadata(0));

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(DataPager), new UIPropertyMetadata(1));

        public string PageSizeList
        {
            get { return (string)GetValue(PageSizeListProperty); }
            set { SetValue(PageSizeListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PageSizeList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSizeListProperty =
            DependencyProperty.Register("PageSizeList", typeof(string), typeof(DataPager), new UIPropertyMetadata("5,10,20", (s, e) => {
                DataPager dp = s as DataPager;
                if (dp.PageSizeItems == null) dp.PageSizeItems = new List<int>();
                else dp.PageSizeItems.Clear();
                dp.RaisePropertyChanged("PageSizeItems");
            }));

        public IEnumerable<object> ItemsSource
        {
            get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource数据源
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(DataPager), new UIPropertyMetadata(null));



        public static readonly RoutedEvent PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged", RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(DataPager));
        /// <summary>
        /// 分页更改事件
        /// </summary>
        public event PageChangedEventHandler PageChanged
        {
            add
            {
                AddHandler(PageChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PageChangedEvent, value);
            }
        }
        #endregion


        #region 通知属性
        private List<int> _pageSizeItems;
        /// <summary>
        /// 显示每页记录数集合
        /// </summary>
        public List<int> PageSizeItems
        {
            get
            {
                if (_pageSizeItems == null)
                {
                    _pageSizeItems = new List<int>();
                }
                if (PageSizeList != null)
                {
                    List<string> strs = PageSizeList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    _pageSizeItems.Clear();
                    strs.ForEach(c => {
                        _pageSizeItems.Add(Convert.ToInt32(c));
                    });
                }
                return _pageSizeItems;
            }
            set
            {
                if (_pageSizeItems != value)
                {
                    _pageSizeItems = value;
                    RaisePropertyChanged("PageSizeItems");
                }
            }
        }

        private int _pageCount;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                    RaisePropertyChanged("PageCount");
                }
            }
        }

        private int _start;
        /// <summary>
        /// 开始记录数
        /// </summary>
        public int Start
        {
            get { return _start; }
            set
            {
                if (_start != value)
                {
                    _start = value;
                    RaisePropertyChanged("Start");
                }
            }
        }

        private int _end;
        /// <summary>
        /// 结束记录数
        /// </summary>
        public int End
        {
            get { return _end; }
            set
            {
                if (_end != value)
                {
                    _end = value;
                    RaisePropertyChanged("End");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region 字段、属性、委托
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs args);
        private PageChangedEventArgs pageChangedEventArgs;
        #endregion



        #region 引发分页更改事件
        /// <summary>
        /// 引发分页更改事件
        /// </summary>
        private void RaisePageChanged()
        {
            if (pageChangedEventArgs == null)
            {
                pageChangedEventArgs = new PageChangedEventArgs(PageChangedEvent, PageSize, PageIndex);
            }
            else
            {
                pageChangedEventArgs.PageSize = this.PageSize;
                pageChangedEventArgs.PageIndex = this.PageIndex;
            }
            RaiseEvent(pageChangedEventArgs);
            //calc start、end
            if (ItemsSource != null)
            {
                int curCount = ItemsSource.Count();
                Start = (PageIndex - 1) * PageSize + 1;
                End = Start + curCount - 1;

                if (Total % PageSize != 0)
                {
                    PageCount = Total / PageSize + 1;
                }
                else
                {
                    PageCount = Total / PageSize;
                }
            }
            else
            {
                Start = End = PageCount = Total = 0;
            }

            //调整图片的显示
            btnFirst.IsEnabled = btnPrev.IsEnabled = (PageIndex != 1);
            btnNext.IsEnabled = btnLast.IsEnabled = (PageIndex != PageCount);
        }
        #endregion

        #region 分页操作事件
        void DataPager_Loaded(object sender, RoutedEventArgs e)
        {
            RaisePageChanged();
        }

        private void cbpPageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                PageSize = (int)cboPageSize.SelectedItem;
                RaisePageChanged();
            }
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            PageIndex = 1;
            RaisePageChanged();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex > 1)
            {
                --PageIndex;
            }
            RaisePageChanged();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (Total % PageSize != 0)
            {
                PageCount = Total / PageSize + 1;
            }
            else
            {
                PageCount = Total / PageSize;
            }

            if (PageIndex < PageCount)
            {
                ++PageIndex;
            }
            RaisePageChanged();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if (Total % PageSize != 0)
            {
                PageCount = Total / PageSize + 1;
            }
            else
            {
                PageCount = Total / PageSize;
            }
            PageIndex = PageCount;
            RaisePageChanged();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RaisePageChanged();
        }

        private void tbPageIndex_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tbPageIndex_LostFocus(sender, null);
            }
        }

        private void tbPageIndex_LostFocus(object sender, RoutedEventArgs e)
        {
            int pIndex = 0;
            try
            {
                pIndex = Convert.ToInt32(tbPageIndex.Text);
            }
            catch { pIndex = 1; }

            if (pIndex < 1) PageIndex = 1;
            else if (pIndex > PageCount) PageIndex = PageCount;
            else PageIndex = pIndex;

            RaisePageChanged();
        }
        #endregion




    }

    /// <summary>
    /// 分页更改参数
    /// </summary>
    public class PageChangedEventArgs : RoutedEventArgs
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public PageChangedEventArgs(RoutedEvent routeEvent, int pageSize, int pageIndex)
            : base(routeEvent)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
        }
    }
}
