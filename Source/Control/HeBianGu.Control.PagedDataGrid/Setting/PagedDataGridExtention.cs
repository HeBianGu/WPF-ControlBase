// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PagedDataGrid;
using SystemSetting = HeBianGu.Base.WpfBase.SystemSetting;

namespace System
{
    public static class PagedDataGridExtention
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UsePagedDataGrid(this IApplicationBuilder service, Action<PagedDataGridSetting> action = null)
        {
            action?.Invoke(PagedDataGridSetting.Instance);

            SystemSetting.Instance?.Add(PagedDataGridSetting.Instance);
        }
    }
}
