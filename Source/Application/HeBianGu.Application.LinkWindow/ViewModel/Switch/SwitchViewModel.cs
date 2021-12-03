using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Application.LinkWindow
{
    [ViewModel("Switch")]
    public class SwitchViewModel : MvcViewModelBase
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

        List<UIElement> controls = new List<UIElement>();

        int value = 1;


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


        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() => new RelayCommand<string>(Loaded, CanLoaded)).Value;

        protected override void Loaded(string args)
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
                    Grid grid = new Grid();
                    grid.Width = 2000;
                    grid.Height = 2000;
                    grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                    grid.VerticalAlignment = VerticalAlignment.Stretch;
                    grid.Background = solids[i % 9];
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

                this.CurrentContent = controls[value - 1];
                value++;
            });

        }


        private bool CanSelectChanged(string args)
        {
            return true;
        }
    }

}
