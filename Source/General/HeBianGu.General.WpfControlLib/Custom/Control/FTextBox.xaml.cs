using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using HeBianGu.Base.WpfBase;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// FTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class FTextBox : TextBox
    {
        static FTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FTextBox), new FrameworkPropertyMetadata(typeof(FTextBox)));
        }
    }

    /// <summary> 缓存历史输入记录功能 </summary>
    public partial class FTextBox
    {
        public bool UseHistory
        {
            get { return (bool)GetValue(UseHistoryProperty); }
            set { SetValue(UseHistoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseHistoryProperty =
            DependencyProperty.Register("UseHistory", typeof(bool), typeof(FTextBox), new PropertyMetadata(true, (d, e) =>
            {
                FTextBox control = d as FTextBox;

                if (control == null) return;

                bool config = (bool)e.NewValue;

            }));


        public int Capacity
        {
            get { return (int)GetValue(CapacityProperty); }
            set { SetValue(CapacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CapacityProperty =
            DependencyProperty.Register("Capacity", typeof(int), typeof(FTextBox), new PropertyMetadata(10, (d, e) =>
             {
                 FTextBox control = d as FTextBox;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        /// <summary> 缓存到列表的正则表达式 </summary>
        public string RegexMatch
        {
            get { return (string)GetValue(RegexMatchProperty); }
            set { SetValue(RegexMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegexMatchProperty =
            DependencyProperty.Register("RegexMatch", typeof(string), typeof(FTextBox), new PropertyMetadata(@"\S", (d, e) =>
             {
                 FTextBox control = d as FTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 control.regex = new Regex(control.RegexMatch);

             }));


        Regex regex;

        public FTextBox()
        {
            regex = new Regex(this.RegexMatch);

            this.LostFocus += FTextBox_LostFocus;
        }

        private void FTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.UseHistory) return;

            if (this.History.Count > this.Capacity)
            {
                this.History = this.History.Take(this.Capacity)?.ToObservable();
            }

            if (!this.regex.IsMatch(this.Text)) return;

            this.History.Remove(this.Text);
            this.History?.Insert(0, this.Text);
        }


        public string SelectedHistroyItem
        {
            get { return (string)GetValue(SelectedHistroyItemProperty); }
            set { SetValue(SelectedHistroyItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedHistroyItemProperty =
            DependencyProperty.Register("SelectedHistroyItem", typeof(string), typeof(FTextBox), new PropertyMetadata(default(string), (d, e) =>
            {
                FTextBox control = d as FTextBox;

                if (control == null) return;

                string config = e.NewValue as string;

                if (config == null) return;

                if (!control.regex.IsMatch(config)) return;

                control.Text = config;

            }));


        public ObservableCollection<string> History
        {
            get { return (ObservableCollection<string>)GetValue(HistoryProperty); }
            set { SetValue(HistoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HistoryProperty =
            DependencyProperty.Register("History", typeof(ObservableCollection<string>), typeof(FTextBox), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
            {
                FTextBox control = d as FTextBox;

                if (control == null) return;

                ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

            }));

    }


    public class FTextBoxHistoryConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var result = values.OfType<bool>()?.ToList();

            var r = result.TrueForAll(l => l == true); 

            var colletion = values.OfType<ObservableCollection<string>>()?.FirstOrDefault();

            if (colletion == null) return r;

            return colletion.Count > 0 && r;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
