// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class Legend : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Legend), "S.Legend.Default");
        public static ComponentResourceKey VerticalKey => new ComponentResourceKey(typeof(Legend), "S.Legend.Vertical");

        static Legend()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Legend), new FrameworkPropertyMetadata(typeof(Legend)));
        }

        public Legend()
        {
            this.Loaded += (l, k) => this.Refresh();
        }

        protected virtual void Refresh()
        {
            List<Series> series = this.GetParent<Chart>()?.GetChildren<Series>()?.ToList();
            if (series == null)
                return;
            List<LegendItem> items = new List<LegendItem>();
            foreach (Series item in series)
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
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PathLegend), "S.PathLegend.Default");

        static PathLegend()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathLegend), new FrameworkPropertyMetadata(typeof(PathLegend)));
        }
        protected override void Refresh()
        {
            List<ShapeLegendItem> items = new List<ShapeLegendItem>();

            //  Do ：刷新饼图
            {
                List<DataLayer> series = this.GetParent<Chart>()?.GetChildren<DataLayer>()?.ToList();
                if (series == null)
                    return;
                foreach (DataLayer item in series)
                {
                    //  Do ：刷新数据源时更新
                    item.Drawed -= Item_Drawed;
                    item.Drawed += Item_Drawed;

                    List<Path> ps = item.GetChildren<Path>()?.Where(l => l.Tag != null)?.ToList();
                    if (ps == null)
                        continue;
                    foreach (Path p in ps)
                    {
                        ShapeLegendItem legendItem = new ShapeLegendItem();
                        legendItem.Shape = p;
                        legendItem.Index = ps.IndexOf(p);
                        legendItem.Layer = item;
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
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(StackLegend), "S.StackLegend.Default");

        static StackLegend()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StackLegend), new FrameworkPropertyMetadata(typeof(StackLegend)));
        }
        protected override void Refresh()
        {
            List<StackLegendItem> items = new List<StackLegendItem>();

            List<StackBarBase> series = this.GetParent<Chart>()?.GetChildren<StackBarBase>()?.ToList();

            if (series == null) return;

            foreach (StackBarBase item in series)
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
        private DataLayer _layer;
        /// <summary> 说明  </summary>
        public DataLayer Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
                RaisePropertyChanged("Layer");
            }
        }


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
            DataLayer layer = this.Layer;
            if (layer.DataBoolean.Count > this.Index)
            {
                layer.DataBoolean[this.Index] = true;
            }

            layer.Draw(layer);
        }

        public void Unchecked()
        {
            DataLayer layer = this.Layer;
            if (layer.DataBoolean.Count > this.Index)
            {
                layer.DataBoolean[this.Index] = false;
            }

            layer.Draw(layer);
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

            this.Layer?.Draw(this.Layer);
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
            this.Layer?.Draw(this.Layer);
        }
    }



}
