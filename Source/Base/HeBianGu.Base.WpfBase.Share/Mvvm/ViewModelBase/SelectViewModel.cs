// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;

namespace HeBianGu.Base.WpfBase
{
    public interface ISelectViewModel
    {
        bool Selected { get; set; }
    }

    public partial class SelectViewModel<T> : ModelViewModel<T>, ISelectViewModel
    {
        public SelectViewModel(T t) : base(t)
        {

        }

        private bool _selected;
        [Browsable(false)]
        /// <summary> 是否选中  </summary>
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged("Selected");
            }
        }

    }
}
