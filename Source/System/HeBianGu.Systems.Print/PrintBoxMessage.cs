using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Systems.Print
{
    public interface IPrintBoxMessageOption
    {

    }

    [Displayer(Name = "打印设置", GroupName = SystemSetting.GroupSystem)]
    public class PrintBoxMessage : ServiceSettingInstance<PrintBoxMessage, IPrintBoxMessage>, IPrintBoxMessage, IPrintBoxMessageOption
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.PageMargin = new Thickness(10, 50, 10, 20);
            this.Foreground = Brushes.Black;
            this.Background = Brushes.White;
            this.BorderBrush = Brushes.LightGray;
            this.FontSize = 12;
        }
        private Thickness _pageMargin = new Thickness(10, 50, 10, 20);
        [Display(Name = "页面边距")]
        public Thickness PageMargin
        {
            get { return _pageMargin; }
            set
            {
                _pageMargin = value;
                RaisePropertyChanged();
            }
        }

        private Brush _foreground;
        [Display(Name = "字体颜色")]
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }

        private int _fontSize;
        [DefaultValue(12)]
        [Display(Name = "字体大小")]
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private Brush _background;
        [Display(Name = "背景颜色")]
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged();
            }
        } 

        private Brush _borderBrush;
        [Display(Name = "边框颜色")]
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                RaisePropertyChanged();
            }
        }



        public async Task<bool> Show(params IPrintPagePresenter[] data)
        {
            if (data == null)
                return false;
            PrintBoxPresenter presenter = new PrintBoxPresenter();
            presenter.Collection = data.ToObservable();
            return await MessageProxy.Presenter.Show(presenter, null, "打印设置", x => x.Margin = new Thickness(0, 5, 0, 5));
        }

        public async Task<bool> PrintTable<T>(IEnumerable<T> source, string title = "标题")
        {
            TablePrintPagePresenter presenter = new TablePrintPagePresenter();
            presenter.Title = title;
            presenter.User = Loginner.Instance?.User?.Name;
            presenter.Total = source.Count();
            presenter.ColumnPropertyInfos = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false).Select(x => new ColumnPropertyInfo(x)).ToObservable();
            foreach (var item in presenter.ColumnPropertyInfos.Take(4))
            {
                item.IsVisible = true;
            }
            presenter.ItemsSource = source;
            return await MessageProxy.Printer.Show(presenter);
        }
    }

    public class PrintBoxPresenter : NotifyPropertyChangedBase
    {
        private ObservableCollection<IPrintPagePresenter> _collection = new ObservableCollection<IPrintPagePresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPrintPagePresenter> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
