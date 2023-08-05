// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Image
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        protected override void Init()
        {

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {

        }


        public RelayCommand ItemDoubleClickCommand => new RelayCommand((s, e) =>
        {
            this.IsViewState = true;
        });


        public RelayCommand CloseViewCommand => new RelayCommand((s, e) =>
        {
            this.IsViewState = false;
        });

        private bool _isViewState;
        /// <summary> 说明  </summary>
        public bool IsViewState
        {
            get { return _isViewState; }
            set
            {
                _isViewState = value;
                RaisePropertyChanged();
            }
        }


        private bool _isMenuExpanded = true;
        /// <summary> 说明  </summary>
        public bool IsMenuExpanded
        {
            get { return _isMenuExpanded; }
            set
            {
                _isMenuExpanded = value;
                RaisePropertyChanged();
            }
        }


    }
}
