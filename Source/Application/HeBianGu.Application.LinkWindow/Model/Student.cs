using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HeBianGu.Application.LinkWindow
{
    public class Student
    {
        [Display(Name = "姓名",Order =0)]
        [Required()]
        public string Name { get; set; }

        [Display(Name = "班级", Order = 0)]
        [Required]
        public string Class { get; set; }

        [Display(Name = "地址", Order = 0)]
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
}
