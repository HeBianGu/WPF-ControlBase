// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.MessageListBox
{
    /// <summary> 说明</summary>
    public class MessageListBoxConfig : LazySettingInstance<MessageListBoxConfig>
    {
        private bool _useInfo = true;
        /// <summary> 说明  </summary>
        public bool UseInfo
        {
            get { return _useInfo; }
            set
            {
                _useInfo = value;
                RaisePropertyChanged();
            }
        }

        private bool _useError = true;
        /// <summary> 说明  </summary>
        public bool UseError
        {
            get { return _useError; }
            set
            {
                _useError = value;
                RaisePropertyChanged();
            }
        }

        private bool _useTrace;
        /// <summary> 说明  </summary>
        public bool UseTrace
        {
            get { return _useTrace; }
            set
            {
                _useTrace = value;
                RaisePropertyChanged();
            }
        }

        private bool _useFatal;
        /// <summary> 说明  </summary>
        public bool UseFatal
        {
            get { return _useFatal; }
            set
            {
                _useFatal = value;
                RaisePropertyChanged();
            }
        }


        private bool _useWarn;
        /// <summary> 说明  </summary>
        public bool UseWarn
        {
            get { return _useWarn; }
            set
            {
                _useWarn = value;
                RaisePropertyChanged();
            }
        }


    }
}
