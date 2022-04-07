// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Control.PagedDataGrid
{
    [SettingConfig(Name = "数据列表", Group = "页面显示")]
    public class PagedDataGridSetting : LazySettingInstance<PagedDataGridSetting>
    {
        private StyleHost _host;
        /// <summary> 说明  </summary>
        [Display(Name = "样式设置")]
        [StyleSettingFilter(Filter = "S.PagedDataGrid.")]
        [Property(Type = typeof(StylesPropertyItem))]
        public StyleHost Host
        {
            get { return _host; }
            set
            {
                _host = value;
                RaisePropertyChanged();
            }
        }
    }
}
