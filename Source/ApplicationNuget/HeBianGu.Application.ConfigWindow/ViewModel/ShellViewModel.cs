using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Application.ConfigWindow
{
    class ShellViewModel : NotifyPropertyChanged
    { 
        private string _filePath = @"D:\WorkArea\AutoTestPlus\Source\JiuJinTech.ATS.Client\bin\Debug\TestItemConfig.xml";
        /// <summary> 说明  </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged("FilePath");
            }
        }


        private TestConfigViewModel _config;
        /// <summary> 说明  </summary>
        public TestConfigViewModel Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged("Config");
            }
        }


        private TestConfig _testConfig;
        /// <summary> 说明  </summary>
        public TestConfig TestConfig
        {
            get { return _testConfig; }
            set
            {
                _testConfig = value;
                RaisePropertyChanged("TestConfig");
            }
        }



        private Student _student=new Student();
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


        AssemblyDomain _domian = new AssemblyDomain();
        protected override void Init()
        {
            if (!File.Exists(this.FilePath)) return;

            var config= _domian.LoadTestConfig(this.FilePath);

            this.TestConfig = new TestConfig();

            this.Config = new TestConfigViewModel(config);
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "Button.Click.Save")
            {
                this.Config.Save(this.FilePath);
            }
            else if (command == "TextBox.TextChanged.FilePath")
            {
                var config = _domian.LoadTestConfig(this.FilePath);


                this.TestConfig = config;

                this.Config = new TestConfigViewModel(config);
            }

        }
    }

    public class Student
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

        static Random random = new Random();

        public Student()
        {
            this.Name = "HeBianGu";
            this.Class = random.Next(1, 20).ToString();
            this.Address = random.Next(30, 50).ToString();
            this.Emall = random.Next(30, 50).ToString();
            this.IsEnbled = random.Next(1, 3) == 2;
            this.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            this.Age = random.Next(30, 50);
            this.Score = random.Next(90, 100);
            this.Tel = random.NextDouble().ToString();
        }

        public static Student Random()
        {
            Student student = new Student();
            student.Name = "HeBianGu";
            student.Class = random.Next(1, 20).ToString();
            student.Address = random.Next(30, 50).ToString();
            student.Emall = random.Next(30, 50).ToString();
            student.IsEnbled = random.Next(1, 3) == 2;
            student.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            student.Age = random.Next(30, 50);
            student.Score = random.Next(90, 100);
            student.Tel = random.NextDouble().ToString();
            return student;
        }
    }
}
