// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public partial class EvaluateControl : ListBox
    {
        public string Icon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(EvaluateControl), new PropertyMetadata("\xecfd", (d, e) =>
             {
                 EvaluateControl control = d as EvaluateControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public int TotalCount
        {
            get { return (int)GetValue(TotalCountProperty); }
            set { SetValue(TotalCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalCountProperty =
            DependencyProperty.Register("TotalCount", typeof(int), typeof(EvaluateControl), new PropertyMetadata(default(int), (d, e) =>
             {
                 EvaluateControl control = d as EvaluateControl;

                 if (control == null) return;

                 int config = (int)e.NewValue;

                 if (config < 0) return;

                 List<EvaluatePoint> result = new List<EvaluatePoint>();

                 for (int i = 0; i < config; i++)
                 {
                     EvaluatePoint point = new EvaluatePoint();
                     point.Value = i + 1;
                     result.Add(point);
                 }

                 control.ItemsSource = result;

                 control.RefreshData();
             }));

        private void RefreshData()
        {
            List<EvaluatePoint> points = this.ItemsSource as List<EvaluatePoint>;

            if (points == null) return;

            for (int i = 0; i < points.Count; i++)
            {
                points[i].IsSelected = i <= (this.SelectedIndex);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            this.RefreshData();

            if (!this.IsLoaded) return;
            if (!this.IsVisible) return;

            this.OnValueChanged();
        }


        public static readonly RoutedEvent ValueChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(EvaluateControl));

        public event RoutedEventHandler ValueChanged
        {
            add { this.AddHandler(ValueChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ValueChangedRoutedEvent, value); }
        }

        protected void OnValueChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(ValueChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }

    internal class EvaluatePoint : NotifyPropertyChanged
    {

        private bool _isSelected;
        /// <summary> 说明  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }


        private int _value;
        /// <summary> 说明  </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


    }

}
