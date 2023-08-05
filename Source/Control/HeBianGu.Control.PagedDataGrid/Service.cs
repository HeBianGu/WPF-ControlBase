// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    public interface IAutoColumnPagedDataGridMessageOption
    {

    }

    [Displayer(Name = "表格消息", GroupName = SystemSetting.GroupMessage)]
    public class AutoColumnPagedDataGridMessage : ServiceSettingInstance<AutoColumnPagedDataGridMessage, IAutoColumnPagedDataGridMessage>, IAutoColumnPagedDataGridMessage, IAutoColumnPagedDataGridMessageOption
    {
        public async Task<bool> Show<T>(T value, Predicate<T> match = null, string title = null, Action<DataGrid> builder = null, ComponentResourceKey key = null) where T : IList
        {
            return await AutoColumnPagedDataGrid.ShowSource<T>(value, match, title, builder, key);
        }
    }


    //[Displayer(Name = "参数设置", GroupName = SystemSetting.GroupControl)]
    //public class Setting : LazySettingInstance<Setting>
    //{

    //}
}
