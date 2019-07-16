using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Mvc
{
    /// <summary>
    /// CenterControl.xaml 的交互逻辑
    /// </summary>
    public partial class CenterControl : UserControl
    {
        public CenterControl()
        {
            InitializeComponent();
        }
    }

    public class Student
    {
        [Display("姓名")]
        [Required]
        public string Name { get; set; }

        [Display("班级")]
        [Required]
        public string Class { get; set; }

        [Display("地址")]
        [Required]
        public string Address { get; set; }

        [Display("邮箱")]
        [Required]
        public string Emall { get; set; }

        [Display("可用")]
        [Required]
        public bool IsEnbled { get; set; }

        [Display("时间")]
        [Required]
        public DateTime time { get; set; }

        [Display("年龄")]
        [Required]
        public int Age { get; set; }

        [Display("平均分")] 
        public double Score { get; set; }

        [Display("电话号码")]
        [Required]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Tel { get; set; }
    }
}
