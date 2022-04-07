// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Component
{
    public class Mapper : NotifyPropertyChangedBase
    {

        private string _from;
        /// <summary> 说明  </summary>
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged("From");
            }
        }


        private string _to;
        /// <summary> 说明  </summary>
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged("To");
            }
        }

    }
}
