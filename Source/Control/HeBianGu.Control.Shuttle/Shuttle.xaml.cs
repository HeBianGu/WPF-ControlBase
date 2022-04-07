// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.Shuttle
{
    public class Shuttle : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Shuttle), "S.Shuttle.Default");


        static Shuttle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Shuttle), new FrameworkPropertyMetadata(typeof(Shuttle)));
        }

        public Shuttle()
        {
            //  Do ：执行移动到下一组
            this.NextCommand = new RelayCommand(l =>
            {
                if (l is ShuttleItem item)
                {
                    if (item.SelectItem == null) return;

                    int index = this.Source.IndexOf(item);

                    ShuttleItem next = this.Source[index == this.Source.Count - 1 ? 0 : index + 1];

                    (next.ItemSource as IList).Add(item.SelectItem);

                    (item.ItemSource as IList).Remove(item.SelectItem);
                }
            });

            //  Do ：执行移动到下一组
            this.PreviousCommand = new RelayCommand(l =>
            {
                if (l is ShuttleItem item)
                {
                    int index = this.Source.IndexOf(item);

                    ShuttleItem next = this.Source[index == this.Source.Count - 1 ? 0 : index + 1];

                    if (next.SelectItem == null) return;

                    (item.ItemSource as IList).Add(next.SelectItem);

                    (next.ItemSource as IList).Remove(next.SelectItem);
                }
            });
        }


        public ICommand NextCommand { get; }

        public ICommand PreviousCommand { get; }

        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(Shuttle), new PropertyMetadata(2, (d, e) =>
             {
                 Shuttle control = d as Shuttle;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));

        public ObservableCollection<ShuttleItem> Source
        {
            get { return (ObservableCollection<ShuttleItem>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ObservableCollection<ShuttleItem>), typeof(Shuttle), new PropertyMetadata(default(ObservableCollection<ShuttleItem>), (d, e) =>
             {
                 Shuttle control = d as Shuttle;

                 if (control == null) return;

                 ObservableCollection<ShuttleItem> config = e.NewValue as ObservableCollection<ShuttleItem>;

                 control.ColumnCount = config == null ? 0 : config.Count;

                 if (config == null) return;

                 //  Do ：设置最后一个隐藏按钮
                 config.LastOrDefault().IsUseButton = false;

             }));
    }




    public class ShuttleItem : NotifyPropertyChanged
    {
        public string Header { get; set; }

        public IEnumerable ItemSource { get; set; }

        private object _selectItem;
        /// <summary> 说明  </summary>
        public object SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }


        private bool _isUseButton = true;
        /// <summary> 是否启用穿梭按钮  </summary>
        public bool IsUseButton
        {
            get { return _isUseButton; }
            set
            {
                _isUseButton = value;
                RaisePropertyChanged("IsUseButton");
            }
        }


    }
}
