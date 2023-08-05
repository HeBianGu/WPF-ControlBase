// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    public class RowNumTextDataGridColumn : DataGridBoundColumn
    {
        public RowNumTextDataGridColumn()
        {
            this.IsReadOnly = false;
        }
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return this.GenerateElement(cell, dataItem);
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            TextBlock textBlock = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Stretch };

            if (this.DataGridOwner is PagedDataGrid pagedDataGrid)
            {
                textBlock.Text = (pagedDataGrid.DataSource.IndexOf(dataItem)+1).ToString();
            }
            else if (this.DataGridOwner.ItemsSource is IList list)
                textBlock.Text = (list.IndexOf(dataItem) + 1).ToString();
            return textBlock;
        }
    }
}
