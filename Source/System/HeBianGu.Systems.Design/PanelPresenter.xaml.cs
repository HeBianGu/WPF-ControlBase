using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Adorner;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.TypeConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    public abstract class DropAdornerDesignPresenterBase : CommandsDesignPresenterBase, IHitTestElementDrag, IHitTestElementDrop
    {
        public virtual Adorner GetDragAdorner(UIElement element)
        {
            return new LineAdorner(element);
        }

        public Adorner GetDropAdorner(UIElement element)
        {
            return new LineAdorner(element);
        }

        public virtual bool CanDrop(UIElement element, DragEventArgs e)
        {
            return true;
        }

        public virtual void Drop(UIElement element, DragEventArgs e)
        {
            //IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
            //if (adorner.GetData() is ICloneable cloneable)
            //{
            //    object value = cloneable.Clone();
            //    var ic = element.GetParent<ItemsControl>();
            //    if (ic.ItemsSource is IList list)
            //        list.Add(e);
            //}

        }

        public virtual void DragEnter(UIElement element, DragEventArgs e)
        {

        }

        public virtual void DragLeave(UIElement element, DragEventArgs e)
        {

        }

        public virtual void DragOver(UIElement element, DragEventArgs e)
        {

        }

        public void RemoveDropAdorner(UIElement element)
        {

        }

        public void RemoveDragAdorner(UIElement element)
        {

        }

        public virtual bool IsHitTest(UIElement element, DragEventArgs e)
        {
            return true;
        }
    }


    [ContentProperty("Presenters")]
    [DefaultProperty("Presenters")]
    public abstract class PanelPresenterBase : DropAdornerDesignPresenterBase
    {
        public PanelPresenterBase()
        {
            //this.MinHeight = 100;
            this.MinWidth = 300;
        }
        private ObservableCollection<DesignPresenterBase> _presenters = new ObservableCollection<DesignPresenterBase>();
        [Browsable(false)]
        [XmlIgnore]
        public ObservableCollection<DesignPresenterBase> Presenters
        {
            get { return _presenters; }
            set
            {
                _presenters = value;
                RaisePropertyChanged();
            }
        }

        private HorizontalAlignment _childrenHorizontalAlignment = HorizontalAlignment.Center;
        [Display(Name = "水平内部对齐", GroupName = "常用,样式")]
        public HorizontalAlignment ChildrenHorizontalAlignment
        {
            get { return _childrenHorizontalAlignment; }
            set
            {
                _childrenHorizontalAlignment = value;
                RaisePropertyChanged();
            }
        }

        private VerticalAlignment _childrenVerticalAlignment = VerticalAlignment.Center;
        [Display(Name = "垂直内部对齐", GroupName = "常用,样式")]
        public VerticalAlignment ChildrenVerticalAlignment
        {
            get { return _childrenVerticalAlignment; }
            set
            {
                _childrenVerticalAlignment = value;
                RaisePropertyChanged();
            }
        }

        public override void DragEnter(UIElement element, DragEventArgs e)
        {
            IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
            if (adorner.GetData() is DesignPresenterBase value)
            {
                this.Presenters.Add(value);
                _dropBackup = value;
                _dropBackup.Opacity = 0.5;
            }
        }

        protected DesignPresenterBase _dropBackup;
        public override void Drop(UIElement element, DragEventArgs e)
        {
            this.Presenters.Remove(_dropBackup);
            _dropBackup.Opacity = 1;
            this.Presenters.Add(_dropBackup.Clone() as DesignPresenterBase);
            _dropBackup = null;
        }

        public override void DragLeave(UIElement element, DragEventArgs e)
        {
            this.Presenters.Remove(_dropBackup);
            _dropBackup = null;

        }
    }

    public abstract class GridPresenterBase : PanelPresenterBase
    {
        public GridPresenterBase()
        {
            this.Background = Brushes.Transparent;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.MinRowHeight = DesignSetting.Instance.RowHeight;
        }
        private Brush _lineBrush = Brushes.LightGray;
        [Display(Name = "网线颜色", GroupName = "常用,样式")]
        public Brush LineBrush
        {
            get { return _lineBrush; }
            set
            {
                _lineBrush = value;
                RaisePropertyChanged();
            }
        }


        private double _lineThickness = 1;
        [Display(Name = "网线粗细", GroupName = "常用,样式")]
        public double LineThickness
        {
            get { return _lineThickness; }
            set
            {
                _lineThickness = value;
                RaisePropertyChanged();
            }
        }

        private double _minRowHeight;
        [Display(Name = "最小行高", GroupName = "常用,样式")]
        public double MinRowHeight
        {
            get { return _minRowHeight; }
            set
            {
                _minRowHeight = value;
                RaisePropertyChanged();
            }
        }


        public override void DragEnter(UIElement element, DragEventArgs e)
        {
            //IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
            //if (adorner.GetData() is ICloneable cloneable)
            //{
            //    object value = cloneable.Clone();
            //    if (value is DesignPresenterBase design)
            //    {
            //        System.Diagnostics.Debug.WriteLine("DragEnter");
            //        var grid = element.GetChild<Grid>();
            //        if (grid.HitTestRow(out int r) && grid.HitTestColumn(out int c))
            //        {
            //            design.Row = r;
            //            design.Column = c;
            //            System.Diagnostics.Debug.WriteLine($"{r}_{c}");
            //        }
            //    }
            //    this.Presenters.Add(value);
            //    _dropBackup = value;
            //}
        }

        public override bool IsHitTest(UIElement element, DragEventArgs e)
        {
            return element.GetContent() != _dropBackup;
        }

        public override void DragOver(UIElement element, DragEventArgs e)
        {
            var p = e.GetPosition(element);
            var grid = element.GetChild<Grid>();
            if (_dropBackup == null)
            {
                IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
                if (adorner.GetData() is DesignPresenterBase value)
                {
                    if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
                    {
                        value.Row = r;
                        value.Column = c;
                        value.Opacity = 0.5;
                    }
                    this.Presenters.Add(value);
                    _dropBackup = value;
                }
            }
            else
            {
                if (grid.HitTestRow(p, out int r) && grid.HitTestColumn(p, out int c))
                {
                    if (_dropBackup.Row == r && _dropBackup.Column == c)
                        return;
                    _dropBackup.Row = r;
                    _dropBackup.Column = c;
                    //this.Presenters.Remove(_dropBackup);
                    //this.Presenters.Add(_dropBackup);
                    element.InvalidateVisual();
                }
            }
        }
    }

    [Displayer(Name = "DefinitionGrid", Icon = "\xe785")]
    public class DefinitionGridPresenter : GridPresenterBase
    {
        private ObservableCollection<GridLength> _rows;
        [Display(Name = "行数", GroupName = "常用,样式")]
        [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
        public ObservableCollection<GridLength> Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<GridLength> _columns;
        [Display(Name = "列数", GroupName = "常用,样式")]
        [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
        public ObservableCollection<GridLength> Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "FixedGrid", Icon = "\xe8f4")]
    public class FixedGridPresenter : GridPresenterBase
    {
        private int _rows;
        [Display(Name = "行数", GroupName = "常用,样式")]
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }


        private int _columns;
        [Display(Name = "列数", GroupName = "常用,样式")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }


        private GridLength _rowGridLength = GridLength.Auto;
        [Display(Name = "行高", GroupName = "常用,样式")]
        public GridLength RowGridLength
        {
            get { return _rowGridLength; }
            set
            {
                _rowGridLength = value;
                RaisePropertyChanged();
            }
        }


        private GridLength _columnGridLength = GridLength.Auto;
        [Display(Name = "列宽", GroupName = "常用,样式")]
        public GridLength ColumnGridLength
        {
            get { return _columnGridLength; }
            set
            {
                _columnGridLength = value;
                RaisePropertyChanged();
            }
        }

    }
}
