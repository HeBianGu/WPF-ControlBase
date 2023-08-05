// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Control.PagedDataGrid
{
    [Displayer(Name = "数据列表", GroupName = SystemSetting.GroupControl)]
    public class PagedDataGridSetting : LazySettingInstance<PagedDataGridSetting>
    {
        private StyleHost _host;
        /// <summary> 说明  </summary>
        [Display(Name = "样式设置")]
        [StyleSettingFilter(Filter = "S.PagedDataGrid.")]
        [PropertyItemType(Type = typeof(StylesPropertyItem))]
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
