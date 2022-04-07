using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Blur
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {
            ServiceRegistry.Instance.Register<ShellViewModel>();
        }
        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();


    }

    public enum Rainbow
    {
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet
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
        public DateTime time { get; set; }

        [Display(Name = "年龄")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "平均分")]
        public double Score { get; set; }

        [Display(Name = "电话号码")]
        [Required]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Tel { get; set; }
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
                return _tel = _tel ?? CreateModelProperty<string>();
            }
            set
            {
                _tel = value;
                RaisePropertyChanged();
            }
        }
    }




}
