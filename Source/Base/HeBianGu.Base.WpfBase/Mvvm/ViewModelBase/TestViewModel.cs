// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 开发原型用的模型 </summary>
    public partial class TestViewModel : TreeNodeBase<DateTime>
    {
        public TestViewModel() : base(DateTime.Now)
        {

        }

        private static Random _random = new Random();

        protected override void Init()
        {
            base.Init();

            System.Reflection.PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo item in ps)
            {
                if (item.PropertyType == typeof(double))
                {
                    if (item.CanWrite)
                    {
                        item.SetValue(this, Math.Round(_random.NextDouble() * 100, 2));
                    }
                }
            }
        }

        private Brush _brush;
        /// <summary> 说明  </summary>
        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                RaisePropertyChanged();
            }
        }



        private bool _bool1;
        public bool Bool1
        {
            get { return _bool1; }
            set
            {
                _bool1 = value;
                RaisePropertyChanged();
            }
        }


        private bool _bool2;
        /// <summary> 说明  </summary>
        public bool Bool2
        {
            get { return _bool2; }
            set
            {
                _bool2 = value;
                RaisePropertyChanged();
            }
        }


        private bool _bool3;
        /// <summary> 说明  </summary>
        public bool Bool3
        {
            get { return _bool3; }
            set
            {
                _bool3 = value;
                RaisePropertyChanged();
            }
        }


        private string _value;
        /// <summary> 说明  </summary>
        public new string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        private string _value1;
        /// <summary> 说明  </summary>
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged("Value1");
            }
        }


        private string _value2;
        /// <summary> 说明  </summary>
        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged("Value2");
            }
        }


        private string _value3;
        /// <summary> 说明  </summary>
        public string Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                RaisePropertyChanged("Value3");
            }
        }


        private string _value4;
        /// <summary> 说明  </summary>
        public string Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                RaisePropertyChanged("Value4");
            }
        }


        private string _value5;
        /// <summary> 说明  </summary>
        public string Value5
        {
            get { return _value5; }
            set
            {
                _value5 = value;
                RaisePropertyChanged("Value5");
            }
        }


        private string _value6;
        /// <summary> 说明  </summary>
        public string Value6
        {
            get { return _value6; }
            set
            {
                _value6 = value;
                RaisePropertyChanged("Value6");
            }
        }


        private string _value7;
        /// <summary> 说明  </summary>
        public string Value7
        {
            get { return _value7; }
            set
            {
                _value7 = value;
                RaisePropertyChanged("Value7");
            }
        }


        private string _value9;
        /// <summary> 说明  </summary>
        public string Value9
        {
            get { return _value9; }
            set
            {
                _value9 = value;
                RaisePropertyChanged("Value9");
            }
        }


        private int _int1;
        /// <summary> 说明  </summary>
        public int Int1
        {
            get { return _int1; }
            set
            {
                _int1 = value;
                RaisePropertyChanged("Int1");
            }
        }

        private int _int2;
        /// <summary> 说明  </summary>
        public int Int2
        {
            get { return _int2; }
            set
            {
                _int2 = value;
                RaisePropertyChanged("Int2");
            }
        }


        private int _int3;
        /// <summary> 说明  </summary>
        public int Int3
        {
            get { return _int3; }
            set
            {
                _int3 = value;
                RaisePropertyChanged("Int3");
            }
        }



        private DateTime _datetime1;
        /// <summary> 说明  </summary>
        public DateTime DateTime1
        {
            get { return _datetime1; }
            set
            {
                _datetime1 = value;
                RaisePropertyChanged("DateTime1");
            }
        }


        private DateTime _datetime2;
        /// <summary> 说明  </summary>
        public DateTime DateTime2
        {
            get { return _datetime2; }
            set
            {
                _datetime2 = value;
                RaisePropertyChanged("DateTime2");
            }
        }

        private DateTime _datetime3;
        /// <summary> 说明  </summary>
        public DateTime DateTime3
        {
            get { return _datetime3; }
            set
            {
                _datetime3 = value;
                RaisePropertyChanged("DateTime3");
            }
        }


        private double _double1;
        /// <summary> 说明  </summary>
        public double Double1
        {
            get { return _double1; }
            set
            {
                _double1 = value;
                RaisePropertyChanged("Double1");
            }
        }

        private double _double2;
        /// <summary> 说明  </summary>
        public double Double2
        {
            get { return _double2; }
            set
            {
                _double2 = value;
                RaisePropertyChanged("Double2");
            }
        }
        private double _double3;
        /// <summary> 说明  </summary>
        public double Double3
        {
            get { return _double3; }
            set
            {
                _double3 = value;
                RaisePropertyChanged("Double3");
            }
        }
        private double _double4;
        /// <summary> 说明  </summary>
        public double Double4
        {
            get { return _double4; }
            set
            {
                _double4 = value;
                RaisePropertyChanged("Double4");
            }
        }
        private double _double5;
        /// <summary> 说明  </summary>
        public double Double5
        {
            get { return _double5; }
            set
            {
                _double5 = value;
                RaisePropertyChanged("Double5");
            }
        }
        private double _double6;
        /// <summary> 说明  </summary>
        public double Double6
        {
            get { return _double6; }
            set
            {
                _double6 = value;
                RaisePropertyChanged("Double6");
            }
        }
        private double _double7;
        /// <summary> 说明  </summary>
        public double Double7
        {
            get { return _double7; }
            set
            {
                _double7 = value;
                RaisePropertyChanged("Double7");
            }
        }
        private double _double8;
        /// <summary> 说明  </summary>
        public double Double8
        {
            get { return _double8; }
            set
            {
                _double8 = value;
                RaisePropertyChanged("Double8");
            }
        }
        private double _double9;
        /// <summary> 说明  </summary>
        public double Double9
        {
            get { return _double9; }
            set
            {
                _double9 = value;
                RaisePropertyChanged("Double9");
            }
        }
        private double _double10;
        /// <summary> 说明  </summary>
        public double Double10
        {
            get { return _double10; }
            set
            {
                _double10 = value;
                RaisePropertyChanged("Double10");
            }
        }
        private double _double11;
        /// <summary> 说明  </summary>
        public double Double11
        {
            get { return _double11; }
            set
            {
                _double11 = value;
                RaisePropertyChanged("Double11");
            }
        }
        private double _double12;
        /// <summary> 说明  </summary>
        public double Double12
        {
            get { return _double12; }
            set
            {
                _double12 = value;
                RaisePropertyChanged("Double12");
            }
        }
        private double _double13;
        /// <summary> 说明  </summary>
        public double Double13
        {
            get { return _double13; }
            set
            {
                _double13 = value;
                RaisePropertyChanged("Double13");
            }
        }
        private double _double14;
        /// <summary> 说明  </summary>
        public double Double14
        {
            get { return _double14; }
            set
            {
                _double14 = value;
                RaisePropertyChanged("Double14");
            }
        }
        private double _double15;
        /// <summary> 说明  </summary>
        public double Double15
        {
            get { return _double15; }
            set
            {
                _double15 = value;
                RaisePropertyChanged("Double15");
            }
        }

        private double _double16;
        /// <summary> 说明  </summary>
        public double Double16
        {
            get { return _double16; }
            set
            {
                _double16 = value;
                RaisePropertyChanged("Double16");
            }
        }
    }


    public class Student : ICloneable
    {
        [DataGridColumn("*")]
        [Display(Name = "姓名", GroupName = "基础信息")]
        [Required()]
        public string Name { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "班级", GroupName = "基础信息")]
        [Required]
        public string Class { get; set; }

        [DataGridColumn("2*")]
        [Display(Name = "地址", GroupName = "基础信息")]
        [Required]
        public string Address { get; set; }

        [DataGridColumn("2*")]
        [Display(Name = "邮箱", GroupName = "基础信息")]
        [Required]
        public string Emall { get; set; }

        [Display(Name = "可用", GroupName = "其他信息")]
        [Required]
        public bool IsEnbled { get; set; }

        [Display(Name = "时间", GroupName = "其他信息")]
        [Required]
        public DateTime Time { get; set; }

        [Display(Name = "年龄", GroupName = "基础信息")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "分数", GroupName = "成绩信息")]
        public double Score { get; set; }

        [DataGridColumn("2*")]
        [Display(Name = "电话号码", GroupName = "基础信息")]
        [Required]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Tel { get; set; }

        private static Random random = new Random();
        private static string[] names = new string[] { "Lucy", "Jim", "Tom", "Lulu", "Angle", "Linda", "Summi" };
        private static string[] mails = new string[] { "@163.com", "@126.com", "@qq.com", "@gmail.com" };
        private static string[] address = new string[] { "浙江省 台州市 临海市", "广西壮族自治区 钦州市 钦北区", "重庆 重庆市 南岸区", "上海 上海市 闵行区", "香港特别行政区 香港岛 南区", "新疆维吾尔自治区 喀什地区 伽师县", "Summi" };
        public Student()
        {
            this.Name = names[random.Next(names.Count())];
            this.Class = random.Next(1, 6).ToString() + "班";
            this.Address = address[random.Next(address.Count())];
            this.Emall = random.Next(0, 99999999).ToString().PadLeft(8, '0') + mails[random.Next(mails.Count())];
            this.IsEnbled = random.Next(1, 3) == 2;
            this.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            this.Age = random.Next(10, 50);
            this.Score = Math.Round(random.NextDouble() * 100, 2);
            this.Tel = "13" + random.Next(9) + random.Next(0, 99999999).ToString().PadLeft(8, '0');
        }

        public static Student Random()
        {
            Student student = new Student();
            student.Name = names[random.Next(names.Count())];
            student.Class = random.Next(1, 6).ToString() + "班";
            student.Address = address[random.Next(address.Count())];
            student.Emall = random.Next(0, 99999999).ToString().PadLeft(8, '0') + mails[random.Next(mails.Count())];
            student.IsEnbled = random.Next(1, 3) == 2;
            student.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            student.Age = random.Next(10, 50);
            student.Score = Math.Round(random.NextDouble() * 100, 2);
            student.Tel = "13" + random.Next(9) + random.Next(0, 99999999).ToString().PadLeft(8, '0');
            return student;
        }

        public object Clone()
        {
            return Student.Random();
        }

        public static List<Student> Randoms(int c = 100)
        {
            return Enumerable.Range(0, c).Select(x => Student.Random()).ToList();
        }
    }
}
