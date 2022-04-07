// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Linq;

namespace HeBianGu.Base.WpfBase
{

    public abstract class SelectedSourceViewModel<T> : SourceViewModel<T> where T : ISelectViewModel
    {
        private bool _isCheckedAll;
        /// <summary> 说明  </summary>
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
                this.Collection.Foreach(K => K.Selected = b);

                this.IsCheckedAll = b;
            }
        }, l => this.SelectedItem != null);

        public RelayCommand DeleteCheckedCommand => new RelayCommand(l =>
        {
            System.Collections.Generic.IEnumerable<T> checks = this.Collection.Where(k => k.Selected);

            if (this.Clear(out string error))
            {
                Message.Instance.ShowSnackMessageWithNotice($"删除成功,共计{checks.Count()}条");
                this.Collection.Clear();
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("删除失败," + error);
            }

        }, l => this.Collection.Any(k => k.Selected));

    }
}
