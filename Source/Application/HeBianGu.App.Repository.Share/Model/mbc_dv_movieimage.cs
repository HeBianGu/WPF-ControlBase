using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace HeBianGu.App.Repository
{
    public class mbc_dv_movieimage : mbc_db_modelbase
    {
        [DataGridColumn("*")]
        [Required]
        [Display(Name = "所属电影")]
        public string MovieID { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "图片内容")]
        public string Image { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "显示名称")]
        public string Text { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "排序")]
        public string OrderNum { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "时间点")]
        public string TimeSpan { get; set; }


    }
}
