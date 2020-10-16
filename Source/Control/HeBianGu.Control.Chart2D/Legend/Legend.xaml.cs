using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class Legend : ItemsControl
    {
        public Legend()
        {
            this.Loaded += (l, k) => this.Refresh();
        }

        protected virtual void Refresh()
        {
            var series = this.GetParent<Chart>()?.GetChildren<Series>()?.ToList();

            if (series == null) return;

            List<LegendItem> items = new List<LegendItem>();

            foreach (var item in series)
            {
                LegendItem legendItem = new LegendItem();
                legendItem.Series = item;
                items.Add(legendItem);
            }

            this.ItemsSource = items;
        }
    }


    public class PathLegend : Legend
    {
        protected override void Refresh()
        {
            List<ShapeLegendItem> items = new List<ShapeLegendItem>();

            //  Do ：刷新饼图
            {
                var series = this.GetParent<Chart>().GetChildren<DataLayer>()?.ToList();

                foreach (var item in series)
                {
                    //  Do ：刷新数据源时更新
                    item.Drawed -= Item_Drawed;
                    item.Drawed += Item_Drawed;

                    var ps = item.GetChildren<Path>()?.Where(l => l.Tag != null)?.ToList();

                    if (ps == null) continue;

                    foreach (var p in ps)
                    {
                        ShapeLegendItem legendItem = new ShapeLegendItem();
                        legendItem.Shape = p;
                        legendItem.Index = ps.IndexOf(p);
                        legendItem.IsChecked = item.DataBoolean.Count > legendItem.Index ? item.DataBoolean[legendItem.Index] : true;
                        items.Add(legendItem);
                    }
                }
            }

            this.ItemsSource = items;
        }

        private void Item_Drawed(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }
    }

    public class StackLegend : Legend
    {
        protected override void Refresh()
        {
            List<StackLegendItem> items = new List<StackLegendItem>();

            var series = this.GetParent<Chart>().GetChildren<StackBarBase>()?.ToList();

            foreach (var item in series)
            {
                //  Do ：刷新数据源时更新
                item.Drawed -= Item_Drawed;
                item.Drawed += Item_Drawed;

                for (int i = 0; i < item.Foreground.Count; i++)
                {
                    StackLegendItem legendItem = new StackLegendItem();
                    legendItem.Brush = new SolidColorBrush(item.Foreground[i]);
                    legendItem.Text = item.xDisplay.Count > i ? item.xDisplay[i] : string.Empty;
                    legendItem.Layer = item;
                    legendItem.Index = i;
                    legendItem.IsChecked = item.DataBoolean.Count > legendItem.Index ? item.DataBoolean[legendItem.Index] : true;
                    items.Add(legendItem);
                }
            }


            this.ItemsSource = items;
        }

        private void Item_Drawed(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }
    }

    /// <summary> 说明</summary>
    internal class LegendItem : NotifyPropertyChanged
    {
        private Series _series;
        /// <summary> 说明  </summary>
        public Series Series
        {
            get { return _series; }
            set
            {
                _series = value;
                RaisePropertyChanged("Series");
            }
        }
    }

    internal class ShapeLegendItem : NotifyPropertyChanged
    {
        private Shape _shape;
        /// <summary> 说明  </summary>
        public Shape Shape
        {
            get { return _shape; }
            set
            {
                _shape = value;
                RaisePropertyChanged("Shape");
            }
        }

        private int _index;
        /// <summary> 说明  </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }

        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        public void Checked()
        {
            if (this.Shape.Parent is DataLayer layer)
            {
                if (layer.DataBoolean.Count > this.Index)
                {
                    layer.DataBoolean[this.Index] = true;
                }

                layer.TryDraw();
            }
        }

        public void Unchecked()
        {
            if (this.Shape.Parent is DataLayer layer)
            {
                if (layer.DataBoolean.Count > this.Index)
                {
                    layer.DataBoolean[this.Index] = false;
                }

                layer.TryDraw();
            }
        }
    }

    internal class StackLegendItem : NotifyPropertyChanged
    {

        private LayerBase _layer;
        /// <summary> 说明  </summary>
        public LayerBase Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
                RaisePropertyChanged("Layer");
            }
        }


        private Brush _brush;
        /// <summary> 说明  </summary>
        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                RaisePropertyChanged("Brush");
            }
        }


        private string _text;
        /// <summary> 说明  </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }


        private int _index;
        /// <summary> 说明  </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }

        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }

        public void Checked()
        {
            if (this.Layer is DataLayer layer)
            {
                if (layer.DataBoolean.Count > this.Index)
                {
                    layer.DataBoolean[this.Index] = true;
                }
            }

            this.Layer?.TryDraw();
        }

        public void Unchecked()
        {
            if (this.Layer is DataLayer layer)
            {
                if (layer.DataBoolean.Count > this.Index)
                {
                    layer.DataBoolean[this.Index] = false;
                }
            }

            this.Layer?.TryDraw();
        }
    }



}
