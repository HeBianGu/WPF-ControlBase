using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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

            ICollectionView vw = CollectionViewSource.GetDefaultView(this.Students);
            vw.GroupDescriptions.Add(new PropertyGroupDescription("Class"));


            for (int i = 0; i < 500; i++)
            {
                this.Datas.Add(random.NextDouble() * 10);
                this.xAxis.Add(i);
            }

        }

        Random random = new Random();

        protected override void RelayMethod(object obj)
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

}
