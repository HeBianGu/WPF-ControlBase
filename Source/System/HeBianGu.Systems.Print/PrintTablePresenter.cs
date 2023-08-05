using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Adorner;
using HeBianGu.Control.PrintBox;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Print
{
    [Displayer(Name = "表格分页", Icon = IconAll.NoteBook)]
    public class TablePrintPagePresenter : PrintDataGridPresenter, IPrintPagePresenter
    {
        private string _title;
        [Display(Name = "标题", GroupName = "常用,数据")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _user;
        [Display(Name = "用户", GroupName = "常用,数据")]
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _date = DateTime.Now;
        [Display(Name = "日期", GroupName = "常用,数据")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged();
            }
        }

        private int _total;
        [Browsable(false)]
        public int Total
        {
            get { return _total; }
            set
            {
                _total = value;
                RaisePropertyChanged();
            }
        }

        [Displayer(Name = "删除", Icon = Icons.Clock)]
        public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            if (e is ContentControl project)
            {
                var adorner = project.GetParent<Adorner>();
                var source = adorner.AdornedElement.GetParent<ItemsControl>();
                source.GetItemsSource<IList>().Remove(project.Content);
            }
        });

        //public object Clone()
        //{
        //    return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
        //}
    }

    public class PrintPagePresenterBase : DisplayerViewModelBase, IPrintPagePresenter, IMetaSettingSerilize, ICloneable
    {
        public PrintPagePresenterBase()
        {
            //this.PageMargin = PrintBoxMessage.Instance.PageMargin;
            this.Foreground = PrintBoxMessage.Instance.Foreground;
            this.Background = PrintBoxMessage.Instance.Background;
            this.FontSize = PrintBoxMessage.Instance.FontSize;
            this.BorderBrush = PrintBoxMessage.Instance.BorderBrush;
        }

        private bool _visible = true;
        [Display(Name = "显示/隐藏", GroupName = "常用,样式")]
        public bool Visisble
        {
            get { return _visible; }
            set
            {
                _visible = value;
                RaisePropertyChanged();
            }
        }

        //private Thickness _pageMargin = new Thickness(10, 50, 10, 20);
        //[Display(Name = "页面边距")]
        //public Thickness PageMargin
        //{
        //    get { return _pageMargin; }
        //    set
        //    {
        //        _pageMargin = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private Brush _foreground;
        [Display(Name = "字体颜色", GroupName = "样式")]
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
        [Display(Name = "字体大小", GroupName = "样式")]
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
        [Display(Name = "背景颜色", GroupName = "样式")]
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
        [Display(Name = "边框颜色", GroupName = "样式")]
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                RaisePropertyChanged();
            }
        }

        //  ToDo：文字颜色 字体大小等 从全局参数中取
        public virtual void Refresh()
        {

        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            //return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
            return this.CloneXml();
        }
    }

    [ContentProperty("Presenters")]
    [DefaultProperty("Presenters")]
    [Displayer(Name = "列表分页", Icon = IconAll.NoteBook)]
    public class ListPrintPagePresenter : PrintPagePresenterBase, IGetDropAdorner
    {
        [Displayer(Name = "删除", Icon = Icons.Clock)]
        public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            if (e is ContentControl project)
            {
                var adorner = project.GetParent<Adorner>();
                var source = adorner.AdornedElement.GetParent<ItemsControl>();
                source.GetItemsSource<IList>().Remove(project.Content);
            }
        });

        private ObservableCollection<object> _presenters = new ObservableCollection<object>();
        [Display(Name = "数据列表")]
        [Browsable(false)]
        public ObservableCollection<object> Presenters
        {
            get { return _presenters; }
            set
            {
                _presenters = value;
                RaisePropertyChanged();
            }
        }

        public Adorner GetDropAdorner(UIElement element)
        {
            var items = element.GetChild<ItemsControl>();
            return new LineAdorner(items) { Dock = Dock.Bottom };
        }

        public void RemoveDropAdorner(UIElement element)
        {
            var items = element.GetChild<ItemsControl>();
            items?.ClearAdorner(x => x.GetType() == typeof(LineAdorner));
        }
    }

    [Displayer(Name = "数据表", Icon = Icons.Clock)]
    [ContentProperty("ItemsSource")]
    [DefaultProperty("ItemsSource")]
    public class PrintDataGridPresenter : DataGridPresenterBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.GridHeaderBackground = Brushes.Black;
            this.GridHeaderForeground = Brushes.White;
            this.HorizontalGridLinesBrush = Brushes.LightGray;
            this.VerticalGridLinesBrush = Brushes.LightGray;
            this.GridForeground = Brushes.Black;
            this.GridBackground = Brushes.White;
            this.AlternatingRowBackground = Brushes.WhiteSmoke;
            this.ColumnHeaderHeight = 40;
            this.RowHeight = 35;
            this.GridBorderBrush = Brushes.LightGray;
            this.ColumnHorizontalContentAlignment = HorizontalAlignment.Left;
            this.CellHorizontalContentAlignment = HorizontalAlignment.Left;
        }
    }
}
