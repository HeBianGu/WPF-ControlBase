using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Shuttle;
using HeBianGu.Control.Step;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [ViewModel("Custom")]
    internal class CustomViewModel : MvcViewModelBase
    {
        //private StudentViewModel _student = new StudentViewModel();
        ///// <summary> 说明  </summary>
        //public StudentViewModel Student
        //{
        //    get { return _student; }
        //    set
        //    {
        //        _student = value;
        //        RaisePropertyChanged("Student");
        //    }
        //}


        //Shuttles


        private ObservableCollection<ShuttleItem> _shuttles = new ObservableCollection<ShuttleItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ShuttleItem> Shuttles
        {
            get { return _shuttles; }
            set
            {
                _shuttles = value;
                RaisePropertyChanged("Shuttles");
            }
        }

        private ObservableCollection<string> _ips = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> Ips
        {
            get { return _ips; }
            set
            {
                _ips = value;
                RaisePropertyChanged("Ips");
            }
        }



        private ObservableCollection<StepItem> _stepItem = new ObservableCollection<StepItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<StepItem> StepItems
        {
            get { return _stepItem; }
            set
            {
                _stepItem = value;
                RaisePropertyChanged("StepItems");
            }
        }


        private ObservableCollection<Teacher> _teachers = new ObservableCollection<Teacher>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                RaisePropertyChanged("Teachers");
            }
        }



        private IEnumerable _outSource1;
        /// <summary> 说明  </summary>
        public IEnumerable OutSource1
        {
            get { return _outSource1; }
            set
            {
                _outSource1 = value;
                RaisePropertyChanged();
            }
        }

        private IEnumerable _outSource2;
        /// <summary> 说明  </summary>
        public IEnumerable OutSource2
        {
            get { return _outSource2; }
            set
            {
                _outSource2 = value;
                RaisePropertyChanged();
            }
        }

        protected override void Init()
        {

            {
                ShuttleItem shuttle = new ShuttleItem
                {
                    Header = "第一组"
                };

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 0; i < 20; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                Shuttles.Add(shuttle);
            }

            {
                ShuttleItem shuttle = new ShuttleItem
                {
                    Header = "第二组"
                };

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 20; i < 30; i++)
                {
                    source.Add(new TestViewModel() { Value = i + 1.ToString() });
                }

                shuttle.ItemSource = source;

                Shuttles.Add(shuttle);
            }

            {
                ShuttleItem shuttle = new ShuttleItem
                {
                    Header = "第三组"
                };

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 30; i < 35; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                Shuttles.Add(shuttle);
            }

            //  Message：获取所有IP
            string hostName = Dns.GetHostName();

            string address = Dns.GetHostEntry(hostName).AddressList?.FirstOrDefault(l => l.AddressFamily == AddressFamily.InterNetwork)?.ToString();

            string format = address.Remove(address.LastIndexOf('.'));

            Ips = Enumerable.Range(1, 255).Select(l => format + "." + l)?.ToObservable();

            //  Message：设置StepState Source

            StepItems.Clear();

            StepItems.Add(new StepItem() { DisplayName = "1", Message = "准备开始" });
            StepItems.Add(new StepItem() { DisplayName = "2", Message = "步骤一" });
            StepItems.Add(new StepItem() { DisplayName = "3", Message = "步骤二" });
            StepItems.Add(new StepItem() { DisplayName = "4", Message = "步骤三" });
            StepItems.Add(new StepItem() { DisplayName = "5", Message = "步骤四" });
            StepItems.Add(new StepItem() { DisplayName = "6", Message = "步骤五" });
            StepItems.Add(new StepItem() { DisplayName = "7", Message = "完成" });

            //  Do ：设置筛选器控件数据
            for (int i = 0; i < 1000; i++)
            {
                Teachers.Add(Teacher.Random());
            }
        }

        protected override void Loaded(string args)
        {
            //this.Student.LoadDefault();




        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);
        private Random _random = new Random();
        protected async void RelayMethod(object obj)

        {
            if (obj?.ToString() == "Button.Click.RunStepState")
            {
                StepItems.Clear();

                StepItems.Add(new StepItem() { DisplayName = "1", Message = "准备开始" });
                StepItems.Add(new StepItem() { DisplayName = "2", Message = "步骤一" });
                StepItems.Add(new StepItem() { DisplayName = "3", Message = "步骤二" });
                StepItems.Add(new StepItem() { DisplayName = "4", Message = "步骤三" });
                StepItems.Add(new StepItem() { DisplayName = "5", Message = "步骤四" });
                StepItems.Add(new StepItem() { DisplayName = "6", Message = "步骤五" });
                StepItems.Add(new StepItem() { DisplayName = "7", Message = "完成" });

                await Task.Run(() =>
                 {
                     foreach (StepItem item in StepItems)
                     {
                         string temp = item.Message;
                         //  Do ：这里面要手动刷新才会更新状态
                         item.State = 1;
                         this.StepItems.Refresh();
                         for (int i = 1; i < 101; i++)
                         {
                             item.Percent = i;
                             item.Message = $"{temp}({i}/100)";
                             Thread.Sleep(30);
                         }

                         item.Message = temp;
                         int v = _random.Next(10);
                         //  Do ：这里面要手动刷新才会更新状态
                         item.State = v == 1 ? -1 : 2;
                         item.State = 2;
                         if (v == 1)
                         {
                             MessageProxy.Snacker.Show("运行错误，请重新运行");
                             return;
                         }
                     }
                     MessageProxy.Snacker.Show("运行完成");
                 });
            }
        }

    }


    public class Teacher
    {
        [Display(Name = "姓名")]
        [Required()]
        public string Name { get; set; }

        [Display(Name = "班级")]
        [Required]
        public string Class { get; set; }

        [Display(Name = "地址")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "邮箱")]
        [Required]
        public string Emall { get; set; }

        [Display(Name = "可用")]
        [Required]
        public bool IsEnbled { get; set; }

        [Display(Name = "时间")]
        [Required]
        public DateTime Time { get; set; }

        [Display(Name = "年龄")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "平均分")]
        public double Score { get; set; }

        [Display(Name = "电话号码")]
        [Required]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Tel { get; set; }

        private static Random random = new Random();

        public static Teacher Random()
        {
            Teacher student = new Teacher
            {
                Name = "HeBianGu",
                Class = random.Next(1, 20).ToString(),
                Address = random.Next(30, 50).ToString(),
                Emall = random.Next(30, 50).ToString(),
                IsEnbled = random.Next(1, 3) == 2,
                Time = DateTime.Now.AddDays(random.Next(-50, 50)),
                Age = random.Next(30, 50),
                Score = random.Next(90, 100),
                Tel = random.NextDouble().ToString()
            };
            return student;
        }
    }
}
