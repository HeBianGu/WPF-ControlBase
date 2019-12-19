using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// TreeViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewPage : Page
    {
        TreeViewNotifyClass treeViewNotifyClass = new TreeViewNotifyClass();
        public TreeViewPage()
        {
            InitializeComponent();

            this.DataContext = treeViewNotifyClass;
        }
    }



    partial class TreeViewNotifyClass
    {

        private ObservableCollection<Staff> _StaffList = new ObservableCollection<Staff>();

        public ObservableCollection<Staff> StaffList
        {
            get { return _StaffList; }
            set
            {
                _StaffList = value;
                this.RaisePropertyChanged("StaffList");
            }
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
                Staff staff = new Staff()
                {
                    Name = "张三",
                    Age = 30,
                    Sex = "男",
                    Duty = "经理",
                    IsExpanded = true
                };
                Staff staff2 = new Staff()
                {
                    Name = "张三1",
                    Age = 21,
                    Sex = "男",
                    Duty = "员工",
                    IsExpanded = true
                };
                Staff staff3 = new Staff()
                {
                    Name = "张三11",
                    Age = 21,
                    Sex = "男",
                    Duty = "员工"
                };
                staff2.StaffList.Add(staff3);
                staff3 = new Staff()
                {
                    Name = "张三22",
                    Age = 21,
                    Sex = "女",
                    Duty = "员工"
                };
                staff2.StaffList.Add(staff3);
                staff.StaffList.Add(staff2);
                staff2 = new Staff()
                {
                    Name = "张三2",
                    Age = 22,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                staff2 = new Staff()
                {
                    Name = "张三3",
                    Age = 23,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                StaffList.Add(staff);

                staff = new Staff()
                {
                    Name = "李四",
                    Age = 31,
                    Sex = "男",
                    Duty = "副经理"
                };
                staff2 = new Staff()
                {
                    Name = "李四1",
                    Age = 24,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                staff2 = new Staff()
                {
                    Name = "李四2",
                    Age = 25,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                staff2 = new Staff()
                {
                    Name = "李四3",
                    Age = 26,
                    Sex = "男",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                StaffList.Add(staff);

                staff = new Staff()
                {
                    Name = "王五",
                    Age = 32,
                    Sex = "女",
                    Duty = "组长"
                };
                staff2 = new Staff()
                {
                    Name = "王五1",
                    Age = 27,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                staff2 = new Staff()
                {
                    Name = "王五2",
                    Age = 28,
                    Sex = "女",
                    Duty = "员工"
                };
                staff.StaffList.Add(staff2);
                StaffList.Add(staff);

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class TreeViewNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public TreeViewNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);
            RelayMethod("init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    

    public partial class Staff 
    {
        private string _Name;
        private int _Age;
        private string _Sex;
        private string _Duty;
        private bool _IsSelected;
        private bool _IsExpanded;

        private ObservableCollection<Staff> _StaffList = new ObservableCollection<Staff>();

        public ObservableCollection<Staff> StaffList
        {
            get { return _StaffList; }
            set
            {
                _StaffList = value;
                this.RaisePropertyChanged("StaffList");
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                this.RaisePropertyChanged("Name");
            }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                this.RaisePropertyChanged("Age");
            }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _Sex; }
            set
            {
                _Sex = value;
                this.RaisePropertyChanged("Sex");
            }
        }
        /// <summary>
        /// 职务
        /// </summary>
        public string Duty
        {
            get { return _Duty; }
            set
            {
                _Duty = value;
                this.RaisePropertyChanged("Duty");
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                _IsExpanded = value;
                this.RaisePropertyChanged("IsExpanded");
            }
        }

        public Staff()
        {
            IsSelected = false;
            IsExpanded = false;
        }
    }

    partial class Staff : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }
       
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
