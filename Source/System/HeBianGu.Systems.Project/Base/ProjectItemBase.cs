// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Systems.Project
{
    public abstract class ProjectItemBase : NotifyPropertyChangedBase, IProjectItem
    {
        private string _title;
        [Required]
        [Display(Name = "标题", Order = 4)]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _path;
        [ReadOnly(true)]
        [Required]
        [Display(Name = "文件路径", Order = 4)]
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        private bool _isFixed;
        [Display(Name = "是否固定", Order = 4)]
        public bool IsFixed
        {
            get { return _isFixed; }
            set
            {
                _isFixed = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _createTime = DateTime.Now;
        [ReadOnly(true)]
        [Display(Name = "创建时间", Order = 4)]
        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _updateTime = DateTime.Now;
        [ReadOnly(true)]
        [Display(Name = "修改时间", Order = 4)]
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set
            {
                _updateTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
