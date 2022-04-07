// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Systems.Project
{
    public abstract class ProjectItemBase : NotifyPropertyChangedBase, IProjectItem
    {
        private DateTime _updateTime = DateTime.Now;
        /// <summary> 说明  </summary>
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set
            {
                _updateTime = value;
                RaisePropertyChanged();
            }
        }

        private bool _isFixed;
        /// <summary> 说明  </summary>
        public bool IsFixed
        {
            get { return _isFixed; }
            set
            {
                _isFixed = value;
                RaisePropertyChanged();
            }
        }

        private string _title;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

    }
}
