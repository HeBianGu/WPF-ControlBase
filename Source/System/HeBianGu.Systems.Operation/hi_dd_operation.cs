
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeBianGu.Systems.Operation
{
    public class hi_dd_operation : mbc_db_modelbase
    {
        [Browsable(false)]
        public string UserID { get; set; }

        [Display(Name = "操作账号")]
        //[Column("account", Order = 1)]
        public string UserName { get; set; }

        //[Browsable(false)]
        //[Display(Name = "操作用户")]
        //public hi_dd_user User { get; set; }

        [Display(Name = "操作")]
        public string Type { get; set; }

        [Display(Name = "信息")]
        public string Title { get; set; }

        [Display(Name = "详情")]
        public string Message { get; set; }

        [Browsable(false)]
        [Display(Name = "操作类型")]
        public OperationType OperationType { get; set; }

        [Display(Name = "结果")]
        public bool Result { get; set; }

        [Display(Name = "操作日期")]
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
