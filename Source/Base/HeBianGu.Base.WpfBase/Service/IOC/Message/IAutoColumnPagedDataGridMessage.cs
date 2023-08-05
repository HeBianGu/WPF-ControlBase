// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public interface IAutoColumnPagedDataGridMessage
    {
        Task<bool> Show<T>(T value, Predicate<T> match = null, string title = null, Action<DataGrid> builder = null, ComponentResourceKey key = null) where T : IList;
    }
}
