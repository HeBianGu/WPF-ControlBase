using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.General.WpfControlLib
{
    public partial class EvaluateControl : ListBox
    {
        public string FIcon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("FIcon", typeof(string), typeof(EvaluateControl), new PropertyMetadata("\xecfd", (d, e) =>
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


        void RefreshData()
        {
            List<EvaluatePoint> points = this.ItemsSource as List<EvaluatePoint>;

            if (points == null) return;

            for (int i = 0; i < points.Count; i++)
            {
                points[i].IsSelected = i <= this.SelectedIndex;
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            this.RefreshData();
        }
    }


    class EvaluatePoint : NotifyPropertyChanged
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
