using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace WpfControlDemo.View
{
    /// <summary>
    /// ButtonPage.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonPage : Page
    {
        ButtonPageVeiwModel _vm = new ButtonPageVeiwModel();
        public ButtonPage()
        {
            InitializeComponent();

            this.DataContext = _vm; 

            //bind command
            this.BusyCommand = new RoutedCommand();
            var bin = new CommandBinding(this.BusyCommand);
            bin.Executed += buzy_Executed;
            this.CommandBindings.Add(bin);

            this.Loaded += ButtonPage_Loaded;
        }

    
        public static readonly DependencyProperty BusyCommandProperty =
    DependencyProperty.Register("BusyCommand", typeof(RoutedCommand), typeof(ButtonPage), new PropertyMetadata(null));

        public RoutedCommand BusyCommand
        {
            get { return (RoutedCommand)GetValue(BusyCommandProperty); }
            set { SetValue(BusyCommandProperty, value); }
        }

        void buzy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var btn = e.Source as FButton;

            this.RefreshBuzy();

        }

        void RefreshBuzy()
        {
            _vm.IsBuzy = true;

            Action action = () =>
            {
                Thread.Sleep(3000);

                this.Dispatcher.Invoke(() => _vm.IsBuzy = false);
            };

            Task.Run(action);
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            MessageWindow.ShowDialog("看到蒙版了么？", this.grid_all);
        }


        private void ButtonPage_Loaded(object sender, RoutedEventArgs e)
        {
           // AdornerLayer layer = AdornerLayer.GetAdornerLayer(grid_all);
           //layer.Add(new PromptAdorner(Prompbutton));
        }


        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            int v = int.Parse(Prompbutton.PromptCount) + 1;

            Prompbutton.PromptCount = v.ToString();
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            Prompbutton.PromptCount = (int.Parse(Prompbutton.PromptCount) - 1).ToString();
        }
    }


    public partial class ButtonPageVeiwModel
    {
        private bool _isBuzy = false;
        /// <summary> 说明 </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;

                RaisePropertyChanged();
            }
        }
    }
    partial class ButtonPageVeiwModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
