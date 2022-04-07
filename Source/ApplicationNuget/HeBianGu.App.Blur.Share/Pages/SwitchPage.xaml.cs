using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Applications.ControlBase.Demo.Pages
{
    /// <summary>
    /// SwitchPage.xaml 的交互逻辑
    /// </summary>
    public partial class SwitchPage : Page
    {
        public SwitchPage()
        {
            InitializeComponent();
        }
    }



    public class SwitchViewModel : NotifyPropertyChanged
    {

        private object _currentContent;
        /// <summary> 说明  </summary>
        public object CurrentContent
        {
            get { return _currentContent; }
            set
            {
                _currentContent = value;
                RaisePropertyChanged("CurrentContent");
            }
        }

        private List<UIElement> controls = new List<UIElement>();
        private int value = 1;

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {



            }
            //  Do：取消
            else if (command == "GroupExpander.SelectChanged")
            {


            }

        }

        protected virtual bool CanLoaded(string args)
        {
            return true;
        }

        protected void Loaded(string args)
        {
            SolidColorBrush[] solids = new SolidColorBrush[] {
                new SolidColorBrush(Colors.Red),
                new SolidColorBrush(Colors.Green),
                new SolidColorBrush(Colors.Yellow),
                new SolidColorBrush(Colors.Purple),
                new SolidColorBrush(Colors.Blue),
                new SolidColorBrush(Colors.DarkBlue),
                new SolidColorBrush(Colors.DarkGray),
                new SolidColorBrush(Colors.DarkGreen),
                new SolidColorBrush(Colors.Orange),
            };

            Random random = new Random();

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    Grid grid = new Grid
                    {
                        Width = 1000,
                        Height = 1000,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Background = solids[i % 9]
                    };
                    controls.Add(grid);
                }
            });

        }

        public RelayCommand<string> SelectChangedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(SelectChanged, CanSelectChanged)).Value;

        private void SelectChanged(string args)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                value = value >= controls.Count ? 1 : value;

                CurrentContent = controls[value - 1];
                value++;
            });

        }


        private bool CanSelectChanged(string args)
        {
            return true;
        }
    }
}
