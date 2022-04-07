using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfControlDemo.View
{
    /// <summary>
    /// ScrollView3DControlPage.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollView3DControlPage : Page
    {
        private ScrollView3DNotifyClass _vm = new ScrollView3DNotifyClass();
        public ScrollView3DControlPage()
        {
            InitializeComponent();

            DataContext = _vm;

            Loaded += ScrollView3DControlPage_Loaded;

            //sv_control.Draw();
        }

        private void ScrollView3DControlPage_Loaded(object sender, RoutedEventArgs e)
        {

            ScrollView3DControl s = new ScrollView3DControl();

            List<Visual> collection = new List<Visual>();

            for (int i = 0; i < 30; i++)
            {
                Button btn = new Button
                {
                    //btn.Background = Brushes.Yellow;
                    //btn.Content = i;
                    //btn.Width = 1000;
                    //btn.Height = 1000;
                    Content = i.ToString()
                };

                btn.Click += (object ss, RoutedEventArgs eee) =>
                {
                    MessageBox.Show(i.ToString());
                };
                collection.Add(btn);
            }

            s.Draw(collection);

            grid.Children.Clear();

            grid.Children.Add(s);


            //_vm.RelayMethod("Init");
        }


    }

    internal partial class ScrollView3DNotifyClass
    {

        private List<Visual> _collection;
        /// <summary> 说明  </summary>
        public List<Visual> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {

                List<Visual> collection = new List<Visual>();

                for (int i = 0; i < 20; i++)
                {
                    Button btn = new Button
                    {
                        Content = i,

                        Background = Brushes.Yellow,

                        Width = 100,
                        Height = 100
                    };

                    collection.Add(btn);
                }

                Collection = collection;

            }
            //  Do：取消
            else if (command == "Refresh")
            {


            }
        }
    }

    internal partial class ScrollView3DNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ScrollView3DNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            RelayMethod("Init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
