// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;

namespace HeBianGu.Base.WpfBase
{
    public interface ISelectViewModel
    {
        bool IsSelected { get; set; }
    }

    public partial class SelectViewModel<T> : ModelViewModel<T>, ISelectViewModel
    { 
        public SelectViewModel(T t) : base(t)
        {

        }

        private bool _isSelected;
        [Browsable(false)]
        /// <summary> 是否选中  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

    }
}
