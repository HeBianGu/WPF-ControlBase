// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.Message
{
    public class InputTextPresenter : NotifyPropertyChangedBase
    {
        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        private string _title = "输入数据";
        /// <summary> 说明  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

    }
}
