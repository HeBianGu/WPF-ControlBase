// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Systems.Project
{
    public class ProjectItem : ProjectItemBase
    {
        private DateTime _createTime = DateTime.Now;
        /// <summary> 创建时间  </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                _createTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
