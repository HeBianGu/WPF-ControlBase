using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeBianGu.App.Repository
{
    public abstract class IndentifyEntityBase : StringEntityBase
    {
        //[Browsable(false)]
        //[Display(Name = "ID")]
        //public string ID { get; set; } = Guid.NewGuid().ToString();

        [Browsable(false)]
        [Display(Name = "创建时间")]
        [Column("create_time", Order = 20)]
        public string CDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Browsable(false)]
        [Display(Name = "修改时间")]
        [Column("edit_time", Order = 21)]
        public string UDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        [Browsable(false)]
        [Display(Name = "是否可用")]
        [Column("is_enabled", Order = 22)]
        public bool IsEnabled { get; set; } = true;
    }
}
