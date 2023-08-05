// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.PagedDataGrid
{
    /// <summary> 说明</summary>
    public class DataGridColumnSetItem : NotifyPropertyChangedBase
    {
        private DataGridColumn _dataGridColumn;

        public DataGridColumnSetItem(DataGridColumn dataGridColumn)
        {
            _dataGridColumn = dataGridColumn;
        }

        [DataGridColumn("*", Template = typeof(TextBoxDataGridColumn))]
        [Display(Name = "标题")]
        public string Header
        {
            get
            {
                if (_dataGridColumn.Header == null)
                    return null;
                Type type = _dataGridColumn.Header.GetType();
                if (type == typeof(string))
                    return _dataGridColumn.Header.ToString();
                if (typeof(ContentControl).IsAssignableFrom(type))
                    return (_dataGridColumn.Header as ContentControl).Content?.ToString();
                return "--";
            }
            set
            {
                if (_dataGridColumn.Header == null)
                {
                    _dataGridColumn.Header = value;
                    return;
                }

                Type type = _dataGridColumn.Header.GetType();

                if (type == typeof(string))
                {
                    _dataGridColumn.Header = value;
                    return;
                }

                if (typeof(ContentControl).IsAssignableFrom(type))
                    (_dataGridColumn.Header as ContentControl).Content = value;
            }
        }

        [Display(Name = "是否可见")]
        public bool Visible
        {
            get { return _dataGridColumn.Visibility == Visibility.Visible; }
            set
            {
                _dataGridColumn.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                RaisePropertyChanged();
            }
        }
        [DataGridColumn("*", Template = typeof(TextBoxDataGridColumn))]
        [Display(Name = "宽度")]
        public DataGridLength Width
        {
            get { return _dataGridColumn.Width; }
            set
            {
                _dataGridColumn.Width = value;
                RaisePropertyChanged();
            }
        }

    }
}
