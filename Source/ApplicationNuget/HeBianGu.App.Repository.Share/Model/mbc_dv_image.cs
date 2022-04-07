using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Repository
{
    public class mbc_dv_image : mbc_db_modelbase
    {
        [Required]
        [Display(Name = "资源名称")]
        public string Name { get; set; }

        [Display(Name = "资源类型")]
        public string MediaType { get; set; }

        [Display(Name = "所属案例")]
        public string CaseType { get; set; }

        [Required]
        [Display(Name = "资源路径")]
        public string Url { get; set; }

        [Display(Name = "资源标签")]
        public string TagTypes { get; set; }

        [Display(Name = "资源区域")]
        public string AreaType { get; set; }

        [Display(Name = "扩展名")]
        public string ExtendType { get; set; }

        [Display(Name = "清晰度")]
        public string ArticulationType { get; set; }

        [Display(Name = "文件大小")]
        public long Size { get; set; }

        [Display(Name = "资源权限")]
        public string VipType { get; set; }

        [Display(Name = "资源来源")]
        public string FromType { get; set; }

        [Display(Name = "资源排序")]
        public string OrderNum { get; set; }

        [Display(Name = "总播放次数")]
        public string PlayCount { get; set; }

        [Display(Name = "总评分")]
        public string Score { get; set; }

        /// <summary> 视频格式 </summary>
        [Display(Name = "视频格式")]
        public string Type { get; set; }

        /// <summary> 分辨率 yuv420p </summary>
        [Display(Name = "分辨率")]
        public string Resoluction { get; set; }

        /// <summary> 宽高比 1920x1080 </summary>
        [Display(Name = "宽高比")]
        public string Aspect { get; set; }



    }
}
