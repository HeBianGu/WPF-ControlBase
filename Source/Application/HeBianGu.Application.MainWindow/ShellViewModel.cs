using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace HeBianGu.Application.MainWindow
{
    /// <summary> 说明</summary>
    internal class ShellViewModel : NotifyPropertyChanged
    {
        #region - 属性 -

        private ObservableCollection<TestViewModel> _collection = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }



        private CustomNameViewModel _customNameViewModel = new CustomNameViewModel();
        /// <summary> 说明  </summary>
        public CustomNameViewModel CustomNameViewModel
        {
            get { return _customNameViewModel; }
            set
            {
                _customNameViewModel = value;
                RaisePropertyChanged("CustomNameViewModel");
            }
        }



        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }



        private ObservableCollection<double> _datas = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> Datas
        {
            get { return _datas; }
            set
            {
                _datas = value;
                RaisePropertyChanged("Datas");
            }
        }


        private ObservableCollection<double> _xAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xAxis
        {
            get { return _xAxis; }
            set
            {
                _xAxis = value;
                RaisePropertyChanged("xAxis");
            }
        }

        private ObservableCollection<string> _autoCompleteSource = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> AutoCompleteSource
        {
            get { return _autoCompleteSource; }
            set
            {
                _autoCompleteSource = value;
                RaisePropertyChanged("AutoCompleteSource");
            }
        }


        private ObservableCollection<string> _autoCompleteSelecteds = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> AutoCompleteSelecteds
        {
            get { return _autoCompleteSelecteds; }
            set
            {
                _autoCompleteSelecteds = value;
                RaisePropertyChanged("AutoCompleteSelecteds");
            }
        }


        private ImageSource _imageSource;
        /// <summary> 说明  </summary>
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private ObservableCollection<SystemInfoModel> _selectedItems=new ObservableCollection<SystemInfoModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<SystemInfoModel> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }



        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void Init()
        {
            base.Init();

            for (int i = 0; i < 2000; i++)
            {
                this.Students.Add(Student.Random());
            }

            //ICollectionView vw = CollectionViewSource.GetDefaultView(this.Students);

            //vw.GroupDescriptions.Add(new PropertyGroupDescription("Class"));


            for (int i = 0; i < 5000; i++)
            {
                this.Datas.Add(random.NextDouble() * 10);
                this.xAxis.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                _autoCompleteSource.Add(random.Next(0,10000000).ToString());
            }

            //_autoCompleteSource.Add("测试");
            //_autoCompleteSource.Add("成功");
            //_autoCompleteSource.Add("失败");
            //_autoCompleteSource.Add("123456789");
            _autoCompleteSource.Add("abcdefghijklmnopqrstuvwxyz");

            //BitmapImage bitmap = new BitmapImage(new Uri(@"C:\Users\Administrator\Desktop\新建文件夹\vs图标.PNG", UriKind.RelativeOrAbsolute));

            //ImageSource grayImage = new FormatConvertedBitmap(bitmap, PixelFormats.Pbgra32, null, 0);

            //this.ImageSource = grayImage;

            _selectedItems.CollectionChanged +=(l, k) =>
             {
                 Debug.WriteLine(this.SelectedItems.Count);
             };

        }

        Random random = new Random();

        protected override async void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            //  Do：应用
            if (command == "Add")
            {
                this.Collection.Add(new TestViewModel() { Value = (this.Collection.Count + 1).ToString() });

            }
            //  Do：取消
            else if (command == "Delete")
            {

                this.Collection.RemoveAt(0);

            }

            //  Do：取消
            else if (command == "ToolBar.ValueChanged.RefreshData")
            {



            }
            //  Do：取消
            else if (command == "Chart.MouseWheel.ChangeValue")
            {

            }

            //  Do：取消
            else if (command == "TaskBar")
            {
                //MessageService.ShowTaskbarPercent(l =>
                //{
                //    for (int i = 0; i < 100; i++)
                //    {
                //        l.ProgressState = TaskbarItemProgressState.Normal;

                //        l.ProgressValue = i / 100.0;

                //        Thread.Sleep(10);
                //    }

                //    l.ProgressValue = 0;
                //});

               await MessageService.ShowTaskbarWaitting(() =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Thread.Sleep(50);
                    }

                    return true;
                });

            }

            //  Do：取消
            else if (command == "ScrollViewer.ScrollingTrigger.ToButton")
            {
                MessageService.ShowWinInfoMessage("移动到了指定按钮18");
            }
            //  Do：取消
            else if (string.IsNullOrEmpty(command))
            {
                MessageService.ShowWinInfoMessage("测试");
            }

            if (obj is MouseWheelEventArgs args)
            {

            }

        }

        #endregion
    }

    /// <summary> 说明</summary>
    internal class CustomNameViewModel : NotifyPropertyChanged
    {
        #region - 属性 -
        private MyEnum _selectedMyEnum;
        /// <summary> 说明  </summary>
        [TypeConverter(typeof(EnumConverter))]
        public MyEnum SelectedMyEnum
        {
            get { return _selectedMyEnum; }
            set
            {
                _selectedMyEnum = value;
                RaisePropertyChanged("SelectedMyEnum");
            }
        }
        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }

    public class LeagueList : List<League>
    {
        public LeagueList()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Add(new League());
            }
        }
    }

    public class League
    {
        public string Name { get; set; }

        public List<Division> Divisions { get; set; } = new List<Division>();

        public static int Count = 0;

        public League()
        {
            Count++;

            this.Name = this.GetType().Name + Count;

            for (int i = 0; i < 5; i++)
            {
                Divisions.Add(new Division());
            }
        }
    }

    public class Division
    {
        public string Name { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public static int Count = 0;

        public Division()
        {
            Count++;

            this.Name = this.GetType().Name + Count;

            for (int i = 0; i < 5; i++)
            {
                Teams.Add(new Team());
            }
        }
    }

    public class Team
    {
        public static int Count = 0;

        public string Name { get; set; }

        public Team()
        {
            Count++;

            this.Name = this.GetType().Name + Count;
        }
    }
}
