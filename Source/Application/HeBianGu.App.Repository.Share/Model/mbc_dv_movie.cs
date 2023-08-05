using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Systems.Repository;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.App.Repository
{
    public class mbc_dv_movie : mbc_db_modelbase
    {
        [DataGridColumn("*")]
        [Required]
        [Display(Name = "资源名称")]
        public string Name { get; set; }
        [DataGridColumn("*")]
        [Display(Name = "资源类型")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_mediatype MediaType { get; set; }
        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "所属案例")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_dc_case CaseType { get; set; }
        [DataGridColumn("*")]
        [Required]
        [Display(Name = "资源路径")]
        public string Url { get; set; }
        [DataGridColumn("*")]
        [XmlIgnore]
        [Display(Name = "资源标签")]
        [PropertyItemType(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public ICollection<mbc_db_tagtype> TagTypes { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "扩展名")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_extendtype ExtendType { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "文件大小")]
        public long Size { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "视频时长")]
        public string Duration { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "分辨率")]
        public string Resoluction { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "清晰度")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_articulationtype ArticulationType { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "缩略图")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_dv_movieimage Image { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "资源权限")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_viptype VipType { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "总评分")]
        public string Score { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "资源来源")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_fromtype FromType { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "资源区域")]
        [PropertyItemType(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_areatype AreaType { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "资源排序")]
        public string OrderNum { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "总播放次数")]
        public string PlayCount { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "比特率")]
        public string Bitrate { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "编码格式")]
        public string MediaCode { get; set; }

        [DataGridColumn("*")]
        [Display(Name = "视频格式")]
        public string VedioType { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "宽高比")]
        public string Aspect { get; set; }

        [DataGridColumn("*")]
        [Browsable(false)]
        [Display(Name = "帧频")]
        public string Rate { get; set; }
    }
}
