// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Window.Ribbon
{
    public class SplitButtonData : MenuButtonData
    {
        public SplitButtonData()
            : this(false)
        {
        }

        public SplitButtonData(bool isApplicationMenu)
            : base(isApplicationMenu)
        {
        }

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isChecked;

        public bool IsCheckable
        {
            get
            {
                return _isCheckable;
            }

            set
            {
                if (_isCheckable != value)
                {
                    _isCheckable = value;
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isCheckable;

        public ButtonData DropDownButtonData
        {
            get
            {
                if (_dropDownButtonData == null)
                {
                    _dropDownButtonData = new ButtonData();
                }

                return _dropDownButtonData;
            }
        }
        private ButtonData _dropDownButtonData;
    }
}
