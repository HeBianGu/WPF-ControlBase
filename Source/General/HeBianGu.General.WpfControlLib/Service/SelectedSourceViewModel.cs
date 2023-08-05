// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Linq;

namespace HeBianGu.General.WpfControlLib
{
    public abstract class SelectedSourceViewModel<T> : SourceViewModel<T> where T : ISelectViewModel
    {
        private bool _isCheckedAll;
        public bool IsCheckedAll
        {
            get { return _isCheckedAll; }
            set
            {
                _isCheckedAll = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand CheckedAllCommand => new RelayCommand(l =>
        {
            if (l is Boolean b)
            {
                this.Collection.Foreach(K => K.IsSelected = b);

                this.IsCheckedAll = b;
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DeleteCheckedCommand => new RelayCommand(l =>
        {
            System.Collections.Generic.IEnumerable<T> checks = this.Collection.Where(k => k.IsSelected);

            if (this.Clear(out string error))
            {
                MessageProxy.Snacker.ShowTime($"删除成功,共计{checks.Count()}条");
                this.Collection.Clear();
            }
            else
            {
                MessageProxy.Snacker.ShowTime("删除失败," + error);
            }

        }, l => this.Collection.Any(k => k.IsSelected));

    }
}
