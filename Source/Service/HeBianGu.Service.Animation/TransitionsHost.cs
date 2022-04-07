// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Service.Animation
{
    public class TransitionsHost : NotifyPropertyChangedBase
    {
        public string Name { get; set; }

        private TransitionCollection _transitions = new TransitionCollection();
        [XmlIgnore]
        [Browsable(false)]
        public TransitionCollection Transitions
        {
            get { return _transitions; }
            set
            {
                _transitions = value;
                RaisePropertyChanged();
            }
        }

    }
}
