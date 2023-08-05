// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Window.Link
{
    [Displayer(Name = "窗口样式", GroupName = SystemSetting.GroupControl)]
    public class LinkGroupsManagerWindowSetting : LazySettingInstance<LinkGroupsManagerWindowSetting>
    {
        private StyleHost _host;
        /// <summary> 说明  </summary>
        [Display(Name = "样式设置")]
        [StyleSettingFilter(Filter = nameof(LinkGroupsManagerWindowBase))]
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
