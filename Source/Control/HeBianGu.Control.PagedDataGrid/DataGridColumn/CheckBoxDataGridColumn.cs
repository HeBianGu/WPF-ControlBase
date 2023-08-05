// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    public class CheckBoxDataGridColumn : DataGridBoundColumn
    {
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            CheckBox ctrl = new CheckBox();
            if (this.Binding != null)
                ctrl.SetBinding(CheckBox.IsCheckedProperty, this.Binding);
            return ctrl;
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            CheckBox ctrl = new CheckBox();
            if (this.Binding != null)
                ctrl.SetBinding(CheckBox.IsCheckedProperty, this.Binding);
            return ctrl;
        }
    }


}
