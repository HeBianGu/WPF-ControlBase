using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Identity
{
    public class hi_dd_role : mbc_db_modelbase
    {
        public hi_dd_role()
        {
            Name = "普通角色";
        }
        [Display(Name = "角色名称")]
        [Column("role_name", Order = 1)]
        [Required]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string Name { get; set; }

        [Display(Name = "角色编码")]
        [Column("role_code", Order = 2)]
        public string Code { get; set; }

        [XmlIgnore]
        [Display(Name = "权限列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public ICollection<hi_dd_author> Authors { get; set; }=new ObservableCollection<hi_dd_author>();

        [XmlIgnore]
        [Display(Name = "用户列表")]
        //[PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public ICollection<hi_dd_user> Users { get; set; }=new ObservableCollection<hi_dd_user>();
    }
}
