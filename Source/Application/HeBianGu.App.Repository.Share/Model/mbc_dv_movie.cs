using HeBianGu.Control.PropertyGrid;
using HeBianGu.Systems.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.App.Repository
{
    public class mbc_dv_movie : mbc_db_modelbase
    {
        [Required]
        [Display(Name = "资源名称")]
        public string Name { get; set; }

        [Display(Name = "资源类型")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_mediatype MediaType { get; set; }

        [Display(Name = "所属案例")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_dc_case CaseType { get; set; }

        [Required]
        [Display(Name = "资源路径")]
        public string Url { get; set; }

        [XmlIgnore]
        [Display(Name = "资源标签")]
        [Property(Type = typeof(MultiSelectRepositoryPropertyItem))]
        public ICollection<mbc_db_tagtype> TagTypes { get; set; }

        [Display(Name = "资源区域")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_areatype AreaType { get; set; }

        [Display(Name = "扩展名")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_extendtype ExtendType { get; set; }

        [Display(Name = "清晰度")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_articulationtype ArticulationType { get; set; }

        [Display(Name = "文件大小")]
        public long Size { get; set; }

        [Display(Name = "缩略图")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_dv_movieimage Image { get; set; }

        [Display(Name = "资源权限")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_viptype VipType { get; set; }

        [Display(Name = "资源来源")]
        [Property(Type = typeof(ListBoxRepositoryPropertyItem))]
        public mbc_db_fromtype FromType { get; set; }

        [Display(Name = "资源排序")]
        public string OrderNum { get; set; }

        [Display(Name = "总播放次数")]
        public string PlayCount { get; set; }

        [Display(Name = "总评分")]
        public string Score { get; set; }

        /// <summary> 视频时长 </summary>
        [Display(Name = "视频时长")]
        public string Duration { get; set; }

        /// <summary> 比特率 </summary>
        [Display(Name = "比特率")]
        public string Bitrate { get; set; }

        /// <summary> 编码格式 h264 (Constrained Baseline) (avc1 / 0x31637661) </summary>
        [Display(Name = "编码格式")]
        public string MediaCode { get; set; }

        /// <summary> 视频格式 </summary>
        [Display(Name = "视频格式")]
        public string VedioType { get; set; }

        /// <summary> 分辨率 yuv420p </summary>
        [Display(Name = "分辨率")]
        public string Resoluction { get; set; }

        /// <summary> 宽高比 1920x1080 </summary>
        [Display(Name = "宽高比")]
        public string Aspect { get; set; }

        /// <summary> 帧频 14.68 fps </summary>
        [Display(Name = "帧频")]
        public string Rate { get; set; }
    }
}
