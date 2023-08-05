// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class DataGridKeys
    {
        public static ComponentResourceKey Dynamic => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Dynamic");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Default");
        public static ComponentResourceKey Single => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Single");
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Accent");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Clear");
        public static ComponentResourceKey VerticalDefault => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Vertical.Default");
    }

    public class DataGridRowKeys
    {
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DataGridRowKeys), "S.DataGridRow.Default");
        public static ComponentResourceKey Clear => new ComponentResourceKey(typeof(DataGridRowKeys), "S.DataGridRow.Clear");
    }

    public class DataGridTemplateColumnKeys
    {
        public static ComponentResourceKey CheckAllHeader => new ComponentResourceKey(typeof(DataGridTemplateColumnKeys), "S.DataTemplate.DataGridColumn.CheckAll.Header");
        public static ComponentResourceKey CheckAllCell => new ComponentResourceKey(typeof(DataGridTemplateColumnKeys), "S.DataTemplate.DataGridColumn.CheckAll.Cell");
        public static ComponentResourceKey OperationCell => new ComponentResourceKey(typeof(DataGridTemplateColumnKeys), "S.DataTemplate.DataGridColumn.Operation");
    }
}
