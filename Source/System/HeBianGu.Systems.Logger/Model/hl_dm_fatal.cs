using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeBianGu.Systems.Logger
{
    public class hl_dm_fatal : LogEntityBase
    {
        [Display(Name = "参数信息")]
        [Column("parameter", Order = 3)]
        public string Parameter
        {
            get;
            set;
        }

        [Display(Name = "异常消息")]
        [Column("message", Order = 4)]
        public string Message
        {
            get;
            set;
        }

        [Display(Name = "异常详情")]
        [Column("excetion", Order = 5)]
        public string Exception
        {
            get;
            set;
        }

        [Display(Name = "堆栈信息")]
        [Column("stack", Order = 6)]
        public string Stack
        {
            get;
            set;
        }

        [Display(Name = "系统信息")]
        [Column("system", Order = 7)]
        public string System
        {
            get;
            set;
        }

        [Display(Name = "进程信息")]
        [Column("process", Order = 8)]
        public string Process
        {
            get;
            set;
        }
    }
}
