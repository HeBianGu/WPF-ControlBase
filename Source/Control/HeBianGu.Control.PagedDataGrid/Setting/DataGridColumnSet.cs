// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    public class DataGridColumnSet : NotifyPropertyChangedBase
    {
        public DataGridColumnSet(IEnumerable<DataGridColumn> dataGridColumns)
        {
            ColumnSetItems = dataGridColumns.Select(l => new DataGridColumnSetItem(l))?.ToList();
        }
        public List<DataGridColumnSetItem> ColumnSetItems { get; private set; }

    }
}
