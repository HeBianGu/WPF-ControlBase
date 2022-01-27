using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Repository
{
    public class mbc_dc_case : mbc_db_modelbase
    {
        [Required]
        [Display(Name = "案例名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "案例路径")]
        //[RegularExpression( @"^(?<fpath>([a - zA - Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w])",ErrorMessage ="请输入有效路径")]  
        public string BaseUrl { get; set; }

        /// <summary> 案例状态 0 = 初始状态 1 = 已加载 </summary>
        [Required]
        [Display(Name = "案例状态")]
        [ReadOnly(true)]
        //[RegularExpression( @"^(?<fpath>([a - zA - Z]:\\)([\s\.\-\w]+\\)*)(?<fname>[\w])",ErrorMessage ="请输入有效路径")]  
        public int State { get; set; } = 0;

    }
}
