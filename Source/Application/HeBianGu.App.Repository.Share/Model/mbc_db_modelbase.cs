using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Repository
{
    public class mbc_db_modelbase : IndentifyEntityBase
    {
        public mbc_db_modelbase()
        {
            var name = this.GetType().GetProperty("Name");

            name?.SetValue(this, "名称");
        }
        [Browsable(false)]
        [Display(Name = "创建时间")]
        [ReadOnly(true)]
        public string CDATE { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff");

        [Browsable(false)]
        [Display(Name = "修改时间")]
        [ReadOnly(true)]
        public string UDATE { get; set; }

        [Browsable(false)]
        [Display(Name = "是否可用")]
        [ReadOnly(true)]
        public int ISENBLED { get; set; } = 1;
    }
}
