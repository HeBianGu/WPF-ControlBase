// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Application.MainWindow
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {
            ServiceRegistry.Instance.Register<ShellViewModel>();
        }
        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();


    }

    public class Property
    {
        [Display(Name = "double")]
        public double Score { get; set; }

        [Display(Name = "int")]
        public int Age { get; set; }

        [Display(Name = "double[]")]
        public double[] Scores { get; set; }

        [Display(Name = "int[]")]
        public int[] Scores1 { get; set; }

        [Display(Name = "List<double>")]
        public List<double> Ages { get; set; }

        [Display(Name = "List<int>")]
        public List<int> Ages1 { get; set; }


        [Display(Name = "byte[]")]
        public byte[] Bytes { get; set; }

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

        private static Random random = new Random();

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

    internal class StudentViewModel : ValidationModelViewModel<Student>
    {
        public StudentViewModel() : base(new Student())
        {

        }
        private ValidationProperty<string> _tel;

        public ValidationProperty<string> Tel
        {
            get
            {
                //  Do ：只有第一遍从实体里面加载数据
                return _tel = _tel ?? this.CreateModelProperty<string>();
            }
            set
            {
                _tel = value;
                RaisePropertyChanged();
            }
        }
    }




}
