// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Explorer;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using HeBianGu.Service.Command;
using HeBianGu.Service.TypeConverter;
using HeBianGu.Service.Validation;
using HeBianGu.Systems.Project;
using HeBianGu.Window.Notify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Application.MainWindow
{
    /// <summary> 说明</summary>
    internal class ShellViewModel : NotifyPropertyChanged
    {
        #region - 属性 - 
        private double _value = 334;
        /// <summary> 说明  </summary>

        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        private double _value1 = 334;
        /// <summary> 说明  </summary>

        public double Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged("Value1");
            }
        }


        private Config _config;
        /// <summary> 说明  </summary>
        public Config Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged("Config");
            }
        }

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

        private Student _student = new Student();
        /// <summary> 说明  </summary>
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                RaisePropertyChanged("Student");
            }
        }


        //private Predicate<PropertyInfo> _predicatePropertyInfo;
        ///// <summary> 说明  </summary>
        //public Predicate<PropertyInfo> PredicatePropertyInfo
        //{
        //    get { return _predicatePropertyInfo; }
        //    set
        //    {
        //        _predicatePropertyInfo = value;
        //        RaisePropertyChanged("PredicatePropertyInfo");
        //    }
        //}



        private Property _property = new Property();
        /// <summary> 说明  </summary>
        public Property Property
        {
            get { return _property; }
            set
            {
                _property = value;
                RaisePropertyChanged("Property");
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

        private ObservableCollection<double> _xdatas = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xDatas
        {
            get { return _xdatas; }
            set
            {
                _xdatas = value;
                RaisePropertyChanged("xDatas");
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

        private ObservableCollection<double> _xGridAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xGridAxis
        {
            get { return _xGridAxis; }
            set
            {
                _xGridAxis = value;
                RaisePropertyChanged("xGridAxis");
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

        private ObservableCollection<SystemInfoModel> _selectedItems = new ObservableCollection<SystemInfoModel>();
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



        private TextBoxViewModel _textBoxViewModel = new TextBoxViewModel();
        /// <summary> 说明  </summary>
        public TextBoxViewModel TextBoxViewModel
        {
            get { return _textBoxViewModel; }
            set
            {
                _textBoxViewModel = value;
                RaisePropertyChanged("TextBoxViewModel");
            }
        }


        private Func<double, string> _xConvert;

        /// <summary> 说明  </summary>
        public Func<double, string> xConvert
        {
            get { return _xConvert; }
            set
            {
                _xConvert = value;
                RaisePropertyChanged("xConvert");
            }
        }


        private ObservableCollection<string> _collection1 = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> Collection1
        {
            get { return _collection1; }
            set
            {
                _collection1 = value;
                RaisePropertyChanged("Collection1");
            }
        }


        private ObservableCollection<TreeNodeBase<Student>> _treeSource = new ObservableCollection<TreeNodeBase<Student>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeBase<Student>> TreeSource
        {
            get { return _treeSource; }
            set
            {
                _treeSource = value;
                RaisePropertyChanged("TreeSource");
            }
        }


        private ObservableCollection<TransitionsHost> _transitionsHosts = new ObservableCollection<TransitionsHost>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TransitionsHost> TransitionsHosts
        {
            get { return _transitionsHosts; }
            set
            {
                _transitionsHosts = value;
                RaisePropertyChanged("TransitionsHosts");
            }
        }

        #endregion

        #region - 命令 -
        public BuzyCommand BuzyCommand { get; set; } = new BuzyCommand(l =>
         {
             Thread.Sleep(3000);
         });

        public PercentCommand PercentCommand { get; set; } = new PercentCommand((l, k) =>
        {
            for (int i = 0; i <= 100; i++)
            {
                l.Value = i;

                Thread.Sleep(30);
            }

            Thread.Sleep(1000);

        });

        public MessageCommand MessageCommand { get; set; } = new MessageCommand((l, k) =>
        {
            for (int i = 0; i <= 100; i++)
            {
                l.Message = $"正在执行{i}/100";

                Thread.Sleep(30);
            }

            Thread.Sleep(1000);

        });

        #endregion

        #region - 方法 -

        protected override void Init()
        {
            base.Init();

            this.TransitionsHosts = TransitionPropertyItem.GetWindowHosts()?.ToObservable();

            //for (int i = 0; i < 200; i++)
            //{
            //    this.Students.Add(Student.Random());
            //}

            this.Students = Enumerable.Range(0, 200).Select(l => Student.Random()).ToObservable();

            for (int i = 0; i < 10; i++)
            {
                this.Collection1.Add(i.ToString());
            }

            //ICollectionView vw = CollectionViewSource.GetDefaultView(this.Students);

            //vw.GroupDescriptions.Add(new PropertyGroupDescription("Class"));

            _xConvert = l =>
              {
                  //return $"10^{l}"; 
                  return l.ToString();
              };

            double count = 1000.0 * 1000.0;

            for (int i = 1; i < count; i++)
            {
                //this.Datas.Add(random.NextDouble() * 10);

                double log = Math.Log10(i);
                this.Datas.Add(Convert.ToDouble(i) / (count / 10.0));
                this.xDatas.Add(log);

                int c = (int)(log);

                if (log % 1 == 0)
                {
                    this.xAxis.Add(log);
                }

                if (i % Math.Pow(10, c) == 0)
                {
                    this.xGridAxis.Add(log);

                    //this.xAxis.Add(log);
                }

            }

            double p = Math.Log10(count);

            double v = p % 1 == 0 ? p : p + 1;

            this.xAxis.Add((int)v);
            this.xGridAxis.Add((int)v);

            for (int i = 0; i < 10; i++)
            {
                _autoCompleteSource.Add(random.Next(0, 10000000).ToString());
            }

            //_autoCompleteSource.Add("测试");
            //_autoCompleteSource.Add("成功");
            //_autoCompleteSource.Add("失败");
            //_autoCompleteSource.Add("123456789");
            _autoCompleteSource.Add("abcdefghijklmnopqrstuvwxyz");




            _selectedItems.CollectionChanged += (l, k) =>
              {
                  Debug.WriteLine(this.SelectedItems.Count);
              };

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "PropertyConfig.xml");

            Config testConfig = new Config();

            testConfig = testConfig.LoadFromFile(filePath);

            this.Config = testConfig;


            //  Do ：构造树节点
            for (int i = 0; i < 5; i++)
            {
                TreeNodeBase<Student> l1 = new TreeNodeBase<Student>(Student.Random());

                for (int j = 0; j < 3; j++)
                {
                    TreeNodeBase<Student> l2 = new TreeNodeBase<Student>(Student.Random());

                    l1.AddNode(l2);

                    for (int k = 0; k < 5; k++)
                    {
                        TreeNodeBase<Student> l3 = new TreeNodeBase<Student>(Student.Random());

                        l2.AddNode(l3);
                    }
                }
                this.TreeSource.Add(l1);
            }
        }

        private Random random = new Random();

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
                //MessageService.ShowPercent(l =>
                //{
                //    for (int i = 0; i < 100; i++)
                //    {
                //        l.ProgressState = TaskbarItemProgressState.Normal;

                //        l.ProgressValue = i / 100.0;

                //        Thread.Sleep(10);
                //    }

                //    l.ProgressValue = 0;
                //});

                await MessageProxy.TaskBar.ShowWaitting(() =>
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
                MessageProxy.Notify.ShowInfo("移动到了指定按钮18");
            }
            //  Do：取消
            else if (string.IsNullOrEmpty(command))
            {
                MessageProxy.Notify.ShowInfo("测试");
            }

            //  Do：取消
            else if (command == "Button.Click.AddProject")
            {
                ProjectService.Instance.Add(new ProjectItem() { Title = "测试", Path = "C:DG/DGD/DS/DFD" });

            }


            if (obj is MouseWheelEventArgs args)
            {

            }
        }



        public void Method()
        {
            Debug.WriteLine("Method");
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



    internal class TextBoxViewModel : ValidationPropertyChanged
    {
        private string _name;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                RaisePropertyChanged("Name");
            }
        }


        private string _age;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }


        private string _email;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮箱地址不合法！")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }


        private string _phone;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }


        private string _account;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$", ErrorMessage = "字母开头，允许5-16字节，允许字母数字下划线！")]
        public string Accont
        {
            get { return _account; }
            set
            {
                _account = value;
                RaisePropertyChanged("Accont");
            }
        }


        private string _passWord;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字母、数字和下划线！！")]
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }


        private string _regin;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "只能5位的数字")]
        public string Regin
        {
            get { return _regin; }
            set
            {
                _regin = value;
                RaisePropertyChanged("Regin");
            }
        }


        private string _limit;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5,8}$", ErrorMessage = "只能5-8位的数字")]
        public string Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                RaisePropertyChanged("Limit");
            }
        }


        private string _cardID;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{15}|\d{18}$", ErrorMessage = "身份证号码不合法！")]
        public string CardID
        {
            get { return _cardID; }
            set
            {
                _cardID = value;
                RaisePropertyChanged("CardID");
            }
        }

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.CheckDataSumit")
            {
                if (this.IsValid())
                {
                    MessageProxy.Snacker.ShowTime("数据校验成功！");
                }
                else
                {
                    MessageProxy.Snacker.ShowTime("数据校验错误 - " + this.Error);
                }

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

    //public class StudentPropertyTemplateSelector : PropertyGridTemplateSelector
    //{
    //    public DataTemplate UnkownTempate { get; set; }

    //    public DataTemplate RunningTempate { get; set; }

    //    public DataTemplate ErrorTempate { get; set; }

    //    public DataTemplate SuccessTempate { get; set; }

    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {

    //        if (item is ObjectPropertyItem objectItem)
    //        {
    //            if (objectItem.PropertyInfo.Name == "Age") return RunningTempate;
    //        }

    //        return base.SelectTemplate(item, container);
    //    }
    //}

    public class Calc : ContentControl
    {
        [TypeConverter(typeof(DelegateConverter<Func<double, double>>))]
        public Func<double, double> Func
        {
            get { return (Func<double, double>)GetValue(FuncProperty); }
            set { SetValue(FuncProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FuncProperty =
            DependencyProperty.Register("Func", typeof(Func<double, double>), typeof(Calc), new FrameworkPropertyMetadata(default(Func<double, double>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Calc control = d as Calc;

                 if (control == null) return;

                 if (e.OldValue is Func<double, double> o)
                 {

                 }

                 if (e.NewValue is Func<double, double> n)
                 {

                 }

                 control.Refresh();
             }));


        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(Calc), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Calc control = d as Calc;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

                 control.Refresh();
             }));

        private void Refresh()
        {
            this.Y = this.Func.Invoke(this.X);
        }


        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(Calc), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Calc control = d as Calc;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


    }
}
